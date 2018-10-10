namespace XiaoMiFlash.form
{
	// Token: 0x02000016 RID: 22
	public partial class DriverFrm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000094F9 File Offset: 0x000076F9
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009518 File Offset: 0x00007718
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::XiaoMiFlash.form.DriverFrm));
			this.DriverBox = new global::System.Windows.Forms.GroupBox();
			this.btnInstall = new global::System.Windows.Forms.Button();
			this.lblDriver = new global::System.Windows.Forms.Label();
			this.lblMsg = new global::System.Windows.Forms.Label();
			this.DriverBox.SuspendLayout();
			base.SuspendLayout();
			this.DriverBox.Controls.Add(this.btnInstall);
			this.DriverBox.Controls.Add(this.lblDriver);
			this.DriverBox.Location = new global::System.Drawing.Point(72, 52);
			this.DriverBox.Name = "DriverBox";
			this.DriverBox.Size = new global::System.Drawing.Size(534, 227);
			this.DriverBox.TabIndex = 0;
			this.DriverBox.TabStop = false;
			this.DriverBox.Text = "Driver";
			this.btnInstall.Location = new global::System.Drawing.Point(44, 192);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new global::System.Drawing.Size(75, 23);
			this.btnInstall.TabIndex = 1;
			this.btnInstall.Text = "Install";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new global::System.EventHandler(this.btnInstall_Click);
			this.lblDriver.AutoSize = true;
			this.lblDriver.Location = new global::System.Drawing.Point(42, 42);
			this.lblDriver.Name = "lblDriver";
			this.lblDriver.Size = new global::System.Drawing.Size(0, 12);
			this.lblDriver.TabIndex = 0;
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new global::System.Drawing.Point(114, 23);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new global::System.Drawing.Size(0, 12);
			this.lblMsg.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(729, 409);
			base.Controls.Add(this.lblMsg);
			base.Controls.Add(this.DriverBox);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "DriverFrm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Driver";
			base.Load += new global::System.EventHandler(this.DriverFrm_Load);
			this.DriverBox.ResumeLayout(false);
			this.DriverBox.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000066 RID: 102
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.GroupBox DriverBox;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.Label lblDriver;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.Button btnInstall;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.Label lblMsg;
	}
}
