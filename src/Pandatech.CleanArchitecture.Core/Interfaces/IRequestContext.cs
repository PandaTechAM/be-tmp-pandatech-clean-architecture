using Pandatech.CleanArchitecture.Core.DTOs.Auth;

namespace Pandatech.CleanArchitecture.Core.Interfaces;

public interface IRequestContext
{
   public Identity Identity { get; set; }
   public MetaData MetaData { get; set; }
   public bool IsAuthenticated { get; set; }
}
