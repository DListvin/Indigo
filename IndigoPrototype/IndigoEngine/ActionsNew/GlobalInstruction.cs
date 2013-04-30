using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// Instuctions such add, delete, etc object
    /// </summary>
    public class GlobalInstruction : IAtomicInstruction
    {
        public Agent TargetAgent { get; private set; }
        public OperationWorld worldOperation { get; private set; }      

        #region Constuctors
        public GlobalInstruction(Agent agent, OperationWorld operation)
        {
            TargetAgent = agent;
            worldOperation = operation;
        }
        #endregion
    }

    /// <summary>
    /// type of operation, that world must perform
    /// </summary>
    public enum OperationWorld 
    { 
        addAgent, 
        deleteAgent, 
        DverMneZapili 
    };
}
