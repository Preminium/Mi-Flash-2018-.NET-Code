using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200003F RID: 63
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SparseImageHeader
	{
		// Token: 0x04000187 RID: 391
		public uint uMagic;

		// Token: 0x04000188 RID: 392
		public ushort uMajorVersion;

		// Token: 0x04000189 RID: 393
		public ushort uMinorVersion;

		// Token: 0x0400018A RID: 394
		public ushort uFileHeaderSize;

		// Token: 0x0400018B RID: 395
		public ushort uChunkHeaderSize;

		// Token: 0x0400018C RID: 396
		public uint uBlockSize;

		// Token: 0x0400018D RID: 397
		public uint uTotalBlocks;

		// Token: 0x0400018E RID: 398
		public uint uTotalChunks;

		// Token: 0x0400018F RID: 399
		public uint uImageChecksum;
	}
}
