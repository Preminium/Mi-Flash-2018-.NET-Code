using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000035 RID: 53
	public class Utility
	{
		// Token: 0x0600018B RID: 395 RVA: 0x000145D0 File Offset: 0x000127D0
		public static string GetMD5HashFromFile(string fileName)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open);
				MD5 md = new MD5CryptoServiceProvider();
				byte[] array = md.ComputeHash(fileStream);
				fileStream.Close();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				result = stringBuilder.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
			}
			return result;
		}
	}
}
