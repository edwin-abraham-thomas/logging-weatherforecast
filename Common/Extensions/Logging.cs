using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace Common.Extensions;

public static class Logging
{
    public static void ConfigureLogging(this IServiceCollection services)
    {
        services.AddSerilog(config => config
            .WriteTo.Console(new RenderedCompactJsonFormatter ()));
    }
}
