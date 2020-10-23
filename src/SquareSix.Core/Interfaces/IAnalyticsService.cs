using System;
using System.Collections.Generic;

namespace SquareSix.Core.Interfaces
{
    public interface IAnalyticsService
    {
        void Setup(string iosAppSecret, string androidAppSecret);
        void TrackEvent(string eventName, Dictionary<string, string> extraValues = null);
        void TrackPage(string pageName);
        void ReportException(Exception e, Dictionary<string, string> properties = null);
    }
}
