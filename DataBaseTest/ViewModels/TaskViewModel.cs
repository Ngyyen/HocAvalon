using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseTest.Model;
using CommunityToolkit.Mvvm.Input;
using Avalonia;
using Microsoft.VisualBasic;

namespace DataBaseTest.ViewModels
{
    public partial class TaskViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string taskName = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private List<TaskModel> taskList = new List<TaskModel>();

        public TaskViewModel()
        {
            getTask();
        }

        [RelayCommand]
        public async void getTask()
        {
            TaskList = await App.TaskDatabase.GetTaskAsync();
        }
        [RelayCommand]
        private async void AddTask()
        {
            var r = await App.TaskDatabase.SaveTaskAsync(new TaskModel
            {
                TaskName = TaskName,
                Description = Description,
            });
            getTask();
        }
        [RelayCommand]
        private async void ClearTask()
        {
            var r = await App.TaskDatabase.ClearTaskAsync();
            getTask();
        }
    }
}
