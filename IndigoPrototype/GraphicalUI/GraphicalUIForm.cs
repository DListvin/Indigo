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

        Dictionary<Type, Image> texturesDict = new Dictionary<Type, Image>();

        public GrapgicalUIForm()
        {
            InitializeComponent();
        }

        private void GrapgicalUIForm_Load(object sender, EventArgs e)
        {
            shiftPoint = new Point(- mapPanel.Width / 2, - mapPanel.Height / 2);

            GraphicalUIShell.Model.ModelTick += new EventHandler(onModelTick);
            mapPanel.MouseWheel += new MouseEventHandler(mapPanel_MouseWheel);

			texturesDict.Add(typeof(AgentLivingIndigo), GraphicalUI.Properties.Resources.indigo_suit64);
			texturesDict.Add(typeof(AgentItemFruit), GraphicalUI.Properties.Resources.fruit64);
			texturesDict.Add(typeof(AgentCamp), GraphicalUI.Properties.Resources.camp64);
			texturesDict.Add(typeof(AgentItemLog), GraphicalUI.Properties.Resources.log64);			
			texturesDict.Add(typeof(AgentPuddle), GraphicalUI.Properties.Resources.water64);		
			texturesDict.Add(typeof(AgentTree), GraphicalUI.Properties.Resources.tree64);            
        }

        private void onModelTick(object sender, EventArgs e)	
        {
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
            //Width of the panel
            int mapWidth = mapPanel.Width;
            //Height of the panel
            int mapHeight = mapPanel.Height;
            //Image to draw
            Image drawedImage = GraphicalUI.Properties.Resources.grass64;
            //Point to draw in
            Point drawPoint = new Point(0, 0);
            //Side of the texture
            int textureSize = GraphicalUI.Properties.Resources.grass64.Width;

            //Points are testing and temporary
            //Draws the background
            for (int i = -(textureSize + zoomModifyer); i < mapHeight + (textureSize + zoomModifyer); i += (textureSize + zoomModifyer))
            {
                for (int j = -(textureSize + zoomModifyer); j < mapWidth + (textureSize + zoomModifyer); j += (textureSize + zoomModifyer))
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
						if (agent.GetType() == typeof(AgentTree) && agent.Inventory.ExistsAgentByType(typeof(AgentItemFruit)))
						{
							drawedImage = GraphicalUI.Properties.Resources.fruit_tree64;
						}

						if ((agent.CurrentLocation.Coords.X * (textureSize + zoomModifyer) - shiftPoint.X > -(textureSize + zoomModifyer)) && (agent.CurrentLocation.Coords.X * (textureSize + zoomModifyer) - shiftPoint.X < mapWidth + (textureSize + zoomModifyer)) &&
							(-agent.CurrentLocation.Coords.Y * (textureSize + zoomModifyer) - shiftPoint.Y > -(textureSize + zoomModifyer)) && (-agent.CurrentLocation.Coords.Y * (textureSize + zoomModifyer) - shiftPoint.Y < mapHeight + (textureSize + zoomModifyer)))
						{
							e.Graphics.DrawImage(drawedImage, agent.CurrentLocation.Coords.X * (textureSize + zoomModifyer) - shiftPoint.X, -agent.CurrentLocation.Coords.Y * (textureSize + zoomModifyer) - shiftPoint.Y, (textureSize + zoomModifyer), (textureSize + zoomModifyer));
						}
					}
				}
			}
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
    }
}
