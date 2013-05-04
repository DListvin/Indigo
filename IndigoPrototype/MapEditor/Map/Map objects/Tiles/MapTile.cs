using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine;
using NLog;


namespace MapEditor.Map
{	
	/// <summary>
	/// Class that represents info about tile(earth, grass e.c.)
	/// </summary>
	public class MapTile : NameableObject
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();		

		#region Properties

			/// <summary>
			/// Image that assosiated with this tile
			/// </summary>
			public Image TileImage { get; private set; }

		#endregion

		#region Constructors

			public MapTile(Image argTileImage)
			{
				TileImage = argTileImage;
			}

			public MapTile()
				:this(MapEditor.Properties.Resources.grass64)
			{
			}

		#endregion
	}
}
