﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	class AgentItemFruit : Agent
	{
		#region Constructors
			
			public AgentItemFruit() : base()
			{
			}

		#endregion

		public override string ToString()
		{
			return "Item fruit: " + Name;
		}

	}
}
