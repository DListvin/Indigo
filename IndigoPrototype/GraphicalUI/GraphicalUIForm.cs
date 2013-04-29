using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IndigoEngine;
using IndigoEngine.Agents;
using NLog;

namespace GraphicalUI
{
    public partial class GrapgicalUIForm : Form
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

			public static int ComparingByDrawLevels(Agent ag1, Agent ag2)
			{
				int lvl1, lvl2;  //Draw levels of the agents
				texturesLevelsDict.TryGetValue(ag1.GetType(), out lvl1);
				texturesLevelsDict.TryGetValue(ag2.GetType(), out lvl2);

				if(lvl1 > lvl2) 
				{
					return -1; 
				}
				if(lvl1 < lvl2) 
				{
					return 1;
				}
				return 0;
			}

		#endregion
		
        private int textureSize = GraphicalUI.Properties.Resources.grass64.Width;  //Side of the texture (default)

        private Point shiftVector;       //shift from basic point (0, 0) to drag the map and to understand which area must be drawn
        private Point mouseDownPoint;    //Point where we start dragging the map
        private bool leftMouseButtonInMapIsPressed = false;  //Flag for dragging the map (if true - DRAG NOW!)
        private int zoomModifyer = 0;    //Zoom modifyer (contains delta adding to the texture size)
        
		public List<Agent> DisplayInfoAgents { get; set; }               //Info about agents that must be displayed in the mapInfoPanel

		#region Textures dictionary

			private static Dictionary<Type, Image> texturesDict = new Dictionary<Type, Image>()
			{
				{
					typeof(AgentLivingIndigo),
					GraphicalUI.Properties.Resources.indigo_suit64
				},			
				{
					typeof(AgentItemFoodFruit),
					GraphicalUI.Properties.Resources.fruit64
				},		
				{
					typeof(AgentManMadeShelterCamp),
					GraphicalUI.Properties.Resources.camp64
				},	
				{
					typeof(AgentItemResLog),
					GraphicalUI.Properties.Resources.log64
				},
				{
					typeof(AgentPuddle),
					GraphicalUI.Properties.Resources.water64
				},
				{
					typeof(AgentTree),
					GraphicalUI.Properties.Resources.tree64_transparent
				},
			}; 

		#endregion

		#region Agent's textures levels dictionary

			//Dictionary for draw agents considering draw levels 0 - toppest level
			private static Dictionary<Type, int> texturesLevelsDict = new Dictionary<Type, int>()
			{
				{
					typeof(AgentLivingIndigo),
					0
				},			
				{
					typeof(AgentItemFoodFruit),
					1
				},		
				{
					typeof(AgentManMadeShelterCamp),
					2
				},	
				{
					typeof(AgentItemResLog),
					1
				},
				{
					typeof(AgentPuddle),
					4
				},
				{
					typeof(AgentTree),
					3
				}
			};
			
		#endregion

		#region Constructors

			public GrapgicalUIForm()
			{
				InitializeComponent();

				//Initialising agents info
				DisplayInfoAgents = new List<Agent>();

				//Setting tag for main menu adding agent element to avoid future exceptions. (Agents always adding to point (0, 0))
				this.MainMenuModelControlAddAgent.Tag = new Point();

				//Setting coordinates center to the panel center
				shiftVector = new Point(- mapPanel.Width / 2, - mapPanel.Height / 2);

				//Model tick linking
				GraphicalUIShell.Model.ModelTick += new EventHandler(onModelTick);

				//Setting double buffering
				SetDoubleBuffered(this.mapPanel);
				SetDoubleBuffered(this.mapInfoPanel);
			
				// Initialize the Label and TextBox controls.
				Label label = new Label();
				label.Location = new Point(1, 1);
				label.Text = "";
				label.AutoSize = true;
				mapInfoPanel.Controls.Add(label);
			}

		#endregion

		#region Form events

			private void GrapgicalUIForm_FormClosing(object sender, FormClosingEventArgs e)
			{		
				GraphicalUIShell.Model.Stop();
			}

			private void GrapgicalUIForm_Paint(object sender, PaintEventArgs e)
			{
				mapPanel.Refresh();
				mapInfoPanel.Refresh();
			}

		#endregion

		#region Menu events

			private void MainMenuFileClose_Click(object sender, EventArgs e)
			{
				this.Close();
			}

			/// <summary>
			/// Event for adding new agent into the model. Used by MainMenu and context agents selection menu
			/// </summary>
			private void MainMenuModelControlAddAgent_Click(object sender, EventArgs e)
			{		
				contextMenuSelectAgents.Close(); 

				GraphicalUIShell.Model.Pause();
				
				var newAddingForm = new FormAgentAdd();
				newAddingForm.ShowDialog(this);
				
				if(newAddingForm.AgentToAdd != null)
				{
					var convertedSenderToPoint = (Point)((sender as ToolStripMenuItem).Tag);
					newAddingForm.AgentToAdd.CurrentLocation = new Location(convertedSenderToPoint.X, convertedSenderToPoint.Y);

					lock(GraphicalUIShell.Model.Agents)
					{
						GraphicalUIShell.Model.AddAgent(newAddingForm.AgentToAdd);
					}
				}

				GraphicalUIShell.Model.Resume();

				this.Refresh();
			}

		#endregion

		#region Model tick events
			
			/// <summary>
			/// The model tick event itself
			/// </summary>
			private void onModelTick(object sender, EventArgs e)	
			{            
				CrossthreadRefreshMapInfoPanel();
				CrossthreadRefreshMapPanel();            
			}

			/// <summary>
			/// Crossthread map refreshing
			/// </summary>
			private void CrossthreadRefreshMapPanel()
			{
				if (mapPanel.InvokeRequired)
				{
					mapPanel.Invoke(new MethodInvoker(CrossthreadRefreshMapPanel));
					return;
				}

				mapPanel.Refresh();
			}
			
			/// <summary>
			/// Crossthread info refreshing
			/// </summary>
			private void CrossthreadRefreshMapInfoPanel()
			{
				if (mapInfoPanel.InvokeRequired)
				{
					mapInfoPanel.Invoke(new MethodInvoker(CrossthreadRefreshMapInfoPanel));
					return;
				} 

				mapInfoPanel.Refresh();  
			}

		#endregion

		#region Coordinates recalculating

			/// <summary>
			/// Calculates the UI coords from the model coords
			/// </summary>
			/// <param name="modelCoord">Coordinates in the hole world (basic point is the world center)</param>
			/// <returns>Coordinates in the panel (basic point is the left top corner)</returns>
			private Point GetUICoord(Point modelCoord)
			{
				return new Point(modelCoord.X * (textureSize + zoomModifyer) - shiftVector.X,
								-modelCoord.Y * (textureSize + zoomModifyer) - shiftVector.Y);
			}

			/// <summary>
			/// Calculates the model coordinates from UI coordinates
			/// </summary>
			/// <param name="UICoord">Coordinates in the panel (basic point is the left top corner)</param>
			/// <returns>Coordinates in the hole world (basic point is the world center)</returns>
			private Point GetModelCoord(Point UICoord)
			{
				double y = -(double)(UICoord.Y + shiftVector.Y) / (double)(textureSize + zoomModifyer);
				double x =  (double)(UICoord.X + shiftVector.X) / (double)(textureSize + zoomModifyer);
				x = x < 0 ? x-1 : x;
				y = y < 0 ? y : y+1;
				return new Point((int)x, (int)y);
			}

		#endregion

		#region Panels refreshing/redrawing

			/// <summary>
			/// Event for refreshing the map panel
			/// </summary>
			private void mapPanel_Paint(object sender, PaintEventArgs e)
			{				
				labelModelTick.Text = "ModelTick = " + GraphicalUIShell.Model.ModelIterationTick.ToString();
				
				drawMap(e);
			}

			/// <summary>
			/// Event for refreshing the info panel
			/// </summary>
			private void mapInfoPanel_Paint(object sender, PaintEventArgs e)
			{   
				mapInfoPanel.Controls[0].Text = "";
				foreach (var ag in DisplayInfoAgents)
				{
					if(GraphicalUIShell.Model.Agents.Contains(ag))
					{
						mapInfoPanel.Controls[0].Text += ag.ToString() + "\n\n";
					}
					else
					{
						DisplayInfoAgents.Remove(ag);
						mapInfoPanel.Refresh();
						mapPanel.Refresh();
						break;             //Get out of here! Collection was modifiyed!
					}
				}   
			}

			/// <summary>
			/// Draws the map in the given region
			/// </summary>
			private void drawMap(PaintEventArgs e)
			{
				//Current texture size to draw (considering zoom)
				int currentTextureSize = textureSize + zoomModifyer;
				//Image to draw
				Image drawedImage = GraphicalUI.Properties.Resources.grass64;
				//Point to draw in
				Point drawBegin = new Point(-currentTextureSize, -currentTextureSize);
				Point drawEnd = new Point(mapPanel.Width + currentTextureSize, mapPanel.Height + currentTextureSize);

				//Draws the background
				for (int i = drawBegin.X; i < drawEnd.X; i += currentTextureSize)
				{
					for (int j = drawBegin.Y; j < drawEnd.Y; j += currentTextureSize)
					{
						e.Graphics.DrawImage(drawedImage, i - shiftVector.X % currentTextureSize, j - shiftVector.Y % currentTextureSize, currentTextureSize, currentTextureSize);
					}
				}
				
				lock(GraphicalUIShell.Model.Agents) //Locking Agents for other threads
				{
					//Using draw levels
					var sortedAgents = GraphicalUIShell.Model.Agents.ToList();
					sortedAgents.Sort(new Comparison<Agent>(GrapgicalUIForm.ComparingByDrawLevels));

					//Draws agents
					foreach (var agent in sortedAgents)
					{
						if (!agent.CurrentLocation.HasOwner)
						{
							texturesDict.TryGetValue(agent.GetType(), out drawedImage);
							if(drawedImage == null)
							{
								logger.Error("Failed to find texture for {0}", agent.GetType());
							}
							if (agent.GetType() == typeof(AgentTree) && agent.Inventory.ExistsAgentByType(typeof(AgentItemFoodFruit)))
							{
								drawedImage = GraphicalUI.Properties.Resources.fruit_tree64_transparent;
							}
							var agentUICoord = GetUICoord(agent.CurrentLocation.Coords);

							if ((agentUICoord.X > drawBegin.X) && (agentUICoord.X < drawEnd.X) &&
								(agentUICoord.Y > drawBegin.Y) && (agentUICoord.Y < drawEnd.Y))
							{
								e.Graphics.DrawImage(drawedImage, agentUICoord.X, agentUICoord.Y, currentTextureSize, currentTextureSize);
							}
						}
					}
				}
				foreach (var agent in DisplayInfoAgents)
				{
					e.Graphics.DrawRectangle(new Pen(Brushes.DarkGreen), new Rectangle(GetUICoord(agent.CurrentLocation.Coords), new Size(currentTextureSize, currentTextureSize)));
				}
			}

		#endregion		

		#region Wheel event for zoom

			private void mapPanel_MouseWheel(object sender, MouseEventArgs e)
			{
				if (e.Delta > 0)
				{
					zoomModifyer += GraphicalUI.Properties.Resources.grass64.Width / 4;
				}
				if (e.Delta < 0 && zoomModifyer >= -GraphicalUI.Properties.Resources.grass64.Width / 2)
				{
					zoomModifyer -= GraphicalUI.Properties.Resources.grass64.Width / 4;
				}
				mapPanel.Refresh();
			}
			
			/// <summary>
			/// Shaytan for zoom to work
			/// </summary>
			private void mapPanel_MouseEnter(object sender, EventArgs e)
			{	
				mapPanel.Select();
			}

		#endregion

		#region Main mouse events

			/// <summary>
			/// Used only for dragging
			/// </summary>
			private void mapPanel_MouseDown(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					mouseDownPoint.X = e.X + shiftVector.X;
					mouseDownPoint.Y = e.Y + shiftVector.Y;
					leftMouseButtonInMapIsPressed = true;
				}
			}

			/// <summary>
			/// Used for ending dragging and for showing agent info in the agent panel
			/// </summary>
			private void mapPanel_MouseUp(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					contextMenuSelectAgents.Close();
					leftMouseButtonInMapIsPressed = false;
				}
				if (e.Button == MouseButtons.Right)
				{					
					ShowContextMenuFor(e.Location);
				}
			}

			/// <summary>
			/// Used for dragging and for updating mouse coords in the map corner
			/// </summary>
			private void mapPanel_MouseMove(object sender, MouseEventArgs e)
			{			
				labelUICoords.Text = "UI coords: " + e.Location.ToString();
				labelModelCoords.Text = "Model coords: " + GetModelCoord(e.Location).ToString();

				if (leftMouseButtonInMapIsPressed)
				{
					shiftVector.X = mouseDownPoint.X - e.X;
					shiftVector.Y = mouseDownPoint.Y - e.Y;
					mapPanel.Refresh();
				}
			}

			/// <summary>
			/// Used for showing agent edit form
			/// </summary>
			private void mapPanel_MouseDoubleClick(object sender, MouseEventArgs e)
			{				
				var currentAgentsInTheCell = GraphicalUIShell.Model.GetAgentsAt(GetModelCoord(e.Location)); //Agents that are in the cell with mouse coords
				if(currentAgentsInTheCell.Count == 1)
				{
					var newEditForm = new FormAgentEdit(currentAgentsInTheCell[0]);
					newEditForm.Show(this);

					newEditForm.FormClosed += new FormClosedEventHandler( (obj, args) => 
					{
						this.Refresh();
					});
				}
				if(currentAgentsInTheCell.Count > 1)
				{
					contextMenuChooseEditAgents.Items.Clear();

					//Adding new context menu elements to choose between several agents
					foreach (var ag in currentAgentsInTheCell)
					{
						var newMenuItemToAdd = new ToolStripMenuItem(ag.Name, null, new EventHandler(contextMenuEditAgentElement_Click));  
						newMenuItemToAdd.Tag = ag;
						contextMenuChooseEditAgents.Items.Add(newMenuItemToAdd);
					}
					contextMenuChooseEditAgents.Show(Cursor.Position);
				}
			}

		#endregion

		#region Model controls events

			private void modelStartButton_Click(object sender, EventArgs e)
			{
				if (GraphicalUIShell.Model.State == ModelState.Initialised)
				{                
					modelStartButton.Text = "Stop!";
					modelPauseButton.Text = "Pause";
					modelPauseButton.Enabled = true;
					GraphicalUIShell.Model.Start();
				}
				else if (GraphicalUIShell.Model.State == ModelState.Running || GraphicalUIShell.Model.State == ModelState.Paused || GraphicalUIShell.Model.State == ModelState.Error)
				{                
					modelStartButton.Text = "Initialize";
					modelPauseButton.Enabled = false;
					GraphicalUIShell.Model.Stop();
				}
				else if (GraphicalUIShell.Model.State == ModelState.Uninitialised)
				{                
					modelStartButton.Text = "Start";
					modelPauseButton.Enabled = false;
					GraphicalUIShell.Model.Initialise();
				}
			}

			private void modelPauseButton_Click(object sender, EventArgs e)
			{
				if (GraphicalUIShell.Model.State == ModelState.Running)
				{
					GraphicalUIShell.Model.Pause();
					modelPauseButton.Text = "Resume";
				}
				else if (GraphicalUIShell.Model.State == ModelState.Paused)
				{
					GraphicalUIShell.Model.Resume();
					modelPauseButton.Text = "Pause";
				}
			}     
			
			private void trackBarModelTick_Scroll(object sender, EventArgs e)
			{
				GraphicalUIShell.Model.ModelIterationTick = TimeSpan.FromMilliseconds(trackBarModelTick.Value * 40);
				labelModelTick.Text = "ModelTick = " + GraphicalUIShell.Model.ModelIterationTick.ToString();
			}

		#endregion

		#region Agents info controls

			private void clearInfobutton_Click(object sender, EventArgs e)
			{
				DisplayInfoAgents.Clear();
				mapInfoPanel.Refresh();
				mapPanel.Refresh();
			}

			/// <summary>
			/// Showing the avaliable options for adding agents and checking thir info
			/// </summary>
			/// <param name="argCoords">UI coords to find agents</param>
			private void ShowContextMenuFor(Point argCoords)
			{
				var currentAgentsInTheCell = GraphicalUIShell.Model.GetAgentsAt(GetModelCoord(argCoords)); //Agents that are in the cell with argCoords
				contextMenuSelectAgents.Items.Clear();
				
				//Adding new context menu elements to choose between several agents
				foreach (var ag in currentAgentsInTheCell)
				{
					var newMenuItemToAdd = new ToolStripMenuItem(ag.Name, null, new EventHandler(contextMenuSelectAgentElement_Click));  

					if (DisplayInfoAgents.Contains(ag))
					{
						newMenuItemToAdd.Checked = true;
					}
					newMenuItemToAdd.Tag = ag;
					contextMenuSelectAgents.Items.Add(newMenuItemToAdd);
				}
				
				//Adding add agent element
				if(currentAgentsInTheCell.Count != 0)
				{
					contextMenuSelectAgents.Items.Add("-");
				}
				var newMenuItemAddingAgent = new ToolStripMenuItem("Add agent", null, new EventHandler(MainMenuModelControlAddAgent_Click)); //Using the same event as in the menu
				newMenuItemAddingAgent.Tag = GetModelCoord(argCoords);
				contextMenuSelectAgents.Items.Add(newMenuItemAddingAgent);

				//Adding close element
				contextMenuSelectAgents.Items.Add("-");
				contextMenuSelectAgents.Items.Add(new ToolStripMenuItem("Ok", null, new EventHandler(contextMenuSelectAgentCloseElement_Click)));

				contextMenuSelectAgents.Show(Cursor.Position);
			}

			/// <summary>
			/// Event for click on context menu item to choose between agents
			/// </summary>
			private void contextMenuSelectAgentElement_Click(object sender, EventArgs e)
			{
				var convertedSender = sender as ToolStripMenuItem; //Sender menu item
				
				convertedSender.Checked = !convertedSender.Checked;
			}

			/// <summary>
			/// Event for click on context menu close button 
			/// </summary>
			private void contextMenuSelectAgentCloseElement_Click(object sender, EventArgs e)
			{
				for(int i = 0; i < contextMenuSelectAgents.Items.Count; i++)
				{			
					if(contextMenuSelectAgents.Items[i] as ToolStripMenuItem == null) //This is separator element, agents ending here
					{
						break;
					}
					
					var agToOperate = (contextMenuSelectAgents.Items[i] as ToolStripMenuItem).Tag as Agent;
							
					if (DisplayInfoAgents.Contains(agToOperate))
					{
						if(!(contextMenuSelectAgents.Items[i] as ToolStripMenuItem).Checked)
						{
							DisplayInfoAgents.Remove(agToOperate);
						}
					}
					else
					{
						if((contextMenuSelectAgents.Items[i] as ToolStripMenuItem).Checked)
						{
							DisplayInfoAgents.Add(agToOperate);
						}
					}
				}

				contextMenuSelectAgents.Close();
				this.Refresh();
			}

		#endregion

		#region Agents editing context menu event

			private void contextMenuEditAgentElement_Click(object sender, EventArgs e)
			{				
				var newEditForm = new FormAgentEdit((sender as ToolStripMenuItem).Tag as Agent);
				newEditForm.Show(this);

				newEditForm.FormClosed += new FormClosedEventHandler( (obj, args) => 
				{
					this.Refresh();
				});
			}

		#endregion

    }
}
