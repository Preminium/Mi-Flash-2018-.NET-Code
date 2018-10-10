using System;
using System.Diagnostics;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000032 RID: 50
	public class MiCmdProcess : Process
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		public new void Kill()
		{
			Log.w(base.ProcessName, "kill process:" + base.ProcessName);
			Log.w("kill process:" + base.ProcessName);
			base.Kill();
		}
	}
}
