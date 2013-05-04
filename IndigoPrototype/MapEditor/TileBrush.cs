using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Map;

namespace MapEditor
{
	/// <summary>
	/// Class to store informution about current brush
	/// </summary>
	public class TileBrush
	{
		#region Properties

			/// <summary>
			/// Current drawing tile
			/// </summary>
			public MapTile CurrentTile { get; set; }

			/// <summary>
			/// Brush size
			/// </summary>
			public int Size { get; set; }

		#endregion

		#region Constructors

			public TileBrush()
			{
				CurrentTile = MapTiles.Earth;
				Size = 1;
			}

		#endregion
	}
}
