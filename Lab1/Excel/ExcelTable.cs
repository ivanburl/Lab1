using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab1.Excel;

public class ExcelTable
{
    public ExcelTable(int rows, int columns, ExcelCell defaultCell)
    {
        Rows = rows;
        Columns = columns;
        Cells = new ObservableCollection<ExcelCell>(generateCells(rows, columns, defaultCell));
    }

    private List<ExcelCell> generateCells(int width, int height, ExcelCell defaultCell)
    {
        var cells = new List<ExcelCell>();
        for (int i = 1; i <= height; i++)
        {
            for (int j = 1; j <= width; j++)
            {
                cells.Add(new ExcelCell((i-1)*width+j,defaultCell));
            }
        }

        return cells;
    }
    public int Rows { get; } 
    public int Columns { get; }
    public ObservableCollection<ExcelCell> Cells { get; }

}