using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	public class ShortMemory : Memory
	{
		private Dictionary<Agent, Action> storedActions;  //Stored actions about some agents during the iteration. After some time it is translated into long memory.

		#region Constructors

		public ShortMemory()
		{
			StoredActions = new Dictionary<Agent, Action>();
		}

		#endregion

		#region Properties

		public Dictionary<Agent, Action> StoredActions
		{
			get { return storedActions; }
			set { storedActions = value; }
		}

		#endregion

		/// <summary>
		/// Storing the action into the short memory
		/// </summary>
		/// <param name="argAgentSender">Agent that sended the action</param>
		/// <param name="argAction">Action that is storing</param>
		public void StoreAction(Agent argAgentSender, Action argAction)
		{
			if(StoredActions.ContainsKey(argAgentSender))
			{
				StoredActions[argAgentSender] = argAction;
			}
			else
			{
				StoredActions.Add(argAgentSender, argAction);
			}
		}

		public override string ToString()
		{
			string result = "";  //Result of the function

			foreach(KeyValuePair<Agent, Action> storedAction in StoredActions)
			{
				result += storedAction.Key.ToString() + " did " + storedAction.Value.ToString() + "\n";
			}

			return result;
		}
	}
}
