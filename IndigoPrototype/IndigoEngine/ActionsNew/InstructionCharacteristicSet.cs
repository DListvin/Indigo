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
    public class InstructionCharacteristicSet : RegularInstructionAbstract , IHaveEffectInstruction
    {
        public Characteristic Characteristic { get; private set; }
        int value;

        #region Constructors

        public InstructionCharacteristicSet(Characteristic argCharacteristic, int value)
            : base()
        {
            Characteristic = argCharacteristic;
            this.value = value;
        }

        #endregion

        #region AbstractRegularInstruction realisation

        public override void Perform(Agent TargetAgent)
        {
            Characteristic ch = TargetAgent.CurrentState.FindByName(Characteristic.Name);
            if (ch == null)
               throw new Exception("InstructionCharacteristicSet.Perform: Target Agent hasn't " + Characteristic.Name + "characteristic!");
            ch.CurrentPercentValue = value;
        }

        #endregion

        public bool PositiveEffect
        {
            get
            {
                return value == Characteristic.MaxValue;
            }
        }
        public bool NegativeEffect
        {
            get
            {
                return value < Characteristic.CriticalUnitValue;
            }
        }

    }
}
