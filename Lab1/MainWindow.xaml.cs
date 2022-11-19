using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ExpressionScript.Compilation.Compiler;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Parsing;
using Lab1.Excel;
using Lab1.Excel.Address;
using Lab1.Excel.Address.Converter;

namespace Lab1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ExcelTable Table { get; }
    public ExcelTree Tree { get; }
    public ObservableCollection<string> ColumnNames { get; }
    public ObservableCollection<string> RowNames { get; }

    private int tableRows = 10;
    private int tableColumns = 10;

    public MainWindow()
    {
        Table = new ExcelTable(
            tableRows,
            tableColumns,
            new ExcelCell(new ExcelAddress("A", "1"), "0", 0),
            new DefaultAndExcelAddressConverter());

        RowNames = new ObservableCollection<string>(generateRowNames(Table.Rows));
        ColumnNames = new ObservableCollection<string>(GenerateColumnNames(Table.Columns));

        Tree = new ExcelTree(Table, 
            new ExpressionStringCompiler(new Dictionary<ExpressionElement, ExpressionElement>()), 
            new ExpressionParser());
        
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var excelCell = Table.GetCell(((sender as Button)?.Tag as ExcelAddress)!);//bad idea but lets make as it
        
        var expressionWindow = new ScriptExpressionWindow(Tree, excelCell);
        
        
        var buttonScreenPosition = ((Button)sender).PointToScreen(new Point(0, 0));
        
        expressionWindow.Top = buttonScreenPosition.Y;
        expressionWindow.Left = buttonScreenPosition.X;
        
        expressionWindow.ShowDialog();
    }

    //TODO create ExcelRows and ExcelColumns
    private List<string> generateRowNames(int rows)
    {
        var names = new List<string>();
        for (var i = 1; i <= rows; i++) names.Add(i.ToString());
        return names;
    }

    private List<string> GenerateColumnNames(int columns)
    {
        var names = new List<string>();
        for (var i = 1; i <= columns; i++) names.Add(generateColumnName(i));
        return names;
    }

    private string generateColumnName(int index)
    {
        var builder = new StringBuilder();
        var letterPeriod = 'Z' - 'A' + 1;
        index--;
        do
        {
            builder.Append(Convert.ToChar('A' + index % letterPeriod));
            index = index / letterPeriod - 1;
        } while (index >= 0);

        var res = builder.ToString().ToCharArray();
        Array.Reverse(res);
        return new string(res);
    }

    private void ButtonInfo_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}