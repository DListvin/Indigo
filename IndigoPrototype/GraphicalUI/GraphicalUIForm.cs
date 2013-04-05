using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphicalUI
{
    public partial class GrapgicalUIForm : Form
    {
        public GrapgicalUIForm()
        {
            InitializeComponent();
        }

        private void GrapgicalUIForm_Load(object sender, EventArgs e)
        {

        }

        private void onDrawTimerTick(object sender, EventArgs e)
        {
            //It should be replaced with the model tick eventhandler
        }
    }
}
