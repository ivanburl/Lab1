namespace Lab1.Excel;

public interface IAddress<T, ID>
{
    ID AddressToId(T address);
    T IdToAddress(ID id);
}