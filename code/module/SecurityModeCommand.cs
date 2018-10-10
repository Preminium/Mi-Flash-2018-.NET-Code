using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000045 RID: 69
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SecurityModeCommand
	{
		// Token: 0x040001A5 RID: 421
		public byte uCommand;

		// Token: 0x040001A6 RID: 422
		public byte uMode;
	}
}
