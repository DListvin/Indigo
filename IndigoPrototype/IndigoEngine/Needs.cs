using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
    public static class Needs
    {
		/// <summary>
		/// Example of need. Delete it after you invent some actual needs
		/// </summary>
        public static Need NeedExample
        {
            get { return new Need("exmpl", 1, 0, new ActionAttack(null, null, 1)); }
        }


    }
}
