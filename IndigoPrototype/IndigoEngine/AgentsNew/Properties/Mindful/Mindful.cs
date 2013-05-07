using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using IndigoEngine.ActionsOld;

namespace IndigoEngine.AgentsNew
{
	/// <summary>
	/// Memory of the agents
	/// </summary>
    [Serializable]
    public class Mindful : Quality
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public Mindful()
        {
            StoredAgents = new Dictionary<Agent, List<StoredInformation>>();
        }

        #endregion

        #region Properties

			public Dictionary<Agent, List<StoredInformation>> StoredAgents { get; set; } //Dictionary with memories about other agents: <reference to the agent(id), info about agent>.

        #endregion

        public void StoreAction(Agent argAgentSender, ActionAbstract argAction)
        {
            StoredInformation st = new StoredInformation();   //New information to store
            st.StoredInfo = argAction.CharacteristicsOfSubject();

            if (StoredAgents.ContainsKey(argAgentSender))
            {
                if (!(StoredAgents[argAgentSender].Exists(obj =>
                    {
                        return obj.StoredInfo == argAction.CharacteristicsOfSubject();
                    })))
                {
                    StoredAgents[argAgentSender].Add(st);
                }
            }
            else
            {
                List<StoredInformation> lst = new List<StoredInformation>();  //New list to store new agent
                lst.Add(st);
                StoredAgents.Add(argAgentSender, lst);
            }
        }

        /// <summary>
        /// Storing the hole state of the agent
        /// </summary>
        /// <param name="argAgentToStore">agent to store</param>
        public void StoreAgent(Agent argAgentToStore)
        {
            StoredInformation st = new StoredInformation();  //Information to store, includes the agent
            st.StoredInfo = argAgentToStore;

            if (StoredAgents.ContainsKey(argAgentToStore))
            {
                StoredAgents[argAgentToStore].Clear();
                StoredAgents[argAgentToStore].Add(st);
            }
            else
            {
                List<StoredInformation> lst = new List<StoredInformation>();
                lst.Add(st);
                StoredAgents.Add(argAgentToStore, lst);
            }
        }

        /// <summary>
        /// Finding stored agent by type
        /// </summary>
        /// <param name="predicate">Predicate, that declares the type of agent wich is searched</param>
        /// <returns>Found agent or null reference</returns>
        public List<NameableObject> FindStoredAgentsOfType<T>()
        {
            List<NameableObject> result = new List<NameableObject>();  //Result of the function

            foreach (Agent ag in StoredAgents.Keys)
            {
                if (ag is T)
                {
                    if (StoredAgents[(Agent)ag].First().StoredInfo is T)
                    {
                        result.Add(StoredAgents[(Agent)ag].First().StoredInfo);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Finding some info about the agent(characteristics and skills)
        /// </summary>
        /// <param name="argAgentKey">Agent which info is serching for</param>
        /// <param name="predicate">Predicate that specify the necessary info</param>
        /// <returns>Found info</returns>
        public NameableObject FindInfoAboutAgent(Agent argAgentKey, Func<NameableObject, bool> predicate)
        {
            return StoredAgents[argAgentKey].Last(info => { return predicate(info.StoredInfo); }).StoredInfo;
        }

        /// <summary>
        /// Memory class override
        /// </summary>
        public void ForgetAll()
        {
            StoredAgents.Clear();
        }

        #region ObjectMethodsOverride

            public override string ToString()
            {
                string result = "";  //Result of the function

                foreach (KeyValuePair<Agent, List<StoredInformation>> storedAgent in StoredAgents)
                {
                    result += storedAgent.Key.Name + " is " + "\n";
                    foreach (StoredInformation st in storedAgent.Value)
                    {
                        result += st.ToString();
                    }
                }

                return result;
            }

        #endregion
    }
}
