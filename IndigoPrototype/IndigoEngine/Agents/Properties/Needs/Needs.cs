using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Actions;
using NLog;

namespace IndigoEngine
{
    [Serializable]
    public static class Needs
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region Indigo needs
			public static Need NeedAttack
			{
				get
				{
					return new Need("attack", 4, 1, new ActionAttack(null, null, 1));
				}
			}

			public static Need NeedEat
			{
				get
				{
                    List<Action> actions = new List<Action>();
                    actions.Add(new ActionEat(null, null));
                    actions.Add(new ActionObtainResourse(null, null, typeof(Agents.AgentItemFruit)));
					return new Need("eat", 1, 2, actions);
				}
			}

			public static Need NeedDrink
			{
				get
				{
					return new Need("drink", 1, 1, new ActionDrink(null, null));
				}
			}

			public static Need NeedLog
			{
				get
				{
					return new Need("log", 2, 2, new ActionObtainResourse(null, null, typeof(Agents.AgentItemLog)));
				}
			}

			public static Need NeedCamp
			{
				get { return new Need("camp", 2, 1, new ActionBreakCamp(null, new System.Drawing.Point())); }
			}

			public static Need NeedRest
			{
				get { return new Need("rest", 2, 3, new ActionRest()); }
			}

			public static Need NeedNothing
			{
				get { return new Need("nothing", 9, 1, new ActionDoNothing(null)); }
			}

		#endregion

		#region Tree needs

			public static Need NeedGrowFruit
			{
				get
				{
					return new Need("grow fruit", 1, 1, new ActionGrowFruit(null));
				}
			}

		#endregion
    }
}
