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
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.MainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuModelControl = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuModelControlAddAgent = new System.Windows.Forms.ToolStripMenuItem();
			this.saveModelDialog = new System.Windows.Forms.SaveFileDialog();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.loadModelDialog = new System.Windows.Forms.OpenFileDialog();
			this.modelControlPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarModelTick)).BeginInit();
			this.MainMenu.SuspendLayout();
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
			this.mapInfoPanel.Location = new System.Drawing.Point(517, 28);
			this.mapInfoPanel.Name = "mapInfoPanel";
			this.mapInfoPanel.Size = new System.Drawing.Size(376, 435);
			this.mapInfoPanel.TabIndex = 2;
			this.mapInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mapInfoPanel_Paint);
			// 
			// mapPanel
			// 
			this.mapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mapPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.mapPanel.Location = new System.Drawing.Point(13, 28);
			this.mapPanel.Name = "mapPanel";
			this.mapPanel.Size = new System.Drawing.Size(498, 435);
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
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFile,
            this.MainMenuModelControl});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(905, 24);
			this.MainMenu.TabIndex = 3;
			this.MainMenu.Text = "menuStrip1";
			// 
			// MainMenuFile
			// 
			this.MainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFileSave,
            this.MainMenuFileLoad,
            this.MainMenuFileClose});
			this.MainMenuFile.Name = "MainMenuFile";
			this.MainMenuFile.Size = new System.Drawing.Size(37, 20);
			this.MainMenuFile.Text = "File";
			// 
			// MainMenuFileSave
			// 
			this.MainMenuFileSave.Name = "MainMenuFileSave";
			this.MainMenuFileSave.Size = new System.Drawing.Size(103, 22);
			this.MainMenuFileSave.Text = "Save";
			this.MainMenuFileSave.Click += new System.EventHandler(this.MainMenuFileSave_Click);
			// 
			// MainMenuFileLoad
			// 
			this.MainMenuFileLoad.Name = "MainMenuFileLoad";
			this.MainMenuFileLoad.Size = new System.Drawing.Size(103, 22);
			this.MainMenuFileLoad.Text = "Load";
			this.MainMenuFileLoad.Click += new System.EventHandler(this.MainMenuFileLoad_Click);
			// 
			// MainMenuFileClose
			// 
			this.MainMenuFileClose.Name = "MainMenuFileClose";
			this.MainMenuFileClose.Size = new System.Drawing.Size(103, 22);
			this.MainMenuFileClose.Text = "Close";
			this.MainMenuFileClose.Click += new System.EventHandler(this.MainMenuFileClose_Click);
			// 
			// MainMenuModelControl
			// 
			this.MainMenuModelControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuModelControlAddAgent});
			this.MainMenuModelControl.Name = "MainMenuModelControl";
			this.MainMenuModelControl.Size = new System.Drawing.Size(94, 20);
			this.MainMenuModelControl.Text = "Model control";
			// 
			// MainMenuModelControlAddAgent
			// 
			this.MainMenuModelControlAddAgent.Name = "MainMenuModelControlAddAgent";
			this.MainMenuModelControlAddAgent.Size = new System.Drawing.Size(129, 22);
			this.MainMenuModelControlAddAgent.Text = "Add agent";
			this.MainMenuModelControlAddAgent.Click += new System.EventHandler(this.MainMenuModelControlAddAgent_Click);
			// 
			// saveModelDialog
			// 
			this.saveModelDialog.DefaultExt = "mdl";
			this.saveModelDialog.FileName = "Default";
			this.saveModelDialog.Filter = "Model files|*.mdl|All extensions|*.*";
			this.saveModelDialog.RestoreDirectory = true;
			this.saveModelDialog.Title = "Save model";
			// 
			// elementHost1
			// 
			this.elementHost1.Location = new System.Drawing.Point(0, 0);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(200, 100);
			this.elementHost1.TabIndex = 0;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = null;
			// 
			// loadModelDialog
			// 
			this.loadModelDialog.DefaultExt = "mdl";
			this.loadModelDialog.FileName = "openFileDialog1";
			this.loadModelDialog.Filter = "Model files|*.mdl|All extensions|*.*";
			this.loadModelDialog.RestoreDirectory = true;
			// 
			// GrapgicalUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(905, 562);
			this.Controls.Add(this.MainMenu);
			this.Controls.Add(this.mapInfoPanel);
			this.Controls.Add(this.modelControlPanel);
			this.Controls.Add(this.mapPanel);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.Name = "GrapgicalUIForm";
			this.Text = "Indigo";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GrapgicalUIForm_FormClosing);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GrapgicalUIForm_Paint);
			this.modelControlPanel.ResumeLayout(false);
			this.modelControlPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarModelTick)).EndInit();
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFile;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFileClose;
		private System.Windows.Forms.ToolStripMenuItem MainMenuModelControl;
		private System.Windows.Forms.ToolStripMenuItem MainMenuModelControlAddAgent;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFileSave;
		private System.Windows.Forms.SaveFileDialog saveModelDialog;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private System.Windows.Forms.OpenFileDialog loadModelDialog;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFileLoad;
    }
}

