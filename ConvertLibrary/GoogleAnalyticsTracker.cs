using GoogleAnalyticsTracker.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertLibrary
{
    public static class GoogleAnalyticsTracker
    {
        public static async Task Tracker(string pageTitle, string pageUrl)
        {
            SimpleTrackerEnvironment ste = new SimpleTrackerEnvironment(Environment.OSVersion.ToString(), Environment.OSVersion.VersionString, "");
            using (SimpleTracker tracker = new SimpleTracker("UA-97814311-2", ste))
            {
                await tracker.TrackPageViewAsync(pageTitle, pageUrl, new Dictionary<int, string>());
            }
           
        }
    }
}
