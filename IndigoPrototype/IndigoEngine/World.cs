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

        }

        /// <summary>
        /// Here we basicly create world.
        /// </summary>
        public void Initialise()
        {
            agents = new List<Agent>();

            //Test init
            agents.Add(new AgentIndigo());
            agents.Add(new AgentItemLog());
        }

        public World()
        {
            Initialise();
        }
    }
}
