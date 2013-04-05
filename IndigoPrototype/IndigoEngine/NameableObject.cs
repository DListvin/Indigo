using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
	public abstract class NameableObject
	{		
		private string name;   //Object name
		
		#region Constructors

			public NameableObject()
			{
				Name = "Untitled " + this.GetType().ToString();
			}

		#endregion

		#region Properties

			/// <summary>
			/// Operations with object name
			/// </summary>
			public string Name
			{
				get
				{
					return name;
				}
				set
				{
					name = value;
				}
			}

		#endregion
	}
}
