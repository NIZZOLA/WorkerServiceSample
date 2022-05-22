using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public static class HelpersLibrary
    {
        public static int CalculateFirstTimeStart(string firstExecution)
        {
            var startTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + firstExecution);
            if (startTime < DateTime.Now)
                startTime = startTime.AddDays(1);

            var timeToStart = startTime.Subtract(DateTime.Now).TotalSeconds;
            return (int)timeToStart;
        }
    }
}
