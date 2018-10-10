using System;
using System.Collections.Generic;
using System.Linq;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.data
{
	// Token: 0x0200000A RID: 10
	public static class FlashingDevice
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00004234 File Offset: 0x00002434
		public static void UpdateDeviceStatus(string deviceName, float? progress, string status, string result, bool isDone)
		{
			try
			{
				foreach (Device device in FlashingDevice.flashDeviceList)
				{
					if (device.Name == deviceName)
					{
						if (!string.IsNullOrEmpty(status))
						{
							device.StatusList.Add(status);
						}
						if (progress != null)
						{
							device.Progress = progress.Value;
						}
						if (!string.IsNullOrEmpty(status))
						{
							device.Status = status;
						}
						if (!string.IsNullOrEmpty(result))
						{
							device.Result = result;
						}
						device.IsDone = new bool?(isDone);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000435C File Offset: 0x0000255C
		public static List<Device> GetFlashDoneDs()
		{
			IEnumerable<Device> source = from d in FlashingDevice.flashDeviceList
			where d.IsDone == null || (d.IsDone != null && d.IsDone.Value && !d.IsUpdate)
			select d;
			return source.ToList<Device>();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004398 File Offset: 0x00002598
		public static bool IsAllDone()
		{
			bool result = false;
			if (FlashingDevice.GetFlashDoneDs().Count == FlashingDevice.flashDeviceList.Count)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0400001F RID: 31
		public static bool consoleMode_UsbInserted = false;

		// Token: 0x04000020 RID: 32
		public static List<Device> flashDeviceList = new List<Device>();
	}
}
