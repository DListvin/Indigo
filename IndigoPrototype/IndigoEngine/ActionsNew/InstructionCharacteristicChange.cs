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
    class InstructionCharacteristicChange : IAtomInstruction
    {
        public Agent TargetAgent { get; private set; } //Do I even need it?

        string characteristicName;
        int value;


        #region Constructors

        public InstructionCharacteristicChange(Agent target, string characteristicName, int value)
        {
            TargetAgent = target;
            this.characteristicName = characteristicName;
            this.value = value;
        }

        #endregion

        #region IAtomInstuction realisation

        public void Perform()
        {
            foreach (var c in TargetAgent.CurrentState)
            {
                if (c.Name == characteristicName)
                    c.CurrentPercentValue = value;
            }
        }

        #endregion
    }
}
