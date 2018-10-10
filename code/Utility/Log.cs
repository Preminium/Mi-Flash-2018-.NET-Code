using System;
using System.IO;
using System.Text;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000031 RID: 49
	public class Log
	{
		// Token: 0x06000121 RID: 289 RVA: 0x0000E968 File Offset: 0x0000CB68
		public static void w(string deviceName, Exception ex, bool stopFlash)
		{
			string text = ex.Message;
			string str = ex.StackTrace;
			if (stopFlash)
			{
				text = "error:" + text;
				str = "error" + str;
			}
			Log.w(deviceName, text, stopFlash);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		public static void w(string deviceName, string msg)
		{
			Log.w(deviceName, msg, true);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		public static void w(string deviceName, string msg, bool throwEx)
		{
			if (string.IsNullOrEmpty(deviceName))
			{
				Log.w(msg);
				return;
			}
			string text = "";
			foreach (Device device in FlashingDevice.flashDeviceList)
			{
				if (device.Name == deviceName)
				{
					text = string.Format("{0}@{1}.txt", device.Name, device.StartTime.ToString("yyyyMdHms"));
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				Log.w(msg, true);
				return;
			}
			string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" + text;
			try
			{
				if (!File.Exists(path))
				{
					File.Create(path).Close();
				}
			}
			catch (Exception)
			{
				Log.w(msg);
				return;
			}
			string arg = DateTime.Now.ToLongTimeString();
			msg = string.Format("[{0}  {1}]:{2}", arg, deviceName, msg);
			Log.WriteFile(msg, path);
			if (msg.ToLower().IndexOf("error") >= 0 || msg.ToLower().IndexOf("fail") >= 0 || msg.ToLower().IndexOf("找不到批处理文件") >= 0)
			{
				FlashingDevice.UpdateDeviceStatus(deviceName, null, msg, "error", true);
				if (throwEx)
				{
					Log.w(string.Format("deivce:{0} error: {1}", deviceName, msg));
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public static void w(string msg)
		{
			Log.w(msg, false);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000EB34 File Offset: 0x0000CD34
		public static void w(string msg, bool throwEx)
		{
			string str = string.Format("{0}@{1}.txt", "miflash", DateTime.Now.ToString("yyyyMd"));
			string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" + str;
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
			msg = string.Format("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg);
			Log.WriteFile(msg, path);
			if ((msg.ToLower().IndexOf("error") >= 0 || msg.ToLower().IndexOf("fail") >= 0 || msg.ToLower().IndexOf("找不到批处理文件") >= 0) && throwEx)
			{
				throw new Exception(msg);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public static void wFlashStatus(string msg)
		{
			try
			{
				string str = string.Format("{0}-Result@{1}.txt", "MiFlash", DateTime.Now.ToString("yyyyMd"));
				string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" + str;
				if (!File.Exists(path))
				{
					File.Create(path).Close();
				}
				FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default);
				streamWriter.WriteLine(string.Format("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg));
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				Log.w(string.Format("wFlashStatus {0}  {1} {2}", msg, ex.Message, ex.StackTrace));
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public static void Installw(string installationPath, string msg)
		{
			string str = string.Format("{0}@{1}.txt", "miflash", DateTime.Now.ToString("yyyyMd"));
			string path = installationPath + "log\\" + str;
			if (!File.Exists(path))
			{
				File.Create(path).Close();
			}
			FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
			StreamWriter streamWriter = new StreamWriter(stream, Encoding.Default);
			streamWriter.WriteLine(string.Format("[{0}]:{1}", DateTime.Now.ToLongTimeString(), msg));
			streamWriter.Close();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public static void LogMsg(string deviceName, string msg, string suffix)
		{
			string str = string.Format("{0}_{1}@{2}.txt", deviceName, suffix, DateTime.Now.ToString("yyyyMdHms"));
			string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\" + str;
			try
			{
				if (!File.Exists(path))
				{
					File.Create(path).Close();
				}
			}
			catch (Exception)
			{
				Log.w(msg);
				return;
			}
			Log.WriteFile(msg, path);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		public static void WriteFile(string log, string path)
		{
			lock (Log._lock)
			{
				if (!File.Exists(path))
				{
					using (new FileStream(path, FileMode.Create))
					{
					}
				}
				using (FileStream fileStream2 = new FileStream(path, FileMode.Append, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream2))
					{
						string value = log.ToString();
						streamWriter.WriteLine(value);
						streamWriter.Flush();
					}
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000EE90 File Offset: 0x0000D090
		public static void WriteLog(string log, string logPath)
		{
			Log.WriteFile(log, logPath);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000EE99 File Offset: 0x0000D099
		public static void WriteErrorLog(string log, string logPath)
		{
			Log.WriteFile(log, logPath);
		}

		// Token: 0x04000104 RID: 260
		public static readonly object _lock = new object();
	}
}
