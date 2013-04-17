using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// Class of need
    /// </summary>
    [Serializable]
    public class Need : NameableObject, ITypicalNeed
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        public static int Comparing(Need n1, Need n2)
        {
            int comp = n1.NeedLevel.CompareTo(n2.NeedLevel);
            int subcomp = n1.NeedSubLevel.CompareTo(n2.NeedSubLevel);
            return comp == 1 ? 1 : comp == -1 ? -1 : subcomp;
        }
        
        #region Constructors

			/// <summary>
			/// Empty constructor, fills the class with default values
			/// </summary>
			public Need()
			{
				NeedLevel = 10;
				NeedSubLevel = 1;
				SatisfyingActions = new List<Type>();
			}

			/// <summary>
			/// Need constuctor from one action
			/// </summary>
			/// <param name="argName">name of need</param>
			/// <param name="argNeedLevel">need level like in Maslow's hierarchy of needs </param>
			/// <param name="argNeedSubLevel">sublevel to more flexible model</param>
			/// <param name="argSatisfyingActionID"> action, that satisfy this need</param>
			public Need(string argName, int argNeedLevel, int argNeedSubLevel, Type argSatisfyingActionID)
			{
				Name = argName;
				NeedLevel = argNeedLevel;
				NeedSubLevel = argNeedSubLevel;
				SatisfyingActions = new List<Type>();
				SatisfyingActions.Add(argSatisfyingActionID);
			}

			/// <summary>
			/// Need constuctor from List of action
			/// </summary>
			/// <param name="argName">name of need</param>
			/// <param name="argNeedLevel">need level like in Maslow's hierarchy of needs</param>
			/// <param name="argNeedSubLevel">sublevel to more flexible model</param>
			/// <param name="argSatisfyingActionIDs">actions, that satisfy this need</param>
			public Need(string argName, int argNeedLevel, int argNeedSubLevel, List<Type> argSatisfyingActionIDs)
			{
				Name = argName;
				NeedLevel = argNeedLevel;
				NeedSubLevel = argNeedSubLevel;
				SatisfyingActions = argSatisfyingActionIDs;
			}

			/// <summary>
			/// Need constuctor without need sublevel
			/// </summary>
			/// <param name="argName">name of need</param>
			/// <param name="argNeedLevel">need level like in Maslow's hierarchy of needs</param>
			/// <param name="argSatisfyingActionIDs">actions, that satisfy this need</param>
			public Need(string argName, int argNeedLevel, List<Type> argSatisfyingActionIDs) 
				: this(argName, argNeedLevel, 0, argSatisfyingActionIDs)
			{
			}

			public Need(Characteristic ch)
			{
            
			}

		#endregion

		#region ITypicalNeed realisation

			public int NeedLevel { get; set; }     //Need level like in Maslow's hierarchy of needs 

			public int NeedSubLevel { get; set; }  //Sublevel to more flexible model

			public List<Type> SatisfyingActions { get; set; } //List of actions, that can satisfy the need

        #endregion

        public override string ToString()
        {
            return Name + ", lvl " + NeedLevel.ToString() + "," + NeedSubLevel.ToString();
        }
    }
}
