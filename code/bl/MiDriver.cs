using System;
using System.IO;
using Microsoft.Win32;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000028 RID: 40
	public class MiDriver
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public void CopyFiles(string installationPath)
		{
			installationPath = installationPath.Substring(0, installationPath.LastIndexOf('\\') + 1);
			try
			{
				string systemDirectory = Environment.SystemDirectory;
				string[] array = new string[]
				{
					"Qualcomm\\Driver\\serial\\i386\\qcCoInstaller.dll"
				};
				systemDirectory + "\\qcCoInstaller.dll";
				installationPath + "Source\\ThirdParty\\";
				for (int i = 0; i < array.Length; i++)
				{
				}
			}
			catch (Exception ex)
			{
				Log.Installw(installationPath, string.Format("copy file failed,{0}", ex.Message));
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000C34C File Offset: 0x0000A54C
		public void InstallAllDriver(string installationPath, bool uninstallOld)
		{
			installationPath = installationPath.Substring(0, installationPath.LastIndexOf('\\') + 1);
			string text = installationPath + "Source\\ThirdParty\\";
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (directoryInfo.Exists)
			{
				for (int i = 0; i < this.infFiles.Length; i++)
				{
					this.InstallDriver(text + this.infFiles[i], installationPath, uninstallOld);
				}
				return;
			}
			Log.Installw(installationPath, "dic " + text + " not exists.");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public void InstallDriver(string infPath, string installationPath, bool uninstallOld)
		{
			try
			{
				string text = "Software\\XiaoMi\\MiFlash\\";
				FileInfo fileInfo = new FileInfo(infPath);
				RegistryKey localMachine = Registry.LocalMachine;
				RegistryKey registryKey = localMachine.OpenSubKey(text, true);
				Log.Installw(installationPath, string.Format("open RegistryKey {0}", text));
				if (registryKey == null)
				{
					registryKey = localMachine.CreateSubKey(text, RegistryKeyPermissionCheck.ReadWriteSubTree);
					Log.Installw(installationPath, string.Format("create RegistryKey {0}", text));
				}
				registryKey.GetValueNames();
				object value = registryKey.GetValue(fileInfo.Name);
				bool flag = true;
				string text2;
				if (value != null && uninstallOld)
				{
					text2 = Driver.UninstallInf(value.ToString(), out flag);
					Log.Installw(installationPath, string.Format("driver {0} exists,uninstall,reuslt {1},GetLastWin32Error{2}", value.ToString(), flag.ToString(), text2));
				}
				string text3 = "";
				string text4 = "";
				text2 = Driver.SetupOEMInf(fileInfo.FullName, out text4, out text3, out flag);
				Log.Installw(installationPath, string.Format("install driver {0} to {1},result {2},GetLastWin32Error {3}", new object[]
				{
					fileInfo.FullName,
					text4,
					flag.ToString(),
					text2
				}));
				if (flag)
				{
					registryKey.SetValue(fileInfo.Name, text3);
					Log.Installw(installationPath, string.Format("set RegistryKey value:{0}--{1}", fileInfo.Name, text3));
				}
				registryKey.Close();
				if (infPath.IndexOf("android_winusb.inf") >= 0)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("USERPROFILE");
					Cmd cmd = new Cmd("", "");
					string text5 = string.Format("mkdir \"{0}\\.android\"", environmentVariable);
					string str = cmd.Execute(null, text5);
					Log.Installw(installationPath, text5);
					Log.Installw(installationPath, "output:" + str);
					text5 = string.Format(" echo 0x2717 >>\"{0}\\.android\\adb_usb.ini\"", environmentVariable);
					str = cmd.Execute(null, text5);
					Log.Installw(installationPath, text5);
					Log.Installw(installationPath, "output:" + str);
				}
			}
			catch (Exception ex)
			{
				Log.Installw(installationPath, string.Format("install driver {0}, exception:{1}", infPath, ex.Message));
			}
		}

		// Token: 0x040000BF RID: 191
		public string[] infFiles = new string[]
		{
			"Google\\Driver\\android_winusb.inf",
			"Nvidia\\Driver\\NvidiaUsb.inf",
			"Microsoft\\Driver\\tetherxp.inf",
			"Microsoft\\Driver\\wpdmtphw.inf",
			"Qualcomm\\Driver\\qcser.inf"
		};
	}
}
