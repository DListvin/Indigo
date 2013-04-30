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
    public class ActionAbstract
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        List<IAtomicInstruction> Instructions;
        Agent Target;

        public ActionAbstract(Agent argTarget, List<IAtomicInstruction> instructions)
        {
            Target = argTarget;
            Instructions = instructions;
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
