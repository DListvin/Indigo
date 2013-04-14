using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using NLog;

namespace IndigoEngine.Actions
{
    [Serializable]
    class ActionRest : ActionAbstract
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static InfoAboutAction CurrentActionInfo = new InfoAboutAction
																		(
																			new List<Type>()
																			{
																			},
																			new List<Type>()
																			{
																			},
																			new List<Skill>()
																			{
																			},
																			true,
																			true
																		);

        #region Constructors

			public ActionRest()
				: base(null, null)
			{
			}

        #endregion	

    }
}
