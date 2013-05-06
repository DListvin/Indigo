using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NLog;

namespace MapEditor.Map
{
	public class HexagonalGrid
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();	

		#region Properties
			
			/// <summary>
			///  Grid itself: list of cells, each of them knows it's coords and other information
			///  Sorted by radius
			/// </summary>
			//public List<HexagonCell> Grid { get; private set; } 
			
			/// <summary>
			///  Grid itself: dictionary with coords(M, R)(third coordinate is automatic), each of cells knows it's coords and other information
			/// </summary>
			public Dictionary<int, Dictionary<int, HexagonCell>> Grid { get; private set; }

			/// <summary>
			/// Hexagon edge lenght for the grid
			/// </summary>
			public int EdgeLenght { get; set; } 

		#endregion

		#region Constructors

			public HexagonalGrid(int argEdgeLength = 50, int argRadius = 5)
			{
				EdgeLenght = argEdgeLength;

				Grid = new Dictionary<int, Dictionary<int, HexagonCell>>();

				var deltas = new int[][] //Deltas for hex coordinates to generate hex with radius argRadius
				{
					new int[] {1, 0, -1}, 
					new int[] {0, 1, -1}, 
					new int[] {-1, 1, 0}, 
					new int[] {-1, 0, 1}, 
					new int[] {0, -1, 1}, 
					new int[] {1, -1, 0}
				};				

				for(int r = 0; r < argRadius; r++)
				{
					var main = 0;   //Main coord of hexas
					var right = -r; //Right coord of hexas
					var left = r;   //Left coord of hexas

					var newCell = new HexagonCell(this, main, right, left);
					AddCell(newCell);

					for(int i = 0; i < 6; i++)
					{
						var NumberOfHexasInEdge = r; //We like go throw some hexagons to reach current
						if (i==5)
						{
							NumberOfHexasInEdge--;
						}
						for(int j = 0; j < NumberOfHexasInEdge; j++)
						{
							main += deltas[i][0];
							right += deltas[i][1];
							left += deltas[i][2];
							newCell = new HexagonCell(this, main, right, left);
							AddCell(newCell);
						}
					}
				}

				logger.Debug("New grid: created {0} hexagons", Grid.Count);
			}

		#endregion

		#region Operation with hole grid

			public void AddCell(HexagonCell argCellToAdd)
			{			
				logger.Trace("Adding new cell {0} to the grid {1}", argCellToAdd, this);	

				if(Grid.ContainsKey(argCellToAdd.HexCoorinates.M))
				{
					Dictionary<int, HexagonCell> rCoordsDict;     //Dictionary with M coord as adding cell M coord
					Grid.TryGetValue(argCellToAdd.HexCoorinates.M, out rCoordsDict);
					if(rCoordsDict == null)
					{
						rCoordsDict = new Dictionary<int,HexagonCell>();
					}	
									
					if(rCoordsDict.ContainsKey(argCellToAdd.HexCoorinates.R))
					{						
						logger.Error("Tried to add two cells in one coords!");
						return;
					}
					else
					{						
						var resultDictToAdd = rCoordsDict;   //New dictionary with M coord as adding cell M coord and with adding cell in it
						resultDictToAdd.Add(argCellToAdd.HexCoorinates.R, argCellToAdd);
						Grid.Remove(argCellToAdd.HexCoorinates.M);
						Grid.Add(argCellToAdd.HexCoorinates.M, resultDictToAdd);
					}
				}
				else
				{
					Grid.Add(argCellToAdd.HexCoorinates.M, null);
					AddCell(argCellToAdd);
				}

				logger.Debug("Added new cell {0} to the grid {1}", argCellToAdd, this);
			}

			/// <summary>
			/// Adding the cell or replacing existed
			/// </summary>
			/// <param name="argCellToAdd">Cell to add or replace</param>
			public void AddOrReplaceCell(HexagonCell argCellToAdd)
			{			
				logger.Trace("Replacing cell {0} in the grid {1}", argCellToAdd, this);	

				if(Grid.ContainsKey(argCellToAdd.HexCoorinates.M))
				{
					Dictionary<int, HexagonCell> rCoordsDict;     //Dictionary with M coord as adding cell M coord
					Grid.TryGetValue(argCellToAdd.HexCoorinates.M, out rCoordsDict);
									
					if(rCoordsDict != null && rCoordsDict.ContainsKey(argCellToAdd.HexCoorinates.R))
					{						
						rCoordsDict.Remove(argCellToAdd.HexCoorinates.R);
					}
				}

				AddCell(argCellToAdd);

				logger.Debug("Replaced cell {0} in the grid {1}", argCellToAdd, this);
			}

			/// <summary>
			/// Getting list of cell neighbours with given radius, including the center cell
			/// </summary>
			/// <param name="argCellCenter">Cell from wich to count radius</param>
			/// <param name="argRadius">Radius (1 - returning argCellCenter)</param>
			/// <returns></returns>
			public List<HexagonCell> GetCellNeighboursOfRadius(HexagonCell argCellCenter, int argRadius)
			{
				var result = new List<HexagonCell>();

				var deltas = new int[][] //Deltas for hex coordinates to find hex with radius argRadius
				{
					new int[] {1, 0, -1}, 
					new int[] {0, 1, -1}, 
					new int[] {-1, 1, 0}, 
					new int[] {-1, 0, 1}, 
					new int[] {0, -1, 1}, 
					new int[] {1, -1, 0}
				};				

				HexagonCell currentAddingCell = null; //Current adding cell for checking if there is no shuch cell in the grid

				for(int r = 0; r < argRadius; r++)
				{
					var main = argCellCenter.HexCoorinates.M;   //Main coord of hexas
					var right = argCellCenter.HexCoorinates.R - r; //Right coord of hexas
					var left = argCellCenter.HexCoorinates.L + r;   //Left coord of hexas

					currentAddingCell = GetCellByHexCoord(main, right, left);
					if(currentAddingCell != null)
					{
						result.Add(currentAddingCell);
					}

					for(int i = 0; i < 6; i++)
					{
						var NumberOfHexasInEdge = r; //We like go throw some hexagons to reach current
						if (i==5)
						{
							NumberOfHexasInEdge--;
						}
						for(int j = 0; j < NumberOfHexasInEdge; j++)
						{
							main += deltas[i][0];
							right += deltas[i][1];
							left += deltas[i][2];
							currentAddingCell = GetCellByHexCoord(main, right, left);
							if(currentAddingCell != null)
							{
								result.Add(currentAddingCell);
							}
						}
					}
				}

				return result;
			}

		#endregion

		#region Operations with cells

			public HexagonCell GetCellByXYCoord(int argX, int argY)
			{
				var hexCoords = HexagonCoord.GetHexCoords(argX, argY, EdgeLenght, new Point());
				return GetCellByHexCoord(hexCoords.M, hexCoords.R, hexCoords.L);
			}

			public HexagonCell GetCellByHexCoord(int argM, int argR, int argL)
			{
				Dictionary<int, HexagonCell> mCoordDict;
				Grid.TryGetValue(argM, out mCoordDict);
				if(mCoordDict == null)
				{
					return null;
				}

				HexagonCell result;
				mCoordDict.TryGetValue(argR, out result);
				
				return result;
			}

		#endregion
		
        /// <summary>
        /// IEnumerator for foreach
        /// </summary>
        public IEnumerator<HexagonCell> GetEnumerator()
        {
            foreach(var mCoordDict in Grid.Values)
            {
				foreach(var rCoordDict in mCoordDict.Values)
				{
					yield return rCoordDict;
				}
            }
        }
	}
}
