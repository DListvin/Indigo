using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.ActionsOld
{
	/// <summary>
	/// Class to store some info about action (accepted subjects, required skills e.c.)
	/// </summary>
	public class InfoAboutAction
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();	

		#region Constructors

			public InfoAboutAction()
			{
				AcceptedSubjects = new List<Type>();
				AcceptedObjects = new List<Type>();
				RequiredSkills = new List<Skill>();

				IsConflict = false;
				RequiresObject = true;

				logger.Debug("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
			}

			public InfoAboutAction(List<Type> argAcceptedSubjects, List<Type> argAcceptedObjects, List<Skill> argRequiredSkills, bool argIsConflict = false, bool argRequiresObject = true)
				:base()
			{
				AcceptedSubjects = argAcceptedSubjects;
				AcceptedObjects = argAcceptedObjects;
				RequiredSkills = argRequiredSkills;

				IsConflict = argIsConflict;
				RequiresObject = argRequiresObject;
			}

		#endregion

		#region Properties

			public List<Type> AcceptedSubjects { get; set; } //List of accepted subjects to this action

			public List<Type> AcceptedObjects { get; set; }  //List of accepted objects to this action

			public List<Skill> RequiredSkills { get; set; }  //List of requiared skills for this action

			public bool IsConflict { get; set; } //Info about if action is conflict: conflict actions can not be performed with one object from different subjects in one moment
        
			public bool RequiresObject { get; set; } //Info about if action requaires object for it

		#endregion
	}
}
