namespace GraphicalUI
{
    partial class GrapgicalUIForm
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
            if (disposing && (components != null))
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrapgicalUIForm));
			this.modelControlPanel = new System.Windows.Forms.Panel();
			this.labelModelCoords = new System.Windows.Forms.Label();
			this.labelUICoords = new System.Windows.Forms.Label();
			this.labelModelTick = new System.Windows.Forms.Label();
			this.trackBarModelTick = new System.Windows.Forms.TrackBar();
			this.clearInfobutton = new System.Windows.Forms.Button();
			this.modelPauseButton = new System.Windows.Forms.Button();
			this.modelStartButton = new System.Windows.Forms.Button();
			this.mapInfoPanel = new System.Windows.Forms.Panel();
			this.mapPanel = new System.Windows.Forms.Panel();
			this.contextMenuSelectAgents = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuChooseEditAgents = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.modelControlPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarModelTick)).BeginInit();
			this.SuspendLayout();
			// 
			// modelControlPanel
			// 
			this.modelControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.modelControlPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.modelControlPanel.Controls.Add(this.labelModelCoords);
			this.modelControlPanel.Controls.Add(this.labelUICoords);
			this.modelControlPanel.Controls.Add(this.labelModelTick);
			this.modelControlPanel.Controls.Add(this.trackBarModelTick);
			this.modelControlPanel.Controls.Add(this.clearInfobutton);
			this.modelControlPanel.Controls.Add(this.modelPauseButton);
			this.modelControlPanel.Controls.Add(this.modelStartButton);
			this.modelControlPanel.Location = new System.Drawing.Point(13, 469);
			this.modelControlPanel.Name = "modelControlPanel";
			this.modelControlPanel.Size = new System.Drawing.Size(880, 81);
			this.modelControlPanel.TabIndex = 1;
			// 
			// labelModelCoords
			// 
			this.labelModelCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelModelCoords.AutoSize = true;
			this.labelModelCoords.Location = new System.Drawing.Point(259, 32);
			this.labelModelCoords.Name = "labelModelCoords";
			this.labelModelCoords.Size = new System.Drawing.Size(74, 13);
			this.labelModelCoords.TabIndex = 5;
			this.labelModelCoords.Text = "Model coords:";
			// 
			// labelUICoords
			// 
			this.labelUICoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelUICoords.AutoSize = true;
			this.labelUICoords.Location = new System.Drawing.Point(277, 13);
			this.labelUICoords.Name = "labelUICoords";
			this.labelUICoords.Size = new System.Drawing.Size(56, 13);
			this.labelUICoords.TabIndex = 5;
			this.labelUICoords.Text = "UI coords:";
			// 
			// labelModelTick
			// 
			this.labelModelTick.AutoSize = true;
			this.labelModelTick.Location = new System.Drawing.Point(166, 43);
			this.labelModelTick.Name = "labelModelTick";
			this.labelModelTick.Size = new System.Drawing.Size(60, 13);
			this.labelModelTick.TabIndex = 4;
			this.labelModelTick.Text = "ModelTick:";
			// 
			// trackBarModelTick
			// 
			this.trackBarModelTick.Location = new System.Drawing.Point(3, 32);
			this.trackBarModelTick.Maximum = 50;
			this.trackBarModelTick.Minimum = 1;
			this.trackBarModelTick.Name = "trackBarModelTick";
			this.trackBarModelTick.Size = new System.Drawing.Size(157, 45);
			this.trackBarModelTick.TabIndex = 3;
			this.trackBarModelTick.Value = 50;
			this.trackBarModelTick.Scroll += new System.EventHandler(this.trackBarModelTick_Scroll);
			// 
			// clearInfobutton
			// 
			this.clearInfobutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearInfobutton.Location = new System.Drawing.Point(504, 4);
			this.clearInfobutton.Name = "clearInfobutton";
			this.clearInfobutton.Size = new System.Drawing.Size(75, 23);
			this.clearInfobutton.TabIndex = 2;
			this.clearInfobutton.Text = "ClearInfo";
			this.clearInfobutton.UseVisualStyleBackColor = true;
			this.clearInfobutton.Click += new System.EventHandler(this.clearInfobutton_Click);
			// 
			// modelPauseButton
			// 
			this.modelPauseButton.Enabled = false;
			this.modelPauseButton.Location = new System.Drawing.Point(85, 4);
			this.modelPauseButton.Name = "modelPauseButton";
			this.modelPauseButton.Size = new System.Drawing.Size(75, 23);
			this.modelPauseButton.TabIndex = 1;
			this.modelPauseButton.Text = "Pause";
			this.modelPauseButton.UseVisualStyleBackColor = true;
			this.modelPauseButton.Click += new System.EventHandler(this.modelPauseButton_Click);
			// 
			// modelStartButton
			// 
			this.modelStartButton.Location = new System.Drawing.Point(3, 3);
			this.modelStartButton.Name = "modelStartButton";
			this.modelStartButton.Size = new System.Drawing.Size(75, 23);
			this.modelStartButton.TabIndex = 0;
			this.modelStartButton.Text = "Start";
			this.modelStartButton.UseVisualStyleBackColor = true;
			this.modelStartButton.Click += new System.EventHandler(this.modelStartButton_Click);
			// 
			// mapInfoPanel
			// 
			this.mapInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mapInfoPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.mapInfoPanel.Location = new System.Drawing.Point(517, 13);
			this.mapInfoPanel.Name = "mapInfoPanel";
			this.mapInfoPanel.Size = new System.Drawing.Size(376, 450);
			this.mapInfoPanel.TabIndex = 2;
			this.mapInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapInfoPanel_Paint);
			// 
			// mapPanel
			// 
			this.mapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mapPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.mapPanel.Location = new System.Drawing.Point(13, 13);
			this.mapPanel.Name = "mapPanel";
			this.mapPanel.Size = new System.Drawing.Size(498, 450);
			this.mapPanel.TabIndex = 0;
			this.mapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapPanel_Paint);
			this.mapPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDoubleClick);
			this.mapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseDown);
			this.mapPanel.MouseEnter += new System.EventHandler(this.mapPanel_MouseEnter);
			this.mapPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseMove);
			this.mapPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseUp);
			this.mapPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mapPanel_MouseWheel);
			// 
			// contextMenuSelectAgents
			// 
			this.contextMenuSelectAgents.AutoClose = false;
			this.contextMenuSelectAgents.Name = "contextMenuSelectAgents";
			this.contextMenuSelectAgents.Size = new System.Drawing.Size(61, 4);
			// 
			// contextMenuChooseEditAgents
			// 
			this.contextMenuChooseEditAgents.Name = "contextMenuChooseEditAgents";
			this.contextMenuChooseEditAgents.Size = new System.Drawing.Size(61, 4);
			// 
			// GrapgicalUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(905, 562);
			this.Controls.Add(this.mapInfoPanel);
			this.Controls.Add(this.modelControlPanel);
			this.Controls.Add(this.mapPanel);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GrapgicalUIForm";
			this.Text = "Indigo";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GrapgicalUIForm_FormClosing);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GrapgicalUIForm_Paint);
			this.modelControlPanel.ResumeLayout(false);
			this.modelControlPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarModelTick)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Panel modelControlPanel;
        private System.Windows.Forms.Panel mapInfoPanel;
        private System.Windows.Forms.Button modelPauseButton;
        private System.Windows.Forms.Button modelStartButton;
        private System.Windows.Forms.Button clearInfobutton;
        private System.Windows.Forms.Label labelModelTick;
        private System.Windows.Forms.TrackBar trackBarModelTick;
		private System.Windows.Forms.Label labelUICoords;
		private System.Windows.Forms.Label labelModelCoords;
		private System.Windows.Forms.ContextMenuStrip contextMenuSelectAgents;
		private System.Windows.Forms.ContextMenuStrip contextMenuChooseEditAgents;
    }
}

