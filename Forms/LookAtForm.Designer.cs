namespace Forms
{
	partial class LookAtForm
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
			this.scrollBarEyeZ = new System.Windows.Forms.HScrollBar();
			this.scrollBarEyeY = new System.Windows.Forms.HScrollBar();
			this.scrollBarEyeX = new System.Windows.Forms.HScrollBar();
			this.groupBoxEye = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBoxCenter = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.scrollBarCenterZ = new System.Windows.Forms.HScrollBar();
			this.label5 = new System.Windows.Forms.Label();
			this.scrollBarCenterX = new System.Windows.Forms.HScrollBar();
			this.label6 = new System.Windows.Forms.Label();
			this.scrollBarCenterY = new System.Windows.Forms.HScrollBar();
			this.groupBoxUp = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.scrollBarUpZ = new System.Windows.Forms.HScrollBar();
			this.label8 = new System.Windows.Forms.Label();
			this.scrollBarUpX = new System.Windows.Forms.HScrollBar();
			this.label9 = new System.Windows.Forms.Label();
			this.scrollBarUpY = new System.Windows.Forms.HScrollBar();
			this.buttonClose = new System.Windows.Forms.Button();
			this.groupBoxEye.SuspendLayout();
			this.groupBoxCenter.SuspendLayout();
			this.groupBoxUp.SuspendLayout();
			this.SuspendLayout();
			// 
			// scrollBarEyeZ
			// 
			this.scrollBarEyeZ.LargeChange = 1;
			this.scrollBarEyeZ.Location = new System.Drawing.Point(52, 81);
			this.scrollBarEyeZ.Maximum = 200;
			this.scrollBarEyeZ.Name = "scrollBarEyeZ";
			this.scrollBarEyeZ.Size = new System.Drawing.Size(136, 17);
			this.scrollBarEyeZ.TabIndex = 1;
			this.scrollBarEyeZ.Value = 101;
			this.scrollBarEyeZ.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarEyeZ_Scroll);
			// 
			// scrollBarEyeY
			// 
			this.scrollBarEyeY.LargeChange = 1;
			this.scrollBarEyeY.Location = new System.Drawing.Point(52, 53);
			this.scrollBarEyeY.Maximum = 200;
			this.scrollBarEyeY.Name = "scrollBarEyeY";
			this.scrollBarEyeY.Size = new System.Drawing.Size(136, 17);
			this.scrollBarEyeY.TabIndex = 2;
			this.scrollBarEyeY.Value = 100;
			this.scrollBarEyeY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarEyeY_Scroll);
			// 
			// scrollBarEyeX
			// 
			this.scrollBarEyeX.LargeChange = 1;
			this.scrollBarEyeX.Location = new System.Drawing.Point(52, 26);
			this.scrollBarEyeX.Maximum = 200;
			this.scrollBarEyeX.Name = "scrollBarEyeX";
			this.scrollBarEyeX.Size = new System.Drawing.Size(136, 17);
			this.scrollBarEyeX.TabIndex = 3;
			this.scrollBarEyeX.Value = 100;
			this.scrollBarEyeX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarEyeX_Scroll);
			// 
			// groupBoxEye
			// 
			this.groupBoxEye.Controls.Add(this.label3);
			this.groupBoxEye.Controls.Add(this.scrollBarEyeZ);
			this.groupBoxEye.Controls.Add(this.label2);
			this.groupBoxEye.Controls.Add(this.scrollBarEyeX);
			this.groupBoxEye.Controls.Add(this.label1);
			this.groupBoxEye.Controls.Add(this.scrollBarEyeY);
			this.groupBoxEye.Location = new System.Drawing.Point(12, 12);
			this.groupBoxEye.Name = "groupBoxEye";
			this.groupBoxEye.Size = new System.Drawing.Size(200, 112);
			this.groupBoxEye.TabIndex = 10;
			this.groupBoxEye.TabStop = false;
			this.groupBoxEye.Text = "Eye";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Z:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Y:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "X:";
			// 
			// groupBoxCenter
			// 
			this.groupBoxCenter.Controls.Add(this.label4);
			this.groupBoxCenter.Controls.Add(this.scrollBarCenterZ);
			this.groupBoxCenter.Controls.Add(this.label5);
			this.groupBoxCenter.Controls.Add(this.scrollBarCenterX);
			this.groupBoxCenter.Controls.Add(this.label6);
			this.groupBoxCenter.Controls.Add(this.scrollBarCenterY);
			this.groupBoxCenter.Location = new System.Drawing.Point(12, 130);
			this.groupBoxCenter.Name = "groupBoxCenter";
			this.groupBoxCenter.Size = new System.Drawing.Size(200, 112);
			this.groupBoxCenter.TabIndex = 10;
			this.groupBoxCenter.TabStop = false;
			this.groupBoxCenter.Text = "Center";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Z:";
			// 
			// scrollBarCenterZ
			// 
			this.scrollBarCenterZ.LargeChange = 1;
			this.scrollBarCenterZ.Location = new System.Drawing.Point(52, 81);
			this.scrollBarCenterZ.Maximum = 200;
			this.scrollBarCenterZ.Name = "scrollBarCenterZ";
			this.scrollBarCenterZ.Size = new System.Drawing.Size(136, 17);
			this.scrollBarCenterZ.TabIndex = 1;
			this.scrollBarCenterZ.Value = 100;
			this.scrollBarCenterZ.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarCenterZ_Scroll);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(20, 55);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Y:";
			// 
			// scrollBarCenterX
			// 
			this.scrollBarCenterX.LargeChange = 1;
			this.scrollBarCenterX.Location = new System.Drawing.Point(52, 26);
			this.scrollBarCenterX.Maximum = 200;
			this.scrollBarCenterX.Name = "scrollBarCenterX";
			this.scrollBarCenterX.Size = new System.Drawing.Size(136, 17);
			this.scrollBarCenterX.TabIndex = 3;
			this.scrollBarCenterX.Value = 100;
			this.scrollBarCenterX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarCenterX_Scroll);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(20, 30);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(17, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "X:";
			// 
			// scrollBarCenterY
			// 
			this.scrollBarCenterY.LargeChange = 1;
			this.scrollBarCenterY.Location = new System.Drawing.Point(52, 53);
			this.scrollBarCenterY.Maximum = 200;
			this.scrollBarCenterY.Name = "scrollBarCenterY";
			this.scrollBarCenterY.Size = new System.Drawing.Size(136, 17);
			this.scrollBarCenterY.TabIndex = 2;
			this.scrollBarCenterY.Value = 100;
			this.scrollBarCenterY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarCenterY_Scroll);
			// 
			// groupBoxUp
			// 
			this.groupBoxUp.Controls.Add(this.label7);
			this.groupBoxUp.Controls.Add(this.scrollBarUpZ);
			this.groupBoxUp.Controls.Add(this.label8);
			this.groupBoxUp.Controls.Add(this.scrollBarUpX);
			this.groupBoxUp.Controls.Add(this.label9);
			this.groupBoxUp.Controls.Add(this.scrollBarUpY);
			this.groupBoxUp.Location = new System.Drawing.Point(12, 248);
			this.groupBoxUp.Name = "groupBoxUp";
			this.groupBoxUp.Size = new System.Drawing.Size(200, 112);
			this.groupBoxUp.TabIndex = 10;
			this.groupBoxUp.TabStop = false;
			this.groupBoxUp.Text = "Up";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(20, 81);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(17, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Z:";
			// 
			// scrollBarUpZ
			// 
			this.scrollBarUpZ.LargeChange = 1;
			this.scrollBarUpZ.Location = new System.Drawing.Point(52, 81);
			this.scrollBarUpZ.Maximum = 200;
			this.scrollBarUpZ.Name = "scrollBarUpZ";
			this.scrollBarUpZ.Size = new System.Drawing.Size(136, 17);
			this.scrollBarUpZ.TabIndex = 1;
			this.scrollBarUpZ.Value = 100;
			this.scrollBarUpZ.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarUpZ_Scroll);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(20, 55);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(17, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "Y:";
			// 
			// scrollBarUpX
			// 
			this.scrollBarUpX.LargeChange = 1;
			this.scrollBarUpX.Location = new System.Drawing.Point(52, 26);
			this.scrollBarUpX.Maximum = 200;
			this.scrollBarUpX.Name = "scrollBarUpX";
			this.scrollBarUpX.Size = new System.Drawing.Size(136, 17);
			this.scrollBarUpX.TabIndex = 3;
			this.scrollBarUpX.Value = 100;
			this.scrollBarUpX.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarUpX_Scroll);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(20, 30);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(17, 13);
			this.label9.TabIndex = 12;
			this.label9.Text = "X:";
			// 
			// scrollBarUpY
			// 
			this.scrollBarUpY.LargeChange = 1;
			this.scrollBarUpY.Location = new System.Drawing.Point(52, 53);
			this.scrollBarUpY.Maximum = 200;
			this.scrollBarUpY.Name = "scrollBarUpY";
			this.scrollBarUpY.Size = new System.Drawing.Size(136, 17);
			this.scrollBarUpY.TabIndex = 2;
			this.scrollBarUpY.Value = 101;
			this.scrollBarUpY.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarUpY_Scroll);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(137, 366);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 23);
			this.buttonClose.TabIndex = 11;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// LookAtForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(220, 397);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.groupBoxUp);
			this.Controls.Add(this.groupBoxCenter);
			this.Controls.Add(this.groupBoxEye);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MinimizeBox = false;
			this.Name = "LookAtForm";
			this.Text = "Look At";
			this.groupBoxEye.ResumeLayout(false);
			this.groupBoxEye.PerformLayout();
			this.groupBoxCenter.ResumeLayout(false);
			this.groupBoxCenter.PerformLayout();
			this.groupBoxUp.ResumeLayout(false);
			this.groupBoxUp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.HScrollBar scrollBarEyeZ;
		private System.Windows.Forms.HScrollBar scrollBarEyeY;
		private System.Windows.Forms.HScrollBar scrollBarEyeX;
		private System.Windows.Forms.GroupBox groupBoxEye;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBoxCenter;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.HScrollBar scrollBarCenterZ;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.HScrollBar scrollBarCenterX;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.HScrollBar scrollBarCenterY;
		private System.Windows.Forms.GroupBox groupBoxUp;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.HScrollBar scrollBarUpZ;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.HScrollBar scrollBarUpX;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.HScrollBar scrollBarUpY;
		private System.Windows.Forms.Button buttonClose;
	}
}