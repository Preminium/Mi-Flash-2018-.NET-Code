using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003B RID: 59
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_64b_readdata_packet
	{
		// Token: 0x04000178 RID: 376
		public uint Command;

		// Token: 0x04000179 RID: 377
		public uint Length;

		// Token: 0x0400017A RID: 378
		public ulong Image_id;

		// Token: 0x0400017B RID: 379
		public ulong Offset;

		// Token: 0x0400017C RID: 380
		public ulong SLength;
	}
}
