namespace Forms
{
	partial class LightPositionForm
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
			this.scrollBarX = new System.Windows.Forms.HScrollBar();
			this.label1 = new System.Windows.Forms.Label();
			this.scrollBarY = new System.Windows.Forms.HScrollBar();
			this.label2 = new System.Windows.Forms.Label();
			this.scrollBarZ = new System.Windows.Forms.HScrollBar();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBoxDrawLightSource = new System.Windows.Forms.CheckBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// scrollBarX
			// 
			this.scrollBarX.Location = new System.Drawing.Point(98, 15);
			this.scrollBarX.Minimum = -100;
			this.scrollBarX.Name = "scrollBarX";
			this.scrollBarX.Size = new System.Drawing.Size(136, 17);
			this.scrollBarX.TabIndex = 0;
			this.scrollBarX.Value = -10;
			this.scrollBarX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarX_Scroll);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "X Position:";
			// 
			// scrollBarY
			// 
			this.scrollBarY.LargeChange = 1;
			this.scrollBarY.Location = new System.Drawing.Point(98, 46);
			this.scrollBarY.Minimum = 40;
			this.scrollBarY.Name = "scrollBarY";
			this.scrollBarY.Size = new System.Drawing.Size(136, 17);
			this.scrollBarY.TabIndex = 0;
			this.scrollBarY.Value = 50;
			this.scrollBarY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarY_Scroll);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Y Position:";
			// 
			// scrollBarZ
			// 
			this.scrollBarZ.Location = new System.Drawing.Point(98, 74);
			this.scrollBarZ.Minimum = -100;
			this.scrollBarZ.Name = "scrollBarZ";
			this.scrollBarZ.Size = new System.Drawing.Size(136, 17);
			this.scrollBarZ.TabIndex = 0;
			this.scrollBarZ.Value = 5;
			this.scrollBarZ.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarZ_Scroll);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Z Position:";
			// 
			// checkBoxDrawLightSource
			// 
			this.checkBoxDrawLightSource.AutoSize = true;
			this.checkBoxDrawLightSource.Location = new System.Drawing.Point(24, 108);
			this.checkBoxDrawLightSource.Name = "checkBoxDrawLightSource";
			this.checkBoxDrawLightSource.Size = new System.Drawing.Size(114, 17);
			this.checkBoxDrawLightSource.TabIndex = 2;
			this.checkBoxDrawLightSource.Text = "Draw Ligth Source";
			this.checkBoxDrawLightSource.UseVisualStyleBackColor = true;
			this.checkBoxDrawLightSource.CheckedChanged += new System.EventHandler(this.checkBoxDrawLightSource_CheckedChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(159, 132);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// LightPositionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 167);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.checkBoxDrawLightSource);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.scrollBarZ);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.scrollBarY);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.scrollBarX);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "LightPositionForm";
			this.Text = "Light Position";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.HScrollBar scrollBarX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.HScrollBar scrollBarY;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.HScrollBar scrollBarZ;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBoxDrawLightSource;
		private System.Windows.Forms.Button buttonClose;
	}
}