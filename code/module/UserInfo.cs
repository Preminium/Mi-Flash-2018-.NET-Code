using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200004B RID: 75
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct UserInfo
	{
		// Token: 0x040001B2 RID: 434
		public IntPtr user_id;

		// Token: 0x040001B3 RID: 435
		public IntPtr user_name;

		// Token: 0x040001B4 RID: 436
		public IntPtr user_icon;

		// Token: 0x040001B5 RID: 437
		public int user_icon_len;
	}
}
