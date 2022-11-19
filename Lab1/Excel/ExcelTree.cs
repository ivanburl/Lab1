using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using ExpressionScript.Compilation;
using ExpressionScript.Compilation.Compiler;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Formatting;
using ExpressionScript.Parsing;
using ExpressionScript.Validation;
using Lab1.Excel.Address;

namespace Lab1.Excel;

public class ExcelTree : IValidator<ExcelCell, Exception?>
{
    private readonly ExcelTable _table;
    private readonly ExpressionStringCompiler _compiler;
    private readonly ExpressionParser _parser;

    private readonly Dictionary<ExcelAddress, HashSet<ExcelAddress>> _graph;
    
    public ExcelTree(ExcelTable table, ExpressionStringCompiler compiler, ExpressionParser parser)
    {
        _table = table;
        _compiler = compiler;
        _parser = parser;
        _graph = new Dictionary<ExcelAddress, HashSet<ExcelAddress>>();
        GenerateAllConstants();
    }

    public void SetExcelCell(ExcelAddress address, ExcelCell cell)
    {
        RemoveConnections(address, _table.GetCell(address));
        AddConnections(address, cell);
        UpdateAndCompileCell(address, cell);
    }
    
    
    private void UpdateAndCompileCell(ExcelAddress address,ExcelCell cell)
    {   
        
        try
        {
            var exe = _compiler.Compile(cell.Expression);
            cell.Value = exe.Invoke();
        }
        catch (Exception e)
        {
            cell.Value = e.Message;
        }

        _table.SetCell(cell);
        SetConstant(address, cell);

        if (_graph.ContainsKey(address))
        {
            foreach (var i in _graph[address])
            {
                UpdateAndCompileCell(i, _table.GetCell(i));
            }
        }


    }

    private void AddConnections(ExcelAddress address, ExcelCell cell)
    {
        //if (!_graph.ContainsKey(address)) _graph[address] = new HashSet<ExcelAddress>();
        var elements = _parser.parse(cell.Expression);
        
        foreach (var i in elements)
        {
            if (i.ExpressionType == ExpressionElementType.Constant)
            {
                AddConnection(ExcelAddress.Convert(i.Expression), address);
            }
        }
    }

    private void RemoveConnections(ExcelAddress address, ExcelCell cell)
    {
        var elements = _parser.parse(cell.Expression);
        foreach (var i in elements)
        {
            if (i.ExpressionType == ExpressionElementType.Constant)
            {
                RemoveConnection(ExcelAddress.Convert(i.Expression), address);
            }
        }
    }

    private void AddConnection(ExcelAddress from, ExcelAddress to)
    {
        if (!_graph.ContainsKey(from)) _graph[from] = new HashSet<ExcelAddress>();
        _graph[from].Add(to);
    }

    private void RemoveConnection(ExcelAddress from, ExcelAddress to)
    {
        if (_graph.ContainsKey(from))
        {
            _graph[from].Remove(to);
        }
    }

    public Exception? ValidationResult(ExcelCell cell)
    {
        AddConnections(cell.Address, cell);
        
        var used = new HashSet<ExcelAddress>();
        var res = ValidationResult(used, cell.Address);
        
        RemoveConnections(cell.Address, cell);
        return res;
    }

    private Exception? ValidationResult(HashSet<ExcelAddress> used, ExcelAddress address)
    {
        if (used.Contains(address)) return new Exception("Cycle detected !!!"); 
        used.Add(address);
        
        foreach (var i in _graph[address])
        {
            var res = ValidationResult(used, i);
            if (res != null) return res;
        }

        return null;
    }

    private void GenerateAllConstants()
    {
        _compiler.Constants = new Dictionary<ExpressionElement, ExpressionElement>();
        foreach (var i in _table.Cells)
        {
            SetConstant(i.Address, i);
        }
    }

    private void SetConstant(ExcelAddress address, ExcelCell cell)
    {
        var type = ExpressionElementType.Constant;
        if (cell.Value is BigInteger) type = ExpressionElementType.Number;
        if (cell.Value is bool) type = ExpressionElementType.Boolean;
            
        _compiler.Constants[new ExpressionElement(address.Address, ExpressionElementType.Constant)] 
            = new ExpressionElement(cell.Value.ToString()!, type);
    }
}