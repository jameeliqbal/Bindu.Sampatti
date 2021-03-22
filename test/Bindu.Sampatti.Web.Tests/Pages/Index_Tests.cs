using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Bindu.Sampatti.Pages
{
    public class Index_Tests : SampattiWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
