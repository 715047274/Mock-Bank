using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WireMock;
using WireMock.Admin.Requests;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;
using WireMock.Util;

namespace MockBank.WebApi.Services
{
    public class WireMockService : IWireMockService
    {
        private WireMockServer _server;
        private readonly ILogger _logger;
        private readonly WireMockServerSettings _settings;

        private class Logger : IWireMockLogger
        {
            private readonly ILogger _logger;

            public Logger(ILogger logger)
            {
                _logger = logger;
            }

            public void Debug(string formatString, params object[] args)
            {
                _logger.LogDebug(formatString, args);
            }

            public void Info(string formatString, params object[] args)
            {
                _logger.LogInformation(formatString, args);
            }

            public void Warn(string formatString, params object[] args)
            {
                _logger.LogWarning(formatString, args);
            }

            public void Error(string formatString, params object[] args)
            {
                _logger.LogError(formatString, args);
            }

            public void DebugRequestResponse(LogEntryModel logEntryModel, bool isAdminrequest)
            {
                string message = JsonConvert.SerializeObject(logEntryModel, Formatting.Indented);
                _logger.LogDebug("Admin[{0}] {1}", isAdminrequest, message);
            }

            public void Error(string formatString, Exception exception)
            {
                _logger.LogError(formatString, exception.Message);
            }
        }

        public WireMockService(ILogger<WireMockService> logger, IOptions<WireMockServerSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;

            _settings.Logger = new Logger(logger);
        }

        public void Start()
        {
            _logger.LogInformation("WireMock.Net server starting");

            _server = WireMockServer.Start(_settings);

            _logger.LogInformation($"WireMock.Net server settings {JsonConvert.SerializeObject(_settings)}");
        }

        public void Stop()
        {
            _logger.LogInformation("WireMock.Net server stopping");
            _server?.Stop();
        }

        public object RespondMessage(BodyData bodyData)
        {
            var something = new ResponseMessage();
            something.AddHeader("Content-Type", "application/json");
            something.BodyData = bodyData;
            something.StatusCode = 200;
               
            return something;
        }

      
        public class AppInfo
        {
            public string Version { get; set; }
            public DateTime Time { get; set; }
            public string Message { get; set; }
        }
        

    }
}