using System;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x0200004E RID: 78
	public class MTKDevice : DeviceCtrl
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00014794 File Offset: 0x00012994
		public override void flash()
		{
			try
			{
				string str = Script.SP_Download_Tool_PATH + "\\SP_download_tool.exe";
				string text = " -scatter {0} -da {1} -dl_type {2} -usb_com {3} -chk_sum {4} -s_order {5} ";
				IniFile iniFile = new IniFile();
				int iniInt = iniFile.GetIniInt("bootrom", string.Format("com{0}", this.comPortIndex), 0);
				string text2;
				if (iniInt == 0)
				{
					text2 = str + string.Format(text, new object[]
					{
						this.scatter,
						this.da,
						this.dl_type,
						this.comPortIndex,
						this.chkSum.ToString(),
						this.sort
					});
				}
				else
				{
					text += " -usb_da_com {6}";
					text2 = str + string.Format(text, new object[]
					{
						this.scatter,
						this.da,
						this.dl_type,
						this.comPortIndex,
						this.chkSum.ToString(),
						this.sort,
						iniInt
					});
				}
				Log.w(text2);
				Cmd cmd = new Cmd("com" + this.comPortIndex.ToString(), "");
				cmd.consoleMode_Execute_returnLine(this.deviceName, text2, 1);
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0001492C File Offset: 0x00012B2C
		public override string[] getDevice()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00014933 File Offset: 0x00012B33
		public override void CheckSha256()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001BE RID: 446
		public int comPortIndex;

		// Token: 0x040001BF RID: 447
		public int chkSum;

		// Token: 0x040001C0 RID: 448
		public int sort;
	}
}
