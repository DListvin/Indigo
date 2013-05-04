using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    class InstuctionInventory : RegularInstructionAbstract
    {
        Agent Object; //What to take in or take out
        OperationInventory inventoryOp;

        public InstuctionInventory(Agent Object, OperationInventory inventoryOperation)
            : base()
        {
            this.Object = Object;
            inventoryOp = inventoryOperation;
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="TargetAgent">Inventory owner</param>
        public override void Perform(Agent TargetAgent)
        {
            if (inventoryOp == OperationInventory.takeIn)
            {
                TargetAgent.Inventory.AddAgentToStorage(Object);
            }
            if (inventoryOp == OperationInventory.takeOut)
            {
                TargetAgent.Inventory.PopAgent(Object);
            }
        }
    }

    enum OperationInventory
    {
        takeIn,
        takeOut,
    }
}
