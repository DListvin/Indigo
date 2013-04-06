using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Short memory of the agent: includes dictionary with agnets and their actions
	/// </summary>
	public class ShortMemory : Memory
	{
		#region Constructors

			public ShortMemory()
			{
				StoredActions = new Dictionary<Agent, Action>();
			}

		#endregion

		#region Properties

			public Dictionary<Agent, Action> StoredActions { get; set; }  //Stored actions about some agents during the iteration. After some time it is translated into long memory.

		#endregion

		/// <summary>
		/// Storing the action into the short memory
		/// </summary>
		/// <param name="argAgentSender">Agent that sended the action</param>
		/// <param name="argAction">Action that is storing</param>
		public override void StoreAction(Agent argAgentSender, Action argAction)
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
		
		/// <summary>
		/// Memory class override
		/// </summary>
		public override void ForgetAll()
		{
			StoredActions.Clear();
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
