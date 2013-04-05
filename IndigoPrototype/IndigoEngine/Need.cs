using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
    /// <summary>
    /// Main interfase of indigo need 
    /// </summary>
    interface ITypicalNeed
    {
        int NeedLevel { get; }
        int NeedSubLevel { get; }
        List<Action> SatisfyingActionIDs { get; }
    }

    /// <summary>
    /// Class of need
    /// </summary>
    public class Need : NameableObject, ITypicalNeed
    {
        string name;
        int needLevel; // need level like in Maslow's hierarchy of needs 
        int needSubLevel; // sublevel to more flexible model
        List<Action> satisfyingActionIDs;

        #region Constructors

        public Need()
        {
            name = "Untitled need";
            needLevel = 10;
            needSubLevel = 1;
            satisfyingActionIDs = new List<Action>();
        }
        /// <summary>
        /// Need constuctor from one action
        /// </summary>
        /// <param name="name">name of need</param>
        /// <param name="needLevel">need level like in Maslow's hierarchy of needs </param>
        /// <param name="needSubLevel">sublevel to more flexible model</param>
        /// <param name="satisfyingActionID"> action, that satisfy this need</param>
        public Need(string name, int needLevel, int needSubLevel, Action satisfyingActionID)
        {
            this.name = name;
            this.needLevel = needLevel;
            this.needSubLevel = needSubLevel;
            this.satisfyingActionIDs = new List<Action>();
            this.satisfyingActionIDs.Add(satisfyingActionID);
        }

        /// <summary>
        /// Need constuctor from List of action
        /// </summary>
        /// <param name="name">name of need</param>
        /// <param name="needLevel">need level like in Maslow's hierarchy of needs</param>
        /// <param name="needSubLevel">sublevel to more flexible model</param>
        /// <param name="satisfyingActionIDs">actions, that satisfy this need</param>
        public Need(string name, int needLevel, int needSubLevel, List<Action> satisfyingActionIDs)
        {
            this.name = name;
            this.needLevel = needLevel;
            this.needSubLevel = needSubLevel;
            this.satisfyingActionIDs = satisfyingActionIDs;
        }

        public Need(string name, int needLevel, List<Action> satisfyingActionIDs)
        {
            this.name = name;
            this.needLevel = needLevel;
            this.needSubLevel = 0;
            this.satisfyingActionIDs = satisfyingActionIDs;
        }
        #endregion

        #region ITypicalNeed realisation
        public int NeedLevel
        {
            get
            {
                return needLevel;
            }
        }
        public int NeedSubLevel
        {
            get
            {
                return needSubLevel;
            }
        }
        public List<Action> SatisfyingActionIDs
        {
            get
            {
                return satisfyingActionIDs;
            }
        }
        #endregion

        public override string ToString()
        {
            return name + ", lvl " + needLevel.ToString() + "," + needSubLevel.ToString();
        }
    }

    public static class Needs
    {
        public static Need NeedExample
        {
            get
            {
                return new Need("exmpl", 1, 0, new ActionExample(null, null, 1));
            }
        }
    }
}
