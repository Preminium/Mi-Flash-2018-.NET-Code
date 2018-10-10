namespace XiaoMiFlash.form
{
	// Token: 0x02000034 RID: 52
	public partial class SwDownloadProgress : global::System.Windows.Forms.Form
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00013FBD File Offset: 0x000121BD
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00013FDC File Offset: 0x000121DC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.progressBar = new global::System.Windows.Forms.ProgressBar();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lblPath = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.lblSize = new global::System.Windows.Forms.Label();
			this.lblPercent = new global::System.Windows.Forms.Label();
			this.dlTimer = new global::System.Windows.Forms.Timer(this.components);
			this.label3 = new global::System.Windows.Forms.Label();
			this.lblLocalPath = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.progressBar.Location = new global::System.Drawing.Point(39, 105);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new global::System.Drawing.Size(475, 32);
			this.progressBar.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(37, 19);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(59, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "Download:";
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new global::System.Drawing.Point(97, 18);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new global::System.Drawing.Size(0, 12);
			this.lblPath.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(37, 75);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(41, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Total:";
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new global::System.Drawing.Point(99, 74);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new global::System.Drawing.Size(0, 12);
			this.lblSize.TabIndex = 4;
			this.lblPercent.AutoSize = true;
			this.lblPercent.Location = new global::System.Drawing.Point(521, 116);
			this.lblPercent.Name = "lblPercent";
			this.lblPercent.Size = new global::System.Drawing.Size(0, 12);
			this.lblPercent.TabIndex = 5;
			this.dlTimer.Tick += new global::System.EventHandler(this.dlTimer_Tick);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(39, 44);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(29, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "To :";
			this.lblLocalPath.AutoSize = true;
			this.lblLocalPath.Location = new global::System.Drawing.Point(97, 44);
			this.lblLocalPath.Name = "lblLocalPath";
			this.lblLocalPath.Size = new global::System.Drawing.Size(0, 12);
			this.lblLocalPath.TabIndex = 7;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(574, 171);
			base.Controls.Add(this.lblLocalPath);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.lblPercent);
			base.Controls.Add(this.lblSize);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lblPath);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SwDownloadProgress";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DownloadProgress";
			base.TopMost = true;
			base.Load += new global::System.EventHandler(this.DownloadProgress_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000153 RID: 339
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000154 RID: 340
		private global::System.Windows.Forms.ProgressBar progressBar;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000156 RID: 342
		private global::System.Windows.Forms.Label lblPath;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.Label lblSize;

		// Token: 0x04000159 RID: 345
		private global::System.Windows.Forms.Label lblPercent;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.Timer dlTimer;

		// Token: 0x0400015B RID: 347
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400015C RID: 348
		private global::System.Windows.Forms.Label lblLocalPath;
	}
}
