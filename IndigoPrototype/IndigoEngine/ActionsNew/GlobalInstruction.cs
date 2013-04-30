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
    class GlobalInstruction : IAtomicInstruction
    {
        public Agent TargetAgent { get; private set; }
        OperationWorld worldOperation;      

        #region Constuctors
        GlobalInstruction(Agent agent, OperationWorld operation)
        {
            TargetAgent = agent;
            worldOperation = operation;
        }
        #endregion
    }

    /// <summary>
    /// type of operation, that world must perform
    /// </summary>
    enum OperationWorld 
    { 
        addAgent, 
        deleteAgent, 
        DverMneZapili 
    };
}
