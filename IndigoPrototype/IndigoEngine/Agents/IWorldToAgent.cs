using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
    /// <summary>
    /// What agent knows of world
    /// </summary>
    public interface IWorldToAgent
    {
        /// <summary>
        /// Function to agent to Ask World For An Action
        /// </summary>
        /// <param name="action">action</param>
        /// <returns>world agree or disagree to action</returns>
        bool AskWorldForAction(Action action);
    }
}
