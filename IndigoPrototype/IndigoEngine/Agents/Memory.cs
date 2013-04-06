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
		/// Clearing the hole memory
		/// </summary>
		public virtual void ForgetAll()
		{
		}
	}
}
