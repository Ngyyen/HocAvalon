using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using System.IO;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.Views;
using System;
using AvaloniaApplication1.Services;


namespace AvaloniaApplication1;

public partial class App : Application
{
    static TaskDatabaseService taskDatabase;

    public static TaskDatabaseService TaskDatabase
    {
        get
        {
            if (taskDatabase == null)
            {
                taskDatabase = new TaskDatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WPFAppDb.db3"));
            }
            return taskDatabase;
        }
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new TaskView
            {
                DataContext = new TaskViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
