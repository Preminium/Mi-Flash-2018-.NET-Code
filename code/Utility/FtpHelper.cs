using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000018 RID: 24
	public class FtpHelper
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public FtpHelper(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
		{
			this.ftpServerIP = FtpServerIP;
			this.ftpRemotePath = FtpRemotePath;
			this.ftpUserID = FtpUserID;
			this.ftpPassword = FtpPassword;
			this.ftpURI = string.Concat(new string[]
			{
				"ftp://",
				this.ftpServerIP,
				"/",
				this.ftpRemotePath,
				"/"
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00009C58 File Offset: 0x00007E58
		public void Upload(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			string uriString = this.ftpURI + fileInfo.Name;
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
			ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.UseBinary = true;
			ftpWebRequest.UsePassive = false;
			ftpWebRequest.ContentLength = fileInfo.Length;
			int num = 2048;
			byte[] buffer = new byte[num];
			FileStream fileStream = fileInfo.OpenRead();
			try
			{
				Stream requestStream = ftpWebRequest.GetRequestStream();
				for (int count = fileStream.Read(buffer, 0, num); count != 0; count = fileStream.Read(buffer, 0, num))
				{
					requestStream.Write(buffer, 0, count);
				}
				requestStream.Close();
				fileStream.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("Ftphelper Upload Error --> " + ex.Message);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00009D50 File Offset: 0x00007F50
		public void Download(string filePath, string fileName)
		{
			try
			{
				double num = (double)this.GetFileSize(fileName);
				MiFlashGlobal.Swdes.fileSize = (double)((long)num);
				FileStream fileStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI + fileName));
				ftpWebRequest.Method = "RETR";
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.UsePassive = false;
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				Stream responseStream = ftpWebResponse.GetResponseStream();
				long contentLength = ftpWebResponse.ContentLength;
				int num2 = 2048;
				byte[] buffer = new byte[num2];
				int num3 = 0;
				int i = responseStream.Read(buffer, 0, num2);
				num3 += i;
				while (i > 0)
				{
					fileStream.Write(buffer, 0, i);
					i = responseStream.Read(buffer, 0, num2);
					num3 += i;
					MiFlashGlobal.Swdes.serverPath = this.ftpURI + fileName;
					MiFlashGlobal.Swdes.localPath = fileStream.Name;
					MiFlashGlobal.Swdes.percent = (double)num3 / num * 100.0;
				}
				responseStream.Close();
				fileStream.Close();
				ftpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper Download Error --> " + ex.Message);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00009EC8 File Offset: 0x000080C8
		public void ThreadDownload()
		{
			string filePath = this.localPath;
			string fileName = this.downloadFile;
			this.Download(filePath, fileName);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00009EEC File Offset: 0x000080EC
		public void Delete(string fileName)
		{
			try
			{
				string uriString = this.ftpURI + fileName;
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				ftpWebRequest.Method = "DELE";
				ftpWebRequest.UsePassive = false;
				string empty = string.Empty;
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				long contentLength = ftpWebResponse.ContentLength;
				Stream responseStream = ftpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				streamReader.ReadToEnd();
				streamReader.Close();
				responseStream.Close();
				ftpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper Delete Error --> " + ex.Message + "  文件名:" + fileName);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00009FB4 File Offset: 0x000081B4
		public void RemoveDirectory(string folderName)
		{
			try
			{
				string uriString = this.ftpURI + folderName;
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				ftpWebRequest.Method = "RMD";
				ftpWebRequest.UsePassive = false;
				string empty = string.Empty;
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				long contentLength = ftpWebResponse.ContentLength;
				Stream responseStream = ftpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);
				streamReader.ReadToEnd();
				streamReader.Close();
				responseStream.Close();
				ftpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper Delete Error --> " + ex.Message + "  文件名:" + folderName);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000A07C File Offset: 0x0000827C
		public string[] GetFilesDetailList()
		{
			string[] result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI));
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				ftpWebRequest.Method = "LIST";
				ftpWebRequest.UsePassive = false;
				WebResponse response = ftpWebRequest.GetResponse();
				StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
				for (string value = streamReader.ReadLine(); value != null; value = streamReader.ReadLine())
				{
					stringBuilder.Append(value);
					stringBuilder.Append("\n");
				}
				if (!string.IsNullOrEmpty(stringBuilder.ToString()))
				{
					stringBuilder.Remove(stringBuilder.ToString().LastIndexOf("\n"), 1);
				}
				streamReader.Close();
				response.Close();
				result = stringBuilder.ToString().Split(new char[]
				{
					'\n'
				});
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper  Error --> " + ex.Message);
			}
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000A18C File Offset: 0x0000838C
		public string[] GetFileList(string mask)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] result;
			try
			{
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI));
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				ftpWebRequest.Method = "NLST";
				ftpWebRequest.UsePassive = false;
				WebResponse response = ftpWebRequest.GetResponse();
				StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
				for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
				{
					if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
					{
						string text2 = mask.Substring(0, mask.IndexOf("*"));
						if (text.Substring(0, text2.Length) == text2)
						{
							stringBuilder.Append(text);
							stringBuilder.Append("\n");
						}
					}
					else
					{
						stringBuilder.Append(text);
						stringBuilder.Append("\n");
					}
				}
				if (!string.IsNullOrEmpty(stringBuilder.ToString()))
				{
					stringBuilder.Remove(stringBuilder.ToString().LastIndexOf('\n'), 1);
				}
				streamReader.Close();
				response.Close();
				result = stringBuilder.ToString().Split(new char[]
				{
					'\n'
				});
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper GetFileList Error --> " + ex.Message.ToString());
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000A320 File Offset: 0x00008520
		public string[] GetDirectoryList()
		{
			string[] filesDetailList = this.GetFilesDetailList();
			string text = string.Empty;
			foreach (string text2 in filesDetailList)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					int num = text2.IndexOf("<DIR>");
					if (num > 0)
					{
						text = text + text2.Substring(num + 5).Trim() + "\n";
					}
					else if (text2.Trim().Substring(0, 1).ToUpper() == "D")
					{
						string text3 = text2.Substring(54).Trim();
						if (text3 != "." && text3 != "..")
						{
							text = text + text3 + "\n";
						}
					}
				}
			}
			char[] separator = new char[]
			{
				'\n'
			};
			return text.Split(separator);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000A404 File Offset: 0x00008604
		public bool DirectoryExist(string RemoteDirectoryName)
		{
			string[] directoryList = this.GetDirectoryList();
			foreach (string text in directoryList)
			{
				if (text.Trim() == RemoteDirectoryName.Trim())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000A44C File Offset: 0x0000864C
		public bool FileExist(string RemoteFileName)
		{
			string[] fileList = this.GetFileList("*.*");
			foreach (string text in fileList)
			{
				if (text.Trim() == RemoteFileName.Trim())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000A498 File Offset: 0x00008698
		public void MakeDir(string dirName)
		{
			try
			{
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI + dirName));
				ftpWebRequest.Method = "MKD";
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.UsePassive = false;
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				Stream responseStream = ftpWebResponse.GetResponseStream();
				responseStream.Close();
				ftpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper MakeDir Error --> " + ex.Message);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000A53C File Offset: 0x0000873C
		public long GetFileSize(string filename)
		{
			long result = 0L;
			try
			{
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI + filename));
				ftpWebRequest.Method = "SIZE";
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.UsePassive = false;
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				Stream responseStream = ftpWebResponse.GetResponseStream();
				result = ftpWebResponse.ContentLength;
				responseStream.Close();
				ftpWebResponse.Close();
			}
			catch (WebException ex)
			{
				string statusDescription = ((FtpWebResponse)ex.Response).StatusDescription;
				result = 1024L;
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000A5F0 File Offset: 0x000087F0
		public void ReName(string currentFilename, string newFilename)
		{
			try
			{
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(this.ftpURI + currentFilename));
				ftpWebRequest.Method = "RENAME";
				ftpWebRequest.RenameTo = newFilename;
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.UsePassive = false;
				ftpWebRequest.Credentials = new NetworkCredential(this.ftpUserID, this.ftpPassword);
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				Stream responseStream = ftpWebResponse.GetResponseStream();
				responseStream.Close();
				ftpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("FtpHelper ReName Error --> " + ex.Message);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000A698 File Offset: 0x00008898
		public void MovieFile(string currentFilename, string newDirectory)
		{
			this.ReName(currentFilename, newDirectory);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public void GotoDirectory(string DirectoryName, bool IsRoot)
		{
			if (IsRoot)
			{
				this.ftpRemotePath = DirectoryName;
			}
			else
			{
				this.ftpRemotePath = this.ftpRemotePath + DirectoryName + "/";
			}
			this.ftpURI = string.Concat(new string[]
			{
				"ftp://",
				this.ftpServerIP,
				"/",
				this.ftpRemotePath,
				"/"
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000A714 File Offset: 0x00008914
		public void DeleteOrderDirectory(string ftpServerIP, string folderToDelete, string ftpUserID, string ftpPassword)
		{
			try
			{
				if (string.IsNullOrEmpty(ftpServerIP) || string.IsNullOrEmpty(folderToDelete) || string.IsNullOrEmpty(ftpUserID) || string.IsNullOrEmpty(ftpPassword))
				{
					throw new Exception("FTP 及路径不能为空！");
				}
				FtpHelper ftpHelper = new FtpHelper(ftpServerIP, folderToDelete, ftpUserID, ftpPassword);
				ftpHelper.GotoDirectory(folderToDelete, true);
				string[] directoryList = ftpHelper.GetDirectoryList();
				foreach (string text in directoryList)
				{
					if (!string.IsNullOrEmpty(text) || text != "")
					{
						string directoryName = folderToDelete + "/" + text;
						ftpHelper.GotoDirectory(directoryName, true);
						string[] fileList = ftpHelper.GetFileList("*.*");
						if (fileList != null)
						{
							foreach (string fileName in fileList)
							{
								ftpHelper.Delete(fileName);
							}
						}
						ftpHelper.GotoDirectory(folderToDelete, true);
						ftpHelper.RemoveDirectory(text);
					}
				}
				string directoryName2 = folderToDelete.Remove(folderToDelete.LastIndexOf('/'));
				string folderName = folderToDelete.Substring(folderToDelete.LastIndexOf('/') + 1);
				ftpHelper.GotoDirectory(directoryName2, true);
				ftpHelper.RemoveDirectory(folderName);
			}
			catch (Exception ex)
			{
				throw new Exception("删除订单时发生错误，错误信息为：" + ex.Message);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000A870 File Offset: 0x00008A70
		public void DownloadAllInDic(string dic, string destinationPath)
		{
			dic = dic.Substring(0, dic.IndexOf(this.swFileName)) + destinationPath.Substring(destinationPath.IndexOf(this.swFileName));
			dic = dic.Replace("\\", "/");
			this.GotoDirectory(dic, true);
			Directory.CreateDirectory(destinationPath);
			List<string> list = new List<string>();
			list = this.GetFileList("").ToList<string>();
			List<string> list2 = this.GetDirectoryList().ToList<string>();
			foreach (string text in list)
			{
				if (list2.IndexOf(text) < 0)
				{
					double fileSize = (double)this.GetFileSize(text);
					MiFlashGlobal.Swdes = new SwDes
					{
						fileSize = fileSize,
						serverPath = this.ftpURI + "\\" + text
					};
					this.Download(destinationPath, text);
				}
			}
			foreach (string text2 in list2)
			{
				text2 == "ota";
				if (!string.IsNullOrEmpty(text2))
				{
					this.DownloadAllInDic(this.ftpRemotePath + "\\" + text2, destinationPath + "/" + text2);
				}
			}
		}

		// Token: 0x04000073 RID: 115
		private string ftpServerIP;

		// Token: 0x04000074 RID: 116
		public string ftpRemotePath;

		// Token: 0x04000075 RID: 117
		private string ftpUserID;

		// Token: 0x04000076 RID: 118
		private string ftpPassword;

		// Token: 0x04000077 RID: 119
		public string ftpURI;

		// Token: 0x04000078 RID: 120
		public string localPath;

		// Token: 0x04000079 RID: 121
		public string downloadFile;

		// Token: 0x0400007A RID: 122
		public string swFileName;
	}
}
