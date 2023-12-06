using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication1.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    public string[] myList = { "foo", "bar", "cha", "arr" };
    [ObservableProperty]
    public int myIndex = 0;
}
