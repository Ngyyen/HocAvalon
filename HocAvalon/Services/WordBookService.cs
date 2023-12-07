using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using HocAvalon.Model;

namespace HocAvalon.Services
{
    public class WordBookService
    {
        readonly SQLiteAsyncConnection database;
        public WordBookService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<SavedWord>().Wait();
        }
        public Task<List<SavedWord>> GetWordsAsync()
        {
            return database.Table<SavedWord>().ToListAsync();
        }
        public Task<int> SaveWordAsync(SavedWord word)
        {
            return database.InsertAsync(word);
        }
        public async Task DeleteTask(SavedWord savedWord)
        {
            await database.DeleteAsync(savedWord);
        }
        public Task<List<SavedWord>> GetaTask(string word)
        {
            var query = database.Table<SavedWord>().Where(s => s.Content == word);
            return query.ToListAsync(); 
        }
    }
}
