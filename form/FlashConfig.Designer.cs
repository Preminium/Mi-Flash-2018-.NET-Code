namespace XiaoMiFlash.form
{
	// Token: 0x02000003 RID: 3
	public partial class FlashConfig : global::System.Windows.Forms.Form
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022B1 File Offset: 0x000004B1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022D0 File Offset: 0x000004D0
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.txtScript = new global::System.Windows.Forms.TextBox();
			this.btnScriptSelect = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txtRaw = new global::System.Windows.Forms.TextBox();
			this.btnRawSelect = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.txtPatch = new global::System.Windows.Forms.TextBox();
			this.btnPatchSelect = new global::System.Windows.Forms.Button();
			this.openFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(13, 40);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(107, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fastboot script：";
			this.txtScript.Location = new global::System.Drawing.Point(126, 37);
			this.txtScript.Name = "txtScript";
			this.txtScript.Size = new global::System.Drawing.Size(293, 21);
			this.txtScript.TabIndex = 1;
			this.btnScriptSelect.Location = new global::System.Drawing.Point(439, 35);
			this.btnScriptSelect.Name = "btnScriptSelect";
			this.btnScriptSelect.Size = new global::System.Drawing.Size(75, 23);
			this.btnScriptSelect.TabIndex = 2;
			this.btnScriptSelect.Text = "select";
			this.btnScriptSelect.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(61, 85);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(59, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "Raw xml：";
			this.txtRaw.Location = new global::System.Drawing.Point(126, 82);
			this.txtRaw.Name = "txtRaw";
			this.txtRaw.Size = new global::System.Drawing.Size(293, 21);
			this.txtRaw.TabIndex = 1;
			this.btnRawSelect.Location = new global::System.Drawing.Point(439, 80);
			this.btnRawSelect.Name = "btnRawSelect";
			this.btnRawSelect.Size = new global::System.Drawing.Size(75, 23);
			this.btnRawSelect.TabIndex = 2;
			this.btnRawSelect.Text = "select";
			this.btnRawSelect.UseVisualStyleBackColor = true;
			this.btnRawSelect.Click += new global::System.EventHandler(this.btnRawSelect_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(49, 140);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(71, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "Patch xml：";
			this.txtPatch.Location = new global::System.Drawing.Point(126, 137);
			this.txtPatch.Name = "txtPatch";
			this.txtPatch.Size = new global::System.Drawing.Size(293, 21);
			this.txtPatch.TabIndex = 1;
			this.btnPatchSelect.Location = new global::System.Drawing.Point(439, 135);
			this.btnPatchSelect.Name = "btnPatchSelect";
			this.btnPatchSelect.Size = new global::System.Drawing.Size(75, 23);
			this.btnPatchSelect.TabIndex = 2;
			this.btnPatchSelect.Text = "select";
			this.btnPatchSelect.UseVisualStyleBackColor = true;
			this.btnPatchSelect.Click += new global::System.EventHandler(this.btnPatchSelect_Click);
			this.openFileDialog.FileName = "openFileDialog1";
			this.openFileDialog.Filter = "xml|*.xml";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(541, 417);
			base.Controls.Add(this.btnPatchSelect);
			base.Controls.Add(this.btnRawSelect);
			base.Controls.Add(this.btnScriptSelect);
			base.Controls.Add(this.txtPatch);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.txtRaw);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtScript);
			base.Controls.Add(this.label1);
			base.Name = "FlashConfig";
			this.Text = "FlashConfig";
			base.Load += new global::System.EventHandler(this.FlashConfig_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000005 RID: 5
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000006 RID: 6
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.TextBox txtScript;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Button btnScriptSelect;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.TextBox txtRaw;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.Button btnRawSelect;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400000D RID: 13
		private global::System.Windows.Forms.TextBox txtPatch;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.Button btnPatchSelect;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}
