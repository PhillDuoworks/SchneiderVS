using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Events;

namespace Schneider.Minefield.Core.Utilties;

public class SerilogBuilder
{
  public ILogger GetLogger(IConfiguration configuration, string version)
  {
    string logLevel = configuration["Logging:Level"] ?? "Debug";


    LoggerConfiguration loggerConfiguration = new LoggerConfiguration().MinimumLevel.ControlledBy(
        new LoggingLevelSwitch()
        {
          MinimumLevel = this.GetLogLevel(logLevel)
        }).Enrich

      .With((ILogEventEnricher)new PropertyEnricher(nameof(version), (object)version)).WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] Environment={environment} Version={version} CausationId={CausationId} CorrelationId={CorrelationId} RequestId={RequestId} {Event} - {Message:lj}{NewLine}{Exception}");

    return (ILogger)loggerConfiguration.CreateLogger();
  }

  private LogEventLevel GetLogLevel(string logLevel)
  {
    if (string.IsNullOrEmpty(logLevel))
      return LogEventLevel.Verbose;
    string lower = logLevel.ToLower();
    if (lower == "debug")
      return LogEventLevel.Debug;
    if (lower == "error")
      return LogEventLevel.Error;
    if (lower == "fatal")
      return LogEventLevel.Fatal;
    if (lower == "information")
      return LogEventLevel.Information;
    return lower == "verbose" || !(lower == "warning") ? LogEventLevel.Verbose : LogEventLevel.Warning;
  }
}