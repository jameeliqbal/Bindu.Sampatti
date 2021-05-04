using Bindu.Sampatti.Locations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Bindu.Sampatti.EntityFrameworkCore
{
    public static class SampattiDbContextModelCreatingExtensions
    {
        public static void ConfigureSampatti(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Location>(b => {
                b.ToTable(SampattiConsts.DbTablePrefix + "Locations",
                    SampattiConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(LocationConsts.MaxNameLength);

                b.HasIndex(x => x.Name);
            
            });
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