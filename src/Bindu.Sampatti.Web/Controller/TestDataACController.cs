using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bindu.Sampatti.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataACController : ControllerBase
    {
        [HttpGet("GetClasses")]
        public string GetClasses()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\" A \", \"Building \", \"Class\"]");
            list.Append(",[\"\",\" A.1 \", \"Borewell & Land Development\", \"Class\"]");
            list.Append(",[\"\",\" B \", \" Plant & Machinery \", \"Class\"]");
            list.Append(",[\"\",\"B.1\", \"Production Machinery \", \"Class\"]");
            list.Append(",[\"\",\"B.1.1\", \"Component 1 \", \"Component\"]");
            list.Append(",[\"\",\"B.1.2\", \"Component 2 \", \"Component\"]");
            list.Append(",[\"\",\"B.1.3\", \"Component 3 \", \"Component\"]");
            //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 7;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }


        [HttpGet("GetComponents")]
        public string GetComponents()
        {
            var list = new StringBuilder();
      
            list.Append("[\"\",\"B.1.1\", \"Component 1 \"]");
            list.Append(",[\"\",\"B.1.2\", \"Component 2 \"]");
            list.Append(",[\"\",\"B.1.3\", \"Component 3 \"]");
            //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 3;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

    }
}
