using Autofac;
using IPAddressTracker.Implementation;
using IPAddressTracker.Interface;
using Serilog;
using Serilog.Events;
using System.Reflection.Metadata;
using System.Text;

namespace IPAddressTracker
{
    internal class Program
    {
        private static ILogger _logger;
        private static IContainer _container;
        private static IAppConfigurationManager _appConfigurationManager;

        public static void Main(string[] args)
        {
            _appConfigurationManager = new AppConfigurationManager();

            _logger = GetLogger();
            _logger.Information("IP Address Check");
            _logger.Information("================");

            _logger.Information($"IP Address File: {_appConfigurationManager.IPAddressFilePath}");

            _logger.Information("====================================");
            _logger.Information("External IP Address Check - complete");
        }

        private static ILogger GetLogger()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File(_appConfigurationManager.LogFile, LogEventLevel.Debug)
                .WriteTo.Console(LogEventLevel.Debug)
                .CreateLogger();

            return logger;
        }
    }
}
