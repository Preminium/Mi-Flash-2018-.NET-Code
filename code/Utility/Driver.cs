using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000019 RID: 25
	public class Driver
	{
		// Token: 0x060000A9 RID: 169
		[DllImport("setupapi.dll", SetLastError = true)]
		private static extern bool SetupCopyOEMInf(string SourceInfFileName, string OEMSourceMediaLocation, OemSourceMediaType OEMSourceMediaType, OemCopyStyle CopyStyle, StringBuilder DestinationInfFileName, int DestinationInfFileNameSize, int RequiredSize, out string DestinationInfFileNameComponent);

		// Token: 0x060000AA RID: 170
		[DllImport("setupapi.dll", SetLastError = true)]
		private static extern bool SetupUninstallOEMInf(string InfFileName, SetupUOInfFlags Flags, IntPtr Reserved);

		// Token: 0x060000AB RID: 171
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetWindowsDirectory(StringBuilder path, int pathLen);

		// Token: 0x060000AC RID: 172 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		public static string SetupOEMInf(string infPath, out string destinationInfFileName, out string destinationInfFileNameComponent, out bool success)
		{
			string result = "";
			StringBuilder stringBuilder = new StringBuilder(260);
			success = Driver.SetupCopyOEMInf(infPath, "", OemSourceMediaType.SPOST_PATH, OemCopyStyle.SP_COPY_NEWER, stringBuilder, stringBuilder.Capacity, 0, out destinationInfFileNameComponent);
			if (!success)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				string message = new Win32Exception(lastWin32Error).Message;
				result = message;
			}
			destinationInfFileName = stringBuilder.ToString();
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000AA40 File Offset: 0x00008C40
		public static string UninstallInf(string infFileName, out bool success)
		{
			string result = "";
			success = Driver.SetupUninstallOEMInf(infFileName, SetupUOInfFlags.SUOI_FORCEDELETE, IntPtr.Zero);
			if (!success)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				string message = new Win32Exception(lastWin32Error).Message;
				result = message;
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public static string UninstallInfByText(string text, out bool success)
		{
			success = false;
			StringBuilder stringBuilder = new StringBuilder(256);
			if (Driver.GetWindowsDirectory(stringBuilder, stringBuilder.Capacity) == 0u)
			{
				return "UninstallInfByText: GetWindowsDirectory failed with system error code " + Marshal.GetLastWin32Error().ToString();
			}
			string path = stringBuilder.ToString() + "\\inf";
			string[] files = Directory.GetFiles(path, "*.inf");
			string text2 = "";
			foreach (string text3 in files)
			{
				string text4 = File.ReadAllText(text3);
				if (text4.Contains(text))
				{
					string text5 = text3.Remove(0, text3.LastIndexOf('\\') + 1);
					if (!Driver.SetupUninstallOEMInf(text5, SetupUOInfFlags.SUOI_FORCEDELETE, IntPtr.Zero))
					{
						string text6 = text2;
						text2 = string.Concat(new string[]
						{
							text6,
							"UninstallInfByText: SetupUninstallOEMInf failed with code ",
							Marshal.GetLastWin32Error().ToString(),
							" for file ",
							text5
						});
					}
					else
					{
						success = true;
					}
				}
			}
			if (text2.Length > 0)
			{
				return text2;
			}
			return null;
		}
	}
}
