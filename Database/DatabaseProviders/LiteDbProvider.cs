using ExpiryFood.Models;
using LiteDB;
using MongoDB.Driver;

namespace ExpiryFood.Database.DatabaseProviders
{

    public class LiteDbProvider : IDatabaseProvider
    {
        private readonly ILiteDatabase _database;
        private readonly ILiteCollection<Product> _products;

        public LiteDbProvider(string connectionString) 
        {
            _database = new LiteDatabase(connectionString);
            _products = _database.GetCollection<Product>("Food");
        }
        public Task AddFoodAsync(Product food)
        {
            return Task.Run(() => _products.Insert(food));
        }
        
        public Task<List<Product>> GetAllFoodAsync()
        {
            return Task.FromResult(_products.FindAll().ToList());
        }

        public Task<Product> GetFoodAsync(int id)
        {
            return Task.FromResult(_products.FindById(id));
        }

        public Task RemoveFoodAsync(int id)
        {
            return Task.Run(() => _products.Delete(id));
        }

        public Task UpdateFoodAsync(Product food)
        {
            return Task.Run(() => _products.Update(food));
        }
        public Task<List<Product>> GetExpiryProducts(DateTime tresholdDate)
        {
            return Task.FromResult(_products.Find(p => p.ExpireAt < tresholdDate).ToList());
        }
    }
}