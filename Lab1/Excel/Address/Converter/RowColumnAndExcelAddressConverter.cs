using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text;

namespace Lab1.Excel.Address.Converter;

public class DefaultAndExcelAddressConverter : IAddressConverter<DefaultAddress, ExcelAddress>
{
    public ExcelAddress Convert(DefaultAddress from)
    {
        StringBuilder builder = new StringBuilder();
        int letterPeriod = 'Z' - 'A' + 1;
        var column = from.Column-1;
        do
        {
            builder.Append(System.Convert.ToChar('A' + column % letterPeriod));
            column = column / letterPeriod - 1;
        } while (column >= 0);

        var res = builder.ToString().ToCharArray();
        Array.Reverse(res);
        return new ExcelAddress(new string(res), from.Row.ToString());
    }

    public DefaultAddress Convert(ExcelAddress from)
    {
        var res = new DefaultAddress(0,0);
        var step = 'Z' - 'A' + 1;
        foreach (var i in from.Column)
        {
            res.Column = res.Column * step + (i - 'A' + 1);
        }
        res.Row = Int32.Parse(from.Row);
        return res;
    }
}