namespace XiaoMiFlash.form
{
	// Token: 0x02000009 RID: 9
	public partial class ValidationFrm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003F74 File Offset: 0x00002174
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003F94 File Offset: 0x00002194
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::XiaoMiFlash.form.ValidationFrm));
			this.btnCheckAll = new global::System.Windows.Forms.Button();
			this.btnSpecify = new global::System.Windows.Forms.Button();
			this.openXmlFile = new global::System.Windows.Forms.OpenFileDialog();
			base.SuspendLayout();
			this.btnCheckAll.Location = new global::System.Drawing.Point(54, 34);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new global::System.Drawing.Size(75, 23);
			this.btnCheckAll.TabIndex = 0;
			this.btnCheckAll.Text = "Check All";
			this.btnCheckAll.UseVisualStyleBackColor = true;
			this.btnCheckAll.Click += new global::System.EventHandler(this.btnCheckAll_Click);
			this.btnSpecify.Location = new global::System.Drawing.Point(54, 86);
			this.btnSpecify.Name = "btnSpecify";
			this.btnSpecify.Size = new global::System.Drawing.Size(75, 23);
			this.btnSpecify.TabIndex = 1;
			this.btnSpecify.Text = "Specify";
			this.btnSpecify.UseVisualStyleBackColor = true;
			this.btnSpecify.Click += new global::System.EventHandler(this.btnSpecify_Click);
			this.openXmlFile.FileName = "*.xml";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(213, 209);
			base.Controls.Add(this.btnSpecify);
			base.Controls.Add(this.btnCheckAll);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ValidationFrm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Check Sha256";
			base.Load += new global::System.EventHandler(this.ValidationFrm_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x0400001B RID: 27
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.Button btnCheckAll;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.Button btnSpecify;

		// Token: 0x0400001E RID: 30
		private global::System.Windows.Forms.OpenFileDialog openXmlFile;
	}
}
