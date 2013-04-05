using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IndigoEngine.Agents
{
    public delegate void ActionFeedback();

	public abstract class Agent : NameableObject, ITypicalAgent 
	{
		private Characteristic health;                //Agent health
		private Point? location;                      //Agent location in the world grid - (X, Y), if null - agent is in some ItemStorage
		private ItemStorage inventory;                //Agent inventory
        private ActionFeedback currentActionFeedback; //Current action result, that is needed to be perform
        private World homeWorld;                      //Agent's world
		
		#region Constructors
			
		public Agent() 
			: base()
		{
			Health = new Characteristic();
			Health.Name = "Health";
			
			Location = new Point(0, 0);

			Inventory = new ItemStorage();

			HomeWorld = null;
		}

		#endregion

		#region Properties
					
		#region ITypicalAgent realisation
				
		public Characteristic Health
		{
			get {return health;}
			set	{health = value;}
		}

		public Point? Location
		{
			get	{return location;}
			set	{location = value;}
		}

		public ItemStorage Inventory
		{
			get	{return inventory;}
			set {inventory = value;}
		}

		public ActionFeedback CurrentActionFeedback
		{
			get	{return currentActionFeedback;}
			set	{currentActionFeedback = value;}
		}

        public World HomeWorld
        {
            get { return homeWorld; }
            set { homeWorld = value; }
        }
					
		#endregion

		#endregion
				
		/// <summary>
		/// ITypicalAgent
		/// </summary>
		public virtual void Decide()
		{
		}
		
		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public virtual void StateRecompute()
        {
        }

		/// <summary>
		/// ITypicalAgent
		/// </summary>
        public void PerformFeedback()
        {
            if (CurrentActionFeedback != null)
			{
                CurrentActionFeedback.Invoke();
			}
        }
	}
}
