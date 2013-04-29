namespace GraphicalUI
{
	partial class FormAgentAdd
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
			this.buttonAddIndigo = new System.Windows.Forms.Button();
			this.buttonAddTree = new System.Windows.Forms.Button();
			this.buttonAddPuddle = new System.Windows.Forms.Button();
			this.buttonAddLog = new System.Windows.Forms.Button();
			this.buttonAddFruit = new System.Windows.Forms.Button();
			this.buttonAddCamp = new System.Windows.Forms.Button();
			this.labelUserHint = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxAgentName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonAddIndigo
			// 
			this.buttonAddIndigo.Location = new System.Drawing.Point(75, 56);
			this.buttonAddIndigo.Name = "buttonAddIndigo";
			this.buttonAddIndigo.Size = new System.Drawing.Size(87, 23);
			this.buttonAddIndigo.TabIndex = 0;
			this.buttonAddIndigo.Text = "Add indigo";
			this.buttonAddIndigo.UseVisualStyleBackColor = true;
			this.buttonAddIndigo.Click += new System.EventHandler(this.buttonAddIndigo_Click);
			// 
			// buttonAddTree
			// 
			this.buttonAddTree.Location = new System.Drawing.Point(75, 85);
			this.buttonAddTree.Name = "buttonAddTree";
			this.buttonAddTree.Size = new System.Drawing.Size(87, 23);
			this.buttonAddTree.TabIndex = 0;
			this.buttonAddTree.Text = "Add tree";
			this.buttonAddTree.UseVisualStyleBackColor = true;
			this.buttonAddTree.Click += new System.EventHandler(this.buttonAddTree_Click);
			// 
			// buttonAddPuddle
			// 
			this.buttonAddPuddle.Location = new System.Drawing.Point(75, 114);
			this.buttonAddPuddle.Name = "buttonAddPuddle";
			this.buttonAddPuddle.Size = new System.Drawing.Size(87, 23);
			this.buttonAddPuddle.TabIndex = 0;
			this.buttonAddPuddle.Text = "Add puddle";
			this.buttonAddPuddle.UseVisualStyleBackColor = true;
			this.buttonAddPuddle.Click += new System.EventHandler(this.buttonAddPuddle_Click);
			// 
			// buttonAddLog
			// 
			this.buttonAddLog.Location = new System.Drawing.Point(75, 143);
			this.buttonAddLog.Name = "buttonAddLog";
			this.buttonAddLog.Size = new System.Drawing.Size(87, 23);
			this.buttonAddLog.TabIndex = 0;
			this.buttonAddLog.Text = "Add log";
			this.buttonAddLog.UseVisualStyleBackColor = true;
			this.buttonAddLog.Click += new System.EventHandler(this.buttonAddLog_Click);
			// 
			// buttonAddFruit
			// 
			this.buttonAddFruit.Location = new System.Drawing.Point(75, 172);
			this.buttonAddFruit.Name = "buttonAddFruit";
			this.buttonAddFruit.Size = new System.Drawing.Size(87, 23);
			this.buttonAddFruit.TabIndex = 0;
			this.buttonAddFruit.Text = "Add fruit";
			this.buttonAddFruit.UseVisualStyleBackColor = true;
			this.buttonAddFruit.Click += new System.EventHandler(this.buttonAddFruit_Click);
			// 
			// buttonAddCamp
			// 
			this.buttonAddCamp.Location = new System.Drawing.Point(75, 201);
			this.buttonAddCamp.Name = "buttonAddCamp";
			this.buttonAddCamp.Size = new System.Drawing.Size(87, 23);
			this.buttonAddCamp.TabIndex = 0;
			this.buttonAddCamp.Text = "Add camp";
			this.buttonAddCamp.UseVisualStyleBackColor = true;
			this.buttonAddCamp.Click += new System.EventHandler(this.buttonAddCamp_Click);
			// 
			// labelUserHint
			// 
			this.labelUserHint.AutoSize = true;
			this.labelUserHint.Location = new System.Drawing.Point(39, 9);
			this.labelUserHint.Name = "labelUserHint";
			this.labelUserHint.Size = new System.Drawing.Size(140, 13);
			this.labelUserHint.TabIndex = 1;
			this.labelUserHint.Text = "Select agent name and type";
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(57, 229);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(122, 49);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxAgentName
			// 
			this.textBoxAgentName.Location = new System.Drawing.Point(12, 30);
			this.textBoxAgentName.Name = "textBoxAgentName";
			this.textBoxAgentName.Size = new System.Drawing.Size(213, 20);
			this.textBoxAgentName.TabIndex = 3;
			// 
			// FormAgentAdd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(237, 289);
			this.Controls.Add(this.textBoxAgentName);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.labelUserHint);
			this.Controls.Add(this.buttonAddCamp);
			this.Controls.Add(this.buttonAddFruit);
			this.Controls.Add(this.buttonAddLog);
			this.Controls.Add(this.buttonAddPuddle);
			this.Controls.Add(this.buttonAddTree);
			this.Controls.Add(this.buttonAddIndigo);
			this.MaximumSize = new System.Drawing.Size(253, 327);
			this.MinimumSize = new System.Drawing.Size(253, 327);
			this.Name = "FormAgentAdd";
			this.Text = "Agent adding form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonAddIndigo;
		private System.Windows.Forms.Button buttonAddTree;
		private System.Windows.Forms.Button buttonAddPuddle;
		private System.Windows.Forms.Button buttonAddLog;
		private System.Windows.Forms.Button buttonAddFruit;
		private System.Windows.Forms.Button buttonAddCamp;
		private System.Windows.Forms.Label labelUserHint;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxAgentName;
	}
}