using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IndigoEngine.Agents
{
	public abstract class Agent : NameableObject, ITypicalAgent 
	{
		private Characteristic health;   //Agent health
		private Point? location;         //Agent location in the world grid - (X, Y), if null - agent is in some ItemStorage
		private ItemStorage inventory;   //Agent inventory
		private int rangeOfView;         //Range of view of the agent (in cells around agent, apparently)
        private ActionFeedback actionFeedback; //Action result
		
		#region Constructors
			
			public Agent() : base()
			{
				Health = new Characteristic();
				Health.Name = "Health";
				
				Location = new Point(0, 0);

				Inventory = new ItemStorage();

				RangeOfView = 0;
			}

		#endregion

		#region Properties
					
			#region ITypicalAgent realisation
				
				public Characteristic Health
				{
					get
					{
						return health;
					}
					set
					{
						health = value;
					}
				}

				public Point? Location
				{
					get
					{
						return location;
					}
					set
					{
						location = value;
					}
				}

				public ItemStorage Inventory
				{
					get
					{
						return inventory;
					}
					set
					{
						inventory = value;
					}
				}

				public int RangeOfView
				{
					get
					{
						return rangeOfView;
					}
					set
					{
						rangeOfView = value;
					}
				}

                public ActionFeedback ActionFeedback
                {
                    get
                    {
                        return actionFeedback;
                    }
                    set
                    {
                        actionFeedback = value;
                    }
                }
					
			#endregion

		#endregion
				
		public virtual void Decide()
		{
		}


        /// <summary>
        /// Modify it's charecterictics each turn.
        /// </summary>
        public virtual void StateRecompute()
        {
        }

        public void PerformFeedback()
        {
            if (actionFeedback != null)
                actionFeedback.Invoke();
        }
	}
}
