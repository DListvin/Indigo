using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine.ActionsNew
{
    //Elementary instuction to movement
    class InstructionGo : RegularInstructionAbstract
    {
        Location end;

        #region Constructors
        public InstructionGo(Location argDirectionEnd)
            : base()
        {
            end = argDirectionEnd;
        }
        public InstructionGo(Agent argDirectionEnd)
            : base()
        {
            end = argDirectionEnd.CurrentLocation;
        }
        #endregion

        #region IAtomInstuction realisation
        public override void Perform(Agent TargetAgent)
        {
            TargetAgent.CurrentLocation += Location.Normilize(end, TargetAgent.CurrentLocation);
        }
        #endregion
    }
}
