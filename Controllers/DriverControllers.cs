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
    [Route("api/v1/driver")]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly IDatabase _database;
        public DriverController(ILogger<DriverController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _database.ReadDriver();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult DriverAdd(Driver driver)
        {
            var result = _database.CreateDriver(driver);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _database.GetByIdDriver(id);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchDriver([FromBody]JsonPatchDocument<Driver> driver, int id)
        {
            var result = _database.UpdateDriver(driver, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(int id)
        {
            var result = _database.DeleteDriver(id);
            return Ok(result);
        }
    }
}