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
    public class World
    {
        private List<Agent> agents;           //List of all agents in the world
        private List<Action> actions; //List of all actions, that must be performed (refreshing each loop iteration)
		
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
            Actions.Clear();

            UpdateAgentFeelings();

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
        }

        /// <summary>
        /// Here we basicly create world.
        /// </summary>
        public void Initialise()
        {
			Agent currentAddingAgent; //Agent, that is adding to the world now. This variable is necessary to configure the agent

            Agents = new List<Agent>();
            Actions = new List<Action>();

            //Test init			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(0, 0);
            currentAddingAgent.Health.MaxValue = 100;
            currentAddingAgent.Health.CurrentUnitValue = 100;
            Agents.Add(currentAddingAgent);			
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(0, 5);
            currentAddingAgent.Health.MaxValue = 100;
            currentAddingAgent.Health.CurrentUnitValue = 100;
            Agents.Add(currentAddingAgent);
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(5, 5);
            currentAddingAgent.Health.MaxValue = 100;
            currentAddingAgent.Health.CurrentUnitValue = 100;
            Agents.Add(currentAddingAgent);

			currentAddingAgent = new AgentItemLog();
			currentAddingAgent.HomeWorld = this;
            Agents.Add(currentAddingAgent);
        }

        /// <summary>
        /// It is for agent for asking world of an action. 
        /// </summary>
        /// <param name="action">Action which agent is asking for</param>
        /// <returns>Positive return mean action is accepted. Negative - vice versa</returns>
        public bool AskWorldForAnAction(Action action)
        {
            Actions.Add(action);
            return true;
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
        }

        void UpdateAgentFeelings()
        {
            foreach (AgentLivingIndigo agent in Agents.Where(a => { return a is AgentLivingIndigo; }))
            {
                agent.FieldOfView.Clear();
                /*
                if (agent.Location.HasValue)
                {
                    agent.FieldOfView.AddRange(agents.Where(a =>
                    {
                        return a != agent && a.Location.HasValue && Math.Sqrt(Math.Pow((agent.Location.Value.X - a.Location.Value.X), 2) +
                            Math.Pow((agent.Location.Value.Y - a.Location.Value.Y), 2)) < agent.RangeOfView;
                    }));
                }*/
                agent.FieldOfView.AddRange(Agents.Where(ag => { return ag != agent; }));
            }
        }
    }
}
