using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XiaoMiFlash.code.data;

namespace XiaoMiFlash.form
{
	// Token: 0x02000034 RID: 52
	public partial class SwDownloadProgress : Form
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0001443F File Offset: 0x0001263F
		public SwDownloadProgress()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00014450 File Offset: 0x00012650
		private void DownloadProgress_Load(object sender, EventArgs e)
		{
			this.dlTimer.Interval = 1000;
			this.dlTimer.Enabled = true;
			this.progressBar.Value = 0;
			this.progressBar.Maximum = 100;
			this.lblPath.Text = MiFlashGlobal.Swdes.serverPath;
			this.lblSize.Text = MiFlashGlobal.Swdes.fileSize.ToString();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000144C4 File Offset: 0x000126C4
		private void dlTimer_Tick(object sender, EventArgs e)
		{
			if (MiFlashGlobal.Swdes == null)
			{
				return;
			}
			Uri uri = new Uri(MiFlashGlobal.Swdes.serverPath);
			this.lblPath.Text = uri.AbsoluteUri;
			this.lblSize.Text = (MiFlashGlobal.Swdes.fileSize / 1024.0).ToString() + " kb";
			this.lblLocalPath.Text = MiFlashGlobal.Swdes.localPath;
			this.progressBar.Value = (int)MiFlashGlobal.Swdes.percent;
			this.lblPercent.Text = string.Format("{0}%", MiFlashGlobal.Swdes.percent.ToString("#0.000"));
			if (MiFlashGlobal.Swdes.percent >= 100.0)
			{
				this.progressBar.Value = this.progressBar.Maximum;
			}
			if (MiFlashGlobal.Swdes.isDone)
			{
				this.dlTimer.Enabled = false;
				base.Close();
				base.Dispose();
			}
		}
	}
}
