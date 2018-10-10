using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000030 RID: 48
	public class ComPortCtrl
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000E77C File Offset: 0x0000C97C
		private static string[] getDevices()
		{
			List<string> list = new List<string>();
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\" and Name LIKE '%Qualcomm HS-USB QDLoader 9008%'");
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					string text = managementObject.GetPropertyValue("Name").ToString();
					string oldValue = managementObject.GetPropertyValue("Description").ToString();
					string text2 = text.Replace(oldValue, "").Replace('(', ' ').Replace(')', ' ');
					list.Add(text2.Trim());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Log.w(ex.Message);
			}
			return list.ToArray();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000E864 File Offset: 0x0000CA64
		public static string[] getDevicesQc()
		{
			List<string> list = new List<string>();
			Regex regex = new Regex("COM\\d+");
			string qcLsUsb = Script.QcLsUsb;
			Log.w("lsusb path:" + qcLsUsb);
			if (File.Exists(qcLsUsb.Replace("\"", "")))
			{
				Cmd cmd = new Cmd("", "");
				string text = cmd.Execute(null, qcLsUsb);
				Log.w("ls ubs :" + text);
				string[] array = Regex.Split(text, "\r\n");
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]) && array[i].IndexOf("9008") > 0)
					{
						Match match = regex.Match(array[i]);
						string value = match.Groups[0].Value;
						list.Add(value.Trim());
					}
				}
				return list.ToArray();
			}
			throw new Exception("no lsusb.");
		}
	}
}
