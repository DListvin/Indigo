using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Map;
using IndigoEngine.Agents;
using NLog;

namespace MapEditor
{
	public partial class ToolBarWindow : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();		

		//Events
		public event EventHandler TabTilesSelected;   //Tab with tiles selected
		public event EventHandler TabTilesDeSelected; //Tab with tiles deselected
		public event EventHandler TileChanged;        //Selected tile changed
		public event EventHandler BrushSizeChanged;   //Brush size changed

		#region Constructors

			public ToolBarWindow()
			{
				InitializeComponent();

				//Initialising toolbars
				var toolDefaultHeight = MapEditor.Properties.Resources.grass64.Width + 2; //Default height of each tool
				
				//Adding agents to toolbar
				foreach(var texture in MainWindow.texturesDict)
				{
					var newToolPanel = new Panel();
					newToolPanel.Name = "AgentsToolPanel" + texture.Key.Name.Split('.').Last();
					newToolPanel.Size = new Size(tabAgentsAdding.Size.Width, toolDefaultHeight);
					newToolPanel.Location = new Point(0, tabAgentsAdding.Controls.Count * toolDefaultHeight);
					newToolPanel.BackgroundImage = texture.Value;
					newToolPanel.BackgroundImageLayout = ImageLayout.Center;
					newToolPanel.MouseDown += new MouseEventHandler(agentTool_MouseDown);   
					newToolPanel.Tag = texture.Key;
					tabAgentsAdding.Controls.Add(newToolPanel);
				}

				//Adding tiles to toolbar
				foreach(var tile in new MapTiles())
				{
					var newToolPanel = new Panel();
					newToolPanel.Name = "TileToolPanel" + tile.Name;
					newToolPanel.Size = new Size(tabTiles.Size.Width, toolDefaultHeight);
					newToolPanel.Location = new Point(0, tabTiles.Controls.Count * toolDefaultHeight);
					newToolPanel.BackgroundImage = tile.TileImage;
					newToolPanel.BackgroundImageLayout = ImageLayout.Center;
					newToolPanel.MouseClick += new MouseEventHandler(tileTool_MouseClick);   
					newToolPanel.Tag = tile;
					tabTiles.Controls.Add(newToolPanel);
				}

				var brushTrackBar = new TrackBar();
				brushTrackBar.Name = "BrushTrackBar";
				brushTrackBar.Size = new Size(tabTiles.Size.Width, toolDefaultHeight);
				brushTrackBar.Location = new Point(0, tabTiles.Controls.Count * toolDefaultHeight);
				brushTrackBar.Minimum = 1;
				brushTrackBar.Maximum = 5;
				brushTrackBar.ValueChanged += new EventHandler(brushTrackBar_ValueChanged);
				tabTiles.Controls.Add(brushTrackBar);
			}

		#endregion

		#region Form events
		
			private void ToolBarWindow_Load(object sender, EventArgs e)
			{			
				(Owner as MainWindow).MouseCoordsChanged += new MouseEventHandler(MainEditor_MouseMove);
				if(TabTilesSelected != null)
				{
					TabTilesSelected(this, new EventArgs());
				}
				chooseTile(tabTiles.Controls[0] as Panel);
			}

			private void ToolBarWindow_FormClosing(object sender, FormClosingEventArgs e)
			{
				(Owner as MainWindow).MouseCoordsChanged -= MainEditor_MouseMove;
			}

		#endregion

		#region Events from main editor

			private void MainEditor_MouseMove(object sender, MouseEventArgs e)
			{
				labelXYCoords.Text = "XY coords: " + e.Location.ToString();
				var mainWindowOwner = (Owner as MainWindow);
				labelHexCoords.Text = "Hex coords: " + HexagonCoord.GetHexCoords(e.X, e.Y, mainWindowOwner.EditingGrid.EdgeLenght, new Point()).ToString();
			}

		#endregion

		#region Tab panel events

			private void tabMainTools_Selecting(object sender, TabControlCancelEventArgs e)
			{
				if(tabMainTools.SelectedIndex == 0)
				{
					if(TabTilesSelected != null)
					{
						TabTilesSelected(this, new EventArgs());		
					}		
					chooseTile(tabTiles.Controls[0] as Panel);
				}
				else
				{					
					if(TabTilesDeSelected != null)
					{
						TabTilesDeSelected(this, new EventArgs());		
					}	
				}
			}

		#endregion

		#region Agents tool events

			private void agentTool_MouseDown(object sender, MouseEventArgs e)
			{
				var convertedSender = sender as Panel;		

				//Getting new agent for this tool
				System.Reflection.ConstructorInfo ci = (convertedSender.Tag as Type).GetConstructor(new Type[] { });
				var newAg = ci.Invoke(new object[] { }) as Agent;
				convertedSender.DoDragDrop(newAg, DragDropEffects.Move);
			}

		#endregion

		#region Tile tool events		
			
			private void tileTool_MouseClick(object sender, MouseEventArgs e)
			{
				var convertedSender = sender as Panel;				
				chooseTile(convertedSender);
			}

			void brushTrackBar_ValueChanged(object sender, EventArgs e)
			{
				if(BrushSizeChanged != null)
				{
					BrushSizeChanged(tabTiles.Controls.Find("BrushTrackBar", false).First(), new EventArgs());
				}
			}

			private void chooseTile(Panel argContainer)
			{
				foreach(var panel in tabTiles.Controls)
				{
					var convertedPanel = panel as Panel;
					if(convertedPanel == null)
					{
						continue;
					}
					if(panel == argContainer)
					{
						convertedPanel.BackColor = Color.Red;
						if(TileChanged != null)
						{
							TileChanged(convertedPanel, new EventArgs());
						}
					}
					else
					{
						convertedPanel.BackColor = Color.White;
					}
				}
			}

		#endregion
	}
}
