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
    public delegate ActionAbstract ActionFunc(params Agent[] agents);

    /// <summary>
    /// All existing actions here
    /// </summary>
    public static class Actions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static BindingList<IAtomicInstruction> instructions; // subsideary variable for Action creating
        static Dictionary<string, List<Type>> ActionAgentConformity; // Dictionary of Action and Agent Conformity
        static Dictionary<Characteristic, List<ActionFunc>> CharactActionConformity; // Dictionary of Action and Characteristic Conformity

        /// <summary>
        /// this is for correct work of GetActionsEstimating and GetBestActionEstimating
        /// It is function for init and perform only one time before all game will be running
        /// </summary>
        public static void Init()
        {
            FillCharactActionConformityDic();
            instructions = new BindingList<IAtomicInstruction>();
            var EventHandler = new ListChangedEventHandler(instructions_ListChanged);
            instructions.ListChanged += EventHandler; // event handler fill CharactActionConformity disctionary
            CharactActionConformity = new Dictionary<Characteristic, List<ActionFunc>>();

            MethodInfo[] methodInfo = typeof(Actions).GetMethods();
            foreach (MethodInfo MI in methodInfo)
            {
                ParameterInfo[] prameterInfo = MI.GetParameters();
                if (prameterInfo.Length > 0)
                {
                    Type tp = prameterInfo[0].ParameterType; // type of first parametr
                    if (tp.IsEquivalentTo(typeof(Agent))) // if first param is Agent
                    {
                        List<object> param = new List<object>(); // list of function params
                        foreach (var p in prameterInfo) // foreach that creates list of params for function
                        {
                            Type argType = p.ParameterType;
                            if (!argType.IsAbstract)
                                param.Add(Activator.CreateInstance(argType));
                            else
                                param.Add(new AgentLivingIndigo());
                        }

                        try
                        {
                            MI.Invoke(null, param.ToArray());  // perform action function 
                        }
                        catch (Exception e) // here may be some trouble situations, that not dangerous, becouse 
                        {                   // dictionaries in the functions filling in the begining of function 
                        }
                    }
                }
            }
            instructions.ListChanged -= EventHandler;
        }

        /// <summary>
        /// fill CharactActionConformity disctionary, when something was added in instructions List
        /// </summary>
        /// <param name="sender">It can only be sunction from this class</param>
        /// <param name="e"></param>
        static void instructions_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                if (instructions[instructions.Count - 1] is IHaveEffectInstruction)
                {
                    IHaveEffectInstruction ICC = instructions[instructions.Count - 1] as IHaveEffectInstruction;
                    if (ICC.PositiveEffect)
                    {
                        if (CharactActionConformity.ContainsKey(ICC.Characteristic))
                        {
                            var ActionsList = new List<ActionFunc>();
                            CharactActionConformity.TryGetValue(ICC.Characteristic, out ActionsList);
                            ActionsList.Add(sender as ActionFunc);
                        }
                        else
                        {
                            CharactActionConformity.Add(ICC.Characteristic,
                                new List<ActionFunc>() { sender as ActionFunc });                            
                        }
                    }
                }
            }
        }

        static void FillCharactActionConformityDic()
        {
            ActionAgentConformity = new Dictionary<string, List<Type>>();
            ActionAgentConformity.Add("Go", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("Eat", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("BreakCamp", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("DoNothing", new List<Type>() { typeof(AgentLiving), typeof(AgentTree) });
            ActionAgentConformity.Add("Rest", new List<Type>() { typeof(AgentLiving), typeof(AgentTree) });
            ActionAgentConformity.Add("DrinkSubject", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("DrinkObject", new List<Type>() { typeof(AgentPuddle) });
            ActionAgentConformity.Add("AtackSubject", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("AtackObject", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("ObtainObject", new List<Type>() { typeof(AgentLiving) });
            ActionAgentConformity.Add("ObtainSubject", new List<Type>() { typeof(AgentTree) });
        }

        public static ActionAbstract Go(Agent agent, Location endPoint)
        {
            instructions.Clear();       
            instructions.Add( new InstructionGo(endPoint));
            var ans = new ActionForOneAgent("Go",agent, instructions.ToList());
            return new ActionAbstract(ans);
        }

        public static ActionAbstract Go(params Agent[] agents)
        {
            instructions.Clear();            
            instructions.Add(new InstructionGo(agents[1]));
            var ans = new ActionForOneAgent("Go", agents[0], instructions.ToList());            
            return new ActionAbstract(ans);
        }

        public static ActionAbstract Eat(params Agent[] agents)
        {
            instructions.Clear();            
            instructions.Add(new InstructionCharacteristicSet(Characteristics.FoodSatiety, 100));
            instructions.Add(new GlobalInstruction(agents[1], OperationWorld.deleteAgent));
            var ans = new ActionForOneAgent("Eat", agents[0], instructions.ToList());
            
            return new ActionAbstract(ans);
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
            return new ActionAbstract(new ActionForOneAgent("BreakCamp", agent, instructions.ToList()));
        }

        public static ActionAbstract DoNothing(params Agent[] agents)
        {
            instructions.Clear();
            return new ActionAbstract(new ActionForOneAgent("DoNothing", agents[0], instructions.ToList()));
        }

        public static ActionAbstract Rest(params Agent[] agents)
        {
            instructions.Clear();            
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, 1));
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, 1));
            return new ActionAbstract(new ActionForOneAgent("Rest", agents[0], instructions.ToList()));
        }

        public static ActionAbstract Drink(params Agent[] agents)
        {
            var ans = new ActionAbstract();
            instructions.Clear();
  
            instructions.Add(new InstructionCharacteristicSet(Characteristics.WaterSatiety, 100));

            List<IAtomicInstruction> actInstr = new List<IAtomicInstruction>();
            IAtomicInstruction[] actInstrArr = new IAtomicInstruction[instructions.Count];
            instructions.CopyTo(actInstrArr,0);
            ans.Add(new ActionForOneAgent("DrinkSubject", agents[0], actInstrArr.ToList()));

            instructions.Clear();
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionForOneAgent("DrinkObject", agents[1], instructions.ToList()));
            return ans;
        }

        public static ActionAbstract Atack(params Agent[] agents)
        {
            var ans = new ActionAbstract();
            instructions.Clear();          

            instructions.Add(new InstructionCharacteristicChange(Characteristics.Peacefulness, 1)); 
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Stamina, -1)); 
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1));

            List<IAtomicInstruction> actInstr = new List<IAtomicInstruction>();
            IAtomicInstruction[] actInstrArr = new IAtomicInstruction[instructions.Count];
            instructions.CopyTo(actInstrArr, 0);
            ans.Add(new ActionForOneAgent("AtackSubject", agents[0], actInstrArr.ToList()));

            instructions = new BindingList<IAtomicInstruction>(); // warning!!!
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Health, -1));
            ans.Add(new ActionForOneAgent("AtackObject", agents[1], instructions.ToList()));
            return ans;
        }

        public static ActionAbstract Obtain(params Agent[] agents)
        {
            var ans = new ActionAbstract();
            instructions.Clear();

            instructions.Add(new InstuctionInventory(agents[2], OperationInventory.takeOut));

            List<IAtomicInstruction> actInstr = new List<IAtomicInstruction>();
            IAtomicInstruction[] actInstrArr = new IAtomicInstruction[instructions.Count];
            instructions.CopyTo(actInstrArr, 0);
            ans.Add(new ActionForOneAgent("ObtainObject", agents[1], actInstrArr.ToList()));

            instructions = new BindingList<IAtomicInstruction>();
            instructions.Add(new InstuctionInventory(agents[2], OperationInventory.takeIn));
            instructions.Add(new InstructionCharacteristicChange(Characteristics.Strenght, -1));
            ans.Add(new ActionForOneAgent("ObtainSubject", agents[0], instructions.ToList()));
            return ans;
        }
        
        // May be next code is not nessesary

        /// <summary>
        /// Get all Actions that estimate this characteristic
        /// </summary>
        public static List<ActionFunc> GetActionsEstimating(Agent agent, Characteristic characteristic)
        {
            var ans = new List<ActionFunc>();
            if (CharactActionConformity.TryGetValue(characteristic, out ans))
                return ans;
            logger.Error("Actions.GetActionsEstimating(): CharactActionConformity dictionary hasn't key " + characteristic.Name);
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