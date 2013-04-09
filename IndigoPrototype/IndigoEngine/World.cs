using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// World - logical moments in model.
    /// </summary>
    public class World : IWorldToAction, IWorldToAgent
    {
        public delegate void modificate();

        private List<Agent> agents;           //List of all agents in the world
        private List<Action> actions; //List of all actions, that must be performed (refreshing each loop iteration)
        private List<modificate> modificatiors; // List of Delegates, to add or Delete agents at right place in MainLoop
		#region Constructors

        public World()
        {
            Initialise();
        }
	
		#endregion

		#region Properties

        public List<Agent> Agents
        {
            get { return agents; }
			set { agents = value; }
        }

        public List<Action> Actions
        {
            get { return actions; }
			set { actions = value; }
        }

		#endregion

        /// <summary>
        /// Here is the main loop
        /// </summary>
        public void MainLoopIteration()
        {    
            UpdateAgentFeelings();

            Actions.Clear();

            foreach (Agent agent in Agents)
			{
                agent.Decide();
			}

            SolveActionConflicts();

            foreach (Action action in Actions)
			{
                action.Perform();
			}

            foreach (Agent agent in Agents)
            {
                agent.PerformFeedback();
                agent.StateRecompute();
            }

            foreach (modificate mod in modificatiors)
            {
                mod.DynamicInvoke();
            }
        }

        /// <summary>
        /// Here we basicly create world.
        /// </summary>
        public void Initialise()
        {
			Agent currentAddingAgent; //Agent, that is adding to the world now. This variable is necessary to configure the agent

            Agents = new List<Agent>();
            Actions = new List<Action>();
            modificatiors = new List<modificate>();

            //Test init			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.Name = "Indigo1";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(10, 10);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
            Agents.Add(currentAddingAgent);			
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.Name = "Indigo2";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(10, 15);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
            Agents.Add(currentAddingAgent);
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.Name = "Indigo3";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(15, 15);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
            Agents.Add(currentAddingAgent);

            currentAddingAgent = new AgentItemFruit();
            currentAddingAgent.Name = "Fruit1";
            currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(0, 2);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            Agents.Add(currentAddingAgent);

            currentAddingAgent = new AgentItemFruit();
            currentAddingAgent.Name = "Fruit2";
            currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(2, 0);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            Agents.Add(currentAddingAgent);

            currentAddingAgent = new AgentPuddle();
            currentAddingAgent.Name = "Puddle1";
            currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(-2, -2);
            currentAddingAgent.CurrentState.Health.MaxValue = 200;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 200;
            Agents.Add(currentAddingAgent);

			currentAddingAgent = new AgentItemLog();
			currentAddingAgent.HomeWorld = this;
			currentAddingAgent.Name = "Log1";
            Agents.Add(currentAddingAgent);
        }



        /// <summary>
        /// Here world somehow decides how to slove conflicts
        /// </summary>
        void SolveActionConflicts()
        {
            //kuyvkhguvkty
			//     ^
			//     |
			//We shouldn't delete this, VERY IMPORTANT!
            //TODO: Confirm, if it's so.
            //Will be deleted 06.02.2015
        }

        /// <summary>
        /// Clears agents FieldOfView and than fills it with agents and action within range of view
        /// </summary>
        void UpdateAgentFeelings()
        {
            foreach (AgentLivingIndigo agent in Agents.Where(a => { return a is AgentLivingIndigo; }))
            {
                agent.CurrentVision.CurrentView.Clear();
                
                if (agent.Location.HasValue)
                {
                    //Adding agents
                    agent.CurrentVision.CurrentView.AddRange(agents.Where(a =>
                    {
                        return a.Location.HasValue && a != agent && Agent.Distance(a, agent) < agent.AgentsRangeOfView;
                    }));
                    //Adding actions
                    agent.CurrentVision.CurrentView.AddRange(actions.Where(a =>
                    {
                        return a.Subject.Location.HasValue && Agent.Distance(agent, a.Subject) < agent.AgentsRangeOfView;
                    }));
                }
            }
        }

        #region IWorldToAgent realisation

            /// <summary>
            /// It is for agent for asking world of an action. 
            /// </summary>
            /// <param name="action">Action which agent is asking for</param>
            /// <returns>Positive return mean action is accepted. Negative - vice versa</returns>
            public bool AskWorldForAction(Action action)
            {
                action.World = this;
				if(action.CheckForLegitimacy())
				{
					if(Actions.Any(act => 
					{
						if(action.CompareTo(act) == 0)
						{
							return true;
						}
						return false;
					}))
					{	
						return false;
					}
					Actions.Add(action);
					return true;
				}		
						
				return false;
            }
			
			public bool AskWorldForDeletion(Agent sender)
			{
                if (!agents.Contains(sender))
				{
                    return false;
				}
                modificatiors.Add(() =>
                {
                    agents.Remove(sender);
                });
				return true;
			}

        #endregion

        #region IWorldToAction realisation

            public bool AskWorldForDeletion(object sender, Agent obj)
            {
                if (sender.GetType().BaseType != typeof(Action))
				{
                    return false;
				}
                if (!agents.Contains(obj))
				{
                    return false;
				}
                modificatiors.Add(() =>
                {
                    agents.Remove(obj);
                });
                return true;
            }

            public bool AskWorldForAddition(object sender, Agent obj)
            {
                if (sender.GetType().BaseType != typeof(Action))
				{
                    return false;
				}
                modificatiors.Add(() =>
                {
                    agents.Add(obj);
                });
                
                return true;
            }

        #endregion
    }
}
