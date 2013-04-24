using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine.Agents.Properties.Memory
{
    /// <summary>
    /// For stored information. Tring to remember, where to go).
    /// </summary>
    class TargetInfo : NameableObject
    {
        #region Properties

            public bool IsTarget { get; set; }

        #endregion

        #region Constructors

            public TargetInfo(bool isTraget)
            {
                IsTarget = isTraget;
            }

            public TargetInfo()
            {
                IsTarget = false;
            }

        #endregion
    }
}
