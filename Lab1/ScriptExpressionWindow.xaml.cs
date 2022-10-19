using System;
using System.Collections.Generic;
using System.Windows;
using ExpressionScript.Compilation.Compiler;
using ExpressionScript.Data.Model;

namespace Lab1;

public partial class ScriptExpressionWindow : Window
{
    public ScriptExpressionWindow()
    {
        InitializeComponent();
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        var constants = new Dictionary<ExpressionElement, ExpressionElement>();
        var compiler = new ExpressionStringCompiler(constants);
        
        ResultValue.Content = "";
        ResultType.Content = "";
        ErrorMessage.Content = "No errors!";

        try
        {
            var exe = compiler.Compile(Expression.Text);
            var res = exe.Invoke();
            ResultValue.Content = res;
            ResultType.Content = res.GetType();
        }
        catch (Exception exception)
        {
            ErrorMessage.Content = exception.Message;
        }
    }
}