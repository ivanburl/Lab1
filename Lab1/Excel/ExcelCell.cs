using System;

namespace Lab1.Excel;

public class ExcelCell
{
    public ExcelCell(long id, object value, Type valueType)
    {
        Id = id;
        Value = value;
        ValueType = valueType;
    }
    
    /// <summary>
    /// Copy constructor from instance of another cell with new id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cellInstance"></param>
    public ExcelCell(long id, ExcelCell cellInstance) : this(id, cellInstance.Value, cellInstance.ValueType)
    {
        
    }

    public long Id { get; set; }
    public object Value { get; set; }
    public Type ValueType { get; set; }
}