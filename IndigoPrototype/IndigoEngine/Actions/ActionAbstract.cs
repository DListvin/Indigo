using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine.ActionsOld
{
    /// <summary>
    /// basic class for action
    /// </summary>
    [Serializable]
    public abstract class ActionAbstract : NameableObject, ITypicalAction, IComparable<ActionAbstract>
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Static methods	

			/// <summary>
			/// From direction gives increment to position. Must not be here. See location. Kostya.
			/// </summary>
			/// <returns>Point like (0,1) (-1,0) or (1,-1)</returns>
			protected static Point Normilize(Point end, Point location)
			{
                Point dir = new Point(end.X - location.X, end.Y - location.Y);
				if (Math.Abs(dir.X) > Math.Abs(dir.Y))
				{
					return new Point((dir.X < 0) ? -1 : 1, 0);
				}
				if (Math.Abs(dir.Y) > Math.Abs(dir.X))
				{
					return new Point(0, (dir.Y < 0) ? -1 : 1);
				}
				return new Point((dir.X < 0) ? -1 : 1, (dir.Y < 0) ? -1 : 1);
			}

			/// <summary>
			/// Checking current subject for containing skills for current action
			/// </summary>
			/// <param name="argAgentToCheck">Agent from wich to take skills</param>
			/// <param name="argListForChecking">List to check</param>
			/// <returns>True agent have required skills, false - vice versa</returns>
			protected static bool CheckForSkills(Agent argAgentToCheck, List<Skill> argListForChecking)
			{
				foreach(Skill sk in argListForChecking)
				{
					if(!argAgentToCheck.SkillsList.Any( skill =>
					{
						if(skill.Name == sk.Name && skill.SkillQuality >= sk.SkillQuality)
						{
							return true;
						}
						return false;
					}))
					{	
						return false;
					}
				}
				return true;
			}

		#endregion   

        #region Constructors

        public ActionAbstract()
            : base()
        {
        }

		public ActionAbstract(Agent argSubj, Agent argObj)
            : this()
        {
            Subject = argSubj;
            Object = argObj;
        }

        #endregion

        #region Properties

            #region ITypicalAgent realisation

                public Agent Object { get; set; } //Object of the action

                public Agent Subject { get; set; } //Subject of the action

                public IWorldToAction World { get; set; }   //Home world of the action

            #endregion

        #endregion

		
        /// <summary>
        /// ITypicalAction here is control for obj and subj types
        /// </summary>
		public virtual Exception CheckForLegitimacy()
		{
			Attribute actionInfo = Attribute.GetCustomAttribute(this.GetType(), typeof(ActionInfoAttribute));  // getting attributes for this class
			if(actionInfo == null)
			{
				logger.Error("Failed to get action info attribute for {0}", this.GetType());
				return new Exception();
			}
			if(!ActionAbstract.CheckForSkills(Subject, (actionInfo as ActionInfoAttribute).RequiredSkills))
			{
				return new SkillRequiredException();
			}
			return null;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public virtual void CalculateFeedbacks()
        {
			if(Object != null)
			{
				Object.CurrentActionFeedback += new ActionFeedback( () => 
				{
					Object.CurrentMemory.StoreAction(Subject, this); 
				});
			}
        }

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public virtual NameableObject CharacteristicsOfSubject()
        {
            return Subject;
        }

		/// <summary>
		/// Comparing 2 actions
		/// </summary>
		/// <param name="argActionToCompare">Action to compare with</param>
		/// <returns> 0 - actions are equal, 1 - actions are unequal</returns>
		public virtual int CompareTo(ActionAbstract argActionToCompare)
		{
			return 1;
		}

        public override string ToString()
        {
            return "Action: " + Name;
        }
    }
}
