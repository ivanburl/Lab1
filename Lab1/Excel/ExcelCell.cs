using System;

namespace Lab1.Excel;

public class ExcelCell<T>
{
    public ExcelCell(long id, T value)
    {
        Id = id;
        Value = value;
    }
    
    /// <summary>
    /// Copy constructor from instance of another cell with new id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cellInstance"></param>
    public ExcelCell(long id, ExcelCell<T> cellInstance) : this(id, cellInstance.Value)
    {
        
    }

    public long Id { get; set; }
    public T Value { get; set; }
}