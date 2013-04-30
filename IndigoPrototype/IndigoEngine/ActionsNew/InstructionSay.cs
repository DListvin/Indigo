using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    class InstructionSay : AbstractRegularInstruction
    {
        string speach;
        InstructionSay(Agent agent, string speach)
            : base(agent)
        {
            this.speach = speach;
        }


        public override void Perform()
        {
            throw new NotImplementedException();
        }
    }
}
