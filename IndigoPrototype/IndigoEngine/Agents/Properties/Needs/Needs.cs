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
					return new Need("attack", 4, 1, typeof(ActionAttack));
				}
			}

			public static Need NeedEat
			{
				get
				{
                    List<Type> actions = new List<Type>()
					{
						typeof(ActionEat),
						typeof(ActionObtainFood)
					}; 
					return new Need("eat", 1, 2, actions);
				}
			}

			public static Need NeedDrink
			{
				get
				{
					return new Need("drink", 1, 1, typeof(ActionDrink));
				}
			}

			public static Need NeedRest
			{
				get 
				{
                    List<Type> actions = new List<Type>()
					{
						typeof(ActionRest),
						typeof(ActionBuildShelter),
						typeof(ActionObtainRes)
					};
					return new Need("camp", 2, 1, actions);
				}
			}

			public static Need NeedNothing
			{
				get 
				{
					return new Need("nothing", 9, 1, typeof(ActionDoNothing)); 
				}
			}

            public static Need NeedWander
            {
                get
                {
                    return new Need("wander", 8, 1, typeof(ActionWander));
                }
            }

		#endregion

		#region Tree needs

			public static Need NeedGrowFruit
			{
				get
				{
					return new Need("grow fruit", 1, 1, typeof(ActionGrowFruit));
				}
			}

		#endregion
    }
}
