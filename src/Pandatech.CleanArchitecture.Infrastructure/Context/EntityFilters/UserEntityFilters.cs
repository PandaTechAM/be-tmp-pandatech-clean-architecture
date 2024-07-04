using BaseConverter;
using GridifyExtensions.Models;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Infrastructure.Context.EntityFilters;

public class UserEntityFilters : FilterMapper<User>
{
   public UserEntityFilters()
   {
      GenerateMappings();
      AddMap("Role", x => x.Role != UserRole.SuperAdmin);
      AddMap("Id", x => x.Id, x => PandaBaseConverter.Base36ToBase10NotNull(x));
      AddMap("FullName", x => x.FullName.ToLower(), x => x.ToLower());
      AddMap("Username", x => x.Username.ToLower(), x => x.ToLower());
      AddMap("Comment", x => x.Comment.ToLower(), x => x.ToLower());
      AddDefaultOrderBy("FullName");
   }
}
