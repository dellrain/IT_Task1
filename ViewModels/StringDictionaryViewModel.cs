using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DictionaryApp.Commands;

namespace DictionaryApp.ViewModels
{
    public class StringDictionaryViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand CheckKeyExistsCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public ObservableCollection<KeyValuePair<string, string>> Items { get; } =
            new ObservableCollection<KeyValuePair<string, string>>();

        public StringDictionaryViewModel()
        {
            AddCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, CanRemoveItem);
            UpdateItems();
            CheckKeyExistsCommand = new RelayCommand(CheckKeyExists);
            ClearCommand = new RelayCommand(ClearDictionary);
        }

        private void CheckKeyExists(object parameter)
        {
            if (parameter is string key)
            {
                bool exists = ContainsKey(key);
                MessageBox.Show(exists ? "Key exists." : "Key does not exist.", "Check Key");
            }
        }

        private void ClearDictionary(object parameter)
        {
            Clear();
            MessageBox.Show("Dictionary cleared.", "Clear Dictionary");
        }

        private void AddItem(object parameter)
        {
            if (parameter is string key && !string.IsNullOrWhiteSpace(key) && !_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, SelectedValue);
                UpdateItems();
                SelectedKey = default;
                SelectedValue = default;
            }
        }

        private bool CanRemoveItem(object parameter)
        {
            return parameter is string key && _dictionary.ContainsKey(key);
        }

        private void RemoveItem(object parameter)
        {
            if (parameter is string key)
            {
                _dictionary.Remove(key);
                UpdateItems();
            }
        }

        private void UpdateItems()
        {
            Items.Clear();
            foreach (var pair in _dictionary)
            {
                Items.Add(pair);
            }
        }

        public void Clear()
        {
            _dictionary.Clear();
            UpdateItems();
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        private string _selectedKey;

        public string SelectedKey
        {
            get => _selectedKey;
            set
            {
                _selectedKey = value;
                OnPropertyChanged();
            }
        }

        private string _selectedValue;

        public string SelectedValue
        {
            get => _selectedValue;
            set
            {
                _selectedValue = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}