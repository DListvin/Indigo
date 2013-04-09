using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Actions;
using NLog;

namespace IndigoEngine
{
    public static class Needs
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Atack need
		/// </summary>
        public static Need NeedAttack
        {
            get { return new Need("attack", 4, 1, new ActionAttack(null, null, 1)); }
        }

        /// <summary>
        /// Eat Need
        /// </summary>
        public static Need NeedEat
        {
            get { return new Need("eat", 1, 2, new ActionEat(null, null)); }
        }

        /// <summary>
        /// Drink Need
        /// </summary>
        public static Need NeedDrink
        {
            get { return new Need("drink", 1, 1, new ActionDrink(null, null)); }
        }

        public static Need NeedLog
        {
            get { return new Need("log", 2, 2, new ActionObtainResourse(null, null, typeof(Agents.AgentItemLog))); }
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
            get { return new Need("nothing", 9, 1, new List<Action>()); }
        }
    }
}
