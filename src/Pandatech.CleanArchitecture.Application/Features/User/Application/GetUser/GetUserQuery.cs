using System.Text.Json.Serialization;
using Pandatech.CleanArchitecture.Application.Features.User.Contracts.GetById;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetUser;

public class GetUserQuery(long id) : IQuery<GetUserQueryResponse>
{
   [JsonIgnore] public long Id { get; set; } = id;
}
