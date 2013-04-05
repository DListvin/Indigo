using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	/// <summary>
	/// Class for storing information about other agents (Includes some restrictions on the info and time of storing)
	/// </summary>
	public class StoredInformation
	{
		private List<NameableObject> storedInfo;  //Info about agent which is stored. 
		private DateTime storingTime;             //Time of storing the information

		#region Constructors

		public StoredInformation()
		{	
			StoredInfo = new List<NameableObject>();
			StoringTime = DateTime.Now;
		}

		#endregion

		#region Properties

		public List<NameableObject> StoredInfo
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
			string result = "\n";  //Result of the function

			foreach(NameableObject obj in StoredInfo)
			{
				result += "     " + obj.ToString() + " at " + StoringTime.ToString() + "\n";
			}

			return result;
		}
	}
}
