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

namespace GraphicalUI
{
    public partial class GrapgicalUIForm : Form
    {
        //shift from basic point
        Point shiftPoint = new Point();
        Point mouseDownPoint = new Point(0, 0);
        bool leftMouseButtonInMapIsPressed = false;

        public GrapgicalUIForm()
        {
            InitializeComponent();
        }

        private void GrapgicalUIForm_Load(object sender, EventArgs e)
        {

            shiftPoint = new Point(- mapPanel.Width / 2, - mapPanel.Height / 2);

            IObservableModel model = new Model();

            GraphicalUIShell.Model = model;
        }

        private void onDrawTimerTick(object sender, EventArgs e)
        {
            //It should be replaced with the model tick eventhandler
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
            int textureSize = GraphicalUI.Properties.Resources.grass64.Width;

            //Testing part (to be deleted later)
            Pen tPen = new Pen(Color.Red, 5);

            for (int i = -textureSize; i < mapHeight; i += textureSize)
            {
                for (int j = -textureSize; j < mapWidth; j += textureSize)
                {
                    e.Graphics.DrawImage(drawedImage, j - shiftPoint.X % textureSize, i - shiftPoint.Y % textureSize, textureSize, textureSize);
                    tPen.Color = Color.Red;
                    e.Graphics.DrawEllipse(tPen, j - shiftPoint.X % textureSize, i - shiftPoint.Y % textureSize, 5, 5);
                    tPen.Color = Color.Blue;
                    e.Graphics.DrawEllipse(tPen,- shiftPoint.X,- shiftPoint.Y, 5, 5);
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
    }
}
