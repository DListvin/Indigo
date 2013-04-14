using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;

namespace IndigoEngine.Agents
{
    [Serializable]
    public class Location
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Point coords; //Agent location in the world grid - (X, Y)

        #region Constructors

        public Location()
        {
            TargetSorage = null;
            Coords = new Point(0, 0);

            logger.Info("Created new {0}", this.GetType());
            logger.Trace("Created new {0}", this);
        }

        public Location(int argX, int argY)
            : base()
        {
            Coords = new Point(argX, argY);
        }

        public Location(ITypicalItemStorage argTargetSorage)
            : base()
        {
            TargetSorage = argTargetSorage;
        }

        #endregion

        #region Properties

        public Agent Owner
        {
            get
            {
                if (HasOwner)
                {
                    return TargetSorage.Owner;
                }
                return null;
            }
        }

        public ITypicalItemStorage TargetSorage { get; set; } //Item storage of the current object(shows, if the object is in some ItemStorage)

        public Point Coords
        {
            get
            {
                if (HasOwner)
                {
                    return Owner.CurrentLocation.Coords;
                }
                return coords;
            }
            set
            {
                coords = value;
            }
        }

        public bool HasOwner
        {
            get
            {
                if (TargetSorage == null)
                {
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region ObjectMethodsOverride

            public override string ToString()
            {
                return HasOwner ? Owner.Name : Coords.ToString();
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != this.GetType())
                    return false;

                var o = obj as Location;

                return this.Coords.Equals(o.Coords) && this.HasOwner.Equals(o.HasOwner) && this.Owner.Equals(o.Owner);
            }

            public static bool operator ==(Location o1, Location o2)
            {
                return o1.Equals(o2);
            }

            public static bool operator !=(Location o1, Location o2)
            {
                return !o1.Equals(o2);
            }

        #endregion
    }
}
