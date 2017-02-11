using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cockpit.Source.DataManagement.Messages.Values;

namespace Cockpit.Source.DataManagement.Messages
{
    class LightsMessage : Message
    {
        private ByteValue value;

        public LightsMessage(ByteValue v)
        {
            value = v;
        }

        //Visitor Accept
        public virtual void Execute(MessageVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
