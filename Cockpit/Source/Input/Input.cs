/**
 * Interface that represents a generic input
 * The generic inputs fills the FIFO with 
 * a proper message related to the input 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Cockpit.Source.DataManagement.Messages;

namespace Cockpit.Source.Input
{
    abstract class Input
    {
        protected ConcurrentQueue<Message> queue;

        //Enqueues a message
        public void enqueue(Message mex)
        {
            queue.Enqueue(mex);
        }
    }
}
