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
		
		public override void StoreAction(Agent argAgentSender, Action argAction)
		{
			ShortMemory tmpMemoryToStoreAction = new ShortMemory();  //Memory, wich used to store the action into short memory and than into long
			tmpMemoryToStoreAction.StoreAction(argAgentSender, argAction);
			StoreShortMemory(tmpMemoryToStoreAction);
		}

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
		/// Finding stored agent by type
		/// </summary>
		/// <param name="predicate">Predicate, that declares the type of agent wich is searched</param>
		/// <returns>Found agent or null reference</returns>
		public List<NameableObject> FindStoredAgentsOfType<T>()
		{
			List<NameableObject> result = new List<NameableObject>();  //Result of the function

			foreach(Agent ag in StoredAgents.Keys)
			{
				if(ag is T)
				{
					if(StoredAgents[(Agent)ag].First().StoredInfo is T)
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
			return StoredAgents[argAgentKey].Last(info => {return predicate(info.StoredInfo);}).StoredInfo;
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
