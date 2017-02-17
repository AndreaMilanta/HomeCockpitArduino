using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cockpit.Test.Tracing
{
    class TraceSourceTest
    {
        private static TraceSource testSource = new TraceSource("TraceTest");

        static void Main(string[] args)
        {
            Activity1();

            //Saves the original setting from the config file.
            EventTypeFilter configFilter = (EventTypeFilter)testSource.Listeners["console"].Filter;

            //New event type filter. Change sourceLevels to test differences
            //This filters what is written by the listener
            testSource.Listeners["console"].Filter = new EventTypeFilter(SourceLevels.Warning);

            //This filters what the source sends to the listeners. Overrides config file
            testSource.Switch.Level = SourceLevels.All;

            Activity2();

            // Restore the original filter settings.
            testSource.Listeners["console"].Filter = configFilter;
            Activity3();
            testSource.Close();
            return;
        }
        
        //generalTest1
        static void Activity1()
        {
            testSource.TraceEvent(TraceEventType.Error, 1,
                "Error message.");
            testSource.TraceEvent(TraceEventType.Warning, 2,
                "Warning message.");
        }

        //generalTest2
        static void Activity2()
        {
            testSource.TraceEvent(TraceEventType.Critical, 3,
                "Critical message.");
            testSource.TraceEvent(TraceEventType.Warning, 2,
                "Warning message.");
        }

        //generalTest3
        static void Activity3()
        {
            testSource.TraceEvent(TraceEventType.Error, 4,
                "Error message.");
            testSource.TraceInformation("Informational message.");
        }
    }
}
