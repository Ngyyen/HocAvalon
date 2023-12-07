using Avalonia.Controls;
using HocAvalon.ViewModels;
using HocAvalon.Model;

namespace HocAvalon.Views
{
    public partial class SavedWordWindow : Window
    {
        public SavedWordWindow()
        {
            InitializeComponent();
            DataContext = new SaveWordViewModel();
        }
        private void Window_Activated_1(object? sender, System.EventArgs e)
        {
            DataContext = new SaveWordViewModel();
        }

        private void ListBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            SavedWord selectedWord = listBox.SelectedItem as SavedWord;
            if (selectedWord is null)
            {
                return;
            }
            SavedWord finalWord = selectedWord;
            WordWindow wordWindow = new WordWindow();
            wordWindow.DataContext = new WordWindowViewModel(finalWord);
            wordWindow.Show();
        }

    }
}
