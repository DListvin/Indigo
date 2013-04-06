using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storing information about other agents 
	/// Includes some learned characteristics, skills or the hole agent as info and storing time for solving conflicts between two same characteristics
	/// </summary>
	public class StoredInformation
	{
		private NameableObject storedInfo;  //Info about agent which is stored. 
		private DateTime storingTime;             //Time of storing the information

		#region Constructors

		public StoredInformation()
		{	
			StoringTime = DateTime.Now;
		}

		#endregion

		#region Properties

		public NameableObject StoredInfo
		{
			get { return storedInfo; }
			set { storedInfo = value;	}
		}

		public DateTime StoringTime
		{
			get { return storingTime; }
			set { storingTime = value; }
		}

		#endregion

		public override string ToString()
		{
			return "     " + StoredInfo.ToString() + " at " + StoringTime.ToString() + "\n";
		}
	}
}
