using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using IndigoEngine.ActionsOld;

namespace IndigoEngine.Agents
{
    [Serializable]
    public class Sighted : Quality
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public Sighted()
        {
            RangeOfView = 0;
            CurrentView = new List<NameableObject>();
        }

        #endregion

        #region Properties

			public int RangeOfView { get; set; } //Range of view of the agent (in cells around agent, apparently)		

			public List<NameableObject> CurrentView { get; set; } 	//Current field ov view. Includes all agents & actions, that current agent can see	

			public Agent Owner { get; set; }	//Owner of the current vision

			/// <summary>
			/// ITypicalFieldOfView
			/// </summary>
			public List<Agent> CurrentViewAgents
			{
				get
				{
					List<Agent> result = new List<Agent>();  //Result of the function
					int currentAddingDistanse = 0;           //Distanse to the agent that is adding in this moment (necessary for list sorting)
					while(result.Count != CurrentView.Where(val => { return val is Agent; }).Count())
					{
						foreach(Agent ag in CurrentView.Where(val => { return val is Agent && (Agent.Distance(Owner, val as Agent) < currentAddingDistanse); }))
						{
							if(!result.Contains(ag))
							{
								result.Add(ag);
							}
						}
						currentAddingDistanse++;
					}
					return result;
				}
			}

			/// <summary>
			/// ITypicalFieldOfView
			/// </summary>
			public List<ActionAbstract> CurrentViewActions
			{
				get
				{
					List<ActionAbstract> result = new List<ActionAbstract>();
					foreach(ActionAbstract act in CurrentView.Where(val => { return val is ActionAbstract; }))
					{
						result.Add(act);
					}
					return result;
				}
			}


        #endregion

        #region ObjectMethodsOverride

        #endregion
    }
}
