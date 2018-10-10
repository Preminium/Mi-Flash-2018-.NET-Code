using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003C RID: 60
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_end_transfer_packet
	{
		// Token: 0x0400017D RID: 381
		public uint Command;

		// Token: 0x0400017E RID: 382
		public uint Length;

		// Token: 0x0400017F RID: 383
		public uint Image_id;

		// Token: 0x04000180 RID: 384
		public uint Status;
	}
}
