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
	}
}
