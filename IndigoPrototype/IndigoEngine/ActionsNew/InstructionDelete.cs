using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// An instruction to change state
    /// </summary>
    class InstructionDelete : IAtomInstruction
    {
        public Agent TargetAgent { get; private set; } //Do I even need it?



        #region Constructors

        public InstructionDelete()
        {

        }

        #endregion

        #region IAtomInstuction realisation

        public void Perform()
        {

        }

        #endregion
    }
}
