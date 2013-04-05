using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine;
using IndigoEngine.Agents;

namespace GraphicalUI
{
    /// <summary>
    /// Static class making graphical UI.
    /// </summary>
    static class GraphicalUIShell
    {
        //Attached model to observe
        static IObservableModel model = null;

        #region Properties

        public static IObservableModel Model
        {
            get { return model; }
            set { model = value; }
        }

        #endregion
    }
}
