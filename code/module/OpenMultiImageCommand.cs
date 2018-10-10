using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000047 RID: 71
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct OpenMultiImageCommand
	{
		// Token: 0x040001A8 RID: 424
		public byte uCommand;

		// Token: 0x040001A9 RID: 425
		public byte uType;

		// Token: 0x040001AA RID: 426
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] uData;
	}
}
