using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;

namespace MapEditor.Map
{
	/// <summary>
	/// Class that represents the hexagon 3 - values coordinates like main, left and right coords
	/// </summary>
	public class HexagonCoord
	{			
		private static Logger logger = LogManager.GetCurrentClassLogger();	

		#region Static methods

			/// <summary>
			/// Getting X and Y coorinate from hex coordinates
			/// </summary>
			/// <param name="argM">M coord</param>
			/// <param name="argR">R coord</param>
			/// <param name="argL">L coord</param>
			/// <param name="argEdgeLenght">Hex edge lenght</param>
			/// <returns>Point: XY coordinates</returns>
			public static Point GetXYCoords(double argM, double argR, double argL, double argEdgeLenght)
			{
				logger.Trace("Getting X and Y coords for {0}, {1}, {2} with edge lenght {3}", argM, argR, argL, argEdgeLenght);

				var result = new Point((int)(Math.Sqrt(3) * argEdgeLenght * (argM/2 + argR)), (int)(1.5 * argEdgeLenght * argM));

				logger.Trace("Result is {0}", result);

				return result;
			}

			/// <summary>
			/// Getting hex coorinates from XY coordinates
			/// </summary>
			/// <param name="argX">X coord</param>
			/// <param name="argY">Y coord</param>
			/// <param name="argEdgeLenght">Hex edge lenght</param>
			/// <returns>HexagonCoord: hex coordinates</returns>
			public static HexagonCoord GetHexCoords(double argX, double argY, double argEdgeLenght)
			{
				logger.Trace("Getting hex coords for {0}, {1} with edge lenght {2}", argX, argY, argEdgeLenght);

				var result = new HexagonCoord((int)(2d/3d * argY / argEdgeLenght), (int)((Math.Sqrt(3)/3d * argX - argY/3d ) / argEdgeLenght), (int)(-(Math.Sqrt(3)/3d * argX + argY/3d) / argEdgeLenght));

				logger.Trace("Result is {0}", result);

				return result;
			}

		#endregion

		#region Properties

			/// <summary>
			/// Main aix - vertical
			/// </summary>
			public int M { get; set; } 

			/// <summary>
			/// Right aix - from top left corner to right bootom
			/// </summary>
			public int R { get; set; } 

			/// <summary>
			/// Left aix - from top right corner to left bootom
			/// </summary>
			public int L { get; set; } 

			/// <summary>
			/// Cell radius from center
			/// </summary>
			public int Radius
			{
				get
				{
					return (int)(Math.Sqrt(M + R + L));
				}
			}

		#endregion

		#region Constructors

			public HexagonCoord(int argM = 0, int argR = 0, int argL = 0)
			{
				M = argM;
				R = argR;
				L = argL;
			}

		#endregion

		public override string ToString()
		{
			return "{" + M.ToString() + "; " + R.ToString() + "; " + L.ToString() + "}";
		}
	}
}
