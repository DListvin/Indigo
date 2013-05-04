namespace MapEditor
{
	partial class ToolBarWindow
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
			this.labelXYCoords = new System.Windows.Forms.Label();
			this.labelHexCoords = new System.Windows.Forms.Label();
			this.tabAgentsAdding = new System.Windows.Forms.TabPage();
			this.tabMainTools = new System.Windows.Forms.TabControl();
			this.tabMainTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelXYCoords
			// 
			this.labelXYCoords.AutoSize = true;
			this.labelXYCoords.Location = new System.Drawing.Point(13, 13);
			this.labelXYCoords.Name = "labelXYCoords";
			this.labelXYCoords.Size = new System.Drawing.Size(59, 13);
			this.labelXYCoords.TabIndex = 0;
			this.labelXYCoords.Text = "XY coords:";
			// 
			// labelHexCoords
			// 
			this.labelHexCoords.AutoSize = true;
			this.labelHexCoords.Location = new System.Drawing.Point(13, 30);
			this.labelHexCoords.Name = "labelHexCoords";
			this.labelHexCoords.Size = new System.Drawing.Size(64, 13);
			this.labelHexCoords.TabIndex = 1;
			this.labelHexCoords.Text = "Hex coords:";
			// 
			// tabAgentsAdding
			// 
			this.tabAgentsAdding.Location = new System.Drawing.Point(4, 22);
			this.tabAgentsAdding.Name = "tabAgentsAdding";
			this.tabAgentsAdding.Padding = new System.Windows.Forms.Padding(3);
			this.tabAgentsAdding.Size = new System.Drawing.Size(196, 439);
			this.tabAgentsAdding.TabIndex = 0;
			this.tabAgentsAdding.Text = "Agents";
			this.tabAgentsAdding.UseVisualStyleBackColor = true;
			// 
			// tabMainTools
			// 
			this.tabMainTools.Controls.Add(this.tabAgentsAdding);
			this.tabMainTools.Location = new System.Drawing.Point(13, 47);
			this.tabMainTools.Name = "tabMainTools";
			this.tabMainTools.SelectedIndex = 0;
			this.tabMainTools.Size = new System.Drawing.Size(204, 465);
			this.tabMainTools.TabIndex = 2;
			// 
			// ToolBarWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(229, 524);
			this.Controls.Add(this.tabMainTools);
			this.Controls.Add(this.labelHexCoords);
			this.Controls.Add(this.labelXYCoords);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(245, 562);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(245, 562);
			this.Name = "ToolBarWindow";
			this.Text = "Terrain tool bar";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolBarWindow_FormClosing);
			this.Load += new System.EventHandler(this.ToolBarWindow_Load);
			this.tabMainTools.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelXYCoords;
		private System.Windows.Forms.Label labelHexCoords;
		private System.Windows.Forms.TabPage tabAgentsAdding;
		private System.Windows.Forms.TabControl tabMainTools;
	}
}