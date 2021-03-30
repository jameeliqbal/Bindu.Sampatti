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
    public class TestDataPOController : ControllerBase
    {

        [HttpGet("GetPOs")]
        public string GetPOs()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"PO0001\", \"23 March 2021 11:00 AM\",\"Vendor One\",\"LQ0001\"]");
            list.Append(",[\"\",\"PO0002\", \"23 March 2021 11:00 AM\", \"Vendor Two\",\"LQ0002\"]");
            list.Append(",[\"\",\"PO0003\", \"23 March 2021 11:00 AM\", \"Vendor Three\",\"LQ0003\"]");
            list.Append(",[\"\",\"PO0004\", \"23 March 2021 11:00 AM\", \"Vendor Four\",\"LQ0004\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

        [HttpGet("GetPOLines")]
        public string GetPOLines()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"Description of Item One\", \"1\", \"100\", \"100\"]");
            list.Append(",[\"\",\"Description of Item Two\", \"2\", \"200\", \"400\"]");
            list.Append(",[\"\",\"Description of Item Three\", \"1\", \"300\", \"300\"]");
            list.Append(",[\"\",\"Description of Item Four\", \"1\", \"400\", \"400\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }










        // GET: api/<TestDataPOController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestDataPOController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestDataPOController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestDataPOController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestDataPOController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
