using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Long memory of the agent: includes dictionary with agent and learned information about it
	/// </summary>
	public class LongMemory : Memory
	{
		private Dictionary<Agent, List<StoredInformation>> storedAgents;   //Dictionary with memories about other agents: <reference to the agent(id), info about agent>.
		
		#region Constructors

		public LongMemory()
		{
			StoredAgents = new Dictionary<Agent, List<StoredInformation>>();
		}

		#endregion

		#region Properties

		public Dictionary<Agent, List<StoredInformation>> StoredAgents
		{
			get { return storedAgents; }
			set { storedAgents = value; }
		}

		#endregion

		/// <summary>
		/// Storing short memory into long memory(converting actions to info about agents)
		/// </summary>
		/// <param name="argShortMemory">Short memory to translate into long</param>
		public void StoreShortMemory(ShortMemory argShortMemory)
		{
			foreach(KeyValuePair<Agent, Action> act in argShortMemory.StoredActions)
			{
				if(StoredAgents.ContainsKey(act.Key))
				{
					if(!(StoredAgents[act.Key].Exists(obj => 
						{
							return obj.StoredInfo == act.Value.CharacteristicsOfSubject();
						})))
					{
						StoredInformation st = new StoredInformation();
						st.StoredInfo = act.Value.CharacteristicsOfSubject();
						StoredAgents[act.Key].Add(st);
					}
				}
				else
				{
					StoredInformation st = new StoredInformation();
					st.StoredInfo = act.Value.CharacteristicsOfSubject();
					List<StoredInformation> lst = new List<StoredInformation>();
					lst.Add(st);
					StoredAgents.Add(act.Key, lst);
				}
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

			if(StoredAgents.ContainsKey(argAgentToStore))
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
		/// Memory class override
		/// </summary>
		public override void ForgetAll()
		{
			StoredAgents.Clear();
		}

		public override string ToString()
		{
			string result = "";  //Result of the function

			foreach(KeyValuePair<Agent, List<StoredInformation>> storedAgent in StoredAgents)
			{
				result += storedAgent.Key.ToString() + " is " + "\n";
				foreach(StoredInformation st in storedAgent.Value)
				{
					result += st.ToString();
				}
			}

			return result;
		}
	}
}
