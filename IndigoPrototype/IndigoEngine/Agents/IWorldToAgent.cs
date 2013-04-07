using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

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
		
        /// <summary>
        /// Ask World For Deletion obj
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="obj">object, which want to delete</param>
        /// <returns></returns>
        bool AskWorldForDeletion(Agent sender);
    }
}
