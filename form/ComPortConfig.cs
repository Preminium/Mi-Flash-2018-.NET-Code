using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.form
{
	// Token: 0x02000060 RID: 96
	public partial class ComPortConfig : Form
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00015533 File Offset: 0x00013733
		public ComPortConfig()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00015544 File Offset: 0x00013744
		private void ComPortConfig_Load(object sender, EventArgs e)
		{
			this.mainFrm = (MainFrm)base.Owner;
			string text = MiAppConfig.Get("mtkComs");
			string[] array = text.Split(new char[]
			{
				','
			});
			int num = 0;
			foreach (object obj in this.groupBoxComs.Controls)
			{
				Control control = (Control)obj;
				if (num > array.Length - 1)
				{
					break;
				}
				if (control is TextBox)
				{
					TextBox textBox = control as TextBox;
					if (string.IsNullOrEmpty(textBox.Text))
					{
						textBox.Text = array[num];
						num++;
					}
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001560C File Offset: 0x0001380C
		private void btnOK_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (object obj in this.groupBoxComs.Controls)
			{
				Control control = (Control)obj;
				if (control is TextBox)
				{
					TextBox textBox = control as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text))
					{
						list.Add(textBox.Text.Trim());
					}
				}
			}
			list.Sort();
			string appValue = string.Join(",", list.ToArray());
			MiAppConfig.SetValue("mtkComs", appValue);
			base.Close();
		}

		// Token: 0x04000209 RID: 521
		private MainFrm mainFrm;
	}
}
