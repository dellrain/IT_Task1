using System.Windows;
using DictionaryApp.ViewModels; // Убедитесь, что используете правильное пространство имен

namespace DictionaryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringDictionaryViewModel _viewModel; // Используйте StringDictionaryViewModel

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new StringDictionaryViewModel(); // Создайте экземпляр StringDictionaryViewModel
            this.DataContext = _viewModel; // Установите DataContext для вашего окна
        }
    }
}