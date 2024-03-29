﻿using Microsoft.AspNetCore.Mvc;
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
    public class TestDataPRController : ControllerBase
    {
        [HttpGet("GetPRS")]
        public string GetPRS()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\"PR0001\", \"2021/03/20 09:00\", \"Manager One\", \"Department One\",\"Section One\",\"Waiting for Approval\"]");
            list.Append(",[\"\",\"PR0002\", \"2021/03-21 09:00\", \"Manager Three\", \"Department Three\",\"Section Three\",\"Approved\"]");
            list.Append(",[\"\",\"PR0003\", \"2021/03/22 09:00\", \"Manager Two\", \"Department Two\",\"Section Two\",\"Rejected\"]");
            list.Append(",[\"\",\"PR0004\", \"2021/03/22 09:00\", \"Manager Three\", \"Department Three\",\"Section Three\",\"Approved Partially\"]");
             //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

        [HttpGet("GetPRLines")]
        public string GetPRLines()
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


        [HttpGet("GetQuotations")]
        public string GetQuotations()
        {
            var list = new StringBuilder();
            list.Append("[\"Q0001\", \"Vendor One\"]");
            list.Append(",[\"Q0002\", \"Vendor Two\"]");
            list.Append(",[\"Q0003\", \"Vendor Three\"]");
            list.Append(",[\"Q0004\", \"Vendor Four\"]");
            var data = "\"data\":[" + list + "]";

            var count = 4;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

        // GET: api/<TestDataPRController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestDataPRController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestDataPRController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestDataPRController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestDataPRController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
