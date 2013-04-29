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
	public partial class FormAgentAdd : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();		

		public Agent AgentToAdd { get; set; }  //Agent to add in the model

		public FormAgentAdd()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Cancel button click
		/// </summary>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			AgentToAdd = null;
			this.Close();
		}

		private void buttonAddIndigo_Click(object sender, EventArgs e)
		{
			AgentToAdd = new AgentLivingIndigo();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalIndigo_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}

		private void buttonAddTree_Click(object sender, EventArgs e)
		{		
			AgentToAdd = new AgentTree();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalTree_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}

		private void buttonAddPuddle_Click(object sender, EventArgs e)
		{		
			AgentToAdd = new AgentPuddle();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalPuddle_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}

		private void buttonAddLog_Click(object sender, EventArgs e)
		{			
			AgentToAdd = new AgentItemResLog();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalLog_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}

		private void buttonAddFruit_Click(object sender, EventArgs e)
		{
			AgentToAdd = new AgentItemFoodFruit();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalFruit_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}

		private void buttonAddCamp_Click(object sender, EventArgs e)
		{
			AgentToAdd = new AgentManMadeShelterCamp();
			AgentToAdd.Name = textBoxAgentName.Text.Length > 0 ? textBoxAgentName.Text : "ExperimentalCamp_" + DateTime.Now.Ticks.ToString();
			this.Close();
		}
	}
}
