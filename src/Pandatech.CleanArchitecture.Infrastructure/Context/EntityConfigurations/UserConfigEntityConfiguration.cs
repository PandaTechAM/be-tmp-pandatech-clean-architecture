using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Infrastructure.Context.EntityConfigurations;

public class UserConfigEntityConfiguration : IEntityTypeConfiguration<UserConfig>
{
   public void Configure(EntityTypeBuilder<UserConfig> builder)
   {
      builder.HasKey(e => e.Id);
      builder.HasIndex(e => new { e.UserId, e.Key }).IsUnique();
   }
}
