using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine.ActionsNew
{
    //Elementary instuction to movement
    class InstructionGo : AbstractRegularInstruction
    {
        Point Direction;

        #region Constructors
        public InstructionGo(Agent argTargetAgent, Location argDirectionEnd)
            : base(argTargetAgent)
        {
            Direction = Location.Normilize(argDirectionEnd, TargetAgent.CurrentLocation);
        }

        public InstructionGo(Agent argTargetAgent)
            : base(argTargetAgent)
        {
            Random rand = new Random();
            Direction = new Point(rand.Next(2), rand.Next(2));
        }
        #endregion

        #region IAtomInstuction realisation
        public override void Perform()
        {
            TargetAgent.CurrentLocation += Direction;
        }
        #endregion
    }
}
