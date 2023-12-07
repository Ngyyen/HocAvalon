using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseTest.Model;

namespace DataBaseTest.Services
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
            return _database.Table<TaskModel>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskModel taskModel)
        {
            return _database.InsertAsync(taskModel);
        }

        public Task<int> ClearTaskAsync()
        {
            return _database.DeleteAllAsync<TaskModel>();
        }
    }
}
