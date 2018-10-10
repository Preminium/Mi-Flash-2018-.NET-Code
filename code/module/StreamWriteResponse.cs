using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000049 RID: 73
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct StreamWriteResponse
	{
		// Token: 0x040001AD RID: 429
		public byte uResponse;

		// Token: 0x040001AE RID: 430
		public int uAddress;
	}
}
