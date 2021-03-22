using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Bindu.Sampatti.EntityFrameworkCore
{
    public static class SampattiDbContextModelCreatingExtensions
    {
        public static void ConfigureSampatti(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SampattiConsts.DbTablePrefix + "YourEntities", SampattiConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}