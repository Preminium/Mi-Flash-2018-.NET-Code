using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using XiaoMiFlash.code.bl;

namespace XiaoMiFlash.form
{
	// Token: 0x02000016 RID: 22
	public partial class DriverFrm : Form
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000093C0 File Offset: 0x000075C0
		public DriverFrm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000093D0 File Offset: 0x000075D0
		private void DriverFrm_Load(object sender, EventArgs e)
		{
			bool flag = CultureInfo.InstalledUICulture.Name.ToLower().IndexOf("zh") >= 0;
			this.lblMsg.Text = (flag ? "请安装驱动" : "Please install driver");
			this.btnInstall.Text = (flag ? "安装" : "Install");
			MiDriver miDriver = new MiDriver();
			for (int i = 0; i < miDriver.infFiles.Length; i++)
			{
				Label label = this.lblDriver;
				label.Text += string.Format("({0}) {1}\r\n", i + 1, miDriver.infFiles[i]);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000947C File Offset: 0x0000767C
		private void btnInstall_Click(object sender, EventArgs e)
		{
			string text = this.btnInstall.Text;
			this.btnInstall.Text = "Wait......";
			this.btnInstall.Enabled = false;
			string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			MiDriver miDriver = new MiDriver();
			miDriver.CopyFiles(applicationBase);
			miDriver.InstallAllDriver(applicationBase, true);
			this.btnInstall.Text = text;
			this.btnInstall.Enabled = true;
			MessageBox.Show("Install done");
		}
	}
}
