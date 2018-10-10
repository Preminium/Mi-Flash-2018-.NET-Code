using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000027 RID: 39
	public class ScriptDevice : DeviceCtrl
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public override void flash()
		{
			try
			{
				Log.w(this.deviceName, string.Format("Thread id:{0} Thread name:{1}", Thread.CurrentThread.ManagedThreadId.ToString(), Thread.CurrentThread.Name));
				string fastboot = Script.fastboot;
				string[] array = FileSearcher.SearchFiles(this.swPath, this.flashScript);
				if (array.Length == 0)
				{
					throw new Exception("can not found file " + this.flashScript);
				}
				string text = array[0];
				string format = "pushd \"{0}\"&&prompt $$&&set PATH=\"{1}\";%PATH%&&\"{2}\" -s {3}&&popd";
				string command = string.Format(format, new object[]
				{
					this.swPath,
					Script.AndroidPath,
					text,
					this.deviceName
				});
				Log.w(this.deviceName, "image path:" + this.swPath);
				Log.w(this.deviceName, "env android path:" + Script.AndroidPath);
				Log.w(this.deviceName, "script :" + text);
				Cmd cmd = new Cmd(this.deviceName, text);
				if (Convert.ToInt32(this.idproduct).ToString("x4") == "0a65" && Convert.ToInt32(this.idvendor).ToString("x4") == "8087")
				{
					string text2 = "DNX fastboot mode";
					Log.w(this.deviceName, text2);
					FlashingDevice.UpdateDeviceStatus(this.deviceName, null, text2, "boot into kernelflinger", false);
					string text3 = this.swPath + "\\images\\loader.efi";
					if (File.Exists(text3))
					{
						text2 = "Boot into kernelflinger " + text3;
						Log.w(this.deviceName, text2);
						FlashingDevice.UpdateDeviceStatus(this.deviceName, null, text2, text2, false);
						string format2 = "pushd \"{0}\"&&prompt $$&&set PATH=\"{1}\";%PATH%&&fastboot boot \"{2}\" -s {3}&&popd";
						string dosCommand = string.Format(format2, new object[]
						{
							this.swPath + "\\images",
							Script.AndroidPath,
							text3,
							this.deviceName
						});
						string msg = cmd.Execute(this.deviceName, dosCommand);
						Log.w(this.deviceName, msg);
						int num = 4;
						bool flag = false;
						while (num-- >= 0 && !flag)
						{
							List<string> list = UsbDevice.GetScriptDevice().ToList<string>();
							if (list.IndexOf(this.deviceName) >= 0)
							{
								flag = true;
								break;
							}
							Thread.Sleep(1000);
						}
						if (!flag)
						{
							Log.w(this.deviceName, "error:device couldn't boot up ");
						}
					}
					else
					{
						Log.w(this.deviceName, "error:couldn't find " + this.swPath + "\\loader.efi");
					}
				}
				cmd.Execute_returnLine(this.deviceName, command, 1);
			}
			catch (Exception ex)
			{
				FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex.Message, "error", true);
				Log.w(this.deviceName, ex, false);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000C114 File Offset: 0x0000A314
		public void GrapLog()
		{
			string text = "C:\\fastbootlog";
			if (!Directory.Exists(text.Replace("\"", "")))
			{
				Directory.CreateDirectory(text);
			}
			Cmd cmd = new Cmd(this.deviceName, "");
			string text2 = "lkmsg";
			FlashingDevice.UpdateDeviceStatus(this.deviceName, null, string.Format("start grab {0} log", text2), "grabbing log", false);
			string str = string.Format("{0}_{1}@{2}.txt", this.deviceName, text2, DateTime.Now.ToString("yyyyMdHms"));
			string arg = text + "\\" + str;
			string dosCommand = string.Format("\"{0}\" -s {1} oem lkmsg > {2}", Script.LKMSG_FASTBOOT, this.deviceName, arg);
			cmd.Execute(this.deviceName, dosCommand);
			FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), string.Format("grab done", text2), "grabbing log", false);
			text2 = "lpmsg";
			FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(0f), string.Format("start grab {0} log", text2), "grabbing log", false);
			str = string.Format("{0}_{1}@{2}.txt", this.deviceName, text2, DateTime.Now.ToString("yyyyMdHms"));
			arg = text + "\\" + str;
			dosCommand = string.Format("\"{0}\" -s {1} oem lpmsg > {2}", Script.LKMSG_FASTBOOT, this.deviceName, arg);
			cmd.Execute(this.deviceName, dosCommand);
			FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), "flash done", "grab done", true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public override string[] getDevice()
		{
			return UsbDevice.GetScriptDevice();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		public override void CheckSha256()
		{
		}
	}
}
