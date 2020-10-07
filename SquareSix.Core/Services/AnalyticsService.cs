using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SquareSix.Core.Interfaces;

namespace SquareSix.Core.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public void Setup(string iosAppSecret, string androidAppSecret)
        {
            AppCenter.Start($"android={androidAppSecret};ios={iosAppSecret}", typeof(Analytics), typeof(Crashes));
        }

        public void ReportException(Exception e, Dictionary<string, string> properties = null)
        {
#if DEBUG
            Console.WriteLine($"Reported Exception: {e}");
#else
            Crashes.TrackError(e, properties);
#endif
        }

        public void TrackEvent(string eventName, Dictionary<string, string> extraValues = null)
        {
#if DEBUG
            Console.WriteLine($"Tracking Event: {eventName} with values: {extraValues}");
#else
            Analytics.TrackEvent(eventName, extraValues);
#endif
        }

        public void TrackPage(string pageName)
        {
#if DEBUG
            Console.WriteLine($"Tracking page view: {pageName}");
#else
            Analytics.TrackEvent(pageName);
#endif
        }
    }
}
