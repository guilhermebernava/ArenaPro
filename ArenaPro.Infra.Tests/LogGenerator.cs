using Microsoft.Extensions.Logging;

namespace ArenaPro.Infra.Tests;
public static class LogGenerator
{
    public static ILogger<T> GetLogger<T>()
    {
        var loggerFactory = LoggerFactory.Create(builder =>{});
        return loggerFactory.CreateLogger<T>();
    }
}
