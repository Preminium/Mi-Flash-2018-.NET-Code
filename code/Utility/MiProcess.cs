using System;
using System.Diagnostics;
using System.Linq;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200002A RID: 42
	public class MiProcess
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000D838 File Offset: 0x0000BA38
		public static void KillProcess(string processName)
		{
			try
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				foreach (Process process in processesByName.ToList<Process>())
				{
					process.Kill();
					process.Dispose();
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}
	}
}
