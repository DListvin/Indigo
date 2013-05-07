using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storing information about other agents 
	/// Includes some learned characteristics, skills or the hole agent as info and storing time for solving conflicts between two same characteristics
	/// </summary>
    [Serializable]
	public class StoredInformation
	{         
		private static Logger logger = LogManager.GetCurrentClassLogger(); 

		#region Constructors

			public StoredInformation()
			{	
				StoringTime = DateTime.Now;
			}

		#endregion

		#region Properties

			public NameableObject StoredInfo { get; set; } //Info about agent which is stored.

			public DateTime StoringTime { get; set; }      //Time of storing the information

		#endregion

		public override string ToString()
		{
			return "     " + StoredInfo.ToString() + " at " + StoringTime.ToString() + "\n";
		}

        #region ObjectMethodsOverride

		/*
			public override bool Equals(object obj)
			{
				if (obj.GetType() != this.GetType())
					return false;

				var o = obj as StoredInformation;
            
				return this.StoredInfo.Equals(o.StoredInfo) && this.StoringTime.Equals(o.StoringTime);
			}

			public static bool operator ==(StoredInformation o1, StoredInformation o2)
			{
				return o1.Equals(o2);
			}

			public static bool operator !=(StoredInformation o1, StoredInformation o2)
			{
				return !o1.Equals(o2);
			}
		 */
		 //Now it is useless. I'm done reading the warnings about overriding GetHashCode();
        #endregion
	}
}
