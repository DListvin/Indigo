using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// usual Instructions, which can be perform inside agent
    /// </summary>
    abstract class AbstractRegularInstruction : IAtomicInstruction
    {
        public Agent TargetAgent { get; protected set; }
        public abstract void Perform();

        protected AbstractRegularInstruction(Agent agent)
        {
            TargetAgent = agent;
        }
    }
}
