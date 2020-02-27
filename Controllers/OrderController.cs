using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using tryout_gigamin1_nugi_mulya_nugraha.Models;

namespace tryout_gigamin1_nugi_mulya_nugraha.Controllers
{
    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IDatabase _database;
        public OrderController(ILogger<OrderController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _database.ReadCustomer();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {
            var result = _database.CreateProduct(product);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _database.GetByIdProduct(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchProduct([FromBody]JsonPatchDocument<Product> product, int id)
        {
            var result = _database.UpdateProduct(product, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _database.DeleteProduct(id);
            return Ok(result);
        }
    }
}