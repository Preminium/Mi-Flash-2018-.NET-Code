using System;

namespace XiaoMiFlash.code.lan
{
	// Token: 0x02000010 RID: 16
	public interface ILanguageSupport
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007B RID: 123
		// (set) Token: 0x0600007C RID: 124
		string LanID { get; set; }

		// Token: 0x0600007D RID: 125
		void SetLanguage();
	}
}
