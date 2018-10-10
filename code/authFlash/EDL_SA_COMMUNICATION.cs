using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace XiaoMiFlash.code.authFlash
{
	// Token: 0x0200006A RID: 106
	public class EDL_SA_COMMUNICATION
	{
		// Token: 0x0600023E RID: 574
		[DllImport("iacj_interprocess_dll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern void IACJ_Interp_Init(string pThis, string pEventProc, string pDataProc);

		// Token: 0x0600023F RID: 575
		[DllImport("iacj_interprocess_dll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern void IACJ_Interp_Finz();

		// Token: 0x06000240 RID: 576
		[DllImport("iacj_interprocess_dll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int IACJ_Interp_SendMsg2Peer(byte[] pBuf);

		// Token: 0x06000241 RID: 577 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public static void DaConnect()
		{
			EDL_SA_COMMUNICATION.IACJ_Interp_Init(null, null, null);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00016DFA File Offset: 0x00014FFA
		public static void DaDisConnect()
		{
			EDL_SA_COMMUNICATION.IACJ_Interp_Finz();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00016E04 File Offset: 0x00015004
		public static PInvokeResultArg RspEdlResult(string deivce, bool reuslt)
		{
			string arg;
			if (reuslt)
			{
				arg = "pass";
			}
			else
			{
				arg = "fail";
			}
			string s = string.Format("DLRESULT={0}:{1}", deivce.Substring(3, deivce.Length - 3), arg);
			int result = EDL_SA_COMMUNICATION.IACJ_Interp_SendMsg2Peer(Encoding.Default.GetBytes(s));
			PInvokeResultArg pinvokeResultArg = new PInvokeResultArg();
			pinvokeResultArg.result = result;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			return pinvokeResultArg;
		}

		// Token: 0x0400022F RID: 559
		private static readonly object locker1 = new object();
	}
}
