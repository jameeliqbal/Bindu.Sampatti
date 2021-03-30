using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bindu.Sampatti.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataVQController : ControllerBase
    {

        [HttpGet("GetVQS")]
        public string GetVQS()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"VQ0001\", \"2021/03/20 09:00\", \"Vendor One\", \"PR0001\",\"Waiting for Approval\"]");
            list.Append(",[\"\",\"VQ0002\", \"2021/03-21 09:00\", \"Vendor Three\", \"PR0002\",\"Approved\"]");
            list.Append(",[\"\",\"VQ0003\", \"2021/03/22 09:00\", \"Vendor Two\", \"PR0003\",\"Rejected\"]");
            list.Append(",[\"\",\"VQ0004\", \"2021/03/22 09:00\", \"Vendor Three\", \"PR0004\",\"Approved Partially\"]");
            //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }


        [HttpGet("GetVQLines")]
        public string GetVQLines()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"Description of Item One\", \"1\", \"100\", \"100\",\"Waiting for Approval\"]");
            list.Append(",[\"\",\"Description of Item Two\", \"2\", \"200\", \"400\",\"Approved\"]");
            list.Append(",[\"\",\"Description of Item Three\", \"1\", \"300\", \"300\",\"Rejected\"]");
            list.Append(",[\"\",\"Description of Item Four\", \"1\", \"400\", \"400\",\"Approved\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

        [HttpGet("GetPOs")]
        public string GetPOs()
        {
            var list = new StringBuilder();
            list.Append("[\"PO0001\", \"23 March 2021 11:00 AM\",\"Vendor One\"]");
            list.Append(",[\"PO0002\", \"23 March 2021 11:00 AM\", \"Vendor Two\"]");
            list.Append(",[\"PO0003\", \"23 March 2021 11:00 AM\", \"Vendor Three\"]");
            list.Append(",[\"PO0004\", \"23 March 2021 11:00 AM\", \"Vendor Four\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }



        // GET: api/<TestDataVQController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestDataVQController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestDataVQController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestDataVQController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestDataVQController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
