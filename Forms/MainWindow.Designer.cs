namespace Forms
{
	partial class MainWindow
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
			this.drawPanel = new System.Windows.Forms.Panel();
			this.menuBar = new System.Windows.Forms.MenuStrip();
			this.menuItemLookAt = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLight = new System.Windows.Forms.ToolStripMenuItem();
			this.drawPanel.SuspendLayout();
			this.menuBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// drawPanel
			// 
			this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drawPanel.Controls.Add(this.menuBar);
			this.drawPanel.Location = new System.Drawing.Point(0, 0);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(684, 661);
			this.drawPanel.TabIndex = 12;
			// 
			// menuBar
			// 
			this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLookAt,
            this.menuItemLight});
			this.menuBar.Location = new System.Drawing.Point(0, 0);
			this.menuBar.Name = "menuBar";
			this.menuBar.Size = new System.Drawing.Size(682, 24);
			this.menuBar.TabIndex = 0;
			this.menuBar.Text = "Menu Bar";
			// 
			// menuItemLookAt
			// 
			this.menuItemLookAt.Name = "menuItemLookAt";
			this.menuItemLookAt.Size = new System.Drawing.Size(60, 20);
			this.menuItemLookAt.Text = "Look At";
			this.menuItemLookAt.Click += new System.EventHandler(this.menuItemLookAt_Click);
			// 
			// menuItemLight
			// 
			this.menuItemLight.Name = "menuItemLight";
			this.menuItemLight.Size = new System.Drawing.Size(46, 20);
			this.menuItemLight.Text = "Light";
			this.menuItemLight.Click += new System.EventHandler(this.menuItemLight_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 661);
			this.Controls.Add(this.drawPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuBar;
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(420, 420);
			this.Name = "MainWindow";
			this.Text = "Helicopter";
			this.drawPanel.ResumeLayout(false);
			this.drawPanel.PerformLayout();
			this.menuBar.ResumeLayout(false);
			this.menuBar.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel drawPanel;
		private System.Windows.Forms.MenuStrip menuBar;
		private System.Windows.Forms.ToolStripMenuItem menuItemLookAt;
		private System.Windows.Forms.ToolStripMenuItem menuItemLight;
	}
}