using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using System.ComponentModel;
using System.Reflection;
using NLog;

namespace IndigoEngine.ActionsNew
{
    /// <summary>
    /// All existing actions here
    /// </summary>
    public static class Actions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //static Dictionary<Characteristic, Func<ActionAbstract>> CharacteristicToAction;
        static BindingList<IAtomicInstruction> instructions;

        /// <summary>
        /// this is for correct work of GetActionsEstimating and GetBestActionEstimating. Not finised yet
        /// </summary>
        public static void Init()
        {
            instructions = new BindingList<IAtomicInstruction>();
            //CharacteristicToAction = new Dictionary<Characteristic, Func<ActionAbstract>>();
            //CharacteristicToAction.Add( Characteristics.FoodSatiety, 
            /*instructions.ListChanged += new ListChangedEventHandler(instructions_ListChanged);

            Type t = typeof(Actions);
            MethodInfo[] MArr = t.GetMethods();
            foreach (MethodInfo MI in MArr)
            {
                ParameterInfo[] PArr = MI.GetParameters();
                if (PArr.Length > 1)
                {
                     Type tp = PArr[0].ParameterType; // type of first parametr
                     if (tp.IsEquivalentTo(typeof(Agent)))
                     {
                         if (PArr.Length == 1)
                             MI.Invoke(new AgentLivingIndigo(), null);
                         else
                         {
                             List<object> param = new List<object>();
                             foreach (var p in PArr)
                                 param.Add(p.
                             MI.Invoke(new AgentLivingIndigo(), param.ToArray());
                         }
                     }
                }
            }*/
        }

        static void instructions_ListChanged(object sender, ListChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static ActionAbstract Go(Agent agent, Location endPoint)
        {
            instructions.Clear();
            instructions.Add( new InstructionGo(endPoint));
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static ActionAbstract Go(Agent agent, Agent endPoint)
        {
            instructions.Clear();
            instructions.Add(new InstructionGo(endPoint));
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static ActionAbstract Eat(Agent agent, Agent food)
        {
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicSet(Characteristics.FoodSatiety, 100));
            instructions.Add(new GlobalInstruction( food, OperationWorld.deleteAgent));
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static ActionAbstract BreakCamp(Agent agent, Point Direction)
        {
            instructions.Clear();
            var takeOuting =  agent.Inventory.GetAgentByType(typeof (AgentItemResLog), 2);
            instructions.Add(new InstuctionInventory(takeOuting[0], OperationInventory.takeOut));
            instructions.Add(new InstuctionInventory(takeOuting[1], OperationInventory.takeOut));
            AgentManMadeShelterCamp camp = new AgentManMadeShelterCamp();
            instructions.Add(new GlobalInstruction(camp, OperationWorld.addAgent));
            camp.CurrentLocation = agent.CurrentLocation + Direction;
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static ActionAbstract DoNothing(Agent agent)
        {
            instructions.Clear();
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static ActionAbstract Rest(Agent agent)
        {
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, 1));
            return new ActionAbstract(agent, instructions.ToList());
        }

        public static List<ActionAbstract> Drink(Agent agent, Agent drinkableAgent)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicSet(Characteristics.WaterSatiety, 100)); // gont like string here
            ans.Add(new ActionAbstract(agent, instructions.ToList()));
            instructions = new BindingList<IAtomicInstruction>(); // warning!!!
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionAbstract(drinkableAgent, instructions.ToList()));
            return ans;
        }

        public static List<ActionAbstract> Atack(Agent agent, Agent victimAgent)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Peacefulness, 1)); //need to refact
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, -1)); //need to refact
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1)); //need to refact
            ans.Add(new ActionAbstract(agent, instructions.ToList()));

            instructions = new BindingList<IAtomicInstruction>(); // warning!!!
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionAbstract(victimAgent, instructions.ToList()));
            return ans;
        }

        public static List<ActionAbstract> Obtain(Agent agent, Agent subject, Agent resourse = null)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            if (resourse != null)
                instructions.Add(new InstuctionInventory(resourse, OperationInventory.takeOut));
            ans.Add(new ActionAbstract(subject, instructions.ToList()));

            instructions = new BindingList<IAtomicInstruction>();
            instructions.Add(new InstuctionInventory(resourse, OperationInventory.takeIn));
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1));
            ans.Add(new ActionAbstract(agent, instructions.ToList()));
            return ans;
        }
        
        // May be next code is not nessesary

        /// <summary>
        /// Get all Actions that estimate this characteristic
        /// </summary>
        public static List<ActionAbstract> GetActionsEstimating(Agent agent, Characteristic characteristic)
        {
            var ans = new List<ActionAbstract>();
            ans.Add(Actions.Go(agent, new Location(0,0)));
            return ans;

        }

        /// <summary>
        /// Get the best Action that estimate this characteristic
        /// </summary>
        public static ActionAbstract GetBestActionEstimating(Agent agent, Characteristic characteristic)
        {
            return Go(agent, new Location(0, -10));
        }
    }
}