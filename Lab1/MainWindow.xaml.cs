using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using Lab1.Excel;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ExcelTable<ExpressionElement> Table { get; }

        public ObservableCollection<String> ColumnNames { get;}
        public ObservableCollection<String> RowNames { get; }
        
        private int tableRows = 10;
        private int tableColumns = 10;

        public MainWindow()
        {
            //TODO move columns and rows to the table
            Table = new ExcelTable<ExpressionElement>(
                tableRows, 
                tableColumns, 
                new ExcelCell<ExpressionElement>(
                    0, 
                    new ExpressionElement("0", ExpressionElementType.Number)));
            
            RowNames = new ObservableCollection<String>(generateRowNames(Table.Rows));
            ColumnNames = new ObservableCollection<String>(generateColumnNames(Table.Columns));

            InitializeComponent();
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var expressionWindow = new ScriptExpressionWindow();
            var buttonScreenPosition = ((Button)sender).PointToScreen(new Point(0, 0));
            expressionWindow.Top =buttonScreenPosition.Y;
            expressionWindow.Left = buttonScreenPosition.X;


            expressionWindow.ShowDialog();
        }
        
        //TODO create ExcelRows and ExcelColumns
        private List<String> generateRowNames(int rows)
        {
            var names = new List<String>();
            for (int i = 1; i <= rows; i++)
            {
                names.Add(i.ToString());
            }
            return names;
        }
        
        private List<String> generateColumnNames(int columns)
        {
            var names = new List<String>();
            for (int i = 1; i <= columns; i++)
            {
                names.Add(generateColumnName(i));
            }
            return names;
        }

        private String generateColumnName(int index)
        {
            StringBuilder builder = new StringBuilder();
            int letterPeriod = 'Z' - 'A'+1;
            index--;
            do
            {
                builder.Append(Convert.ToChar('A' + index % letterPeriod));
                index = index / letterPeriod - 1;
            } while(index >= 0);

            var res = builder.ToString().ToCharArray();
            Array.Reverse(res);
            return new string(res);
        }
    }
}
