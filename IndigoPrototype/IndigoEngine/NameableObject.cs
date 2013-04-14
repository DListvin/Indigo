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

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType())
                    return false;

                var o = obj as NameableObject;

                return this.Name.Equals(o.Name);
            }

            public static bool operator ==(NameableObject o1, NameableObject o2)
            {
                return o1.Equals(o2);
            }

            public static bool operator !=(NameableObject o1, NameableObject o2)
            {
                return !o1.Equals(o2);
            }

        #endregion
    }
}
