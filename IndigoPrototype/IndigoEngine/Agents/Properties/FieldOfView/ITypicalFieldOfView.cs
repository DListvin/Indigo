using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	interface ITypicalFieldOfView
	{
		/// <summary>
		/// Range of view of the agent (in cells around agent, apparently)
		/// </summary>
		int RangeOfView	{ get; set; }

		/// <summary>
		/// Current field ov view. Includes all agents & actions, that current agent can see
		/// </summary>
		List<NameableObject> CurrentView { get;	set; }

		/// <summary>
		/// List of all actions, that current agent can see
		/// </summary>
		List<NameableObject> CurrentViewActions { get; }

		/// <summary>
		/// List of all agents, that current agent can see
		/// </summary>
		List<NameableObject> CurrentViewAgents {	get; }
	}
}
