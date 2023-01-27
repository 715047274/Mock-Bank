using System;

namespace MockBank.Application.Features.Berkeleys.HealthCheck.Queres.GetAppInfo
{
    public class AppInfoResponse
    {
        public AppInfoDto data { get; set; }
    }

    public class AppInfoDto
    {
        // TODO Return Real Application time
        private long _mockTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        public string Version { get; set; }
        public long Time {
            get
            {
                return _mockTime;
            }
        }
        public string Message { get; set; }
    }
}