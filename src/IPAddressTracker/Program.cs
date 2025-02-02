using System.Diagnostics;
using Autofac;
using IPAddressTracker.Implementation;
using IPAddressTracker.Interface;
using Serilog;
using Serilog.Events;

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
            _container = BuildContainer();

            _logger.Information("IP Address Check");
            _logger.Information("================");

            var ipAddressFileManager = _container.Resolve<IIPAddressFileManager>();
            var ipAddressRemoteReader = _container.Resolve<IIPAddressRemoteReader>();

            string recordedIPAddress = ipAddressFileManager.IPAddress;
            string remoteIPAddress = ipAddressRemoteReader.GetIPAddress();

            _logger.Information($"Current IP Address recorded: {recordedIPAddress}");
            _logger.Information($"IP Address from remote: {remoteIPAddress}");

            if (!recordedIPAddress.Equals(remoteIPAddress))
            {
                _logger.Information($"IP address mismatch, new IP Address: {remoteIPAddress}");
                ipAddressFileManager.UpdateIPAddress(remoteIPAddress);
                
                RunExternalIPChangeExecutable();
            }

            _logger.Information("====================================");
            _logger.Information("External IP Address Check - complete");
        }

        private static void RunExternalIPChangeExecutable()
        {
            _logger.Information($"Launching external program on IP change: {_appConfigurationManager.ExternalIPAddressChangeExe}");
            var psi = new ProcessStartInfo
            {
                FileName = _appConfigurationManager.ExternalIPAddressChangeExe,
                Arguments = _appConfigurationManager.ExternalExeArgs,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    _logger.Information($"External program: {process.StandardOutput.ReadLine()}");
                }

                string error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    _logger.Error($"External program error: {error}");
                }

                process.WaitForExit();
            }
        }

        private static ILogger GetLogger()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File(_appConfigurationManager.LogFile, LogEventLevel.Debug)
                .WriteTo.Console(LogEventLevel.Debug)
                .CreateLogger();

            return logger;
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(x => _logger).As<ILogger>().SingleInstance();
            builder.Register(x => _appConfigurationManager).As<IAppConfigurationManager>().SingleInstance();

            builder.RegisterType<IPAddressFileManager>().As<IIPAddressFileManager>();
            builder.RegisterType<IPAddressRemoteReader>().As<IIPAddressRemoteReader>();

            return builder.Build();
        }
    }
}
