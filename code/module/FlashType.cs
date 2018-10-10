using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000057 RID: 87
	public struct FlashType
	{
		// Token: 0x040001E1 RID: 481
		public static string CleanAll = "flash_all.bat";

		// Token: 0x040001E2 RID: 482
		public static string SaveUserData = "flash_all_except.*\\.bat";

		// Token: 0x040001E3 RID: 483
		public static string CleanAllAndLock = "flash_all_lock.bat";

		// Token: 0x040001E4 RID: 484
		public static string flash_all_lock_crc = "flash_all_lock_crc.bat";
	}
}
