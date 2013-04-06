using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
	public abstract class NameableObject
	{		
		#region Constructors

			public NameableObject()
			{
				Name = "Untitled " + this.GetType().ToString().Split('.').Last();
			}

		#endregion

		#region Properties

			public string Name { get; set; }  //Object name

		#endregion
	}
}
