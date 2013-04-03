using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Мир, у него есть агенты и он следит, чтобы они думали и не конфликтовали. Идейное наполнение модели.
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
        /// Это главный цикл обработки мира.
        /// </summary>
        public void MainLoopIteration()
        {

        }

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
