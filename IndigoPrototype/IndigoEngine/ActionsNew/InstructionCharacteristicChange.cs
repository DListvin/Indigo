﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// An instruction to change state
    /// </summary>
    class InstructionCharacteristicChange : AbstractRegularInstruction
    {
        string characteristicName;
        int delta;

        #region Constructors

        public InstructionCharacteristicChange(Agent target, string characteristicName, int value)
            : base(target)
        {
            this.characteristicName = characteristicName;
            delta = value;
        }

        #endregion

        #region AbstractRegularInstruction realisation

        public override void Perform()
        {
            Characteristic ch = TargetAgent.CurrentState.FindByName(characteristicName);
            if (ch == null)
                throw new Exception("InstructionCharacteristicChange.Perform: Target Agent hasn't " + characteristicName + "characteristic!");
            ch.CurrentPercentValue += delta;
        }

        #endregion
    }
}
