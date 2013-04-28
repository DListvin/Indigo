namespace GraphicalUI
{
	partial class AgentEditForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelAgentGeneralInfo = new System.Windows.Forms.Label();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.buttonCloseNotSave = new System.Windows.Forms.Button();
			this.buttonDeleteAndClose = new System.Windows.Forms.Button();
			this.textBoxAgentName = new System.Windows.Forms.TextBox();
			this.panelWithCharacteristics = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// labelAgentGeneralInfo
			// 
			this.labelAgentGeneralInfo.AutoSize = true;
			this.labelAgentGeneralInfo.Location = new System.Drawing.Point(13, 13);
			this.labelAgentGeneralInfo.Name = "labelAgentGeneralInfo";
			this.labelAgentGeneralInfo.Size = new System.Drawing.Size(66, 13);
			this.labelAgentGeneralInfo.TabIndex = 0;
			this.labelAgentGeneralInfo.Text = "<agent info>";
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSaveAndClose.Location = new System.Drawing.Point(16, 462);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(115, 23);
			this.buttonSaveAndClose.TabIndex = 1;
			this.buttonSaveAndClose.Text = "Save and close";
			this.buttonSaveAndClose.UseVisualStyleBackColor = true;
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// buttonCloseNotSave
			// 
			this.buttonCloseNotSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonCloseNotSave.Location = new System.Drawing.Point(137, 462);
			this.buttonCloseNotSave.Name = "buttonCloseNotSave";
			this.buttonCloseNotSave.Size = new System.Drawing.Size(126, 23);
			this.buttonCloseNotSave.TabIndex = 1;
			this.buttonCloseNotSave.Text = "Close without saving";
			this.buttonCloseNotSave.UseVisualStyleBackColor = true;
			this.buttonCloseNotSave.Click += new System.EventHandler(this.buttonCloseNotSave_Click);
			// 
			// buttonDeleteAndClose
			// 
			this.buttonDeleteAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDeleteAndClose.Location = new System.Drawing.Point(269, 462);
			this.buttonDeleteAndClose.Name = "buttonDeleteAndClose";
			this.buttonDeleteAndClose.Size = new System.Drawing.Size(108, 23);
			this.buttonDeleteAndClose.TabIndex = 1;
			this.buttonDeleteAndClose.Text = "Delete this agent";
			this.buttonDeleteAndClose.UseVisualStyleBackColor = true;
			this.buttonDeleteAndClose.Click += new System.EventHandler(this.buttonDeleteAndClose_Click);
			// 
			// textBoxAgentName
			// 
			this.textBoxAgentName.Location = new System.Drawing.Point(16, 176);
			this.textBoxAgentName.Name = "textBoxAgentName";
			this.textBoxAgentName.Size = new System.Drawing.Size(145, 20);
			this.textBoxAgentName.TabIndex = 2;
			// 
			// panelWithCharacteristics
			// 
			this.panelWithCharacteristics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelWithCharacteristics.BackColor = System.Drawing.Color.White;
			this.panelWithCharacteristics.Location = new System.Drawing.Point(16, 203);
			this.panelWithCharacteristics.Name = "panelWithCharacteristics";
			this.panelWithCharacteristics.Size = new System.Drawing.Size(503, 253);
			this.panelWithCharacteristics.TabIndex = 3;
			// 
			// AgentEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(531, 497);
			this.Controls.Add(this.panelWithCharacteristics);
			this.Controls.Add(this.textBoxAgentName);
			this.Controls.Add(this.buttonDeleteAndClose);
			this.Controls.Add(this.buttonCloseNotSave);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.labelAgentGeneralInfo);
			this.MinimumSize = new System.Drawing.Size(547, 535);
			this.Name = "AgentEditForm";
			this.Text = "AgentInfoForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelAgentGeneralInfo;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private System.Windows.Forms.Button buttonCloseNotSave;
		private System.Windows.Forms.Button buttonDeleteAndClose;
		private System.Windows.Forms.TextBox textBoxAgentName;
		private System.Windows.Forms.Panel panelWithCharacteristics;

	}
}