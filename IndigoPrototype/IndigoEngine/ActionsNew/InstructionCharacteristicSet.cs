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
    class InstructionCharacteristicSet : AbstractRegularInstruction
    {
        string characteristicName;
        int value;

        #region Constructors

        public InstructionCharacteristicSet(Agent target, string characteristicName, int value)
            : base(target)
        {
            this.characteristicName = characteristicName;
            this.value = value;
        }

        #endregion

        #region AbstractRegularInstruction realisation

        public override void Perform()
        {
            Characteristic ch = TargetAgent.CurrentState.FindByName(characteristicName);
            if (ch == null)
               throw new Exception("InstructionCharacteristicSet.Perform: Target Agent hasn't " + characteristicName + "characteristic!");
            ch.CurrentPercentValue = value;
        }

        #endregion
    }
}
