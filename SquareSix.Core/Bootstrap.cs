﻿using System;
using SquareSix.Core.Interfaces;
using SquareSix.Core.Models;
using SquareSix.Core.Services;

namespace SquareSix.Core
{
    public static class Bootstrap
    {
        public static void Initialize(SetupConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("No 'SetupConfiguration' was found when running 'Bootstrap.Initialize'");
            }

            if (config.UseAlertService)
            {
                SimpleIOC.Container.Register<IAlertService>(new AlertService());
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

                var analyticsService = new AnalyticsService();
                analyticsService.Setup(config?.iOSAppCenterSecret, config?.AndroidAppCenterSecret);
                SimpleIOC.Container.Register<IAnalyticsService>(analyticsService);
            }

            if (config.UseSecureCacheService)
            {
                var cacheService = new SecureCacheService();
                cacheService.Setup(config?.SecureCacheName);
                SimpleIOC.Container.Register<ISecureCacheService>(cacheService);
            }
        }
    }
}