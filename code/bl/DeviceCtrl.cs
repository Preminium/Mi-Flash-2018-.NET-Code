using System;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x0200000B RID: 11
	public abstract class DeviceCtrl
	{
		// Token: 0x06000036 RID: 54
		public abstract void flash();

		// Token: 0x06000037 RID: 55
		public abstract string[] getDevice();

		// Token: 0x06000038 RID: 56
		public abstract void CheckSha256();

		// Token: 0x04000022 RID: 34
		public string swPath = "D:\\SW\\A1\\FDL153I\\images\\";

		// Token: 0x04000023 RID: 35
		public string scatter = "";

		// Token: 0x04000024 RID: 36
		public ushort idproduct;

		// Token: 0x04000025 RID: 37
		public ushort idvendor;

		// Token: 0x04000026 RID: 38
		public string da = "";

		// Token: 0x04000027 RID: 39
		public string dl_type = "";

		// Token: 0x04000028 RID: 40
		public string sha256Path = "";

		// Token: 0x04000029 RID: 41
		public string deviceName = "";

		// Token: 0x0400002A RID: 42
		public string flashScript;

		// Token: 0x0400002B RID: 43
		public bool readBackVerify;

		// Token: 0x0400002C RID: 44
		public bool openWriteDump;

		// Token: 0x0400002D RID: 45
		public bool openReadDump;

		// Token: 0x0400002E RID: 46
		public bool verbose;
	}
}
