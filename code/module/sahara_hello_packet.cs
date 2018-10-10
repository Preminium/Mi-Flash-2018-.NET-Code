using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000038 RID: 56
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_hello_packet
	{
		// Token: 0x04000165 RID: 357
		public uint Command;

		// Token: 0x04000166 RID: 358
		public uint Length;

		// Token: 0x04000167 RID: 359
		public uint Version;

		// Token: 0x04000168 RID: 360
		public uint Version_min;

		// Token: 0x04000169 RID: 361
		public uint Max_Command_Length;

		// Token: 0x0400016A RID: 362
		public uint Mode;

		// Token: 0x0400016B RID: 363
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public uint[] Reserved;
	}
}
