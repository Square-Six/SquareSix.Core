using System;

namespace SquareSix.Core
{
    public class SetupConfiguration
    {
        public bool UseAnalyticsService { get; set; }
        public bool UseAlertService { get; set; }
        public bool UseSecureCacheService { get; set; }
        public string iOSAppCenterSecret { get; set; }
        public string AndroidAppCenterSecret { get; set; }
        public string SecureCacheName { get; set; }
    }
}
