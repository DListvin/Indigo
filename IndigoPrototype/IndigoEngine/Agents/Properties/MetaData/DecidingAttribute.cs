using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Attribute to show if this agent can decide something meaningful
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DecidingAttribute : Attribute
	{
	}
}
