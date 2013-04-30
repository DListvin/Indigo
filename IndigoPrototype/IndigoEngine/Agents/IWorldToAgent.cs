using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using IndigoEngine.ActionsNew;

namespace IndigoEngine
{
    /// <summary>
    /// What agent knows of world
    /// </summary>
    public interface IWorldToAgent
    {        
		/// <summary>
		/// It is for agent for asking world of an action. 
		/// </summary>
		/// <param name="action">Action which agent is asking for</param>
		/// <returns>Positive return mean action is accepted. Negative - vice versa</returns>
        Exception AskWorldForAction(ActionAbstract action);
		
        /// <summary>
        /// Ask World For Deletion obj
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="obj">object, which want to delete</param>
        /// <returns></returns>
        bool AskWorldForEuthanasia(Agent sender);
    }
}
