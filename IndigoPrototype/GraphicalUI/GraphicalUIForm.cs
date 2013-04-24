using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        //shift from basic point
        Point shiftPoint = new Point();
        Point mouseDownPoint = new Point(0, 0);
        bool leftMouseButtonInMapIsPressed = false;
        int zoomModifyer = 0;
        int textureSize;//Side of the texture
        List<Agent> displayInfoAgents;

        Dictionary<Type, Image> texturesDict = new Dictionary<Type, Image>();

        public GrapgicalUIForm()
        {
            InitializeComponent();
            Label label = new Label();
            // Initialize the Label and TextBox controls.
            label.Location = new Point(1, 1);
            label.Text = "";
            label.AutoSize = true;
            mapInfoPanel.Controls.Add(label);
        }

        private void GrapgicalUIForm_Load(object sender, EventArgs e)
        {
            shiftPoint = new Point(- mapPanel.Width / 2, - mapPanel.Height / 2);

            GraphicalUIShell.Model.ModelTick += new EventHandler(onModelTick);
            mapPanel.MouseWheel += new MouseEventHandler(mapPanel_MouseWheel);

			texturesDict.Add(typeof(AgentLivingIndigo), GraphicalUI.Properties.Resources.indigo_suit64);
			texturesDict.Add(typeof(AgentItemFoodFruit), GraphicalUI.Properties.Resources.fruit64);
			texturesDict.Add(typeof(AgentManMadeShelterCamp), GraphicalUI.Properties.Resources.camp64);
			texturesDict.Add(typeof(AgentItemResLog), GraphicalUI.Properties.Resources.log64);			
			texturesDict.Add(typeof(AgentPuddle), GraphicalUI.Properties.Resources.water64);		
			texturesDict.Add(typeof(AgentTree), GraphicalUI.Properties.Resources.tree64);

            displayInfoAgents = new List<Agent>();

            textureSize = GraphicalUI.Properties.Resources.grass64.Width;
        }

        private void onModelTick(object sender, EventArgs e)	
        {            
            UpdateMapInfoPanel();
            CrossthreadRefreshMapPanel();            
        }

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
        /// Draws the map in the mapPanel
        /// </summary>
        /// <param name="e"></param>
        private void drawMap(PaintEventArgs e)
        {
            labelModelTick.Text = "ModelTick = " + GraphicalUIShell.Model.ModelIterationTick.ToString();
            //Width of the panel
            int mapWidth = mapPanel.Width;
            //Height of the panel
            int mapHeight = mapPanel.Height;
            //Image to draw
            Image drawedImage = GraphicalUI.Properties.Resources.grass64;
            //Point to draw in
            Point drawPoint = new Point(0, 0);
            Point drawBegin = new Point(-textureSize - zoomModifyer, -textureSize - zoomModifyer);
            Point drawEnd = new Point(mapHeight + (textureSize + zoomModifyer), mapWidth + (textureSize + zoomModifyer));
            //Points are testing and temporary
            //Draws the background
            for (int i = drawBegin.X; i < drawEnd.X; i += (textureSize + zoomModifyer))
            {
                for (int j = drawBegin.Y; j < drawEnd.Y; j += (textureSize + zoomModifyer))
                {
                    e.Graphics.DrawImage(drawedImage, j - shiftPoint.X % (textureSize + zoomModifyer), i - shiftPoint.Y % (textureSize + zoomModifyer), (textureSize + zoomModifyer), (textureSize + zoomModifyer));
                }
            }

			lock(GraphicalUIShell.Model.Agents) //Locking Agents for other threads
			{
				//Draws agents
				foreach (Agent agent in GraphicalUIShell.Model.Agents)
				{
					if (!agent.CurrentLocation.HasOwner)
					{
						texturesDict.TryGetValue(agent.GetType(), out drawedImage);
						if (agent.GetType() == typeof(AgentTree) && agent.Inventory.ExistsAgentByType(typeof(AgentItemFoodFruit)))
						{
							drawedImage = GraphicalUI.Properties.Resources.fruit_tree64;
						}
                        Point agentUICoord = GetUICoord(agent.CurrentLocation);
                        if ((agentUICoord.X > -(textureSize + zoomModifyer)) && (agentUICoord.X < mapWidth + (textureSize + zoomModifyer)) &&
                            (agentUICoord.Y > -(textureSize + zoomModifyer)) && (agentUICoord.Y < mapHeight + (textureSize + zoomModifyer)))
						{
                            e.Graphics.DrawImage(drawedImage, agentUICoord.X, agentUICoord.Y, (textureSize + zoomModifyer), (textureSize + zoomModifyer));
						}
					}
				}
			}
            foreach (Agent agent in displayInfoAgents)
            {
                e.Graphics.DrawRectangle(new Pen(Brushes.DarkGreen), new Rectangle(GetUICoord(agent.CurrentLocation), new Size((textureSize + zoomModifyer), (textureSize + zoomModifyer))));
            }
        }

        private Point GetUICoord(Location modelCoord)
        {
            return new Point(modelCoord.Coords.X * (textureSize + zoomModifyer) - shiftPoint.X,
                            -modelCoord.Coords.Y * (textureSize + zoomModifyer) - shiftPoint.Y);
        }
        private Location GetModelCoord(Point UICoord)
        {
            double y = -(double)(UICoord.Y + shiftPoint.Y) / (double)(textureSize + zoomModifyer);
            double x =  (double)(UICoord.X + shiftPoint.X) / (double)(textureSize + zoomModifyer);
            x = x < 0 ? x-1 : x;
            y = y < 0 ? y : y+1;
            return new Location((int)x, (int)y);
        }


        /// <summary>
        /// Paints the mapPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            drawMap(e);
        }

        private void mapPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = e.X + shiftPoint.X;
                mouseDownPoint.Y = e.Y + shiftPoint.Y;
                leftMouseButtonInMapIsPressed = true;
            }
        }

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

        private void mapPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftMouseButtonInMapIsPressed = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                CalcDisplayInfoAgents(new Point(e.X, e.Y));
                UpdateMapInfoPanel();
                mapPanel.Refresh();
            }
        }

        private void CalcDisplayInfoAgents(Point coord)
        {
            List<Agent> ans = new List<Agent>();
            Location loc = GetModelCoord(coord);
            foreach (Agent ag in GraphicalUIShell.Model.Agents)
            {
                if (!ag.CurrentLocation.HasOwner)
                {     
                    if (ag.CurrentLocation.Coords.X == loc.Coords.X && ag.CurrentLocation.Coords.Y == loc.Coords.Y)
                    {
                        if (displayInfoAgents.Contains(ag))
                        {
                            displayInfoAgents.Remove(ag);
                        }
                        else
                        {
                            displayInfoAgents.Add(ag);
                        }
                    }
                }
            }
        }

        private void UpdateMapInfoPanel()
        {
            if (mapInfoPanel.InvokeRequired)
            {
                mapInfoPanel.Invoke(new MethodInvoker(UpdateMapInfoPanel));
                return;
            }            
            mapInfoPanel.Controls[0].Text = "";
            foreach (Agent ag in displayInfoAgents)
            {
                mapInfoPanel.Controls[0].Text += ag.ToString() + "\n\n";
            }          

        }

        private void mapPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMouseButtonInMapIsPressed)
            {
                shiftPoint.X = mouseDownPoint.X - e.X;
                shiftPoint.Y = mouseDownPoint.Y - e.Y;
                mapPanel.Refresh();
            }
        }

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

		private void GrapgicalUIForm_FormClosing(object sender, FormClosingEventArgs e)
		{		
            GraphicalUIShell.Model.Stop();
		}

        private void mapPanel_MouseEnter(object sender, EventArgs e)
        {
            mapPanel.Select();
        }

        private void mapPanel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void mapPanel_Click(object sender, EventArgs e)
        {

        }

        private void clearInfobutton_Click(object sender, EventArgs e)
        {
            displayInfoAgents.Clear();
        }

        private void trackBarModelTick_Scroll(object sender, EventArgs e)
        {
            GraphicalUIShell.Model.ModelIterationTick = TimeSpan.FromMilliseconds(trackBarModelTick.Value * 40);
            labelModelTick.Text = "ModelTick = " + GraphicalUIShell.Model.ModelIterationTick.ToString();
        }
    }
}
