using System;
using System.Collections.Generic;
using System.Windows;
using ExpressionScript.Compilation.Compiler;
using ExpressionScript.Data.Model;
using ExpressionScript.Parsing;
using Lab1.Excel;
using Lab1.Excel.Address;

namespace Lab1;

public partial class ScriptExpressionWindow : Window
{
    private ExcelCell _cell;
    private ExcelTree _tree;

    public ScriptExpressionWindow(ExcelTree tree, ExcelCell cell)
    {
        _cell = cell;
        _tree = tree;

        InitializeComponent();

        Expression.Text = _cell.Expression;
        ResultType.Content = _cell.Value.GetType();
        ResultValue.Content = _cell.Value;
        CellAddress.Content = _cell.Address.Address;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        var constants = new Dictionary<ExpressionElement, ExpressionElement>();
        var compiler = new ExpressionStringCompiler(constants);
        var parser = new ExpressionParser();

        ResultValue.Content = _cell.Value;
        ResultType.Content = _cell.Value.GetType();
        
        _cell.Expression = Expression.Text;
        
        _tree.SetExcelCell(_cell.Address, _cell);

        ResultValue.Content = _cell.Value;
        ResultType.Content = _cell.Value.GetType();
        
        MessageBox.Show("Зміни успішно застосовані!" + _cell);
    }


    private void updateChanges(string espression)
    {
        var parse = new ExpressionParser();
    }
}