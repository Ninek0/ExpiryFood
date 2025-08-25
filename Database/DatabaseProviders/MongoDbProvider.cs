using ExpiryFood.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ExpiryFood.Database.DatabaseProviders
{
    public class MongoDbProvider : IDatabaseProvider
    {
        private readonly IMongoCollection<Product> _foods;
        public MongoDbProvider(string connectionString) 
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Food");
            _foods = database.GetCollection<Product>("Food");
        }
        public async Task AddFoodAsync(Product food)
        {
            await _foods.InsertOneAsync(food);
        }

        public async Task<List<Product>> GetAllFoodAsync()
        {
            return await _foods.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetFoodAsync(int id)
        {
            return await _foods.Find(f => f.Id == id).FirstAsync();
        }

        public async Task RemoveFoodAsync(int id)
        {
            await _foods.FindOneAndDeleteAsync(f => f.Id == id);
        }

        public async Task UpdateFoodAsync(Product food)
        {
            await _foods.ReplaceOneAsync(f => f.Id == food.Id, food);
        }
    }
}
