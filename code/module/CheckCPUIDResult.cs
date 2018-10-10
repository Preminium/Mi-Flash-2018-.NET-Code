using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000005 RID: 5
	public class CheckCPUIDResult
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00003CC8 File Offset: 0x00001EC8
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public string Device
		{
			get
			{
				return this._device;
			}
			set
			{
				this._device = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003CD9 File Offset: 0x00001ED9
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public bool Result
		{
			get
			{
				return this._bool;
			}
			set
			{
				this._bool = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003CEA File Offset: 0x00001EEA
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00003CF2 File Offset: 0x00001EF2
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003CFB File Offset: 0x00001EFB
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00003D03 File Offset: 0x00001F03
		public string Msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}

		// Token: 0x04000016 RID: 22
		private string _device;

		// Token: 0x04000017 RID: 23
		private bool _bool;

		// Token: 0x04000018 RID: 24
		private string _path;

		// Token: 0x04000019 RID: 25
		private string _msg;
	}
}
