using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IndigoEngine
{
    [Serializable]
    public abstract class NameableObject
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Constructors

        public NameableObject()
        {
            Name = "Untitled " + this.GetType().ToString().Split('.').Last();
        }

        #endregion

        #region Properties

        public string Name { get; set; }  //Object name

        #endregion

        #region ObjectMethodsOverride

        #endregion
    }
}
