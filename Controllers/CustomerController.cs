using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using tryout_gigamin1_nugi_mulya_nugraha.Models;

namespace tryout_gigamin1_nugi_mulya_nugraha
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IDatabase _database;
        public CustomerController(ILogger<CustomerController> logger, IDatabase database)
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
        public IActionResult CustomerAdd(Customer customer)
        {
            var result = _database.CreateCustomer(customer);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _database.GetByIdCustomer(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchCustomer([FromBody]JsonPatchDocument<Customer> customer, int id)
        {
            var result = _database.UpdateCustomer(customer, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var result = _database.DeleteCustomer(id);
            return Ok(result);
        }
    }
}