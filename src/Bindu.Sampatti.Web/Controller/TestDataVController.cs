using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bindu.Sampatti.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataVController : ControllerBase
    {

        [HttpGet("GetVendors")]
        public string GetVendors()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"Vendor One\", \"098764321\", \"vendor1@email.com\"]");
            list.Append(",[\"\",\"Vendor Two\", \"098764321\", \"vendor2@email.com\"]");
            list.Append(",[\"\",\"Vendor Three\", \"098764321\", \"vendor3@email.com\"]");
            list.Append(",[\"\",\"Vendor Four\", \"098764321\", \"vendor4@email.com\"]");
            //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

        [HttpGet("GetPOs")]
        public string GetPOs()
        {
            var list = new StringBuilder();
            list.Append("[\"PO0001\", \"23 March 2021 11:00 AM\"]");
            list.Append(",[\"PO0002\", \"23 March 2021 11:00 AM\"]");
            list.Append(",[\"PO0003\", \"23 March 2021 11:00 AM\"]");
            list.Append(",[\"PO0004\", \"23 March 2021 11:00 AM\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }
        // GET: api/<TestDataVController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestDataVController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestDataVController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestDataVController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestDataVController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
