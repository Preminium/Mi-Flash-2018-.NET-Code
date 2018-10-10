using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XiaoMiFlash.code.bl;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.form
{
	// Token: 0x02000029 RID: 41
	public partial class ConfigurationFrm : Form
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000C61D File Offset: 0x0000A81D
		public ConfigurationFrm()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000C62C File Offset: 0x0000A82C
		private void chkMD5_CheckedChanged(object sender, EventArgs e)
		{
			MiAppConfig.SetValue("checkMD5", this.chkMD5.Checked.ToString());
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C658 File Offset: 0x0000A858
		private void ConfigurationFrm_Load(object sender, EventArgs e)
		{
			this.chkMD5.Checked = (MiAppConfig.GetAppConfig("checkMD5").ToLower() == "true");
			this.mainFrm = (MainFrm)base.Owner;
			this.chkRbv.Checked = this.mainFrm.ReadBackVerify;
			this.chkWriteDump.Checked = this.mainFrm.OpenWriteDump;
			this.chkReadDump.Checked = this.mainFrm.OpenReadDump;
			this.chkVerbose.Checked = this.mainFrm.Verbose;
			this.chkAutoDetect.Checked = this.mainFrm.AutoDetectDevice;
			this.cmbFactory.SelectedItem = MiAppConfig.Get("factory").ToString();
			this.cmbChip.SelectedItem = MiAppConfig.Get("chip").ToString();
			this.cmbMainProgram.SelectedItem = MiAppConfig.Get("mainProgram").ToString();
			this.openFileDialog.InitialDirectory = this.mainFrm.SwPath;
			this.txtRaw.Text = MiAppConfig.Get("rawprogram").ToString();
			this.txtPatch.Text = MiAppConfig.Get("patch").ToString();
			this.txtCheckPoint.Text = MiAppConfig.Get("checkPoint").ToString();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		private void chkRbv_CheckedChanged(object sender, EventArgs e)
		{
			this.mainFrm.ReadBackVerify = this.chkRbv.Checked;
			if (this.mainFrm.ReadBackVerify)
			{
				this.chkReadDump.Checked = true;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		private void chkWriteDump_CheckedChanged(object sender, EventArgs e)
		{
			this.mainFrm.OpenWriteDump = this.chkWriteDump.Checked;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000C803 File Offset: 0x0000AA03
		private void chkReadDump_CheckedChanged(object sender, EventArgs e)
		{
			this.mainFrm.OpenReadDump = this.chkReadDump.Checked;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000C81B File Offset: 0x0000AA1B
		private void chkVerbose_CheckedChanged(object sender, EventArgs e)
		{
			this.mainFrm.Verbose = this.chkVerbose.Checked;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000C834 File Offset: 0x0000AA34
		private void btnOK_Click(object sender, EventArgs e)
		{
			MiAppConfig.SetValue("rawprogram", this.txtRaw.Text.Trim());
			MiAppConfig.SetValue("patch", this.txtPatch.Text.Trim());
			MiAppConfig.SetValue("checkPoint", this.txtCheckPoint.Text.Trim());
			base.Close();
			base.Dispose();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000C89C File Offset: 0x0000AA9C
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.mainFrm.AutoDetectDevice = this.chkAutoDetect.Checked;
			this.mainFrm.AutoDetectUsb();
			Log.w("set AutoDetectDevice " + this.chkAutoDetect.Checked.ToString());
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private void cmbFactory_SelectedValueChanged(object sender, EventArgs e)
		{
			string text = this.cmbFactory.SelectedItem.ToString();
			if (!(text != "please choose"))
			{
				MiAppConfig.SetValue("factory", "");
				this.mainFrm.factory = "";
				this.mainFrm.SetFactory("");
				return;
			}
			if (FactoryCtrl.SetFactory(text))
			{
				MiAppConfig.SetValue("factory", text);
				this.mainFrm.factory = text;
				this.mainFrm.SetFactory(text);
				return;
			}
			MessageBox.Show("set factory failed!");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000C980 File Offset: 0x0000AB80
		private void cmbChip_SelectedValueChanged(object sender, EventArgs e)
		{
			string text = this.cmbChip.SelectedItem.ToString();
			MiAppConfig.SetValue("chip", text);
			this.mainFrm.chip = text;
			this.mainFrm.SetChip(text);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		private void cmbMainProgram_SelectedValueChanged(object sender, EventArgs e)
		{
			string appValue = this.cmbMainProgram.SelectedItem.ToString();
			MiAppConfig.SetValue("mainProgram", appValue);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		private void btnRawSelect_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.openFileDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				string fileName = this.openFileDialog.FileName;
				if (!string.IsNullOrEmpty(fileName))
				{
					this.txtRaw.Text = fileName;
					MiAppConfig.SetValue("rawprogram", fileName);
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000CA38 File Offset: 0x0000AC38
		private void btnPatchSelect_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.openFileDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				string fileName = this.openFileDialog.FileName;
				if (!string.IsNullOrEmpty(fileName))
				{
					this.txtPatch.Text = fileName;
					MiAppConfig.SetValue("patch", fileName);
				}
			}
		}

		// Token: 0x040000C0 RID: 192
		private MainFrm mainFrm;
	}
}
