using ExpiryFood.Database;
using ExpiryFood.Models;
using ExpiryFood.Repositories.Interface;

namespace ExpiryFood.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void Add(Product product) 
        { 
            _databaseContext.AddFoodAsync(product);
        }
        public void Update(Product product) 
        {
            _databaseContext.UpdateFoodAsync(product);
        }
        public void Delete(int id) 
        {
            _databaseContext.RemoveFoodAsync(id);
        }
        public async Task<Product> Get(int id)
        {
            return await _databaseContext.GetFoodAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAll() 
        {
            return await _databaseContext.GetAllFoodAsync();
        }
    }
}
