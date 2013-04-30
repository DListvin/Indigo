using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine.ActionsNew
{
    class InstuctionInventory : AbstractRegularInstruction
    {
        Agent Object;
        OperationInventory inventoryOp;

        InstuctionInventory(Agent Subject, Agent Object, OperationInventory inventoryOperation)
            : base(Subject)
        {
            this.Object = Object;
            inventoryOp = inventoryOperation;
        }

        public override void Perform()
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
        takeOut
    }
}
