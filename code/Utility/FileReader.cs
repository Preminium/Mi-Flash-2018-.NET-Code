using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200002F RID: 47
	internal class FileReader
	{
		// Token: 0x06000119 RID: 281
		[DllImport("kernel32", SetLastError = true)]
		public static extern IntPtr CreateFile(string FileName, uint DesiredAccess, uint ShareMode, uint SecurityAttributes, uint CreationDisposition, uint FlagsAndAttributes, int hTemplateFile);

		// Token: 0x0600011A RID: 282
		[DllImport("kernel32", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x0600011B RID: 283 RVA: 0x0000E72B File Offset: 0x0000C92B
		public IntPtr Open(string FileName)
		{
			this.handle = FileReader.CreateFile(FileName, 2147483648u, 0u, 0u, 3u, 0u, 0);
			if (this.handle != IntPtr.Zero)
			{
				return this.handle;
			}
			throw new Exception("打开文件失败");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000E766 File Offset: 0x0000C966
		public bool Close()
		{
			return FileReader.CloseHandle(this.handle);
		}

		// Token: 0x04000101 RID: 257
		private const uint GENERIC_READ = 2147483648u;

		// Token: 0x04000102 RID: 258
		private const uint OPEN_EXISTING = 3u;

		// Token: 0x04000103 RID: 259
		private IntPtr handle;
	}
}
