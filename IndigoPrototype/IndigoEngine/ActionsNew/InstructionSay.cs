using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    class InstructionSay : RegularInstructionAbstract
    {
        string speach;
        InstructionSay(string speach)
            : base()
        {
            this.speach = speach;
        }


        public override void Perform(Agent TargetAgent)
        {
            throw new NotImplementedException();
        }
    }
}
