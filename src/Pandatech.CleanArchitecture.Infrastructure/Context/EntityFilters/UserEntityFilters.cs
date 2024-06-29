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
      AddDefaultOrderBy("FullName");
   }
}
