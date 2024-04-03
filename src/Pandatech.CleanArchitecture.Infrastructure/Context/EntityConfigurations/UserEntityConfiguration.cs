using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Infrastructure.Context.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
  public void Configure(EntityTypeBuilder<UserEntity> builder)
  {
    builder.HasKey(b => b.Id);
    

    builder.HasMany(b => b.UserTokens)
      .WithOne(u => u.User)
      .HasForeignKey(u => u.UserId)
      .IsRequired();

    builder.HasIndex(b => b.Username).IsUnique();
    builder.HasIndex(b => b.FullName);
  }
}
