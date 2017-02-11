/**
 * Logger for the whole project.
 * It is a singleton
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cockpit.Source.Debugging
{
    public class Logger
    {
        private static Logger instance = new Logger();

        private Logger()
        {
            //Create / Opens a log file
        }

        //Singoleton implementation
        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
