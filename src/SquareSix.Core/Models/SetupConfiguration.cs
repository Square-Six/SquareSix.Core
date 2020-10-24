using System;

namespace SquareSix.Core
{
    public class SetupConfiguration
    {
        public string iOSAppCenterSecret { get; set; } = string.Empty;
        public string AndroidAppCenterSecret { get; set; } = string.Empty;
        public string SecureCacheName { get; set; } = "MySecureCache";
    }
}
