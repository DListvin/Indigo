using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.AgentsNew
{
    class AgentBase : Quality
    {
        public Location Location { get; set; }
        public State State { get; set; }
    }
}
