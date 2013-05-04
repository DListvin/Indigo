using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Map
{
	/// <summary>
	/// Class that contains some static fields that represents tiles
	/// </summary>
	public class MapTiles
	{
		/// <summary>
		/// Grass tile
		/// </summary>
		public static MapTile Grass
		{
			get
			{
				var newTile = new MapTile(MapEditor.Properties.Resources.grass64);
				newTile.Name = "Grass";
				return newTile;
			}
		}

		/// <summary>
		/// Earth tile
		/// </summary>
		public static MapTile Earth //TODO: replace texture
		{
			get
			{
				var newTile = new MapTile(MapEditor.Properties.Resources.fruit_tree64);
				newTile.Name = "Earth";
				return newTile;
			}
		}

		public IEnumerator<MapTile> GetEnumerator()
		{
            foreach (System.Reflection.PropertyInfo ch in typeof(MapTiles).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance| System.Reflection.BindingFlags.Public).Where(field =>
            {
                return field.PropertyType == typeof(MapTile);
            }))
            {
                yield return ch.GetValue(typeof(MapTiles), null) as MapTile;
            }
		}
	}
}
