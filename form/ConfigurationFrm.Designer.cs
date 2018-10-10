namespace XiaoMiFlash.form
{
	// Token: 0x02000029 RID: 41
	public partial class ConfigurationFrm : global::System.Windows.Forms.Form
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x0000CA80 File Offset: 0x0000AC80
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::XiaoMiFlash.form.ConfigurationFrm));
			this.chkMD5 = new global::System.Windows.Forms.CheckBox();
			this.chkRbv = new global::System.Windows.Forms.CheckBox();
			this.chkWriteDump = new global::System.Windows.Forms.CheckBox();
			this.chkReadDump = new global::System.Windows.Forms.CheckBox();
			this.chkVerbose = new global::System.Windows.Forms.CheckBox();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.chkAutoDetect = new global::System.Windows.Forms.CheckBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.cmbFactory = new global::System.Windows.Forms.ComboBox();
			this.cmbChip = new global::System.Windows.Forms.ComboBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.cmbMainProgram = new global::System.Windows.Forms.ComboBox();
			this.btnPatchSelect = new global::System.Windows.Forms.Button();
			this.btnRawSelect = new global::System.Windows.Forms.Button();
			this.txtPatch = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.txtRaw = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.openFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.txtCheckPoint = new global::System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.chkMD5.AutoSize = true;
			this.chkMD5.Location = new global::System.Drawing.Point(6, 20);
			this.chkMD5.Name = "chkMD5";
			this.chkMD5.Size = new global::System.Drawing.Size(156, 16);
			this.chkMD5.TabIndex = 0;
			this.chkMD5.Text = "check MD5 before flash";
			this.chkMD5.UseVisualStyleBackColor = true;
			this.chkMD5.CheckedChanged += new global::System.EventHandler(this.chkMD5_CheckedChanged);
			this.chkRbv.AutoSize = true;
			this.chkRbv.Location = new global::System.Drawing.Point(6, 87);
			this.chkRbv.Name = "chkRbv";
			this.chkRbv.Size = new global::System.Drawing.Size(120, 16);
			this.chkRbv.TabIndex = 1;
			this.chkRbv.Text = "Read Back Verify";
			this.chkRbv.UseVisualStyleBackColor = true;
			this.chkRbv.CheckedChanged += new global::System.EventHandler(this.chkRbv_CheckedChanged);
			this.chkWriteDump.AutoSize = true;
			this.chkWriteDump.Location = new global::System.Drawing.Point(6, 121);
			this.chkWriteDump.Name = "chkWriteDump";
			this.chkWriteDump.Size = new global::System.Drawing.Size(114, 16);
			this.chkWriteDump.TabIndex = 2;
			this.chkWriteDump.Text = "Open Write Dump";
			this.chkWriteDump.UseVisualStyleBackColor = true;
			this.chkWriteDump.CheckedChanged += new global::System.EventHandler(this.chkWriteDump_CheckedChanged);
			this.chkReadDump.AutoSize = true;
			this.chkReadDump.Location = new global::System.Drawing.Point(6, 52);
			this.chkReadDump.Name = "chkReadDump";
			this.chkReadDump.Size = new global::System.Drawing.Size(108, 16);
			this.chkReadDump.TabIndex = 2;
			this.chkReadDump.Text = "Open Read Dump";
			this.chkReadDump.UseVisualStyleBackColor = true;
			this.chkReadDump.CheckedChanged += new global::System.EventHandler(this.chkReadDump_CheckedChanged);
			this.chkVerbose.AutoSize = true;
			this.chkVerbose.Location = new global::System.Drawing.Point(6, 155);
			this.chkVerbose.Name = "chkVerbose";
			this.chkVerbose.Size = new global::System.Drawing.Size(66, 16);
			this.chkVerbose.TabIndex = 3;
			this.chkVerbose.Text = "Verbose";
			this.chkVerbose.UseVisualStyleBackColor = true;
			this.chkVerbose.CheckedChanged += new global::System.EventHandler(this.chkVerbose_CheckedChanged);
			this.btnOK.Location = new global::System.Drawing.Point(53, 460);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new global::System.EventHandler(this.btnOK_Click);
			this.chkAutoDetect.AutoSize = true;
			this.chkAutoDetect.Location = new global::System.Drawing.Point(228, 20);
			this.chkAutoDetect.Name = "chkAutoDetect";
			this.chkAutoDetect.Size = new global::System.Drawing.Size(186, 16);
			this.chkAutoDetect.TabIndex = 5;
			this.chkAutoDetect.Text = "Detect Device Automatically";
			this.chkAutoDetect.UseVisualStyleBackColor = true;
			this.chkAutoDetect.Visible = false;
			this.chkAutoDetect.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(228, 52);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(53, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "Factory:";
			this.cmbFactory.FormattingEnabled = true;
			this.cmbFactory.Items.AddRange(new object[]
			{
				"please choose",
				"Longcheer",
				"Foxconn",
				"Inventec",
				"Hipad",
				"PTSN",
				"Zowee",
				"test"
			});
			this.cmbFactory.Location = new global::System.Drawing.Point(287, 50);
			this.cmbFactory.Name = "cmbFactory";
			this.cmbFactory.Size = new global::System.Drawing.Size(121, 20);
			this.cmbFactory.TabIndex = 7;
			this.cmbFactory.SelectedValueChanged += new global::System.EventHandler(this.cmbFactory_SelectedValueChanged);
			this.cmbChip.FormattingEnabled = true;
			this.cmbChip.Items.AddRange(new object[]
			{
				"Qualcomm",
				"MTK"
			});
			this.cmbChip.Location = new global::System.Drawing.Point(287, 87);
			this.cmbChip.Name = "cmbChip";
			this.cmbChip.Size = new global::System.Drawing.Size(121, 20);
			this.cmbChip.TabIndex = 8;
			this.cmbChip.SelectedValueChanged += new global::System.EventHandler(this.cmbChip_SelectedValueChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(246, 90);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(35, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "Chip:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(204, 121);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(83, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "Main program:";
			this.cmbMainProgram.FormattingEnabled = true;
			this.cmbMainProgram.Items.AddRange(new object[]
			{
				"xiaomi",
				"fh_loader"
			});
			this.cmbMainProgram.Location = new global::System.Drawing.Point(288, 121);
			this.cmbMainProgram.Name = "cmbMainProgram";
			this.cmbMainProgram.Size = new global::System.Drawing.Size(121, 20);
			this.cmbMainProgram.TabIndex = 11;
			this.cmbMainProgram.SelectedValueChanged += new global::System.EventHandler(this.cmbMainProgram_SelectedValueChanged);
			this.btnPatchSelect.Location = new global::System.Drawing.Point(440, 386);
			this.btnPatchSelect.Name = "btnPatchSelect";
			this.btnPatchSelect.Size = new global::System.Drawing.Size(75, 23);
			this.btnPatchSelect.TabIndex = 16;
			this.btnPatchSelect.Text = "select";
			this.btnPatchSelect.UseVisualStyleBackColor = true;
			this.btnPatchSelect.Click += new global::System.EventHandler(this.btnPatchSelect_Click);
			this.btnRawSelect.Location = new global::System.Drawing.Point(440, 331);
			this.btnRawSelect.Name = "btnRawSelect";
			this.btnRawSelect.Size = new global::System.Drawing.Size(75, 23);
			this.btnRawSelect.TabIndex = 17;
			this.btnRawSelect.Text = "select";
			this.btnRawSelect.UseVisualStyleBackColor = true;
			this.btnRawSelect.Click += new global::System.EventHandler(this.btnRawSelect_Click);
			this.txtPatch.Location = new global::System.Drawing.Point(127, 388);
			this.txtPatch.Name = "txtPatch";
			this.txtPatch.Size = new global::System.Drawing.Size(293, 21);
			this.txtPatch.TabIndex = 15;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(50, 391);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(71, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "Patch xml：";
			this.txtRaw.Location = new global::System.Drawing.Point(127, 333);
			this.txtRaw.Name = "txtRaw";
			this.txtRaw.Size = new global::System.Drawing.Size(293, 21);
			this.txtRaw.TabIndex = 14;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(20, 336);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(101, 12);
			this.label5.TabIndex = 13;
			this.label5.Text = "RawProgram xml：";
			this.openFileDialog.Filter = "xml|*.xml";
			this.groupBox1.Controls.Add(this.chkMD5);
			this.groupBox1.Controls.Add(this.chkRbv);
			this.groupBox1.Controls.Add(this.chkWriteDump);
			this.groupBox1.Controls.Add(this.chkReadDump);
			this.groupBox1.Controls.Add(this.chkVerbose);
			this.groupBox1.Controls.Add(this.chkAutoDetect);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmbFactory);
			this.groupBox1.Controls.Add(this.cmbChip);
			this.groupBox1.Controls.Add(this.cmbMainProgram);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new global::System.Drawing.Point(40, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(470, 207);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(51, 241);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(77, 12);
			this.label6.TabIndex = 21;
			this.label6.Text = "CheckPoint：";
			this.txtCheckPoint.Location = new global::System.Drawing.Point(127, 238);
			this.txtCheckPoint.Name = "txtCheckPoint";
			this.txtCheckPoint.Size = new global::System.Drawing.Size(160, 21);
			this.txtCheckPoint.TabIndex = 22;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(548, 551);
			base.Controls.Add(this.txtCheckPoint);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.btnPatchSelect);
			base.Controls.Add(this.btnRawSelect);
			base.Controls.Add(this.txtPatch);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.txtRaw);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.btnOK);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ConfigurationFrm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configuration";
			base.Load += new global::System.EventHandler(this.ConfigurationFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000C1 RID: 193
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000C2 RID: 194
		private global::System.Windows.Forms.CheckBox chkMD5;

		// Token: 0x040000C3 RID: 195
		private global::System.Windows.Forms.CheckBox chkRbv;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.CheckBox chkWriteDump;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.CheckBox chkReadDump;

		// Token: 0x040000C6 RID: 198
		private global::System.Windows.Forms.CheckBox chkVerbose;

		// Token: 0x040000C7 RID: 199
		private global::System.Windows.Forms.Button btnOK;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.CheckBox chkAutoDetect;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.ComboBox cmbFactory;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.ComboBox cmbChip;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.ComboBox cmbMainProgram;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.Button btnPatchSelect;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.Button btnRawSelect;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.TextBox txtPatch;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040000D3 RID: 211
		private global::System.Windows.Forms.TextBox txtRaw;

		// Token: 0x040000D4 RID: 212
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;

		// Token: 0x040000D6 RID: 214
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x040000D7 RID: 215
		private global::System.Windows.Forms.Label label6;

		// Token: 0x040000D8 RID: 216
		private global::System.Windows.Forms.TextBox txtCheckPoint;
	}
}
