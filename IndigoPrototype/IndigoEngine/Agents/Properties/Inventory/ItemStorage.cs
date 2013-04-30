using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{	
	/// <summary>
	/// Class for inventory of the agent
	/// </summary>
    [Serializable]
	public class ItemStorage : ITypicalItemStorage
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private int storageSize;       //Size of the storage (in number of items)

		#region Constructors
			
			public ItemStorage()
			{
				ItemList = new List<Agent>();
				Owner = null;
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
				
				public Agent Owner { get; set; } 

			#endregion			


		#endregion

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public void DropAll()
		{
			foreach(Agent ag in ItemList)
			{
				ag.CurrentLocation = Owner.CurrentLocation;
			}	
			ItemList.Clear();
		}

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public bool AddAgentToStorage(Agent argAgent)
		{
			if(ItemList.Count + 1 > StorageSize)
			{
				logger.Error("Failed to add {0} to storage {1} cause of lack of space", argAgent, this);
				return false;
			}
			argAgent.CurrentLocation.TargetStorage = this;
			ItemList.Add(argAgent);
			return true;
		}

        /// <summary>
        ///  ITypicalItemStorage
        /// </summary>
        public void DeleteAgentsByType(Type agentType, int argCountToDelete = 1)
        {
            if (argCountToDelete <= 0)
                return;
			for(int i = 0; i < argCountToDelete; i++)
			{
				foreach(Agent ag in ItemList)
				{
					if(ag.GetType() == agentType)
					{
						ItemList.Remove(ag);
						break;
					}
				}
			}
        }

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
        public List<Agent> GetAgentByType(Type agentType, int argCountToGet = 1)
        {
            if (argCountToGet <= 0)
                return null;
            var ans = ItemList.FindAll(ag => { return ag.GetType() == agentType; });
            while (ans.Count > argCountToGet)
                ans.RemoveAt(0);
            return ans;
        }

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public Agent PopAgent(Agent argAgent)
		{	
			ItemList.Remove(argAgent);
			return argAgent;
		}

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public Agent PopAgentByType(Type argType)
		{
			return PopAgent(ItemList.Find(ag => { return ag.GetType() == argType; }));
		}

		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
        public bool ExistsAgentByType(Type argType)
        {
            return ItemList.Exists(ag => { return ag.GetType() == argType; });
        }
		
		/// <summary>
		/// ITypicalItemStorage
		/// </summary>
		public int NumberOfAgentsByType(Type argType)
		{
			int result = 0;  //Result of the function

            foreach (Agent ag in ItemList)
            {
                if (ag.GetType() == argType)
				{
                    result++;
				}
            }
			return result;
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

        #region ObjectMethodsOverride

            public override string ToString()
            {
                string result = "Storage: " + ItemList.Count.ToString() + "/" + StorageSize.ToString();

                foreach (Agent ag in ItemList)
                {
                    result += "\n   " + ag.Name;
                }

                return result;
            }

        #endregion
	}
}
