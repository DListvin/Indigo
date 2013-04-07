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
        bool AskWorldForAnAction(Action action);
    }
}
