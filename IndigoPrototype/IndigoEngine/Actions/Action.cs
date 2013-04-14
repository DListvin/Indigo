﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// basic class for action
    /// </summary>
    [Serializable]
    public abstract class Action : NameableObject, ITypicalAction, IComparable<Action>
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Static methods	

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
        }

		public Action(Agent argSubj, Agent argObj)
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
		public virtual bool CheckForLegitimacy()
		{
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
			return 1;
		}
    }
}
