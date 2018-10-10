using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000043 RID: 67
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct HelloResponse
	{
		// Token: 0x0400019A RID: 410
		public byte uResponse;

		// Token: 0x0400019B RID: 411
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] uMagicNumber;

		// Token: 0x0400019C RID: 412
		public byte uVersionNumber;

		// Token: 0x0400019D RID: 413
		public byte uCompatibleVersion;

		// Token: 0x0400019E RID: 414
		public uint uMaxBlockSize;

		// Token: 0x0400019F RID: 415
		public uint uFlashBaseAddress;

		// Token: 0x040001A0 RID: 416
		public byte uFlashIdLength;

		// Token: 0x040001A1 RID: 417
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] uVariantBuffer;
	}
}
