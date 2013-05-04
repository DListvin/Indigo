using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using NLog;

namespace MapEditor.Map
{
	public class HexagonCell
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();		

		#region Properties 

			/// <summary>
			/// Owner grid
			/// </summary>
			public HexagonalGrid Owner { get; private set; }
			
			/// <summary>
			/// Cell hex coordinates
			/// </summary>
			public HexagonCoord HexCoorinates { get; private set; }

			/// <summary>
			/// Cell XY coordinates
			/// </summary>
			public Point XYCoordinates
			{
				get
				{
					return HexagonCoord.GetXYCoords(HexCoorinates.M, HexCoorinates.R, HexCoorinates.L, Owner.EdgeLenght, new Point());
				}
			}

			/// <summary>
			/// Agent that is in the cell now
			/// </summary>
			public Agent InnerAgent { get; set; }

		#endregion

		#region Constructors

			/// <summary>
			/// Constructor for hex coords (numbers)
			/// </summary>
			public HexagonCell(HexagonalGrid argOwner, int argM = 0, int argR = 0, int argL = 0)
			{	
				Owner = argOwner;
				HexCoorinates = new HexagonCoord(argM, argR, argL);

				//Default initialisation for none-parametrised properties
				InnerAgent = null;
			}

			/// <summary>
			/// Constructor for hex coords (class)
			/// </summary>
			public HexagonCell(HexagonalGrid argOwner, HexagonCoord argHexCoords)
				:this(argOwner, argHexCoords.M, argHexCoords.R, argHexCoords.L)
			{	
			}
			
			/// <summary>
			/// Constructor for XY coords (numbers)
			/// </summary>
			public HexagonCell(HexagonalGrid argOwner, int argX = 0, int argY = 0)
				:this(argOwner, HexagonCoord.GetHexCoords(argX, argY, argOwner.EdgeLenght, new Point()))
			{	
			}
			
			/// <summary>
			/// Constructor for XY coords (class)
			/// </summary>
			public HexagonCell(HexagonalGrid argOwner, Point argXYcoords)
				:this(argOwner, HexagonCoord.GetHexCoords(argXYcoords.X, argXYcoords.Y, argOwner.EdgeLenght, new Point()))
			{	
			}

		#endregion

		/// Getting cell corners coordinate in XY using cell center coordinates
		/// </summary>
		/// <param name="argShiftVector">Shift vector for all points (need for drawing optimisation)</param>
		/// <returns>Array of points: XY coordinates of the corners</returns>
		public Point[] GetCorners(Point argShiftVector)
		{
			logger.Trace("Getting corners coords for {0} ", this.HexCoorinates);

			var edgeLenght = Owner.EdgeLenght;  //Edge length for optimisation
			var xCenterCoord = XYCoordinates.X; //X center coordinate for optimisation
			var yCenterCoord = XYCoordinates.Y; //Y center coordinate for optimisation
			var xShiftCoord = argShiftVector.X; //X shift coordinate for optimisation
			var yShiftCoord = argShiftVector.Y; //Y shift coordinate for optimisation
			var cos60 = 0.5;                    //Cos 60 for optimisation
			var sin60 = Math.Sqrt(3) * 0.5;     //Sin 60 for optimisation

			//Result of the function
			var result = new Point[]
			{
				new Point(xCenterCoord + xShiftCoord, yCenterCoord - edgeLenght + yShiftCoord),
				new Point((int)(xCenterCoord + edgeLenght * sin60 + xShiftCoord), (int)(yCenterCoord - edgeLenght * cos60 + yShiftCoord)),
				new Point((int)(xCenterCoord + edgeLenght * sin60 + xShiftCoord), (int)(yCenterCoord + edgeLenght * cos60 + yShiftCoord)),
				new Point(xCenterCoord + xShiftCoord, yCenterCoord + edgeLenght + yShiftCoord),
				new Point((int)(xCenterCoord - edgeLenght * sin60 + xShiftCoord), (int)(yCenterCoord + edgeLenght * cos60 + yShiftCoord)),
				new Point((int)(xCenterCoord - edgeLenght * sin60 + xShiftCoord), (int)(yCenterCoord - edgeLenght * cos60 + yShiftCoord)),

				new Point(xCenterCoord + xShiftCoord, yCenterCoord - edgeLenght + yShiftCoord), //Seventh corner is equal to first, need for drawing optimisation to draw closed figure
			}; 

			logger.Trace("Result is {0}", result);

			return result;
		}
	}
}
