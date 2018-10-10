using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000042 RID: 66
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct HelloCommand
	{
		// Token: 0x04000195 RID: 405
		public byte uCommand;

		// Token: 0x04000196 RID: 406
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] uMagicNumber;

		// Token: 0x04000197 RID: 407
		public byte uVersionNumber;

		// Token: 0x04000198 RID: 408
		public byte uCompatibleVersion;

		// Token: 0x04000199 RID: 409
		public byte uFeatureBits;
	}
}
