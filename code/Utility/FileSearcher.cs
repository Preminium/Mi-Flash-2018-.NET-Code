using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000021 RID: 33
	public class FileSearcher
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000B1FC File Offset: 0x000093FC
		public static string[] SearchFiles(string destinationDic, string pattern)
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(destinationDic);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				Regex regex = new Regex(pattern);
				Match match = regex.Match(fileInfo.Name);
				if (match.Groups.Count > 0 && match.Groups[0].Value == fileInfo.Name)
				{
					list.Add(fileInfo.FullName);
				}
			}
			return list.ToArray();
		}
	}
}
