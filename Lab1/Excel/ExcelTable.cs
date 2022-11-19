using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Automation;
using ExpressionScript.Compilation;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Parsing;
using Lab1.Excel.Address;
using Lab1.Excel.Address.Converter;

namespace Lab1.Excel;

public class ExcelTable
{
    private readonly IAddressConverter<DefaultAddress, ExcelAddress> _converter;
    
    public int Rows { get; }
    public int Columns { get; }
    public ObservableCollection<ExcelCell> Cells { get;  }

    public ExcelTable(int rows, int columns, ExcelCell defaultCell, IAddressConverter<DefaultAddress, ExcelAddress> converter)
    {
        Rows = rows;
        Columns = columns;
        _converter = converter;
        Cells = new ObservableCollection<ExcelCell>(generateCells(rows, columns, defaultCell));
    }
    
    public void SetCell(ExcelCell cell)
    {
        var defaultAddress = _converter.Convert(cell.Address);
        var id = (defaultAddress.Row - 1) * Columns + defaultAddress.Column - 1;
        Cells[id] = cell;
    }

    public ExcelCell GetCell(ExcelAddress excelAddress)
    {
        var defaultAddress = _converter.Convert(excelAddress);
        var id = (defaultAddress.Row - 1) * Columns + defaultAddress.Column - 1;
        return Cells[id];
    }
    private List<ExcelCell> generateCells(int width, int height, ExcelCell defaultCell)
    {
        var cells = new List<ExcelCell>();
        
        for (var i = 1; i <= width; i++)
        for (var j = 1; j <= height; j++)
        {
            var address = _converter.Convert(new DefaultAddress(i, j));
            cells.Add(new ExcelCell(address, defaultCell.Expression, defaultCell.Value));
        }

        return cells;
    }
}