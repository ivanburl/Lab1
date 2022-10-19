using System;
using System.Data;
using System.Windows.Media.TextFormatting;
using ExpressionScript.Data.Model;

namespace Lab1.Excel;

public class CellAddressConverter : IAddress<ExpressionElement, long>
{
    private readonly int _columnStep = 'Z' - 'A' + 1;
    private readonly int _rowStep = '9' - '1' + 1;
    public int MaxRowNum { get; } 
    public int MaxColumnNum { get; }
    
    public CellAddressConverter(int maxRowNum, int maxColumnNum)
    {
        MaxRowNum = maxRowNum;
        MaxColumnNum = maxColumnNum;
    }

    public long AddressToId(ExpressionElement address)
    {
        int i = 0, column = 0, row = 0;
        var expression = address.Expression;
        while (i<expression.Length && 'A' <= expression[i] && expression[i] <= 'Z')
        {
            column = column * _columnStep + (expression[i] - 'A' + 1);
            i++;
        }
        
        while (i<expression.Length && '1' <= expression[i] && expression[i] <= '9')
        {
            row = row * _rowStep + (expression[i] - 'A' + 1);
            i++;
        }

        if (row <= 0 || row > MaxRowNum || column <= 0 || column > MaxColumnNum)
        {
            throw new SyntaxErrorException("Cell cant have such address " + address);
        }

        return (column - 1) * MaxRowNum + row;
    }

    public ExpressionElement IdToAddress(long id)
    {
        throw new NotImplementedException();
    }
}