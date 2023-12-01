using Avalonia.Controls;
using System.Threading.Tasks;

namespace HocAvalon.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int numWord = CountWord(ShareData.transText);
        if (numWord == 1)
        {
            WordWindow wordWindow = new WordWindow();
            wordWindow.Show();
        }
        else
        {
            Window2 window2 = new Window2();
            window2.Show();
        }
        this.Close();
    }
    private void SumButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SumWindow sumWindow = new SumWindow();
        sumWindow.Show();
        this.Close();
    }
    private void Window_Deactivated(object? sender, System.EventArgs e)
    {
        this.Close();
    }
    private int CountWord(string inputString)
    {
        int wordCount = 0;
        bool isPreviousCharSpace = true;

        foreach (char character in inputString)
        {
            if (character == ' ')
            {
                isPreviousCharSpace = true;
            }
            else if (isPreviousCharSpace)
            {
                wordCount++;
                isPreviousCharSpace = false;
            }
        }
        return wordCount;
    }
}
