using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SquareSix.Core
{
    public static class SquareSix
    {
        public static void Initialize(SetupConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("No 'SetupConfiguration' was found when running 'SquareSix.Initialize'");
            }

            if (config.UseAlertService)
            {
                SimpleIOC.Container.Register<ISquaredAlertService>(new AlertService());
            }

            if (config.UseAnalyticsService)
            {
                if (string.IsNullOrEmpty(config.iOSAppCenterSecret))
                {
                    throw new ArgumentException("iOSAppCenterSecret is missing");
                }

                if (string.IsNullOrEmpty(config.AndroidAppCenterSecret))
                {
                    throw new ArgumentException("AndroidAppCenterSecret is missing");
                }

                var analyticsService = new SquaredAnalyticsService();
                analyticsService.Setup(config?.iOSAppCenterSecret, config?.AndroidAppCenterSecret);
                SimpleIOC.Container.Register<ISquaredAnalyticsService>(analyticsService);
            }

            if (config.UseSecureCacheService)
            {
                var cacheService = new SquaredCacheService();
                cacheService.Setup(config?.SecureCacheName);
                SimpleIOC.Container.Register<ISquaredCacheService>(cacheService);
            }

            ConfigureJson();
        }

        private static void ConfigureJson()
        {
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
                };
                return settings;
            });
        }
    }
}
