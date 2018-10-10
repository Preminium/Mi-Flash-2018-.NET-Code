using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XiaoMiFlash.form
{
	// Token: 0x02000009 RID: 9
	public partial class ValidationFrm : Form
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00004175 File Offset: 0x00002375
		public ValidationFrm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004184 File Offset: 0x00002384
		private void btnCheckAll_Click(object sender, EventArgs e)
		{
			MainFrm mainFrm = (MainFrm)base.Owner;
			mainFrm.ValidateSpecifyXml = "";
			mainFrm.RefreshDevice();
			mainFrm.CheckSha256();
			base.Close();
			base.Dispose();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000041C0 File Offset: 0x000023C0
		private void btnSpecify_Click(object sender, EventArgs e)
		{
			MainFrm mainFrm = (MainFrm)base.Owner;
			this.openXmlFile.InitialDirectory = mainFrm.SwPath;
			DialogResult dialogResult = this.openXmlFile.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				string fileName = this.openXmlFile.FileName;
				mainFrm.ValidateSpecifyXml = fileName;
				mainFrm.RefreshDevice();
				mainFrm.CheckSha256();
				base.Close();
				base.Dispose();
				return;
			}
			MessageBox.Show("Please select a xml file.");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004231 File Offset: 0x00002431
		private void ValidationFrm_Load(object sender, EventArgs e)
		{
		}
	}
}
