namespace MapEditor
{
	partial class NewFileWindow
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
			this.labelChunckSize = new System.Windows.Forms.Label();
			this.textBoxChunckSize = new System.Windows.Forms.TextBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelChunckSize
			// 
			this.labelChunckSize.AutoSize = true;
			this.labelChunckSize.Location = new System.Drawing.Point(13, 13);
			this.labelChunckSize.Name = "labelChunckSize";
			this.labelChunckSize.Size = new System.Drawing.Size(68, 13);
			this.labelChunckSize.TabIndex = 0;
			this.labelChunckSize.Text = "Chunck size:";
			// 
			// textBoxChunckSize
			// 
			this.textBoxChunckSize.Location = new System.Drawing.Point(88, 13);
			this.textBoxChunckSize.Name = "textBoxChunckSize";
			this.textBoxChunckSize.Size = new System.Drawing.Size(74, 20);
			this.textBoxChunckSize.TabIndex = 1;
			this.textBoxChunckSize.Text = "10";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonOk.Location = new System.Drawing.Point(13, 65);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(182, 65);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// NewFileWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(269, 100);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.textBoxChunckSize);
			this.Controls.Add(this.labelChunckSize);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(285, 138);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(285, 138);
			this.Name = "NewFileWindow";
			this.Text = "Create new chunck";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelChunckSize;
		private System.Windows.Forms.TextBox textBoxChunckSize;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
	}
}