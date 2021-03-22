using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Bindu.Sampatti.Data
{
    /* This is used if database provider does't define
     * ISampattiDbSchemaMigrator implementation.
     */
    public class NullSampattiDbSchemaMigrator : ISampattiDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}