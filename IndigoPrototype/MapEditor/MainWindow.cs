using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapEditor.Map;
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

		private HexagonalGrid editingGrid;  //Grid to edit in the editor		
        private Point shiftVector;          //shift from basic point (0, 0) to drag the grid and to understand which area must be drawn
        private Point dragVector;           //shift from drag begining point to drag the grid 
        private Point mouseDownPoint;       //Point where we started dragging the map

		//Flags		
        private bool leftMouseButtonInMapIsPressed = false;  //Flag for dragging the map (if true - DRAG NOW!)

		#region Constructors

			public MainWindow()
			{
				InitializeComponent();

				//Setting double buffering
				SetDoubleBuffered(MainEditorPanel);

				shiftVector = new Point(MainEditorPanel.Width / 2, MainEditorPanel.Height / 2);
				dragVector = new Point(0, 0);

				editingGrid = new HexagonalGrid();
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

		#endregion

		#region Editor main panel draw

			private void MainEditorPanel_Paint(object sender, PaintEventArgs e)
			{
				var totalShiftVector = new Point();
				totalShiftVector.X = shiftVector.X + dragVector.X;
				totalShiftVector.Y = shiftVector.Y + dragVector.Y;

				foreach(var cell in editingGrid)
				{
					e.Graphics.DrawLines(new Pen(Brushes.Black), cell.GetCorners(totalShiftVector));
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
			/// Used for dragging and for updating mouse coords in the map corner
			/// </summary>
			private void MainEditorPanel_MouseMove(object sender, MouseEventArgs e)
			{			
				//TODO: add coordinates monitoring

				//labelUICoords.Text = "UI coords: " + e.Location.ToString();
				//labelModelCoords.Text = "Model coords: " + GetModelCoord(e.Location).ToString();

				if (leftMouseButtonInMapIsPressed)
				{
					dragVector.X = e.X - mouseDownPoint.X;
					dragVector.Y = e.Y - mouseDownPoint.Y ;
					MainEditorPanel.Refresh();
				}
			}

			/// <summary>
			/// Used for ending dragging 
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
			}

		#endregion
	}
}
