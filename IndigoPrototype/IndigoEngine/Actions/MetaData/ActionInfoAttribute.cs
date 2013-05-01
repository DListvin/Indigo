using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;


namespace IndigoEngine.ActionsOld
{
	/// <summary>
	/// Attribute to store meta action info: accepted agents and other requirements
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ActionInfoAttribute : Attribute
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();	

		#region Constructors

			public ActionInfoAttribute()
			{
				AcceptedSubjects = null;
				AcceptedObjects = null;
				RequiredSkills = new List<Skill>();

				IsConflict = false;
				RequiresObject = true;

				logger.Debug("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
			}

			public ActionInfoAttribute(Type[] argAcceptedSubjects, Type[] argAcceptedObjects, params object[] argSkills)
				:this()
			{
				AcceptedSubjects = argAcceptedSubjects;
				AcceptedObjects = argAcceptedObjects;
				foreach(object ob in argSkills)
				{
					if(ob is string)
					{
						RequiredSkills.Add(Skills.GetSkillByName(ob as string));
					}
				}
			}

		#endregion

		#region Properties

			public Type[] AcceptedSubjects { get; set; } //List of accepted subjects to this action

			public Type[] AcceptedObjects { get; set; }  //List of accepted objects to this action

			public List<Skill> RequiredSkills { get; set; }  //List of requiared skills for this action

			public bool IsConflict { get; set; } //Info about if action is conflict: conflict actions can not be performed with one object from different subjects in one moment
        
			public bool RequiresObject { get; set; } //Info about if action requaires object for it

		#endregion
	}
}
