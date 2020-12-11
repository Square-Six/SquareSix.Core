using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace SquareSix.Core
{
    public class SquaredAnalyticsService : ISquaredAnalyticsService
    {
        public void Setup(string iosAppSecret, string androidAppSecret)
        {
            AppCenter.Start($"android={androidAppSecret};ios={iosAppSecret}", typeof(Analytics), typeof(Crashes));
        }

        public void ReportException(Exception e, Dictionary<string, string> properties = null)
        {
            Crashes.TrackError(e, properties);
        }

        public void TrackEvent(string eventName, Dictionary<string, string> extraValues = null)
        {
            Analytics.TrackEvent(eventName, extraValues);
        }

        public void TrackPage(string pageName)
        {
            Analytics.TrackEvent(pageName);
        }
    }
}
