using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DictionaryApp.ViewModels;

public class DictionaryViewModel<TKey, TValue> : INotifyPropertyChanged
{
    private Models.Dictionary<TKey, TValue> _dictionary = new Models.Dictionary<TKey, TValue>();

    public ObservableCollection<Models.KeyValuePair<TKey, TValue>> Items { get; } = new ObservableCollection<Models.KeyValuePair<TKey, TValue>>();

    private TKey _selectedKey;
    public TKey SelectedKey
    {
        get => _selectedKey;
        set
        {
            _selectedKey = value;
            OnPropertyChanged();
        }
    }

    private TValue _selectedValue;
    public TValue SelectedValue
    {
        get => _selectedValue;
        set
        {
            _selectedValue = value;
            OnPropertyChanged();
        }
    }

    public DictionaryViewModel()
    {
        // Здесь можно инициализировать словарь начальными значениями, если это необходимо
    }

    public void Add(TKey key, TValue value)
    {
        _dictionary.Add(key, value);
        UpdateItems();
    }

    public void Remove(TKey key)
    {
        if (_dictionary.ContainsKey(key))
        {
            _dictionary.Remove(key);
            UpdateItems();
        }
    }

    private void UpdateItems()
    {
        Items.Clear();
        foreach (var pair in _dictionary.Pairs)
        {
            Items.Add(pair);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}