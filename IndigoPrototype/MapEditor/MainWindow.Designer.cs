namespace MapEditor
{
	partial class MainWindow
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.MainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuEditorTerrainToolbar = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuEditorDeselectCells = new System.Windows.Forms.ToolStripMenuItem();
			this.MainEditorPanel = new System.Windows.Forms.Panel();
			this.contextMenuCell = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuCellSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuCellClearAgents = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenu.SuspendLayout();
			this.contextMenuCell.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFile,
            this.MainMenuEditor});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(811, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// MainMenuFile
			// 
			this.MainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFileClose});
			this.MainMenuFile.Name = "MainMenuFile";
			this.MainMenuFile.Size = new System.Drawing.Size(37, 20);
			this.MainMenuFile.Text = "File";
			// 
			// MainMenuFileClose
			// 
			this.MainMenuFileClose.Name = "MainMenuFileClose";
			this.MainMenuFileClose.Size = new System.Drawing.Size(103, 22);
			this.MainMenuFileClose.Text = "Close";
			this.MainMenuFileClose.Click += new System.EventHandler(this.MainMenuFileClose_Click);
			// 
			// MainMenuEditor
			// 
			this.MainMenuEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuEditorTerrainToolbar,
            this.MainMenuEditorDeselectCells});
			this.MainMenuEditor.Name = "MainMenuEditor";
			this.MainMenuEditor.Size = new System.Drawing.Size(50, 20);
			this.MainMenuEditor.Text = "Editor";
			// 
			// MainMenuEditorTerrainToolbar
			// 
			this.MainMenuEditorTerrainToolbar.Name = "MainMenuEditorTerrainToolbar";
			this.MainMenuEditorTerrainToolbar.Size = new System.Drawing.Size(152, 22);
			this.MainMenuEditorTerrainToolbar.Text = "Terrain toolbar";
			this.MainMenuEditorTerrainToolbar.Click += new System.EventHandler(this.MainMenuEditorTerrainToolbar_Click);
			// 
			// MainMenuEditorDeselectCells
			// 
			this.MainMenuEditorDeselectCells.Name = "MainMenuEditorDeselectCells";
			this.MainMenuEditorDeselectCells.Size = new System.Drawing.Size(152, 22);
			this.MainMenuEditorDeselectCells.Text = "Deselect cells";
			this.MainMenuEditorDeselectCells.Click += new System.EventHandler(this.MainMenuEditorDeselectCells_Click);
			// 
			// MainEditorPanel
			// 
			this.MainEditorPanel.AllowDrop = true;
			this.MainEditorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainEditorPanel.BackColor = System.Drawing.Color.White;
			this.MainEditorPanel.Location = new System.Drawing.Point(13, 28);
			this.MainEditorPanel.Name = "MainEditorPanel";
			this.MainEditorPanel.Size = new System.Drawing.Size(786, 431);
			this.MainEditorPanel.TabIndex = 1;
			this.MainEditorPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainEditorPanel_DragDrop);
			this.MainEditorPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainEditorPanel_DragEnter);
			this.MainEditorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainEditorPanel_Paint);
			this.MainEditorPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainEditorPanel_MouseDown);
			this.MainEditorPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainEditorPanel_MouseMove);
			this.MainEditorPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainEditorPanel_MouseUp);
			// 
			// contextMenuCell
			// 
			this.contextMenuCell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuCellSelect,
            this.contextMenuCellClearAgents});
			this.contextMenuCell.Name = "contextMenuCell";
			this.contextMenuCell.Size = new System.Drawing.Size(153, 70);
			// 
			// contextMenuCellSelect
			// 
			this.contextMenuCellSelect.Name = "contextMenuCellSelect";
			this.contextMenuCellSelect.Size = new System.Drawing.Size(152, 22);
			this.contextMenuCellSelect.Text = "Select";
			this.contextMenuCellSelect.Click += new System.EventHandler(this.contextMenuCellSelect_Click);
			// 
			// contextMenuCellClearAgents
			// 
			this.contextMenuCellClearAgents.Name = "contextMenuCellClearAgents";
			this.contextMenuCellClearAgents.Size = new System.Drawing.Size(152, 22);
			this.contextMenuCellClearAgents.Text = "Clear agents";
			this.contextMenuCellClearAgents.Click += new System.EventHandler(this.contextMenuCellClearAgents_Click);
			// 
			// MainWindow
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(811, 471);
			this.Controls.Add(this.MainEditorPanel);
			this.Controls.Add(this.MainMenu);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "MainWindow";
			this.Text = "Indigo project map editor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.contextMenuCell.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFile;
		private System.Windows.Forms.ToolStripMenuItem MainMenuFileClose;
		private System.Windows.Forms.Panel MainEditorPanel;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEditor;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEditorTerrainToolbar;
		private System.Windows.Forms.ToolStripMenuItem MainMenuEditorDeselectCells;
		private System.Windows.Forms.ContextMenuStrip contextMenuCell;
		private System.Windows.Forms.ToolStripMenuItem contextMenuCellSelect;
		private System.Windows.Forms.ToolStripMenuItem contextMenuCellClearAgents;
	}
}

