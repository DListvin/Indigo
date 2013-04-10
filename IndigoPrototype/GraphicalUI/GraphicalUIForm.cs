﻿using System;
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

			texturesDict.Add(typeof(AgentLivingIndigo), GraphicalUI.Properties.Resources.indigo_suit64);
			texturesDict.Add(typeof(AgentItemFruit), GraphicalUI.Properties.Resources.fruit64);
			texturesDict.Add(typeof(AgentCamp), GraphicalUI.Properties.Resources.camp64);
			texturesDict.Add(typeof(AgentItemLog), GraphicalUI.Properties.Resources.log64);			
			texturesDict.Add(typeof(AgentPuddle), GraphicalUI.Properties.Resources.water64);		
			texturesDict.Add(typeof(AgentTree), GraphicalUI.Properties.Resources.tree64);

            //mapPanel.MouseWheel += new EventHandler(mapPanel_MouseWheel);

            //testing
            drawTimer.Start();
        }

        private void onDrawTimerTick(object sender, EventArgs e)
        {
            //It should be replaced with the model tick eventhandler
            //mapPanel.Refresh();
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

            //Testing part (to be deleted later)
            Pen tPen = new Pen(Color.Red, 5);

            //Points are testing and temporary
            //Draws the background
            for (int i = -(textureSize + zoomModifyer); i < mapHeight + (textureSize + zoomModifyer); i += (textureSize + zoomModifyer))
            {
                for (int j = -(textureSize + zoomModifyer); j < mapWidth + (textureSize + zoomModifyer); j += (textureSize + zoomModifyer))
                {
                    e.Graphics.DrawImage(drawedImage, j - shiftPoint.X % (textureSize + zoomModifyer), i - shiftPoint.Y % (textureSize + zoomModifyer), (textureSize + zoomModifyer), (textureSize + zoomModifyer));
                }
            }

            //Draws agents
            foreach (Agent agent in GraphicalUIShell.Model.Agents)
            {
                if (agent.Location != null)
                {
                    texturesDict.TryGetValue(agent.GetType(), out drawedImage);

                    if ((agent.Location.Value.X * (textureSize + zoomModifyer) - shiftPoint.X > -(textureSize + zoomModifyer)) && (agent.Location.Value.X * (textureSize + zoomModifyer) - shiftPoint.X < mapWidth + (textureSize + zoomModifyer)) &&
                        (-agent.Location.Value.Y * (textureSize + zoomModifyer) - shiftPoint.Y > -(textureSize + zoomModifyer)) && (-agent.Location.Value.Y * (textureSize + zoomModifyer) - shiftPoint.Y < mapHeight + (textureSize + zoomModifyer)))
                    {
                        e.Graphics.DrawImage(drawedImage, agent.Location.Value.X * (textureSize + zoomModifyer) - shiftPoint.X, -agent.Location.Value.Y * (textureSize + zoomModifyer) - shiftPoint.Y, (textureSize + zoomModifyer), (textureSize + zoomModifyer));
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
            GraphicalUIShell.Model.Start();
        }

        private void modelPauseButton_Click(object sender, EventArgs e)
        {
            GraphicalUIShell.Model.Pause();
        }

        private void modelStopButton_Click(object sender, EventArgs e)
        {
            GraphicalUIShell.Model.Stop();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            GraphicalUIShell.Model.Resume();
		}

		private void GrapgicalUIForm_FormClosing(object sender, FormClosingEventArgs e)
		{		
            GraphicalUIShell.Model.Stop();
		}
    }
}
