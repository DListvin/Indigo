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
        static Dictionary<string, List<Type>> ActionAgentConformity;
        /// <summary>
        /// this is for correct work of GetActionsEstimating and GetBestActionEstimating. Not finised yet
        /// </summary>
        public static void Init()
        {
            ActionAgentConformity = new Dictionary<string, List<Type>>();
            instructions = new BindingList<IAtomicInstruction>();
           
            // CharacteristicToAction = new Dictionary<Characteristic, Func<ActionAbstract>>();
            //CharacteristicToAction.Add( Characteristics.FoodSatiety, 
            instructions.ListChanged += new ListChangedEventHandler(instructions_ListChanged);

           /* Type t = typeof(Actions);
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
                             {
                                 Type argType = p.ParameterType;
                                 if(!argType.IsAbstract)
                                     param.Add(Activator.CreateInstance(argType));
                                 else
                                     param.Add(new AgentLivingIndigo());

                             }
                             MI.Invoke(null,param.ToArray());
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
            var ans = new ActionAbstract("Go",agent, instructions.ToList());
            ActionAgentConformity.Add(ans.Name, new List<Type>(){ typeof(AgentLiving)});
            return ans;
        }

        public static ActionAbstract Go(Agent agent, Agent endPoint)
        {
            instructions.Clear();
            instructions.Add(new InstructionGo(endPoint));
            var ans = new ActionAbstract("Go", agent, instructions.ToList());
            ActionAgentConformity.Add(ans.Name, new List<Type>() { typeof(AgentLiving) });
            return ans;
        }

        public static ActionAbstract Eat(Agent agent, Agent food)
        {
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicSet(Characteristics.FoodSatiety, 100));
            instructions.Add(new GlobalInstruction( food, OperationWorld.deleteAgent));
            var ans = new ActionAbstract("Eat", agent, instructions.ToList());
            ActionAgentConformity.Add(ans.Name, new List<Type>() { typeof(AgentLiving) });
            return ans;
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
            return new ActionAbstract("BreakCamp", agent, instructions.ToList());
        }

        public static ActionAbstract DoNothing(Agent agent)
        {
            instructions.Clear();
            return new ActionAbstract("DoNothing", agent, instructions.ToList());
        }

        public static ActionAbstract Rest(Agent agent)
        {
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, 1));
            return new ActionAbstract("Rest", agent, instructions.ToList());
        }

        public static List<ActionAbstract> Drink(Agent agent, Agent drinkableAgent)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicSet(Characteristics.WaterSatiety, 100));
            ans.Add(new ActionAbstract("DrinkSubject", agent, instructions.ToList()));
            instructions = new BindingList<IAtomicInstruction>(); // warning!!!
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionAbstract("DrinkObject", drinkableAgent, instructions.ToList()));
            return ans;
        }

        public static List<ActionAbstract> Atack(Agent agent, Agent victimAgent)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Peacefulness, 1)); 
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, -1)); 
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1));
            ans.Add(new ActionAbstract("AtackSubject",agent, instructions.ToList()));

            instructions = new BindingList<IAtomicInstruction>(); // warning!!!
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionAbstract("AtackObject",victimAgent, instructions.ToList()));
            return ans;
        }

        public static List<ActionAbstract> Obtain(Agent agent, Agent Object, Agent resourse)
        {
            var ans = new List<ActionAbstract>();
            instructions.Clear();
            instructions.Add(new InstuctionInventory(resourse, OperationInventory.takeOut));
            ans.Add(new ActionAbstract("ObtainObject",Object, instructions.ToList()));

            instructions = new BindingList<IAtomicInstruction>();
            instructions.Add(new InstuctionInventory(resourse, OperationInventory.takeIn));
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1));
            ans.Add(new ActionAbstract("ObtainSubject", agent, instructions.ToList()));
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