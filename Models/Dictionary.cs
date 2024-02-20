namespace DictionaryApp.Models;

public class KeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public KeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class Dictionary<TKey, TValue>
{
    private List<KeyValuePair<TKey, TValue>> pairs = new List<KeyValuePair<TKey, TValue>>();

    public int Count => pairs.Count;
    public bool IsEmpty => !pairs.Any();
    public TKey[] Keys => pairs.Select(p => p.Key).ToArray();
    public TValue[] Values => pairs.Select(p => p.Value).ToArray();
    public KeyValuePair<TKey, TValue>[] Pairs => pairs.ToArray();

    public TValue this[TKey key]
    {
        get
        {
            var pair = pairs.FirstOrDefault(p => p.Key.Equals(key));
            if (pair == null) throw new KeyNotFoundException("Key not found");
            return pair.Value;
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (pairs.Any(p => p.Key.Equals(key)))
            throw new System.Exception("Key already exists");
        pairs.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool Remove(TKey key)
    {
        var pair = pairs.FirstOrDefault(p => p.Key.Equals(key));
        if (pair != null)
        {
            return pairs.Remove(pair);
        }
        return false;
    }

    public bool ContainsKey(TKey key) => pairs.Any(p => p.Key.Equals(key));

    public void Clear() => pairs.Clear();
}