using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Basic class for memory. Don't know, if it is helpfull
	/// </summary>
	public abstract class Memory
	{		
		/// <summary>
		/// Storing the action into the short memory
		/// </summary>
		/// <param name="argAgentSender">Agent that sended the action</param>
		/// <param name="argAction">Action that is storing</param>
		public virtual void StoreAction(Agent argAgentSender, Action argAction)
		{
		}

		/// <summary>
		/// Clearing the hole memory
		/// </summary>
		public virtual void ForgetAll()
		{
		}
	}
}
