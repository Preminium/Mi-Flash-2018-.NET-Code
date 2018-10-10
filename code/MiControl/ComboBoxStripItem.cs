using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace XiaoMiFlash.code.MiControl
{
	// Token: 0x0200005B RID: 91
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ComboBoxStripItem : ToolStripControlHost
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00014FB8 File Offset: 0x000131B8
		public ComboBoxStripItem() : base(new ComboBox())
		{
			this.comboBox = (base.Control as ComboBox);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00014FD8 File Offset: 0x000131D8
		public void SetItem(string[] items)
		{
			this.comboBox.Items.Clear();
			for (int i = 0; i < items.Length; i++)
			{
				this.comboBox.Items.Add(items[i]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00015017 File Offset: 0x00013217
		public string SelectedText
		{
			get
			{
				if (this.comboBox.SelectedItem != null)
				{
					return this.comboBox.SelectedItem.ToString();
				}
				return "";
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001503C File Offset: 0x0001323C
		public void SetText(string text)
		{
			bool flag = false;
			foreach (object obj in this.comboBox.Items)
			{
				if (obj.ToString() == text)
				{
					this.comboBox.SelectedItem = obj;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.comboBox.SelectedItem = null;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000150C0 File Offset: 0x000132C0
		public void Enable(bool enable)
		{
			this.comboBox.Enabled = enable;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000150CE File Offset: 0x000132CE
		public void OnlyDrop()
		{
			this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		// Token: 0x040001F5 RID: 501
		private ComboBox comboBox;
	}
}
