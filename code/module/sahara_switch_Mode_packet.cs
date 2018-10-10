using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003E RID: 62
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct sahara_switch_Mode_packet
	{
		// Token: 0x04000184 RID: 388
		public uint Command;

		// Token: 0x04000185 RID: 389
		public uint Length;

		// Token: 0x04000186 RID: 390
		public uint Mode;
	}
}
