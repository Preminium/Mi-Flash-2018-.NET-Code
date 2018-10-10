using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MiUSB;
using XiaoMiFlash.code.bl;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000068 RID: 104
	public class UsbDevice
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00016439 File Offset: 0x00014639
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00016440 File Offset: 0x00014640
		public static List<string> MtkDevice
		{
			get
			{
				return UsbDevice._mtkdevice;
			}
			set
			{
				UsbDevice._mtkdevice = value;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00016448 File Offset: 0x00014648
		public static List<Device> GetDevice()
		{
			List<Device> list = new List<Device>();
			string a = MiAppConfig.Get("chip");
			if (a == "MTK")
			{
				using (List<string>.Enumerator enumerator = UsbDevice.MtkDevice.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						int index = int.Parse(text.ToLower().Replace("com", ""));
						list.Add(new Device
						{
							Index = index,
							Name = text,
							DeviceCtrl = new MTKDevice()
						});
					}
					return list;
				}
			}
			string[] devicesQc = ComPortCtrl.getDevicesQc();
			foreach (string text2 in devicesQc)
			{
				int num = int.Parse(text2.ToLower().Replace("com", ""));
				list.Add(new Device
				{
					Index = num,
					Name = text2,
					DeviceCtrl = new SerialPortDevice()
				});
			}
			List<TreeViewUsbItem> allUsbDevices = TreeViewUsbItem.AllUsbDevices;
			Log.w("GetScriptDevices");
			List<UsbNodeConnectionInformation> scriptDevices = UsbDevice.GetScriptDevices(allUsbDevices);
			List<string> list2 = UsbDevice.GetScriptDevice().ToList<string>();
			foreach (string text3 in list2)
			{
				int num = UsbDevice.GetDeviceIndex(text3);
				int num2 = 5;
				while (num <= 0 && num2-- >= 0)
				{
					if (num2 < 4)
					{
						Thread.Sleep(200);
					}
					num = UsbDevice.GetDeviceIndex(text3);
				}
				list.Add(new Device
				{
					Index = num,
					Name = text3,
					DeviceCtrl = new ScriptDevice()
				});
			}
			foreach (UsbNodeConnectionInformation usbNodeConnectionInformation in scriptDevices)
			{
				if (!string.IsNullOrEmpty(usbNodeConnectionInformation.DeviceDescriptor.SerialNumber) && list2.IndexOf(usbNodeConnectionInformation.DeviceDescriptor.SerialNumber) < 0)
				{
					int num = UsbDevice.GetDeviceIndex(usbNodeConnectionInformation.DeviceDescriptor.SerialNumber);
					int num3 = 10;
					while (num <= 0 && num3-- >= 0)
					{
						if (num3 < 9)
						{
							Thread.Sleep(200);
						}
						num = UsbDevice.GetDeviceIndex(usbNodeConnectionInformation.DeviceDescriptor.SerialNumber);
					}
					list.Add(new Device
					{
						Index = num,
						Name = usbNodeConnectionInformation.DeviceDescriptor.SerialNumber,
						DeviceCtrl = new ScriptDevice()
					});
				}
			}
			return list;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00016724 File Offset: 0x00014924
		public static string[] GetScriptDevice()
		{
			List<string> list = new List<string>();
			string fastboot = Script.fastboot;
			if (File.Exists(fastboot.Replace("\"", "")))
			{
				Cmd cmd = new Cmd("", "");
				string input = cmd.Execute(null, fastboot + " devices");
				string[] array = Regex.Split(input, "\r\n");
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						list.Add(Regex.Split(array[i], "\t")[0]);
					}
				}
				return list.ToArray();
			}
			throw new Exception("no fastboot.");
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000167D0 File Offset: 0x000149D0
		public static int GetQcDeviceIndex(string comName)
		{
			int num = 10;
			int num2 = 10;
			int num3 = int.Parse(comName.ToLower().Replace("com", ""));
			int num4 = num3;
			num4 -= num;
			num4 /= num2;
			num4++;
			return 0;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00016810 File Offset: 0x00014A10
		public static int GetBaseNum(string comName)
		{
			RegistryKey localMachine = Registry.LocalMachine;
			RegistryKey registryKey = localMachine.OpenSubKey("Software\\Wow6432Node\\XiaoMi\\MiFlash\\");
			registryKey.GetValue("test").ToString();
			registryKey.Close();
			return 0;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00016848 File Offset: 0x00014A48
		public static int GetDeviceIndex(string devicesSn)
		{
			int result;
			lock (UsbDevice.obj_lock)
			{
				int num = -1;
				string qcCoInstaller = Script.qcCoInstaller;
				if (File.Exists(qcCoInstaller.Replace("\"", "")))
				{
					string text = "";
					string text2 = "";
					try
					{
						Cmd cmd = new Cmd("", "");
						text2 = string.Format("rundll32.exe {0},qcGetDeviceIndex {1}", "qcCoInstaller.dll", devicesSn);
						text = cmd.Execute(null, text2);
						if (!string.IsNullOrEmpty(text))
						{
							num = Convert.ToInt32(text);
						}
					}
					catch (Exception ex)
					{
						Log.w(text2 + ":" + text);
						Log.w(ex.Message + ":" + ex.StackTrace);
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00016928 File Offset: 0x00014B28
		private static List<UsbNodeConnectionInformation> GetScriptDevices(List<TreeViewUsbItem> UsbItems)
		{
			List<UsbNodeConnectionInformation> result = new List<UsbNodeConnectionInformation>();
			foreach (TreeViewUsbItem item in UsbItems)
			{
				UsbDevice.GetAndroidDevices(item, ref result);
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00016980 File Offset: 0x00014B80
		private static void GetAndroidDevices(TreeViewUsbItem item, ref List<UsbNodeConnectionInformation> outItems)
		{
			try
			{
				if (item.Children != null && item.Children.Count > 0)
				{
					List<TreeViewUsbItem> children = item.Children;
					using (List<TreeViewUsbItem>.Enumerator enumerator = children.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TreeViewUsbItem item2 = enumerator.Current;
							UsbDevice.GetAndroidDevices(item2, ref outItems);
						}
						goto IL_1DF;
					}
				}
				UsbNodeConnectionInformation item3 = (UsbNodeConnectionInformation)item.Data;
				if (item3.DeviceDescriptor.Manufacturer != null && (item3.DeviceDescriptor.Product.ToLower() == "android" || item3.DeviceDescriptor.Product.ToLower() == "fastboot" || item3.DeviceDescriptor.Product.ToLower() == "intel android ad" || item3.DeviceDescriptor.Manufacturer.ToLower().IndexOf("xiaomi inc") >= 0 || Convert.ToInt32(item3.DeviceDescriptor.idVendor).ToString("x4") == "8087" || Convert.ToInt32(item3.DeviceDescriptor.idVendor).ToString("x4") == "0955" || Convert.ToInt32(item3.DeviceDescriptor.idVendor).ToString("x4").ToLower() == "05c6" || Convert.ToInt32(item3.DeviceDescriptor.idVendor).ToString("x4").ToLower() == "18d1" || Convert.ToInt32(item3.DeviceDescriptor.idVendor).ToString("x4") == "2717"))
				{
					outItems.Add(item3);
				}
				IL_1DF:;
			}
			catch (Exception ex)
			{
				Log.w(ex.Message + " " + ex.StackTrace);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00016BC0 File Offset: 0x00014DC0
		private static List<UsbNodeConnectionInformation> GetDeviceIndex(string[] deviceSn, List<TreeViewUsbItem> UsbItems)
		{
			List<UsbNodeConnectionInformation> list = new List<UsbNodeConnectionInformation>();
			foreach (TreeViewUsbItem item in UsbItems)
			{
				UsbDevice.GetLastChild(item, deviceSn, ref list);
				if (list.Count == deviceSn.Length)
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00016C24 File Offset: 0x00014E24
		private static TreeViewUsbItem GetLastChild(TreeViewUsbItem item, string[] deviceSn, ref List<UsbNodeConnectionInformation> outItems)
		{
			if (item.Children != null && item.Children.Count > 0)
			{
				List<TreeViewUsbItem> children = item.Children;
				using (List<TreeViewUsbItem>.Enumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TreeViewUsbItem item2 = enumerator.Current;
						UsbDevice.GetLastChild(item2, deviceSn, ref outItems);
					}
					return item;
				}
			}
			try
			{
				UsbNodeConnectionInformation item3 = (UsbNodeConnectionInformation)item.Data;
				if (item.Data != null && deviceSn.ToList<string>().Contains(item3.DeviceDescriptor.SerialNumber))
				{
					outItems.Add(item3);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Log.w(ex.Message + " " + ex.StackTrace);
			}
			return item;
		}

		// Token: 0x0400022B RID: 555
		private static object obj_lock = new object();

		// Token: 0x0400022C RID: 556
		private static List<string> _mtkdevice = new List<string>();
	}
}
