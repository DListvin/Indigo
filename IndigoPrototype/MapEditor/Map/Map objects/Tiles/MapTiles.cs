using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Map
{
	/// <summary>
	/// Class that contains some static field that represents tiles
	/// </summary>
	public static class MapTiles
	{
		/// <summary>
		/// Grass tile
		/// </summary>
		public static MapTile Grass
		{
			get
			{
				return new MapTile(MapEditor.Properties.Resources.grass64);
			}
		}

		/// <summary>
		/// Earth tile
		/// </summary>
		public static MapTile Earth //TODO: replace texture
		{
			get
			{
				return new MapTile(MapEditor.Properties.Resources.fruit_tree64);
			}
		}
	}
}
