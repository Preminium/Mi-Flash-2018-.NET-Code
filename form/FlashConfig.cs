using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XiaoMiFlash.form
{
	// Token: 0x02000003 RID: 3
	public partial class FlashConfig : Form
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000223A File Offset: 0x0000043A
		public FlashConfig()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002248 File Offset: 0x00000448
		private void FlashConfig_Load(object sender, EventArgs e)
		{
			this.mainFrm = (MainFrm)base.Owner;
			this.openFileDialog.InitialDirectory = this.mainFrm.SwPath;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002274 File Offset: 0x00000474
		private void btnRawSelect_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.openFileDialog.ShowDialog();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002294 File Offset: 0x00000494
		private void btnPatchSelect_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.openFileDialog.ShowDialog();
		}

		// Token: 0x04000004 RID: 4
		private MainFrm mainFrm;
	}
}
