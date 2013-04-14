using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine.Actions
{
    [Serializable]
    class ActionRest : Action
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
