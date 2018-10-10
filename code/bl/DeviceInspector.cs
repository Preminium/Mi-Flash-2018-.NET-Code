using System;
using System.Text.RegularExpressions;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x0200005F RID: 95
	public class DeviceInspector
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00015410 File Offset: 0x00013610
		public static bool checkLockStatus(string device, out string msg)
		{
			msg = "";
			Cmd cmd = new Cmd(device, "");
			string dosCommand = string.Format("fastboot -s {0} getvar token", device);
			string text = cmd.Execute(device, dosCommand);
			string[] array = Regex.Split(text, "\\r\\n");
			foreach (string text2 in array)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					string b = "token:";
					if (text2.IndexOf("token:") >= 0)
					{
						if (text2.Trim() == b)
						{
							msg = "token is null.";
							return false;
						}
						break;
					}
				}
			}
			dosCommand = string.Format("fastboot -s {0} oem device-info", device);
			text = cmd.Execute(device, dosCommand);
			string[] array3 = new string[]
			{
				"Device unlocked: false",
				" Device critical unlocked: false"
			};
			bool flag = true;
			foreach (string value in array3)
			{
				flag = (flag && text.IndexOf(value) >= 0);
				if (!flag)
				{
					break;
				}
			}
			if (!flag)
			{
				msg = "device is unlocked!";
			}
			return flag;
		}
	}
}
