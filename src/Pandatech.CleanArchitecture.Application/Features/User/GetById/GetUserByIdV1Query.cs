using System.Text.Json.Serialization;
using Pandatech.CleanArchitecture.Application.Contracts.User.GetById;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.GetById;

public class GetUserByIdV1Query(long id) : IQuery<GetUserByIdV1QueryResponse>
{
   [JsonIgnore] public long Id { get; set; } = id;
}
