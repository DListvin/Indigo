using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor
{
	public partial class NewFileWindow : Form
	{
		#region Properties

			/// <summary>
			/// Grid size to create
			/// </summary>
			public int GridSize { get; set; } 

		#endregion

		#region Constructors

			public NewFileWindow()
			{
				InitializeComponent();
			}

		#endregion

		#region Control buttons events

			private void buttonCancel_Click(object sender, EventArgs e)
			{
				GridSize = -1;
				this.Close();
			}

			private void buttonOk_Click(object sender, EventArgs e)
			{				
				GridSize = int.Parse(textBoxChunckSize.Text);
				this.Close();
			}

		#endregion
	}
}
