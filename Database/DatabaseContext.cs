using ExpiryFood.Models;

namespace ExpiryFood.Database
{
    public class DatabaseContext
    {
        private readonly IDatabaseProvider _databaseProvider;
        public DatabaseContext(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }
        public Task AddFoodAsync(Product product) => _databaseProvider.AddFoodAsync(product);
        public Task RemoveFoodAsync(int id) => _databaseProvider.RemoveFoodAsync(id);
        public Task<Product> GetFoodAsync(int id) => _databaseProvider.GetFoodAsync(id);
        public Task UpdateFoodAsync(Product product) => _databaseProvider.UpdateFoodAsync(product);
        public Task<List<Product>> GetAllFoodAsync() => _databaseProvider.GetAllFoodAsync();
    }
}
