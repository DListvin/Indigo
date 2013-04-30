using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    class InstuctionInventory : RegularInstructionAbstract
    {
        Agent Object;
        OperationInventory inventoryOp;

        public InstuctionInventory(Agent Object, OperationInventory inventoryOperation)
            : base()
        {
            this.Object = Object;
            inventoryOp = inventoryOperation;
        }

        public override void Perform(Agent TargetAgent)
        {
            if (inventoryOp == OperationInventory.takeIn)
            {
                TargetAgent.Inventory.AddAgentToStorage(Object);
            }
            if (inventoryOp == OperationInventory.takeOut)
            {
                TargetAgent.Inventory.PopAgent(Object).CurrentLocation = Object.CurrentLocation;
            }
        }
    }

    enum OperationInventory
    {
        takeIn,
        takeOut,
    }
}
