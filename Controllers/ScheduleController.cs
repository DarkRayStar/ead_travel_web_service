using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// This controller manages schedules.
namespace TransportManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        // Retrieve a list of schedules
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Retrieve a schedule by ID
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // Create a new schedule
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // Update a schedule by ID
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // Delete a schedule by ID
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
