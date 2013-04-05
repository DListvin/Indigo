using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	interface ITypicalItemStorage
	{
		/// <summary>
		/// Operations with item storage content
		/// </summary>
		List<Agent> ItemList{get; set;}

		/// <summary>
		/// Operations with storage size
		/// </summary>
		int StorageSize{get; set;}

		/// <summary>
		/// Adding a new agent into storage
		/// </summary>
		/// <param name="argAgent">Agent to add</param>
		void AddAgentToStorage(Agent argAgent);

		/// <summary>
		/// Getting the specified agent from the storage
		/// </summary>
		/// <param name="argAgent">Specifier for agent</param>
		/// <returns>Agent, that matches and no more in storage</returns>
		Agent GetAgentFromStorage(Agent argAgent);

		/// <summary>
		/// Getting the agent, that matches the type, from the storage
		/// </summary>
		/// <param name="argType">Type of the agent to find</param>
		/// <returns>Agent, that matches and no more in storage</returns>
		Agent GetAgentByTypeFromStorage(Type argType);
	}
}
