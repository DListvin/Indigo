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
    class World
    {
        List<Agent> agents;
        List<IAction> actions;

        public IEnumerable<Agent> Agents
        {
            get
            {
                return agents;
            }
        }

        /// <summary>
        /// Here is the main loop
        /// </summary>
        public void MainLoopIteration()
        {
            actions.Clear();

            UpdateAgentFeelings();

            foreach (Agent agent in agents)
                agent.Decide();

            SolveActionConflicts();

            foreach (IAction action in actions)
                action.Perform();

            foreach (Agent agent in agents)
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
            agents = new List<Agent>();
            actions = new List<IAction>();

            //Test init
            agents.Add(new AgentIndigo(this));
            agents.Last().Location = new System.Drawing.Point(0, 0);
            agents.Last().Health.MaxValue = 100;
            agents.Last().Health.CurrentUnitValue = 100;

            agents.Add(new AgentIndigo(this));
            agents.Last().Location = new System.Drawing.Point(0, 5);
            agents.Last().Health.MaxValue = 100;
            agents.Last().Health.CurrentUnitValue = 100;

            agents.Add(new AgentIndigo(this));
            agents.Last().Location = new System.Drawing.Point(5, 5);
            agents.Last().Health.MaxValue = 100;
            agents.Last().Health.CurrentUnitValue = 100;

            agents.Add(new AgentItemLog());
        }

        public World()
        {
            Initialise();
        }

        /// <summary>
        /// It is for agent for asking world of an action. Positive return mean action is accepted.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool AskWorldForAnAction(IAction action)
        {
            actions.Add(action);
            return true;
        }

        /// <summary>
        /// Here world somehow decides how to slove conflicts
        /// </summary>
        void SolveActionConflicts()
        {
            //kuyvkhguvkty
        }

        void UpdateAgentFeelings()
        {
            foreach (AgentIndigo agent in agents.Where(a => { return a is AgentIndigo; }))
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
                agent.FieldOfView.AddRange(agents.Where(a => { return a != agent; }));
            }
        }
    }
}
