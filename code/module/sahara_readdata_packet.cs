using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003A RID: 58
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_readdata_packet
	{
		// Token: 0x04000173 RID: 371
		public uint Command;

		// Token: 0x04000174 RID: 372
		public uint Length;

		// Token: 0x04000175 RID: 373
		public uint Image_id;

		// Token: 0x04000176 RID: 374
		public uint Offset;

		// Token: 0x04000177 RID: 375
		public uint SLength;
	}
}
