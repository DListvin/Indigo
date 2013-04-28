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
	public partial class AgentEditForm : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();	

		private Agent agentToEdit; //Agent to be presented and edited in this form

		public AgentEditForm(Agent argAgentToEdit)
		{
			InitializeComponent();

			agentToEdit = argAgentToEdit;

			this.labelAgentGeneralInfo.Text = agentToEdit.GetType().ToString().Split('.').Last() + " " + agentToEdit.ToString();
			this.textBoxAgentName.Text = agentToEdit.Name;

			//Adding characteristics track bars

			var totalTracksAdded = 0;   //Count how many tracks are added to calculate their coordinates
			var trackBarHeight = 45;    //Track bar height
			var trackBarWidth = 300;    //Track bar width
			var labelWidth = 100;       //Label with characteristic name width
			var textBoxMinMaxWidth = 30;//Width of the text box with minimum and maximum

			var newX = 5; //X coordinate of the controls

			foreach(var ch in agentToEdit.CurrentState)
			{
				var newY = 5 + totalTracksAdded * trackBarHeight; //Y coordinate of the controls

				var newLabel = new Label();
				newLabel.Name = "LabelFor" + ch.Name;
				newLabel.Text = ch.Name;
				newLabel.Location = new Point(newX, newY);
				newLabel.Size = new Size(labelWidth, trackBarHeight);
				this.panelWithCharacteristics.Controls.Add(newLabel);

				var newTextBoxCharacteristicMinimum = new TextBox();
				newTextBoxCharacteristicMinimum.Name = "TextBoxMinimumFor" + ch.Name;
				newTextBoxCharacteristicMinimum.Text = ch.MinValue.ToString();
				newTextBoxCharacteristicMinimum.Size = new Size(textBoxMinMaxWidth, newTextBoxCharacteristicMinimum.Size.Height);
				newTextBoxCharacteristicMinimum.Location = new Point(newX + labelWidth, newY);
				this.panelWithCharacteristics.Controls.Add(newTextBoxCharacteristicMinimum);

				var newTrackBar = new TrackBar();
				newTrackBar.Name = "TrackBarFor" + ch.Name;
				newTrackBar.Minimum = 0;
				newTrackBar.Maximum = 100;
				newTrackBar.Size = new Size(trackBarWidth, trackBarHeight);
				newTrackBar.Location = new Point(newX + labelWidth + textBoxMinMaxWidth, newY);
				newTrackBar.Value = ch.CurrentPercentValue;
				this.panelWithCharacteristics.Controls.Add(newTrackBar);

				var newTextBoxCharacteristicMaximum = new TextBox();
				newTextBoxCharacteristicMaximum.Name = "TextBoxMaximumFor" + ch.Name;
				newTextBoxCharacteristicMaximum.Text = ch.MaxValue.ToString();
				newTextBoxCharacteristicMaximum.Size = new Size(textBoxMinMaxWidth, newTextBoxCharacteristicMaximum.Size.Height);
				newTextBoxCharacteristicMaximum.Location = new Point(newX + labelWidth + textBoxMinMaxWidth + trackBarWidth, newY);
				this.panelWithCharacteristics.Controls.Add(newTextBoxCharacteristicMaximum);

				totalTracksAdded++;
			}
		}

		#region Form closing buttons

			private void buttonSaveAndClose_Click(object sender, EventArgs e)
			{	
				//Saving name
				agentToEdit.Name = this.textBoxAgentName.Text;
				
				//Saving characteristics
				foreach(var ch in agentToEdit.CurrentState)
				{
					var foundArrayTrackBarForCurrentCharacteristic = this.panelWithCharacteristics.Controls.Find("TrackBarFor" + ch.Name, false);
					var foundTrackBarForCurrentCharacteristic = foundArrayTrackBarForCurrentCharacteristic[0] as TrackBar;
					ch.CurrentPercentValue = foundTrackBarForCurrentCharacteristic.Value;

					var foundArrayTextMinValueForCurrentCharacteristic = this.panelWithCharacteristics.Controls.Find("TextBoxMinimumFor" + ch.Name, false);
					var foundTextMinValueForCurrentCharacteristic = foundArrayTextMinValueForCurrentCharacteristic[0] as TextBox;
					ch.MinValue = int.Parse(foundTextMinValueForCurrentCharacteristic.Text);

					var foundArrayTextMaxValueForCurrentCharacteristic = this.panelWithCharacteristics.Controls.Find("TextBoxMaximumFor" + ch.Name, false);
					var foundTextMaxValueForCurrentCharacteristic = foundArrayTextMaxValueForCurrentCharacteristic[0] as TextBox;
					ch.MaxValue = int.Parse(foundTextMaxValueForCurrentCharacteristic.Text);
				}

				this.Close();
			}			

			private void buttonCloseNotSave_Click(object sender, EventArgs e)
			{			
				this.Close();
			}

			private void buttonDeleteAndClose_Click(object sender, EventArgs e)
			{
				lock(GraphicalUIShell.Model.Agents)
				{
					GraphicalUIShell.Model.DeleteAgent(agentToEdit);
				}
							
				this.Close();
			}

		#endregion

	}
}
