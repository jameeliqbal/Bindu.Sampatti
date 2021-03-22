using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bindu.Sampatti.Data;
using Volo.Abp.DependencyInjection;

namespace Bindu.Sampatti.EntityFrameworkCore
{
    public class EntityFrameworkCoreSampattiDbSchemaMigrator
        : ISampattiDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreSampattiDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the SampattiMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<SampattiMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}