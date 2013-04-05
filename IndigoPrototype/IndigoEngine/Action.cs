using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// An action, suggested by agent and performed by world
    /// </summary>
    interface IAction
    {
        Agent Object { get; set; }
        Agent Subject { get; set; }

        bool MayBeConflict { get; }

        void Perform();
    }

    abstract public class Action : IAction
    {
        Agent obj, subj;
        protected bool mayBeConflict;

        public Action(Agent obj, Agent subj)
        {
            this.obj = obj;
            this.subj = subj;
        }

        public Agent Object
        {
            get
            {
                return obj;
            }
            set
            {
                obj = value;
            }
        }

        public Agent Subject
        {
            get
            {
                return subj;
            }
            set
            {
                subj = value;
            }
        }

        public bool MayBeConflict { get { return mayBeConflict; } }

        public virtual void Perform() { }
    }

    public delegate void ActionFeedback();
}
