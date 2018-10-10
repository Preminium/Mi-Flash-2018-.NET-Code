using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003D RID: 61
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_done_response
	{
		// Token: 0x04000181 RID: 385
		public uint Command;

		// Token: 0x04000182 RID: 386
		public uint Length;

		// Token: 0x04000183 RID: 387
		public uint Status;
	}
}
