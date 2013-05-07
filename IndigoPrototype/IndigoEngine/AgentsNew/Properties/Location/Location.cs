using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;

namespace IndigoEngine.AgentsNew
{
    [Serializable]
    public class Location : Quality
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

            /// <summary>
            /// From direction gives increment to position
            /// </summary>
            /// <returns>Point like (0,1) (-1,0) or (1,-1)</returns>
            public static Point Normilize(Location end, Location currentLocation)
            {
                if (end.HasOwner || currentLocation.HasOwner)
                    throw new Exception("Location.Normalize: Some of args has owner");
                Point dir = new Point(end.Coords.X - currentLocation.Coords.X, end.Coords.Y - currentLocation.Coords.Y);
                if (dir.X == 0 && dir.Y == 0)
                    return dir;
                if (Math.Abs(dir.X) > Math.Abs(dir.Y))
                {
                    return new Point((dir.X < 0) ? -1 : 1, 0);
                }
                if (Math.Abs(dir.Y) > Math.Abs(dir.X))
                {
                    return new Point(0, (dir.Y < 0) ? -1 : 1);
                }
                return new Point((dir.X < 0) ? -1 : 1, (dir.Y < 0) ? -1 : 1);
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
