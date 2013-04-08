using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;

namespace IndigoEngine
{
    /// <summary>
    /// basic class for action
    /// </summary>
    public abstract class Action : NameableObject, ITypicalAction, IComparable<Action>
    {
		#region Static members	

			/// <summary>
			/// From direction gives increment to position
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

		#endregion   

        #region Constructors

        public Action()
            : base()
        {
            AcceptedObj = new List<Type>();
            AcceptedSubj = new List<Type>();
        }

        public Action(Agent argObj, Agent argSubj)
            : this()
        {
            Object = argObj;
            Subject = argSubj;
        }

        #endregion

        #region Properties

            #region ITypicalAgent realisation

                public Agent Object { get; set; } //Object of the action

                public Agent Subject { get; set; } //Subject of the action

                protected bool MayBeConflict { get; set; } //Info about if action is conflict: conflict actions can not be performed with one object from different subjects in one moment

                public List<Type> AcceptedSubj { get; set; } //Meant to be set in descendant class

                public List<Type> AcceptedObj { get; set; }

                public IWorldToAction World { get; set; }

            #endregion

        #endregion

		
        /// <summary>
        /// ITypicalAction here is control for obj and subj types
        /// </summary>
		public virtual bool CheckForLegitimacy()
		{
            if ((Object!=null) && !AcceptedObj.Contains(Object.GetType()))
            {
                return false;
            }
            if (!AcceptedSubj.Contains(Subject.GetType()))
            {
                return false;
            }
			return true;
		}

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public virtual void Perform()
        {
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
		public virtual int CompareTo(Action argActionToCompare)
		{
			if(!MayBeConflict)
			{
				return 1;
			}
			if(this.GetType().BaseType != argActionToCompare.GetType().BaseType)
			{
				return 1;
			}
			return 0;
		}
    }
}
