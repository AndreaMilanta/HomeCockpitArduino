﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cockpit.Source.DataManagement.Messages
{
    interface MessageVisitor
    {
        void visit(LightsMessage mex);
    }
}
