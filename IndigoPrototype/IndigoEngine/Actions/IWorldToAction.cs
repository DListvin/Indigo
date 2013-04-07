using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Here is the interface for action to ask worl of nescessary functionality
    /// </summary>
    public interface IWorldToAction
    {
        /// <summary>
        /// Ask World For Deletion obj
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="obj">object, which want to delete</param>
        /// <returns></returns>
        bool AskWorldForDeletion(object sender, Agent obj);

        bool AskWorlForAddition(object sender, Agent obj);
    }
}
