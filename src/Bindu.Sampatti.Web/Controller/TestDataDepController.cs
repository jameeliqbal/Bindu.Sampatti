using Microsoft.AspNetCore.Http;
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
    public class TestDataDepController : ControllerBase
    {
        [HttpGet("GetRates")]
        public string GetRates()
        {
            var list = new StringBuilder();
            list.Append("[\"\",\" A.1 \", \"Borewell & Land Development\", \"9.50\"]");
            list.Append(",[\"\",\"B.1\", \"Production Machinery \", \"18.10\"]");
            list.Append(",[\"\",\"B.1.1\", \"Component 1 \", \"18.10\"]");
            list.Append(",[\"\",\"B.1.2\", \"Component 2 \", \"18.10\"]");
            list.Append(",[\"\",\"B.1.3\", \"Component 3 \", \"18.10\"]");
            //Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            var data = "\"data\":[" + list + "]";

            var count = 5;

            var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return result;

        }

    }
}
