using System;
using System.Collections.Generic;
using System.IO;

namespace XiaoMiFlash.code.data
{
	// Token: 0x0200001E RID: 30
	public static class MemImg
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static long mapImg(string filePath)
		{
			long result;
			lock (MemImg.obj_lock)
			{
				try
				{
					long num2;
					if (!MemImg.shareMemTable.ContainsKey(filePath))
					{
						FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
						int num = (int)fileStream.Length;
						byte[] buffer = new byte[num];
						fileStream.Read(buffer, 0, num);
						MemoryStream value = new MemoryStream(buffer);
						MemImg.shareMemTable[filePath] = value;
						num2 = (long)num;
					}
					else
					{
						num2 = MemImg.shareMemTable[filePath].Length;
					}
					result = num2;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000AC54 File Offset: 0x00008E54
		public static byte[] GetBytesFromFile(string filePath, long offset, int size, out float percent)
		{
			byte[] result;
			lock (MemImg.obj_lock)
			{
				MemoryStream memoryStream = MemImg.shareMemTable[filePath];
				byte[] array = new byte[size];
				memoryStream.Position = offset;
				memoryStream.Read(array, 0, size);
				percent = (float)offset / (float)memoryStream.Length;
				result = array;
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public static void distory()
		{
			if (MemImg.isHighSpeed)
			{
				foreach (KeyValuePair<string, MemoryStream> keyValuePair in MemImg.shareMemTable)
				{
					keyValuePair.Value.Close();
					keyValuePair.Value.Dispose();
				}
				MemImg.shareMemTable.Clear();
			}
		}

		// Token: 0x0400009F RID: 159
		private static object obj_lock = new object();

		// Token: 0x040000A0 RID: 160
		public static bool isHighSpeed = false;

		// Token: 0x040000A1 RID: 161
		public static Dictionary<string, MemoryStream> shareMemTable = new Dictionary<string, MemoryStream>();
	}
}
