using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	public class Vision : ITypicalFieldOfView
	{
		#region Constructors

			public Vision()
			{
				RangeOfView = 0;
				CurrentView = new List<NameableObject>();
			}

		#endregion
		
		#region Properties

			public int RangeOfView { get; set; } //Range of view of the agent (in cells around agent, apparently)		

			public List<NameableObject> CurrentView { get; set; } 	//Current field ov view. Includes all agents & actions, that current agent can see		

			/// <summary>
			/// ITypicalFieldOfView
			/// </summary>
			public List<NameableObject> CurrentViewAgents
			{
				get
				{
					return CurrentView.Where(val => { return val is Agent; }).ToList();
				}
			}			

			/// <summary>
			/// ITypicalFieldOfView
			/// </summary>
			public List<NameableObject> CurrentViewActions
			{
				get
				{
					return CurrentView.Where(val => { return val is Action; }).ToList();
				}
			}


		#endregion
	}
}
