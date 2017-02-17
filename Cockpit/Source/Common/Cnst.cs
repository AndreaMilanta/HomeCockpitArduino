/**
 * Class containing constants
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cockpit.Source.Common
{
    //This must match the Arduino equivalent
    enum Channel
    {
        LIGHTS,
        LAST          //This must always remain last as it works as an upper bound
    }

    public static class Cnst
    {
        //TraceSource
        public const String SERIAL_TRACESRC = "TraceTest";
        
    }
}
