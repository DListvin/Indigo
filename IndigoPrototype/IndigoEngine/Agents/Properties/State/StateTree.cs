using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;


namespace IndigoEngine.Agents
{
    [Serializable]
	public class StateTree : State
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Constructors

			public StateTree()
			{
				Prolificacy = new Characteristic();
				Prolificacy.Name = "Prolificacy";
                Prolificacy.CriticalPercentValue = 10;
                Prolificacy.CurrentUnitValue = Prolificacy.MaxValue;

				logger.Info("Created new {0}", this.GetType());
				logger.Trace("Created new {0}", this);
			}

		#endregion

		#region Properties
			
			public Characteristic Prolificacy { get; set; }

		#endregion

		#region Static methods

			public static StateTree operator+(StateTree argState1, StateTree argState2)
			{
				StateTree result = new StateTree();

      
				result.Health = argState1.Health + argState2.Health;
				result.Prolificacy = argState1.Prolificacy + argState2.Prolificacy;

				return result;
			}

			public static StateTree operator-(StateTree argState1, StateTree argState2)
			{
				StateTree result = new StateTree();

      
				result.Health = argState1.Health - argState2.Health;
				result.Prolificacy = argState1.Prolificacy - argState2.Prolificacy;

				return result;
			}

		#endregion
	}
}
