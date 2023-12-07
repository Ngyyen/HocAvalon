using AvaloniaApplication1.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Services
{
    public class TaskDatabaseService
    {
        readonly SQLite.SQLiteAsyncConnection _database;

        public TaskDatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskModel>().Wait();
        }

        public Task<List<TaskModel>> GetTaskAsync()
        {
            return _database.Table<TaskModel>().OrderByDescending(x => x.DueDate).ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskModel taskModel)
        {
            return _database.InsertAsync(taskModel);

        }
    }
}
