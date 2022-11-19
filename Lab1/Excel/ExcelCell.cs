using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab1.Excel.Address;

namespace Lab1.Excel;

public class ExcelCell : INotifyPropertyChanged
{
    private ExcelAddress _address;
    private object _value;
    private string _expression;
    
    public ExcelCell(ExcelAddress address, string expression, object value)
    {
        Address = address;
        Expression = expression;
        Value = value;
    }

    public string Expression
    {
        get => _expression;
        set => SetField(ref _expression, value ?? throw new ArgumentNullException(nameof(value)), "Expression");
    }

    public object Value
    {
        get => _value;
        set => SetField(ref _value, value ?? throw new ArgumentNullException(nameof(value)), "Value");
    }

    public ExcelAddress Address
    {
        get => _address;
        set => SetField(ref _address, value ?? throw new ArgumentNullException(nameof(value)), "Address");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public override string ToString()
    {
        return $"{nameof(Address)}: {Address}, {nameof(Expression)}: {Expression}, {nameof(Value)}: {Value}";
    }
    
    private bool SetField<T>(ref T field, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}