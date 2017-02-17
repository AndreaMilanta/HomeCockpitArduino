/**
 * Generic message
 * There will be implementation for every command
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cockpit.Source.DataManagement.Messages.Values;

namespace Cockpit.Source.DataManagement.Messages
{
    abstract class Message
    {
        private Value value;
        
        public Value getValue()
        {
            return value;
        }

        //Visitor Accept
        public abstract void Execute(MessageVisitor visitor);    
    }
}
