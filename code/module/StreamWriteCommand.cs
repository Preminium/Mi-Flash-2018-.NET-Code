using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200004A RID: 74
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StreamWriteCommand
	{
		// Token: 0x040001AF RID: 431
		public byte uCommand;

		// Token: 0x040001B0 RID: 432
		public int uAddress;

		// Token: 0x040001B1 RID: 433
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] uData;
	}
}
