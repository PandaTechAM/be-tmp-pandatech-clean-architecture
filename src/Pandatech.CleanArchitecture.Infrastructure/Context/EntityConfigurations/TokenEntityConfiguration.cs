using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Infrastructure.Context.EntityConfigurations;

public class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
{
   public void Configure(EntityTypeBuilder<Token> builder)
   {
      builder.HasKey(x => x.Id);

      builder.HasOne(x => x.User)
         .WithMany(u => u.Tokens)
         .HasForeignKey(x => x.UserId)
         .IsRequired();

      builder.HasOne(x => x.PreviousToken)
         .WithOne()
         .HasForeignKey<Token>(x => x.PreviousTokenId)
         .IsRequired(false);

      builder.HasIndex(x => x.AccessTokenHash).IsUnique();
      builder.HasIndex(x => x.RefreshTokenHash).IsUnique();
   }
}
