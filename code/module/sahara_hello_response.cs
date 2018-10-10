using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000039 RID: 57
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_hello_response
	{
		// Token: 0x0400016C RID: 364
		public uint Command;

		// Token: 0x0400016D RID: 365
		public uint Length;

		// Token: 0x0400016E RID: 366
		public uint Version;

		// Token: 0x0400016F RID: 367
		public uint Version_min;

		// Token: 0x04000170 RID: 368
		public uint Status;

		// Token: 0x04000171 RID: 369
		public uint Mode;

		// Token: 0x04000172 RID: 370
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public uint[] Reserved;
	}
}
