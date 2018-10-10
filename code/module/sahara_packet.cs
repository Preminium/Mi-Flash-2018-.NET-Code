using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000037 RID: 55
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_packet
	{
		// Token: 0x04000163 RID: 355
		public uint Command;

		// Token: 0x04000164 RID: 356
		public uint Length;
	}
}
