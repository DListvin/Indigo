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
		/// <summary>
		/// Operations with need level like in Maslow's hierarchy of needs 
		/// </summary>
        int NeedLevel { get; set; }

		/// <summary>
		/// Operations with need sublevel to more flexible model
		/// </summary>
        int NeedSubLevel { get; set; }

		/// <summary>
		/// Operation with list of actions, that can satisfy the need
		/// </summary>
        List<Action> SatisfyingActions { get; set; }
    }
}
