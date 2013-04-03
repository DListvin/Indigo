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

            foreach (Agent agent in agents)
                agent.Decide();

            SolveActionConflicts();

            foreach (IAction action in actions)
                action.Perform();

            foreach (Agent agent in agents)
            {
                //do update and action result performing
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
            agents.Add(new AgentIndigo());
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
            throw new NotImplementedException();
            //Decide wheather to accept or decline action

            //If true,
            actions.Add(action);
            return true;
        }

        /// <summary>
        /// Here world somehow decides how to slove conflicts
        /// </summary>
        void SolveActionConflicts()
        {
            
        }
    }
}
