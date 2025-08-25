using ExpiryFood.Database;
using ExpiryFood.Models;

namespace ExpiryFood.Repositories.Interface
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        Task<Product> Get(int id);
        Task<IEnumerable<Product>> GetAll();
    }
}
