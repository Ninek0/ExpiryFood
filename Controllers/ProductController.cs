using ExpiryFood.Models;
using ExpiryFood.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpiryFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) 
        {
            _productService = productService;
        }
        // Add new product to storage
        // POST api/<FoodController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _productService.Add(product);
        }

        // Update Existing product in storage
        // PUT api/<FoodController>/5
        [HttpPut]
        public void Put([FromBody] Product value)
        {
            _productService.Update(value);
        }

        // Remove products from storage
        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
        
        // Get product from storage by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id) 
        {
            var result = await _productService.Get(id);
            return result == null ? NotFound(): Ok(result);
        }

        // Get List of all products in storage
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll() 
        {
            var result = await _productService.GetAll();
            return result == null ? NotFound() : Ok(result);
        }
    }
}
