using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// All Actions is that
    /// </summary>
    public class ActionForOneAgent: NameableObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public List<Characteristic> PositiveEffect { get; private set; }
        public List<Characteristic> NegativeEffect { get; private set; }
        
        Agent Target;
        List<IAtomicInstruction> Instructions;

        public ActionForOneAgent(string name, Agent argTarget, List<IAtomicInstruction> instructions)
        {
            Target = argTarget;
            Instructions = instructions;
            PositiveEffect = new List<Characteristic>();
            NegativeEffect = new List<Characteristic>();
            Name = name;

            //PositiveEffect and NegativeEffect init (it can be more optimal)
            foreach (var instr in instructions)
            {
                if (instr is IHaveEffectInstruction)
                {
                    if ((instr as IHaveEffectInstruction).PositiveEffect)
                        PositiveEffect.Add((instr as IHaveEffectInstruction).Characteristic);
                    else if ((instr as IHaveEffectInstruction).NegativeEffect)
                        NegativeEffect.Add((instr as IHaveEffectInstruction).Characteristic);
                }
            }
        }

        /// <summary>
        /// Complete all regular instructions of action and return all global instructions of action. It is optimization
        /// </summary>
        /// <returns>all global instructions of action</returns>
        public List<GlobalInstruction> CompleteRegular()
        {
            var ans = new List<GlobalInstruction>();
            if (Instructions == null)
                throw new Exception("ActionAbstract.Complete: Instructions list is null");
            foreach(var instr in Instructions)
            {
                if (instr is RegularInstructionAbstract)
                {
                    (instr as RegularInstructionAbstract).Perform(Target);
                }
                else
                    ans.Add(instr as GlobalInstruction);
            }
            return ans;
        }
         
    }	
}
