using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Listify.Models;

namespace Listify.Data
{
    public class ComprasDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ComprasDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Compra>().Wait();
        }

        public Task<List<Compra>> GetComprasAsync()
        {
            return _database.Table<Compra>().ToListAsync();
        }

        public Task<Compra> GetCompraAsync(int id)
        {
            return _database.Table<Compra>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCompraAsync(Compra compra)
        {
            if (compra.Id != 0)
                return _database.UpdateAsync(compra);
            else
                return _database.InsertAsync(compra);
        }

        public Task<int> DeleteCompraAsync(Compra compra)
        {
            return _database.DeleteAsync(compra);
        }

        public Task<List<Compra>> BuscarComprasAsync(string texto)
        {
            return _database.Table<Compra>()
                .Where(c => c.Nombre.ToLower().Contains(texto.ToLower()))
                .ToListAsync();
        }
    }
}
