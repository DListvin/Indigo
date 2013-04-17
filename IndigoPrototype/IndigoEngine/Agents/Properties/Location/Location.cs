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

		#region Static methods

			/// <summary>
			/// Operator + for locations
			/// </summary>
			/// <param name="argLoc1">Location 1</param>
			/// <param name="argLoc2">Location 2</param>
			/// <returns>Addition result</returns>
			public static Location operator+(Location argLoc1, Location argLoc2)
			{
				Location result = new Location();

				result.Coords = new Point(argLoc1.Coords.X + argLoc2.Coords.X, argLoc1.Coords.Y + argLoc2.Coords.Y);

				return result;
			}
			
			/// <summary>
			/// Operator + for location and point
			/// </summary>
			/// <param name="argLoc1">Location 1</param>
			/// <param name="argLoc2">Point 2</param>
			/// <returns>Addition result</returns>
			public static Location operator+(Location argLoc1, Point argLoc2)
			{
				Location result = new Location();

				result.Coords = new Point(argLoc1.Coords.X + argLoc2.X, argLoc1.Coords.Y + argLoc2.Y);

				return result;
			}

			/// <summary>
			/// Operator - for locations
			/// </summary>
			/// <param name="argLoc1">Location 1</param>
			/// <param name="argLoc2">Location 2</param>
			/// <returns>Substraction result</returns>
			public static Location operator-(Location argLoc1, Location argLoc2)
			{
				Location result = new Location();

				result.Coords = new Point(argLoc1.Coords.X - argLoc2.Coords.X, argLoc1.Coords.Y - argLoc2.Coords.Y);

				return result;
			}
			
			/// <summary>
			/// Operator - for location and point
			/// </summary>
			/// <param name="argLoc1">Location 1</param>
			/// <param name="argLoc2">Point 2</param>
			/// <returns>Substraction result</returns>
			public static Location operator-(Location argLoc1, Point argLoc2)
			{
				Location result = new Location();

				result.Coords = new Point(argLoc1.Coords.X - argLoc2.X, argLoc1.Coords.Y - argLoc2.Y);

				return result;
			}

		#endregion

        #region Constructors

			public Location()
			{
				TargetStorage = null;
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
				TargetStorage = argTargetSorage;
			}

        #endregion

        #region Properties

			public Agent Owner
			{
				get
				{
					if (HasOwner)
					{
						return TargetStorage.Owner;
					}
					return null;
				}
			}

			public ITypicalItemStorage TargetStorage { get; set; } //Item storage of the current object(shows, if the object is in some ItemStorage)

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
					if (TargetStorage == null)
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

        #endregion
    }
}
