using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000051 RID: 81
	internal class IniFile
	{
		// Token: 0x060001A5 RID: 421
		[DllImport("kernel32", CharSet = CharSet.Auto)]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x060001A6 RID: 422
		[DllImport("kernel32", CharSet = CharSet.Auto)]
		private static extern long GetPrivateProfileString(string section, string key, string strDefault, StringBuilder retVal, int size, string filePath);

		// Token: 0x060001A7 RID: 423
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetPrivateProfileInt(string section, string key, int nDefault, string filePath);

		// Token: 0x060001A8 RID: 424 RVA: 0x000149F8 File Offset: 0x00012BF8
		public IniFile()
		{
			string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string str = applicationBase.Substring(0, applicationBase.IndexOf('\\') + 1);
			this.strIniFilePath = str + "MiFlashvcom.ini";
			if (!File.Exists(this.strIniFilePath))
			{
				using (FileStream fileStream = File.Create(this.strIniFilePath))
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00014A7C File Offset: 0x00012C7C
		public bool GetIniString(string section, string key, string strDefault, StringBuilder retVal, int size)
		{
			long privateProfileString = IniFile.GetPrivateProfileString(section, key, strDefault, retVal, size, this.strIniFilePath);
			return privateProfileString >= 1L;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00014AA4 File Offset: 0x00012CA4
		public int GetIniInt(string section, string key, int nDefault)
		{
			return IniFile.GetPrivateProfileInt(section, key, nDefault, this.strIniFilePath);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public bool WriteIniString(string section, string key, string val)
		{
			long num = IniFile.WritePrivateProfileString(section, key, val, this.strIniFilePath);
			return num != 0L;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00014AD8 File Offset: 0x00012CD8
		public bool WriteIniInt(string section, string key, int val)
		{
			return this.WriteIniString(section, key, val.ToString());
		}

		// Token: 0x040001C4 RID: 452
		private string strIniFilePath;
	}
}
