using ExpiryFood.Models;

namespace ExpiryFood.Database
{
    public interface IDatabaseProvider
    {
        Task AddFoodAsync(Product food);
        Task RemoveFoodAsync(int id);
        Task<Product> GetFoodAsync(int id);
        Task UpdateFoodAsync(Product food);
        Task<List<Product>> GetAllFoodAsync();
    }
}
