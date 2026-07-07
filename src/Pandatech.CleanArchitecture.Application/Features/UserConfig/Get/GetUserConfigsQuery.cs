using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Get;

public record GetUserConfigsQuery(string[] Keys) : IQuery<Dictionary<string, string>>;
