using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using HocAvalon.Services;
using System.IO;
using HocAvalon.ViewModels;
using HocAvalon.Views;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;

namespace HocAvalon;

public partial class App : Application
{
    //private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        SetToStartup(true); // Enable startup
        //rkApp.SetValue("HocAvalon", System.Environment.ProcessPath);
    }
    public static void SetToStartup(bool enabled)
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        if (enabled)
        {
            key.SetValue("HocAvalon", System.Environment.ProcessPath);
        }
        else
        {
            key.DeleteValue("HocAvalon", false);
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Window1
            {
                DataContext = new MainViewModel()
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

    static WordBookService wordBookDatabase;

    public static WordBookService WordBookDatabase
    {
        get
        {
            if (wordBookDatabase == null)
            {
                wordBookDatabase = new WordBookService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WordBookDatabase.db3"));
            }
            return wordBookDatabase;
        }
    }
}
