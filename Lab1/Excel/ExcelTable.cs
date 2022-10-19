using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExpressionScript.Data.Model;

namespace Lab1.Excel;

public class ExcelTable<T> where T : notnull
{
    private readonly IAddress<T, long> _addressConverter;
    public ExcelTable(int rows, int columns, ExcelCell<T> defaultCell, IAddress<T, long> addressConverter)
    {
        Rows = rows;
        Columns = columns;
        Cells = new ObservableCollection<ExcelCell<T>>(generateCells(rows, columns, defaultCell));
        Constants = new Dictionary<T, T>();
        _addressConverter = addressConverter;
    }

    private List<ExcelCell<T>> generateCells(int width, int height, ExcelCell<T> defaultCell)
    {
        var cells = new List<ExcelCell<T>>();
        for (int i = 1; i <= height; i++)
        {
            for (int j = 1; j <= width; j++)
            {
                int cellId = (i - 1) * width + j;
                cells.Add(new ExcelCell<T>(cellId,defaultCell));
                Constants.Add(_addressConverter.IdToAddress(cellId), defaultCell.Value);
            }
        }

        return cells;
    }

    public void SetCelL(int id, ExcelCell<T> cell)
    {
        var address = _addressConverter.IdToAddress(id);
        Cells[id] = cell;
        Constants[address] = cell.Value;
    }
    public int Rows { get; } 
    public int Columns { get; }
    public ObservableCollection<ExcelCell<T>> Cells { get; }
    public IDictionary<T, T> Constants { get; }
}