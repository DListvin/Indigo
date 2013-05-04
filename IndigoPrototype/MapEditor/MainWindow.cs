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
	public partial class MainWindow : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();	
		
		#region Static methods
			
			/// <summary>
			/// Setting double buffering for any control
			/// </summary>
			/// <param name="c">Control to set double buffering</param>
			public static void SetDoubleBuffered(System.Windows.Forms.Control c)
			{
				if (System.Windows.Forms.SystemInformation.TerminalServerSession)
				{
					return;
				}
					System.Reflection.PropertyInfo aProp =
						  typeof(System.Windows.Forms.Control).GetProperty(
								"DoubleBuffered",
								System.Reflection.BindingFlags.NonPublic |
								System.Reflection.BindingFlags.Instance);

				aProp.SetValue(c, true, null);
			}

		#endregion
	

		//Map dragging
        private Point shiftVector;          //shift from basic point (0, 0) to drag the grid and to understand which area must be drawn
        private Point dragVector;           //shift from drag begining point to drag the grid 
        private Point mouseDownPoint;       //Point where we started dragging the map

		//Cells selecting
		private List<HexagonCell> selectedCells; //List of selected cells

		//Events
		public event MouseEventHandler MouseCoordsChanged; //Event for coords of the mouse changed in the owner window

		//Flags		
        private bool leftMouseButtonInMapIsPressed = false;  //Flag for dragging the map (if true - DRAG NOW!)

		#region Textures dictionary

			public static Dictionary<Type, Image> texturesDict = new Dictionary<Type, Image>()
			{
				{
					typeof(AgentLivingIndigo),
					MapEditor.Properties.Resources.indigo_suit64
				},			
				{
					typeof(AgentItemFoodFruit),
					MapEditor.Properties.Resources.fruit64
				},		
				{
					typeof(AgentManMadeShelterCamp),
					MapEditor.Properties.Resources.camp64
				},	
				{
					typeof(AgentItemResLog),
					MapEditor.Properties.Resources.log64
				},
				{
					typeof(AgentPuddle),
					MapEditor.Properties.Resources.water64
				},
				{
					typeof(AgentTree),
					MapEditor.Properties.Resources.tree64_transparent
				},
			}; 

		#endregion

		#region Constructors

			public MainWindow()
			{
				InitializeComponent();

				//Setting double buffering
				SetDoubleBuffered(MainEditorPanel);

				//Setting grid
				EditingGrid = new HexagonalGrid();

				//Setting variables for map dragging
				shiftVector = new Point(MainEditorPanel.Width / 2, MainEditorPanel.Height / 2);
				dragVector = new Point(0, 0);

				//Detting variables for cells selecting
				selectedCells = new List<HexagonCell>();
			}

		#endregion

		#region Properties
		
			/// <summary>
			/// Grid to edit in the editor	
			/// </summary>
			public HexagonalGrid EditingGrid { get; set; }

			/// <summary>
			/// Total shifting from base point(0, 0), including current drugging
			/// </summary>
			private Point totalShiftVector
			{
				get
				{
					return new Point(shiftVector.X + dragVector.X, shiftVector.Y + dragVector.Y);
				}
			}

		#endregion

		#region Menu event handlers

			private void MainMenuFileClose_Click(object sender, EventArgs e)
			{
				this.Close();
			}

			private void MainMenuEditorTerrainToolbar_Click(object sender, EventArgs e)
			{
				var newToolBar = new ToolBarWindow();
				newToolBar.Show(this);
			}

			private void MainMenuEditorDeselectCells_Click(object sender, EventArgs e)
			{
				selectedCells.Clear();
				MainEditorPanel.Refresh();
			}

		#endregion

		#region Editor main panel draw

			private void MainEditorPanel_Paint(object sender, PaintEventArgs e)
			{
				foreach(var cell in EditingGrid)
				{
					var penToDraw = selectedCells.Contains(cell) ? new Pen(Brushes.Red, 3) : new Pen(Brushes.Black);
					e.Graphics.DrawLines(penToDraw, cell.GetCorners(totalShiftVector));
					
					var currentTextureSize = MapEditor.Properties.Resources.grass64.Width;
					var imageCoords = new Point((cell.XYCoordinates.X + totalShiftVector.X - EditingGrid.EdgeLenght / 2), (cell.XYCoordinates.Y + totalShiftVector.Y - EditingGrid.EdgeLenght / 2));

					//Drawing tile
					var drawedImage = cell.PresentedTile.TileImage;
					
					e.Graphics.DrawImage(drawedImage, imageCoords.X, imageCoords.Y, currentTextureSize, currentTextureSize);

					//Drawing agent
					if(cell.InnerAgent == null)
					{
						continue;
					}
					else
					{
						texturesDict.TryGetValue(cell.InnerAgent.GetType(), out drawedImage);
						e.Graphics.DrawImage(drawedImage, imageCoords.X, imageCoords.Y, currentTextureSize, currentTextureSize);
					}

				}
			}

		#endregion

		#region Editor main panel mouse events
		
			/// <summary>
			/// Used only for dragging
			/// </summary>
			private void MainEditorPanel_MouseDown(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					mouseDownPoint.X = e.X ;
					mouseDownPoint.Y = e.Y ;
					leftMouseButtonInMapIsPressed = true;
				}
			}

			/// <summary>
			/// Used for dragging and for updating mouse coords in the tool bar
			/// </summary>
			private void MainEditorPanel_MouseMove(object sender, MouseEventArgs e)
			{			
				if (leftMouseButtonInMapIsPressed)
				{
					dragVector.X = e.X - mouseDownPoint.X;
					dragVector.Y = e.Y - mouseDownPoint.Y ;
					MainEditorPanel.Refresh();
				}
				if(MouseCoordsChanged != null)
				{
					var newMouseEventArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - totalShiftVector.X, e.Y - totalShiftVector.Y, e.Delta);
					MouseCoordsChanged(this, newMouseEventArgs);
				}
			}

			/// <summary>
			/// Used for ending dragging and for cells selecting
			/// </summary>
			private void MainEditorPanel_MouseUp(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					shiftVector.X += dragVector.X;
					shiftVector.Y += dragVector.Y;
					dragVector = new Point(0, 0);
					leftMouseButtonInMapIsPressed = false;
				}
				if (e.Button == MouseButtons.Right)
				{
					var cellUnderMouse = EditingGrid.GetCellByXYCoord(e.X - totalShiftVector.X, e.Y - totalShiftVector.Y);
					contextMenuCellSelect.Checked = selectedCells.Contains(cellUnderMouse) ? true : false;
					contextMenuCell.Tag = cellUnderMouse;
					contextMenuCell.Show(Cursor.Position);
				}
			}

		#endregion

		#region Editor main panel drag and drop from toolbars

			private void MainEditorPanel_DragEnter(object sender, DragEventArgs e)
			{
				if(e.AllowedEffect.HasFlag(DragDropEffects.Move))
				{
					e.Effect = DragDropEffects.Move;
				}
			}

			private void MainEditorPanel_DragDrop(object sender, DragEventArgs e)
			{
				var agentDropped = e.Data.GetData(e.Data.GetFormats()[0]) as Agent; //Getting dropped data

				var newCoords = MainEditorPanel.PointToClient(new Point(e.X, e.Y)); //Getting client coords from screen
				var cellToEdit = EditingGrid.GetCellByXYCoord(newCoords.X - totalShiftVector.X, newCoords.Y - totalShiftVector.Y);//Getting cell from coords to add agent in it
				if(cellToEdit != null)
				{
					cellToEdit.InnerAgent = agentDropped;
					EditingGrid.AddOrReplaceCell(cellToEdit);
				}

				MainEditorPanel.Refresh();
			}

		#endregion

		#region Context menu cell events

			private void contextMenuCellSelect_Click(object sender, EventArgs e)
			{		
				var convertedSender = sender as ToolStripMenuItem;
				var cellToSelect = convertedSender.Owner.Tag as HexagonCell;
				if(cellToSelect != null)
				{
					if(selectedCells.Contains(cellToSelect))
					{
						selectedCells.Remove(cellToSelect);
					}
					else
					{
						selectedCells.Add(cellToSelect);
					}
				}
				MainEditorPanel.Refresh();	
			}

			private void contextMenuCellClearAgents_Click(object sender, EventArgs e)
			{			
				var convertedSender = sender as ToolStripMenuItem;
				var cellToClear = convertedSender.Owner.Tag as HexagonCell;
				if(cellToClear != null)
				{
					cellToClear.InnerAgent = null;
				}
				MainEditorPanel.Refresh();	
			}

		#endregion
	}
}
