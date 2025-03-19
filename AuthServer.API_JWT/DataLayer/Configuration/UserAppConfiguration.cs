using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configuration
{
    public class UserAppConfiguration : IEntityTypeConfiguration<UserAppModel>
    {
        public void Configure(EntityTypeBuilder<UserAppModel> builder)
        {
            builder.Property(x => x.City).HasMaxLength(50);
        }
    }
}
