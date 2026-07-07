using Microsoft.AspNetCore.Builder;
using SharedKernel.Helpers;

namespace Pandatech.CleanArchitecture.Core;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddCoreLayer(this WebApplicationBuilder builder)
    {
        AssemblyRegistry.Add(typeof(DependencyInjection).Assembly);
        return builder;
    }
}
