using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace XiaoMiFlash.code.MiControl
{
	// Token: 0x02000006 RID: 6
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class RadioStripItem : ToolStripControlHost
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00003D14 File Offset: 0x00001F14
		public RadioStripItem() : base(new RadioButton())
		{
			this.radio = (base.Control as RadioButton);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003D32 File Offset: 0x00001F32
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00003D3F File Offset: 0x00001F3F
		public bool IsChecked
		{
			get
			{
				return this.radio.Checked;
			}
			set
			{
				this.radio.Checked = value;
			}
		}

		// Token: 0x0400001A RID: 26
		private RadioButton radio;
	}
}
