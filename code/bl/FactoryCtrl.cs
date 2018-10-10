using System;
using System.IO;
using PUB_TEST_FUNC_DLL;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000022 RID: 34
	public class FactoryCtrl
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000B294 File Offset: 0x00009494
		public static bool SetFactory(string factory)
		{
			bool result = false;
			string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string text = FactoryCtrl.thirdParty + factory;
			try
			{
				if (Directory.Exists(applicationBase + text))
				{
					string[] files = Directory.GetFiles(applicationBase + text);
					foreach (string text2 in files)
					{
						string fileName = Path.GetFileName(text2);
						if (File.Exists(applicationBase + fileName))
						{
							File.Delete(applicationBase + fileName);
						}
						File.Copy(text2, applicationBase + fileName, true);
						Log.w("Factory:" + factory + " switch dll " + text2);
					}
					result = true;
				}
				else
				{
					Log.w(string.Format("file not exit {0}", text));
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000B384 File Offset: 0x00009584
		public static FlashResult SetFlashResultD(string deivce, bool reuslt)
		{
			Log.w(deivce, "start SetFlashResultD");
			FlashResult flashResult = new FlashResult();
			flashResult.Result = false;
			flashResult.Msg = "upload result failed";
			try
			{
				string text = "";
				if (string.IsNullOrEmpty(deivce))
				{
					flashResult.Result = false;
					flashResult.Msg = "device is null";
					return flashResult;
				}
				Log.w(deivce, "upload flash result");
				TPUB_TEST_FUNC_DLL factoryObject = FactoryCtrl.GetFactoryObject(deivce, out text);
				if (factoryObject == null)
				{
					flashResult.Result = false;
					flashResult.Msg = "error:couldn't get TPUB_TEST_FUNC_DLL";
				}
				else
				{
					bool flag = factoryObject.SaveDataByCPUID(deivce, reuslt, out text);
					text = string.Format("SaveDataByCPUID result {0} status {1}", flag.ToString(), text);
					flashResult.Result = flag;
					flashResult.Msg = text;
					Log.w(deivce, text);
					if (flag)
					{
						Log.w(deivce, "upload result success");
					}
					else
					{
						Log.w(deivce, "upload result failed");
					}
				}
			}
			catch (Exception ex)
			{
				Log.w(string.Concat(new string[]
				{
					deivce,
					" ",
					ex.Message,
					"  ",
					ex.StackTrace
				}));
				FlashResult flashResult2 = flashResult;
				flashResult2.Msg = flashResult2.Msg + " " + ex.Message;
			}
			Log.w(deivce, flashResult.Msg);
			return flashResult;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000B4DC File Offset: 0x000096DC
		public static CheckCPUIDResult GetSearchPathD(string deivce, string swPath)
		{
			CheckCPUIDResult checkCPUIDResult = new CheckCPUIDResult();
			Log.w(deivce, "GetSearchPath");
			bool flag = false;
			string text = "";
			string text2 = "";
			try
			{
				TPUB_TEST_FUNC_DLL factoryObject = FactoryCtrl.GetFactoryObject(deivce, out text2);
				if (factoryObject == null)
				{
					if (string.IsNullOrEmpty(text2))
					{
						text2 = "can not GetFactoryObject";
					}
					Log.w(deivce + " CheckCPUID failed!");
				}
				else
				{
					flag = factoryObject.CheckCPUID(deivce, out text, out text2);
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.Name == deivce)
						{
							device.CheckCPUID = flag;
						}
					}
					Log.w(string.Format("{0} CheckCPUID result {1} status {2} imgPath {3}", new object[]
					{
						deivce,
						flag.ToString(),
						text2,
						text
					}));
				}
				checkCPUIDResult.Msg = text2;
			}
			catch (Exception ex)
			{
				checkCPUIDResult.Msg = ex.Message;
			}
			checkCPUIDResult.Device = deivce;
			checkCPUIDResult.Result = flag;
			checkCPUIDResult.Path = text;
			Log.w(deivce, string.Format("device {0} CheckCPUID result {1} status {2}", deivce, checkCPUIDResult.Result.ToString(), checkCPUIDResult.Msg));
			return checkCPUIDResult;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000B634 File Offset: 0x00009834
		public static TPUB_TEST_FUNC_DLL GetFactoryObject(string deivce, out string msg)
		{
			TPUB_TEST_FUNC_DLL tpub_TEST_FUNC_DLL = new TPUB_TEST_FUNC_DLL();
			msg = string.Empty;
			bool flag = tpub_TEST_FUNC_DLL.InitCheck(out msg);
			Log.w(string.Format("{0} InitCheck result {1} status {2}", deivce, flag.ToString(), msg));
			Log.w(deivce, string.Format("{0} InitCheck result {1} status {2}", deivce, flag.ToString(), msg));
			if (flag)
			{
				return tpub_TEST_FUNC_DLL;
			}
			FlashingDevice.UpdateDeviceStatus(deivce, null, "can't init factory ev " + msg, "factory ev error", true);
			return null;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public static CheckCPUIDResult FactorySignEdl(string deivce, string orignKey, out string signedKey)
		{
			signedKey = "";
			Log.w("vboytest factory 111");
			CheckCPUIDResult checkCPUIDResult = new CheckCPUIDResult();
			bool result = false;
			string msg = "";
			try
			{
				TPUB_TEST_FUNC_DLL tpub_TEST_FUNC_DLL = new TPUB_TEST_FUNC_DLL();
				lock (FactoryCtrl.locker1)
				{
					result = tpub_TEST_FUNC_DLL.SLA_Challenge("qcomFlash", orignKey, out signedKey, out msg);
				}
				checkCPUIDResult.Msg = msg;
			}
			catch (Exception ex)
			{
				checkCPUIDResult.Msg = ex.Message;
			}
			checkCPUIDResult.Device = deivce;
			checkCPUIDResult.Result = result;
			Log.w(deivce, string.Format("device {0} SLA_Challenge result {1} status {2}", deivce, checkCPUIDResult.Result.ToString(), checkCPUIDResult.Msg));
			return checkCPUIDResult;
		}

		// Token: 0x040000A4 RID: 164
		private static string funcDll = "PUB_TEST_FUNC_DLL.dll";

		// Token: 0x040000A5 RID: 165
		private static string routingDll = "RoutingObject.dll";

		// Token: 0x040000A6 RID: 166
		private static string thirdParty = "Source\\ThirdParty\\";

		// Token: 0x040000A7 RID: 167
		private static bool firstCall = true;

		// Token: 0x040000A8 RID: 168
		private static readonly object locker1 = new object();
	}
}
