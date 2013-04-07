using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Class for inventory of the agent
	/// </summary>
	public class ItemStorage : ITypicalItemStorage
	{
		private int storageSize;       //Size of the storage (in number of items)

		#region Constructors
			
			public ItemStorage()
			{
				ItemList = new List<Agent>();
				StorageSize = 0;
			}

		#endregion

		#region Properties 
			
			#region ITypicalItemStorage realisation

				public List<Agent> ItemList { get; set; } //List of items that are in this storage

				public int StorageSize
				{
					get	
					{
						return storageSize; 
					}
					set
					{
						if(value < ItemList.Count)
						{
							throw(new Exception(String.Format("Storage Size of {0} is less than count of objects in it: {1}", this, value)));
						}
						storageSize = value;
					}
				}

			#endregion

		#endregion

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public void AddAgentToStorage(Agent argAgent)
		{
			if(ItemList.Count + 1 > StorageSize)
			{
				throw(new Exception(String.Format("Failed to add {0} to storage {1} cause of lack of space", argAgent, this)));
			}
			argAgent.Location = null;
			ItemList.Add(argAgent);
		}

        /// <summary>
        ///  If ItemList consists Agent with type agentType, it will be removed from list
        /// </summary>
        /// <param name="agentType">type of agent</param>
        /// <returns>removed agent</returns>
        public Agent DeleteAgentByType(Type agentType)
        {
            foreach( Agent ag in ItemList)
            {
                if(ag.GetType() == agentType)
                {
                    ItemList.Remove(ag);
                    return ag;
                }
            }
            return null;
        }

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public Agent GetAgentFromStorage(Agent argAgent)
		{	
            //This is useless function(Kostya). Ask me why.
			ItemList.Remove(argAgent); 
			return argAgent;
		}

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public Agent GetAgentByTypeFromStorage(Type argType)
		{
			return GetAgentFromStorage(ItemList.Find(ag => { return ag.GetType() == argType; }));
		}

        /// <summary>
        /// IEnumerator for foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Agent> GetEnumerator()
        {
            for (int i = 0; i < ItemList.Count; ++i)
                yield return ItemList[i];
        }

		public override string ToString()
		{
			return "Storage: " + ItemList.Count.ToString() + "/" + StorageSize.ToString();
		}
	}
}
