using System;
using System.Collections.Generic;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.data
{
	// Token: 0x02000015 RID: 21
	public class MiFlashGlobal
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000937C File Offset: 0x0000757C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00009383 File Offset: 0x00007583
		public static string Version
		{
			get
			{
				return MiFlashGlobal._version;
			}
			set
			{
				MiFlashGlobal._version = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000938B File Offset: 0x0000758B
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00009392 File Offset: 0x00007592
		public static bool IsFactory
		{
			get
			{
				return MiFlashGlobal._isfactory;
			}
			set
			{
				MiFlashGlobal._isfactory = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000939A File Offset: 0x0000759A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000093A1 File Offset: 0x000075A1
		public static List<string> DownloadTag
		{
			get
			{
				return MiFlashGlobal._downloadTag;
			}
			set
			{
				MiFlashGlobal._downloadTag = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000093A9 File Offset: 0x000075A9
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000093B0 File Offset: 0x000075B0
		public static SwDes Swdes
		{
			get
			{
				return MiFlashGlobal._swdes;
			}
			set
			{
				MiFlashGlobal._swdes = value;
			}
		}

		// Token: 0x04000062 RID: 98
		private static string _version;

		// Token: 0x04000063 RID: 99
		private static bool _isfactory;

		// Token: 0x04000064 RID: 100
		private static List<string> _downloadTag;

		// Token: 0x04000065 RID: 101
		private static SwDes _swdes;
	}
}
