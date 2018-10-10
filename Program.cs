using System;
using System.Windows.Forms;

namespace XiaoMiFlash
{
	// Token: 0x0200000F RID: 15
	internal static class Program
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00009207 File Offset: 0x00007407
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainFrm());
		}
	}
}
