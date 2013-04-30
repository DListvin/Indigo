using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// All existing actions here
    /// </summary>
    public static class Actions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static ActionAbstract Go(Agent agent, Location endPoint)
        {
            var instructions = new List<IAtomicInstruction>();
            instructions.Add( new InstructionGo(endPoint));
            return new ActionAbstract(agent, instructions);
        }

        public static ActionAbstract Go(Agent agent, Agent endPoint)
        {
            var instructions = new List<IAtomicInstruction>();
            instructions.Add(new InstructionGo(endPoint));
            return new ActionAbstract(agent, instructions);
        }

        public static ActionAbstract Eat(Agent agent, Agent food)
        {
            var instructions = new List<IAtomicInstruction>();
            instructions.Add(new InstructionCharacteristicSet("FoodSatiety", 100)); // gont like string here
            instructions.Add(new GlobalInstruction( food, OperationWorld.deleteAgent));
            return new ActionAbstract(agent, instructions);
        }

        public static ActionAbstract Drink(Agent agent, Agent drinkableAgent)
        {
            var instructions = new List<IAtomicInstruction>();
            instructions.Add(new InstructionCharacteristicSet("WaterSatiety", 100)); // gont like string here
            instructions.Add(new InstructionCharacteristicChange("Health",-1));
            return new ActionAbstract(agent, instructions);
        }

        public static ActionAbstract BreakCamp(Agent agent, Point Direction)
        {
            var instructions = new List<IAtomicInstruction>();
            var takeOuting =  agent.Inventory.GetAgentByType(typeof (AgentItemResLog), 2);
            instructions.Add(new InstuctionInventory(takeOuting[0], OperationInventory.takeOut));
            instructions.Add(new InstuctionInventory(takeOuting[1], OperationInventory.takeOut));
            AgentManMadeShelterCamp camp = new AgentManMadeShelterCamp();
            instructions.Add(new GlobalInstruction(camp, OperationWorld.addAgent));
            camp.CurrentLocation = agent.CurrentLocation + Direction;
            return new ActionAbstract(agent, instructions);
        }
        public static List<ActionAbstract> GetActionsEstimating(Agent agent, Characteristic characteristic)
        {
            var ans = new List<ActionAbstract>();
            ans.Add(Actions.Go(agent, new Location(0,0)));
            return ans;

        }
        public static ActionAbstract GetBestActionEstimating(Agent agent, Characteristic characteristic)
        {
            return Go(agent, new Location(0, 0));
        }
    }
}