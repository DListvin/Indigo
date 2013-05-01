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
    class InstructionCharacteristicChange : RegularInstructionAbstract, IHaveEffectInstruction
    {
        public Characteristic Characteristic { get; private set; }
        int delta;

        #region Constructors

        public InstructionCharacteristicChange(Characteristic characteristic, int value)
            : base()
        {
            Characteristic = characteristic;
            delta = value;
        }

        #endregion

        #region AbstractRegularInstruction realisation

        public override void Perform(Agent TargetAgent)
        {
            Characteristic ch = TargetAgent.CurrentState.FindByName(Characteristic.Name);
            if (ch == null)
                throw new Exception("InstructionCharacteristicChange.Perform: Target Agent hasn't " + Characteristic.Name + "characteristic!");
            ch.CurrentPercentValue += delta;
        }

        #endregion

        public bool PositiveEffect 
        {
            get
            {
                return delta < 0;
            }
        }
        public bool NegativeEffect
        {
            get
            {
                return delta > 0;
            }
        }
    }
}
