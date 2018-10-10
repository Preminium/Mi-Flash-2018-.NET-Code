using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000025 RID: 37
	public class MiProperty
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000BC80 File Offset: 0x00009E80
		public static Hashtable DlTable
		{
			get
			{
				MiProperty._dltable = new Hashtable();
				MiProperty._dltable.Add("download_only", "dl_only");
				MiProperty._dltable.Add("format_and_download", "fm_and_dl");
				MiProperty._dltable.Add("firmware_upgrade", "firmware_upgrade");
				MiProperty._dltable.Add("format_all", "fm");
				return MiProperty._dltable;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public static Dictionary<string, int> ChkSumTable
		{
			get
			{
				MiProperty._chksumtable = new Dictionary<string, int>();
				MiProperty._chksumtable.Add("None", 0);
				MiProperty._chksumtable.Add("Usb+dram checksum", 1);
				MiProperty._chksumtable.Add("Flash checksum", 2);
				MiProperty._chksumtable.Add("All checksum", 3);
				return MiProperty._chksumtable;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000BD48 File Offset: 0x00009F48
		public static string[] DaList
		{
			get
			{
				string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
				string path = applicationBase + "\\da";
				if (Directory.Exists(path))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					FileInfo[] files = directoryInfo.GetFiles();
					MiProperty._dalist = new string[files.Count<FileInfo>()];
					for (int i = 0; i < files.Count<FileInfo>(); i++)
					{
						MiProperty._dalist[i] = files[i].Name;
					}
				}
				return MiProperty._dalist;
			}
		}

		// Token: 0x040000BA RID: 186
		private static Hashtable _dltable;

		// Token: 0x040000BB RID: 187
		private static Dictionary<string, int> _chksumtable;

		// Token: 0x040000BC RID: 188
		private static string[] _dalist;
	}
}
