namespace Lab1.Excel.Address.Converter;

public interface IAddressConverter<TF,T> 
    where T : IAddress 
    where TF : IAddress
{
    T Convert(TF from);
    TF Convert(T from);
}