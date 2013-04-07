using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// basic class for action
    /// </summary>
    public abstract class Action : NameableObject, ITypicalAction
    {
        Agent obj, subj;            //Object and subject of the action

        private bool mayBeConflict; //Info about if action is conflict: conflict actions can not be performed with one object from different subjects in one moment

        private List<Type> acceptedSubj; //Meant to be set in descendant class
        private List<Type> acceptedObj;

        #region Constructors

        public Action()
            : base()
        {
        }

        public Action(Agent argObj, Agent argSubj)
            : this()
        {
            Object = argObj;
            Subject = argSubj;
            acceptedObj = new List<Type>();
            acceptedSubj = new List<Type>();
        }

        #endregion

        #region Properties

            #region ITypicalAgent realisation

                public Agent Object
                {
                    get { return obj; }
                    set
                    {
                        obj = value;
                    }
                }

                public Agent Subject
                {
                    get { return subj; }
                    set { subj = value; }
                }

                public bool MayBeConflict
                {
                    get { return mayBeConflict; }
                    set { mayBeConflict = value; }
                }

                public List<Type> AcceptedSubj
                {
                    get { return acceptedSubj; }
                    set { acceptedSubj = value; }
                }

                public List<Type> AcceptedObj
                {
                    get { return acceptedObj; }
                    set { acceptedObj = value; }
                }

                public IWorldToAction World { get; set; }

            #endregion

        #endregion

        /// <summary>
        /// ITypicalAction here is control for obj and subj types
        /// </summary>
        public virtual void Perform()
        {
            if (!acceptedObj.Contains(obj.GetType()))
            {
                throw (new Exception("Object " + obj.ToString() + "have not the nessesary type. See acceptedObj."));
            }
            if (!acceptedSubj.Contains(subj.GetType()))
            {
                throw (new Exception("Subject " + subj.ToString() + "have not the nessesary type. See acceptedSubj."));
            }
        }

        /// <summary>
        /// ITypicalAction
        /// </summary>
        public virtual NameableObject CharacteristicsOfSubject()
        {
            return Subject;
        }
    }
}
