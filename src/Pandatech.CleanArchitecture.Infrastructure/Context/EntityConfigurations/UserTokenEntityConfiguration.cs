using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Infrastructure.Context.EntityConfigurations;

public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserTokenEntity>
{
   public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
   {
      builder.HasKey(x => x.Id);

      builder.HasOne(x => x.User)
         .WithMany(u => u.UserTokens)
         .HasForeignKey(x => x.UserId)
         .IsRequired();

      builder.HasOne(x => x.PreviousUserTokenEntity)
         .WithOne()
         .HasForeignKey<UserTokenEntity>(x => x.PreviousUserTokenId)
         .IsRequired(false);

      builder.HasIndex(x => x.AccessTokenHash).IsUnique();
      builder.HasIndex(x => x.RefreshTokenHash).IsUnique();
   }
}
