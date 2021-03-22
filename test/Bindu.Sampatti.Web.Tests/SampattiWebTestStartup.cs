using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Bindu.Sampatti
{
    public class SampattiWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<SampattiWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}