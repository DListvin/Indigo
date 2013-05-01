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
    public abstract class RegularInstructionAbstract : IAtomicInstruction
    {
        public abstract void Perform(Agent TargetAgent);

        protected RegularInstructionAbstract()
        {
        }
    }
}
