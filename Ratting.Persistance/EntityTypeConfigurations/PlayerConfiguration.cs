using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ratting.Domain;

namespace Ratting.Persistance.EntityTypeConfigurations
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(player => player.Id);
            builder.Property(player => player.Name).HasMaxLength(20);
            builder.HasIndex(player => player.Name).IsUnique();

            builder.Property(player => player.Money);
            builder.Property(player => player.BestResult);
        }
    }
}
