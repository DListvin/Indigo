using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using IndigoEngine.Actions;

namespace IndigoEngine.Agents
{
    [Serializable]
    public class Vision : ITypicalFieldOfView
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public Vision()
        {
            RangeOfView = 0;
            CurrentView = new List<NameableObject>();
        }

        #endregion

        #region Properties

        public int RangeOfView { get; set; } //Range of view of the agent (in cells around agent, apparently)		

        public List<NameableObject> CurrentView { get; set; } 	//Current field ov view. Includes all agents & actions, that current agent can see		

        /// <summary>
        /// ITypicalFieldOfView
        /// </summary>
        public List<NameableObject> CurrentViewAgents
        {
            get
            {
                return CurrentView.Where(val => { return val is Agent; }).ToList();
            }
        }

        /// <summary>
        /// ITypicalFieldOfView
        /// </summary>
        public List<NameableObject> CurrentViewActions
        {
            get
            {
                return CurrentView.Where(val => { return val is ActionAbstract; }).ToList();
            }
        }


        #endregion

        #region ObjectMethodsOverride

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType())
                    return false;

                var o = obj as Vision;

                return this.RangeOfView.Equals(o.RangeOfView);
            }

            public static bool operator ==(Vision o1, Vision o2)
            {
                return o1.Equals(o2);
            }

            public static bool operator !=(Vision o1, Vision o2)
            {
                return !o1.Equals(o2);
            }

        #endregion
    }
}
