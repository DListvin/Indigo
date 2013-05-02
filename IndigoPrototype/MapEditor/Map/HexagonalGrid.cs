using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			public List<HexagonCell> Grid { get; private set; } 

			/// <summary>
			/// Hexagon edge lenght for the grid
			/// </summary>
			public int EdgeLenght { get; set; } 

		#endregion

		#region Constructors

			public HexagonalGrid(int argEdgeLength = 50, int argRadius = 5)
			{
				EdgeLenght = argEdgeLength;

				Grid = new List<HexagonCell>();

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
					Grid.Add(newCell);

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
							Grid.Add(newCell);
						}
					}
				}

				logger.Debug("New grid: created {0} hexagons", Grid.Count);
			}

		#endregion

		
        /// <summary>
        /// IEnumerator for foreach
        /// </summary>
        public IEnumerator<HexagonCell> GetEnumerator()
        {
            foreach(var cell in Grid)
            {
                yield return cell;
            }
        }
	}
}
