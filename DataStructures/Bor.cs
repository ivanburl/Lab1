namespace DataStructures;

public class Bor<TKey, TValue> where TKey : notnull
{
    public TValue this[IEnumerable<TKey> element] => GetValue(element);
    
    private readonly Dictionary<TKey, Bor<TKey, TValue>> _next;
    private TValue _value;
    
    public Bor()
    {
        _value = default!;
        _next = new Dictionary<TKey, Bor<TKey, TValue>>();
    }

    public void AddElement(IEnumerable<TKey> element, TValue value)
    {
        var tmpBor = this;
        foreach (var key in element)
        {
            if (!tmpBor._next.ContainsKey(key))
            {
                tmpBor._next.Add(key, new Bor<TKey, TValue>());
            }
            tmpBor = tmpBor._next[key];
        }
        tmpBor._value = value;
    }

    public bool HasPrefix(IEnumerable<TKey> element)
    {
        var tmpBor = this;
        foreach (var key in element)
        {
            if (!tmpBor._next.ContainsKey(key))
            {
                return false;
            }
            tmpBor = tmpBor._next[key];
        }
        return true;
    }

    private TValue GetValue(IEnumerable<TKey> element)
    {
        var tmpBor = this;
        foreach (var key in element) tmpBor = tmpBor._next[key];
        return tmpBor._value;
    }
    
    
}