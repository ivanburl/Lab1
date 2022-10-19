using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab1.Excel;

public class ExcelTable<T>
{
    public ExcelTable(int rows, int columns, ExcelCell<T> defaultCell)
    {
        Rows = rows;
        Columns = columns;
        Cells = new ObservableCollection<ExcelCell<T>>(generateCells(rows, columns, defaultCell));
    }

    private List<ExcelCell<T>> generateCells(int width, int height, ExcelCell<T> defaultCell)
    {
        var cells = new List<ExcelCell<T>>();
        for (int i = 1; i <= height; i++)
        {
            for (int j = 1; j <= width; j++)
            {
                cells.Add(new ExcelCell<T>((i-1)*width+j,defaultCell));
            }
        }

        return cells;
    }
    public int Rows { get; } 
    public int Columns { get; }
    public ObservableCollection<ExcelCell<T>> Cells { get; }

}