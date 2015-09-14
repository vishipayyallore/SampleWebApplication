using System;
using System.Diagnostics.Tracing;

namespace copwebapplication.Services
{
    public class CloudSampleEventSource : EventSource
    {
        private static readonly Lazy<CloudSampleEventSource> Instance = new Lazy<CloudSampleEventSource>(() => new CloudSampleEventSource());

        public static CloudSampleEventSource Log 
        {
            get
            {
                return Instance.Value;
            }
        }

        [Event(7001, Level=EventLevel.Informational, Message="Search called ... ")]
        public void SearchCalled(string firtName, string lastName) 
        {
            if (this.IsEnabled()) 
            {
                WriteEvent(7001, firtName, lastName);
            }
        }
    }
}
