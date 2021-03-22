using System.Threading.Tasks;

namespace Bindu.Sampatti.Data
{
    public interface ISampattiDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
