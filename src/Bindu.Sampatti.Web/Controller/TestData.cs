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
    public class TestDataController : ControllerBase
    {
        // GET: api/<TestData>
        [HttpGet("GetAssets")]
        public  string  GetAssets()
        {
            var list = new StringBuilder();
            list.Append( "[\"E.1.1\", \"LAPTOP - DATA CENTER\", \"Laptop\"],");
            list.Append( "[\"E.1.2\",\"LAPTOP FOR STORES\", \"Laptop\"]" );

             var data= "\"data\":[" + list  + "]";

            var count = 2;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data +"}" ;
             return result;

        }

        // GET api/<TestData>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestData>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestData>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestData>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
