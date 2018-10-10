using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000026 RID: 38
	public class FlashResult
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000BDCA File Offset: 0x00009FCA
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000BDD2 File Offset: 0x00009FD2
		public bool Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000BDDB File Offset: 0x00009FDB
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000BDE3 File Offset: 0x00009FE3
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

		// Token: 0x040000BD RID: 189
		private bool _result;

		// Token: 0x040000BE RID: 190
		private string _msg;
	}
}
