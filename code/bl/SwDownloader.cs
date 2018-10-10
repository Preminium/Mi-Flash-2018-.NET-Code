using System;
using System.IO;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000066 RID: 102
	public class SwDownloader
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00016338 File Offset: 0x00014538
		public SwDownloader()
		{
			this.ftp = new FtpHelper("10.237.107.75", "SW", "daemon", "xampp");
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0001635F File Offset: 0x0001455F
		public void ThreadDownloadSw()
		{
			this.DownloadSw(this.dlReturnPath);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00016370 File Offset: 0x00014570
		public void DownloadSw(string dllReturnPath)
		{
			string str = dllReturnPath.Substring(dllReturnPath.IndexOf("\\"));
			string swFileName = dllReturnPath.Substring(dllReturnPath.LastIndexOf("\\")).Replace("\\", "");
			this.ftp.swFileName = swFileName;
			string dic = this.ftp.ftpRemotePath + "/" + str;
			Directory.CreateDirectory(dllReturnPath);
			this.ftp.DownloadAllInDic(dic, dllReturnPath);
			MiFlashGlobal.Swdes.isDone = true;
		}

		// Token: 0x04000228 RID: 552
		private FtpHelper ftp;

		// Token: 0x04000229 RID: 553
		public string dlReturnPath;
	}
}
