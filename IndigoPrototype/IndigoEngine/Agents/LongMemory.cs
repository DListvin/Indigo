using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	public class LongMemory : Memory
	{
		private Dictionary<Agent, StoredInformation> storedAgents;   //Dictionary with memories about other agents: <reference to the agent(id), info about agent>.
		
		#region Constructors

		public LongMemory()
		{
			StoredAgents = new Dictionary<Agent, StoredInformation>();
		}

		#endregion

		#region Properties

		public Dictionary<Agent, StoredInformation> StoredAgents
		{
			get { return storedAgents; }
			set { storedAgents = value; }
		}

		#endregion

		public void StoreShortMemory(ShortMemory argShortMemory)
		{
			foreach(KeyValuePair<Agent, Action> act in argShortMemory.StoredActions)
			{
				if(StoredAgents.ContainsKey(act.Key))
				{
					if(!(StoredAgents[act.Key].StoredInfo.Exists(obj => 
						{
							return obj == act.Value.CharacteristicsOfSubject();
						})))
					{
						StoredAgents[act.Key].StoredInfo.Add(act.Value.CharacteristicsOfSubject());
					}
				}
				else
				{
					StoredInformation st = new StoredInformation();
					st.StoredInfo.Add(act.Value.CharacteristicsOfSubject());
					StoredAgents.Add(act.Key, st);
				}
			}
		}

		public override string ToString()
		{
			string result = "";  //Result of the function

			foreach(KeyValuePair<Agent, StoredInformation> storedAgent in StoredAgents)
			{
				result += storedAgent.Key.ToString() + " is " + storedAgent.Value.ToString() + "\n";
			}

			return result;
		}
	}
}
