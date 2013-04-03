using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// An action, suggested by agent and performed by world
    /// </summary>
    interface IAction
    {
        //Object and Subject of action.
        Agent Object { get; }
        Agent Subject { set; }

        void Perform();
    }
}
