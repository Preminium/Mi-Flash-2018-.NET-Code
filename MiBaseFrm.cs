using System;
using System.Windows.Forms;
using XiaoMiFlash.code.lan;

namespace XiaoMiFlash
{
	// Token: 0x02000011 RID: 17
	public partial class MiBaseFrm : Form, ILanguageSupport
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000921E File Offset: 0x0000741E
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00009226 File Offset: 0x00007426
		public string LanID
		{
			get
			{
				return this.lanid;
			}
			set
			{
				this.lanid = value;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000922F File Offset: 0x0000742F
		public virtual void SetLanguage()
		{
		}

		// Token: 0x04000045 RID: 69
		private string lanid = "";
	}
}
