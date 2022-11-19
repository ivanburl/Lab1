using System;
using System.Text;

namespace Lab1.Excel.Address;

public class ExcelAddress : IAddress
{
    private readonly string _row;
    private readonly string _column;
    public ExcelAddress(string column, string row)
    {
        Row = row;
        Column = column;
    }
    
    private bool Equals(ExcelAddress other)
    {
        return _row == other._row && _column == other._column;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ExcelAddress)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_row, _column);
    }
    public string Column
    {
        get => _column;
        private init
        { 
            if (value is null || value.Length==0) throw new ArgumentNullException(nameof(value));
            foreach (var i in value)
            {
                if (!(i is >= 'A' and <= 'Z')) throw new ArgumentException("Incorrect column!");
            }

            _column = value;
        }
    }

    public string Row
    {
        get => _row;
        private init {
            if (value is null || value.Length==0) throw new ArgumentNullException(nameof(value));
            foreach (var i in value)
            {
                if (!Char.IsDigit(i) && i!='0')
                    throw new ArgumentException("Incorrect row!");
            }

            _row = value;
        }
    }

    public static ExcelAddress Convert(string address)
    {
        var rowBuilder = new StringBuilder(); 
        var columnBuilder = new StringBuilder();
        int i = 0;

        while (i<address.Length && address[i] >= 'A' && address[i] <= 'Z')
        {
            columnBuilder.Append(address[i]);
            i++;
        }

        while (i < address.Length && address[i] >= '1' && address[i] <= '9')
        {
            rowBuilder.Append(address[i]);
            i++;
        }

        if (i != address.Length) throw new ArgumentException("Incorrect string format!");
        return new ExcelAddress(columnBuilder.ToString(), rowBuilder.ToString());
    }

    public string Address => _column+_row;
}


