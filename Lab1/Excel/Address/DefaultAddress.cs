using Lab1.Excel.Address.Converter;

namespace Lab1.Excel.Address;

public class DefaultAddress : IAddress
{
    public DefaultAddress(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; set; }
    public int Column { get; set; }

    public string Address => ToString();

    public override string ToString()
    {
        return $"{nameof(Row)}: {Row}, {nameof(Column)}: {Column}";
    }
}