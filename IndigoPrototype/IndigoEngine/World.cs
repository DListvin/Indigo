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
    public class World : IWorldToAction
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
			currentAddingAgent.Name = "Indigo1";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(0, 0);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).RangeOfView = 100;
            Agents.Add(currentAddingAgent);			
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.Name = "Indigo2";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(0, 5);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).RangeOfView = 100;
            Agents.Add(currentAddingAgent);
			
			currentAddingAgent = new AgentLivingIndigo();
			currentAddingAgent.Name = "Indigo3";
			currentAddingAgent.HomeWorld = this;
            currentAddingAgent.Location = new System.Drawing.Point(5, 5);
            currentAddingAgent.CurrentState.Health.MaxValue = 100;
            currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
            (currentAddingAgent as AgentLivingIndigo).RangeOfView = 100;
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

			currentAddingAgent = new AgentItemLog();
			currentAddingAgent.HomeWorld = this;
			currentAddingAgent.Name = "Log1";
            Agents.Add(currentAddingAgent);
        }

        /// <summary>
        /// It is for agent for asking world of an action. 
        /// </summary>
        /// <param name="action">Action which agent is asking for</param>
        /// <returns>Positive return mean action is accepted. Negative - vice versa</returns>
        public bool AskWorldForAnAction(Action action)
        {
            action.World = this;
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
                agent.FieldOfView.Clear();
                
                if (agent.Location.HasValue)
                {
                    //Adding agents
                    agent.FieldOfView.AddRange(agents.Where(a =>
                    {
                        return a.Location.HasValue && a != agent && Distance(a, agent) < agent.RangeOfView;
                    }));
                    //Adding actions
                    agent.FieldOfView.AddRange(actions.Where(a =>
                    {
                        return a.Subject.Location.HasValue && Distance(agent, a.Subject) < agent.RangeOfView;
                    }));
                }
            }
        }

        /// <summary>
        /// Computes distance between two agents (May be we shoud make static class AgentAlgebra? We'll see if there will be more computational funcs with agents)
        /// </summary>
        /// <param name="agent1">First agent</param>
        /// <param name="agent2">Second agent</param>
        /// <returns>Distance or NaN, if any of agents doesn'n have location</returns>
        double Distance(Agent agent1, Agent agent2)
        {
            if (!agent1.Location.HasValue || !agent2.Location.HasValue)
            {
                return Double.NaN;
            }
            return Math.Sqrt(Math.Pow(agent1.Location.Value.X - agent2.Location.Value.X, 2) + Math.Pow(agent1.Location.Value.Y - agent2.Location.Value.Y, 2));
        }

        #region IWorldToAction realisation

        public bool AskWorldForDeletion(object sender, Agent obj)
        {
            throw new NotImplementedException();
        }

        public bool AskWorlForAddition(object sender, Agent obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
