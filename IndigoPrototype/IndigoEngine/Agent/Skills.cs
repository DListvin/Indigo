using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents
{
	public static class Skills
	{
		public static Skill Woodcutting	
		{
			get
			{
				//VAR
					Skill result;	//Default skill of woodcutting
				//VAR
				result = new Skill();
				result.Name = "Woodcutting";
				return result;
			}
		}

		public static Skill CampConstructing
		{
			get
			{
				//VAR
					Skill result;	//Default skill of camp constructing
				//VAR
				result = new Skill();
				result.Name = "CampConstructing";
				return result;
			}
		}

		public static Skill Gathering
		{
			get
			{
				//VAR
					Skill result;	//Default skill of camp gathering
				//VAR
				result = new Skill();
				result.Name = "Gathering";
				return result;
			}
		}

		public static Skill Communicationing
		{
			get
			{
				//VAR
					Skill result;	//Default skill of camp communicationing
				//VAR
				result = new Skill();
				result.Name = "Communicationing";
				return result;
			}
		}
	}
}
