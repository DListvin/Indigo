using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
    public static class Needs
    {
		/// <summary>
		/// Atack need
		/// </summary>
        public static Need NeedExample
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
            get { return new Need("log", 2, 1, new ActionObtainResourse(null, null, typeof(Agents.AgentItemLog))); }
        }
    }
}
