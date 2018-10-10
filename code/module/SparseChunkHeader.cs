using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000040 RID: 64
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SparseChunkHeader
	{
		// Token: 0x04000190 RID: 400
		public ushort uChunkType;

		// Token: 0x04000191 RID: 401
		public ushort uReserved1;

		// Token: 0x04000192 RID: 402
		public uint uChunkSize;

		// Token: 0x04000193 RID: 403
		public uint uTotalSize;
	}
}
