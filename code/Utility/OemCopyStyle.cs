﻿using System;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200001B RID: 27
	public enum OemCopyStyle
	{
		// Token: 0x04000081 RID: 129
		SP_COPY_DELETESOURCE = 1,
		// Token: 0x04000082 RID: 130
		SP_COPY_REPLACEONLY,
		// Token: 0x04000083 RID: 131
		SP_COPY_NEWER = 4,
		// Token: 0x04000084 RID: 132
		SP_COPY_NEWER_OR_SAME = 4,
		// Token: 0x04000085 RID: 133
		SP_COPY_NOOVERWRITE = 8,
		// Token: 0x04000086 RID: 134
		SP_COPY_NODECOMP = 16,
		// Token: 0x04000087 RID: 135
		SP_COPY_LANGUAGEAWARE = 32,
		// Token: 0x04000088 RID: 136
		SP_COPY_SOURCE_ABSOLUTE = 64,
		// Token: 0x04000089 RID: 137
		SP_COPY_SOURCEPATH_ABSOLUTE = 128,
		// Token: 0x0400008A RID: 138
		SP_COPY_IN_USE_NEEDS_REBOOT = 256,
		// Token: 0x0400008B RID: 139
		SP_COPY_FORCE_IN_USE = 512,
		// Token: 0x0400008C RID: 140
		SP_COPY_NOSKIP = 1024,
		// Token: 0x0400008D RID: 141
		SP_FLAG_CABINETCONTINUATION = 2048,
		// Token: 0x0400008E RID: 142
		SP_COPY_FORCE_NOOVERWRITE = 4096,
		// Token: 0x0400008F RID: 143
		SP_COPY_FORCE_NEWER = 8192,
		// Token: 0x04000090 RID: 144
		SP_COPY_WARNIFSKIP = 16384,
		// Token: 0x04000091 RID: 145
		SP_COPY_NOBROWSE = 32768,
		// Token: 0x04000092 RID: 146
		SP_COPY_NEWER_ONLY = 65536,
		// Token: 0x04000093 RID: 147
		SP_COPY_SOURCE_SIS_MASTER = 131072,
		// Token: 0x04000094 RID: 148
		SP_COPY_OEMINF_CATALOG_ONLY = 262144,
		// Token: 0x04000095 RID: 149
		SP_COPY_REPLACE_BOOT_FILE = 524288,
		// Token: 0x04000096 RID: 150
		SP_COPY_NOPRUNE = 1048576
	}
}
