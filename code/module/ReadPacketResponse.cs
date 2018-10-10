using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000044 RID: 68
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ReadPacketResponse
	{
		// Token: 0x040001A2 RID: 418
		public byte uResponse;

		// Token: 0x040001A3 RID: 419
		public int uAddress;

		// Token: 0x040001A4 RID: 420
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] uData;
	}
}
