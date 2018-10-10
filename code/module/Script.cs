using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200005E RID: 94
	public class Script
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00015306 File Offset: 0x00013506
		public static string AndroidPath
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Google\\Android\"";
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00015326 File Offset: 0x00013526
		public static string fastboot
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Google\\Android\\fastboot.exe\"";
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00015346 File Offset: 0x00013546
		public static string QcLsUsb
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Qualcomm\\fh_loader\\lsusb.exe\"";
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00015366 File Offset: 0x00013566
		public static string SP_Download_Tool_PATH
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "SP_Download_tool\"";
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00015386 File Offset: 0x00013586
		public static string LKMSG_FASTBOOT
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Mi\\fastboot.exe\"";
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000153A6 File Offset: 0x000135A6
		public static string qcCoInstaller
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Mi\\qcCoInstaller.dll\"";
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000153C6 File Offset: 0x000135C6
		public static string msiexec
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Mi\\msiexec.exe\"";
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000153E6 File Offset: 0x000135E6
		public static string pythonMsi
		{
			get
			{
				return "\"" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\ThirdParty\\Mi\\python-2.7.13.msi\"";
			}
		}
	}
}
