using System.Collections;
using ExpiryFood.Database;
using ExpiryFood.Models;
using ExpiryFood.Repositories.Interface;

namespace ExpiryFood.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository) 
        {
            _repository = repository;
        }
        public void Add(Product product)
        {
            _repository.Add(product);
        }
        public void Update(Product product)
        {
            _repository.Update(product);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public async Task<Product> Get(int id)
        {
            return await _repository.Get(id);
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<IEnumerable<Product>> GetExpiryProducts(int daysThreshold)
        {
            var tresholdDate = DateTime.Now.AddDays(daysThreshold);
            return await _repository.GetExpiryProducts(tresholdDate);
        }
    }
}
