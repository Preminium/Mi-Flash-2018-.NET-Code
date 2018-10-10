namespace XiaoMiFlash.form
{
	// Token: 0x02000017 RID: 23
	public partial class Download : global::System.Windows.Forms.Form
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000097D8 File Offset: 0x000079D8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000097F8 File Offset: 0x000079F8
		private void InitializeComponent()
		{
			this.cmbVersionCat = new global::System.Windows.Forms.ComboBox();
			this.rdoNoRoot = new global::System.Windows.Forms.RadioButton();
			this.rdoRoot = new global::System.Windows.Forms.RadioButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cmbVersions = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtServer = new global::System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.cmbVersionCat.FormattingEnabled = true;
			this.cmbVersionCat.Location = new global::System.Drawing.Point(91, 145);
			this.cmbVersionCat.Name = "cmbVersionCat";
			this.cmbVersionCat.Size = new global::System.Drawing.Size(121, 20);
			this.cmbVersionCat.TabIndex = 0;
			this.rdoNoRoot.AutoSize = true;
			this.rdoNoRoot.Location = new global::System.Drawing.Point(17, 12);
			this.rdoNoRoot.Name = "rdoNoRoot";
			this.rdoNoRoot.Size = new global::System.Drawing.Size(59, 16);
			this.rdoNoRoot.TabIndex = 1;
			this.rdoNoRoot.TabStop = true;
			this.rdoNoRoot.Text = "非root";
			this.rdoNoRoot.UseVisualStyleBackColor = true;
			this.rdoRoot.AutoSize = true;
			this.rdoRoot.Location = new global::System.Drawing.Point(103, 12);
			this.rdoRoot.Name = "rdoRoot";
			this.rdoRoot.Size = new global::System.Drawing.Size(47, 16);
			this.rdoRoot.TabIndex = 1;
			this.rdoRoot.TabStop = true;
			this.rdoRoot.Text = "root";
			this.rdoRoot.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.rdoNoRoot);
			this.panel1.Controls.Add(this.rdoRoot);
			this.panel1.Location = new global::System.Drawing.Point(74, 35);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(200, 46);
			this.panel1.TabIndex = 2;
			this.cmbVersions.FormattingEnabled = true;
			this.cmbVersions.Location = new global::System.Drawing.Point(235, 145);
			this.cmbVersions.Name = "cmbVersions";
			this.cmbVersions.Size = new global::System.Drawing.Size(121, 20);
			this.cmbVersions.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(91, 106);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(47, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "Server:";
			this.txtServer.Location = new global::System.Drawing.Point(156, 103);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new global::System.Drawing.Size(200, 21);
			this.txtServer.TabIndex = 5;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(449, 379);
			base.Controls.Add(this.txtServer);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmbVersions);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.cmbVersionCat);
			base.Name = "Download";
			this.Text = "Download";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400006B RID: 107
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.ComboBox cmbVersionCat;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.RadioButton rdoNoRoot;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.RadioButton rdoRoot;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.ComboBox cmbVersions;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.TextBox txtServer;
	}
}
