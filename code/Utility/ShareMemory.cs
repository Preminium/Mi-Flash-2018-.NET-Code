using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200002D RID: 45
	public class ShareMemory
	{
		// Token: 0x06000103 RID: 259
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

		// Token: 0x06000104 RID: 260
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

		// Token: 0x06000105 RID: 261
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		// Token: 0x06000106 RID: 262
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

		// Token: 0x06000107 RID: 263
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

		// Token: 0x06000108 RID: 264
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000109 RID: 265
		[DllImport("kernel32")]
		public static extern int GetLastError();

		// Token: 0x0600010A RID: 266
		[DllImport("kernel32.dll")]
		private static extern void GetSystemInfo(out ShareMemory.SYSTEM_INFO lpSystemInfo);

		// Token: 0x0600010B RID: 267 RVA: 0x0000E290 File Offset: 0x0000C490
		public static uint GetPartitionsize()
		{
			ShareMemory.SYSTEM_INFO system_INFO;
			ShareMemory.GetSystemInfo(out system_INFO);
			return system_INFO.allocationGranularity;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public ShareMemory(string filename, uint memSize)
		{
			this.m_MemSize = memSize;
			this.Init(filename);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		public ShareMemory(string filename)
		{
			this.m_MemSize = 20971520u;
			this.Init(filename);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000E350 File Offset: 0x0000C550
		~ShareMemory()
		{
			this.Close();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000E37C File Offset: 0x0000C57C
		protected void Init(string strName)
		{
			if (!System.IO.File.Exists(strName))
			{
				throw new Exception("未找到文件");
			}
			FileInfo fileInfo = new FileInfo(strName);
			this.m_FileSize = fileInfo.Length;
			this.m_fHandle = this.File.Open(strName);
			if (strName.Length > 0)
			{
				this.m_hSharedMemoryFile = ShareMemory.CreateFileMapping(this.m_fHandle, IntPtr.Zero, 2u, 0u, (uint)this.m_FileSize, "mdata");
				if (this.m_hSharedMemoryFile == IntPtr.Zero)
				{
					this.m_bAlreadyExist = false;
					this.m_bInit = false;
					throw new Exception("CreateFileMapping失败LastError=" + ShareMemory.GetLastError().ToString());
				}
				this.m_bInit = true;
				if ((ulong)this.m_MemSize > (ulong)this.m_FileSize)
				{
					this.m_MemSize = (uint)this.m_FileSize;
				}
				this.m_pwData = ShareMemory.MapViewOfFile(this.m_hSharedMemoryFile, 4u, 0u, 0u, this.m_MemSize);
				if (this.m_pwData == IntPtr.Zero)
				{
					this.m_bInit = false;
					throw new Exception("m_hSharedMemoryFile失败LastError=" + ShareMemory.GetLastError().ToString() + "  " + new Win32Exception(ShareMemory.GetLastError()).Message);
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000E4B5 File Offset: 0x0000C6B5
		private static uint GetHighWord(ulong intValue)
		{
			return Convert.ToUInt32(intValue >> 32);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		private static uint GetLowWord(ulong intValue)
		{
			return Convert.ToUInt32(intValue & (ulong)-1);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public uint GetNextblock()
		{
			if (!this.m_bInit)
			{
				throw new Exception("文件未初始化。");
			}
			uint memberSize = this.GetMemberSize();
			if (memberSize == 0u)
			{
				return memberSize;
			}
			this.m_MemSize = memberSize;
			this.m_pwData = ShareMemory.MapViewOfFile(this.m_hSharedMemoryFile, 4u, ShareMemory.GetHighWord((ulong)this.m_offsetBegin), ShareMemory.GetLowWord((ulong)this.m_offsetBegin), memberSize);
			if (this.m_pwData == IntPtr.Zero)
			{
				this.m_bInit = false;
				throw new Exception("映射文件块失败" + ShareMemory.GetLastError().ToString());
			}
			this.m_offsetBegin += (long)((ulong)memberSize);
			return memberSize;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000E570 File Offset: 0x0000C770
		private uint GetMemberSize()
		{
			if (this.m_offsetBegin >= this.m_FileSize)
			{
				return 0u;
			}
			if (this.m_offsetBegin + (long)((ulong)this.m_MemSize) >= this.m_FileSize)
			{
				long num = this.m_FileSize - this.m_offsetBegin;
				return (uint)num;
			}
			return this.m_MemSize;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000E5BA File Offset: 0x0000C7BA
		public void Close()
		{
			if (this.m_bInit)
			{
				ShareMemory.UnmapViewOfFile(this.m_pwData);
				ShareMemory.CloseHandle(this.m_hSharedMemoryFile);
				this.File.Close();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public void Read(ref byte[] bytData, int lngAddr, int lngSize, bool Unmap)
		{
			if ((long)(lngAddr + lngSize) > (long)((ulong)this.m_MemSize))
			{
				throw new Exception("Read操作超出数据区");
			}
			if (this.m_bInit)
			{
				Marshal.Copy(this.m_pwData, bytData, lngAddr, lngSize);
				if (Unmap)
				{
					bool flag = ShareMemory.UnmapViewOfFile(this.m_pwData);
					if (flag)
					{
						this.m_pwData = IntPtr.Zero;
					}
				}
				return;
			}
			throw new Exception("文件未初始化");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000E64F File Offset: 0x0000C84F
		public void Read(ref byte[] bytData, int lngAddr, int lngSize)
		{
			if ((long)(lngAddr + lngSize) > (long)((ulong)this.m_MemSize))
			{
				throw new Exception("Read操作超出数据区");
			}
			if (this.m_bInit)
			{
				Marshal.Copy(this.m_pwData, bytData, lngAddr, lngSize);
				return;
			}
			throw new Exception("文件未初始化");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000E68C File Offset: 0x0000C88C
		public uint ReadBytes(int lngAddr, ref byte[] byteData, int StartIndex, uint intSize)
		{
			if ((long)lngAddr >= (long)((ulong)this.m_MemSize))
			{
				throw new Exception("起始数据超过缓冲区长度");
			}
			if ((long)lngAddr + (long)((ulong)intSize) > (long)((ulong)this.m_MemSize))
			{
				intSize = this.m_MemSize - (uint)lngAddr;
			}
			if (this.m_bInit)
			{
				IntPtr source = new IntPtr((long)this.m_pwData + (long)lngAddr);
				Marshal.Copy(source, byteData, StartIndex, (int)intSize);
				return intSize;
			}
			throw new Exception("文件未初始化");
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		private int Write(byte[] bytData, int lngAddr, int lngSize)
		{
			if ((long)(lngAddr + lngSize) > (long)((ulong)this.m_MemSize))
			{
				return 2;
			}
			if (this.m_bInit)
			{
				Marshal.Copy(bytData, lngAddr, this.m_pwData, lngSize);
				return 0;
			}
			return 1;
		}

		// Token: 0x040000DE RID: 222
		private const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x040000DF RID: 223
		private const int FILE_MAP_COPY = 1;

		// Token: 0x040000E0 RID: 224
		private const int FILE_MAP_WRITE = 2;

		// Token: 0x040000E1 RID: 225
		private const int FILE_MAP_READ = 4;

		// Token: 0x040000E2 RID: 226
		private const int FILE_MAP_ALL_ACCESS = 6;

		// Token: 0x040000E3 RID: 227
		private const int PAGE_READONLY = 2;

		// Token: 0x040000E4 RID: 228
		private const int PAGE_READWRITE = 4;

		// Token: 0x040000E5 RID: 229
		private const int PAGE_WRITECOPY = 8;

		// Token: 0x040000E6 RID: 230
		private const int PAGE_EXECUTE = 16;

		// Token: 0x040000E7 RID: 231
		private const int PAGE_EXECUTE_READ = 32;

		// Token: 0x040000E8 RID: 232
		private const int PAGE_EXECUTE_READWRITE = 64;

		// Token: 0x040000E9 RID: 233
		private const int SEC_COMMIT = 134217728;

		// Token: 0x040000EA RID: 234
		private const int SEC_IMAGE = 16777216;

		// Token: 0x040000EB RID: 235
		private const int SEC_NOCACHE = 268435456;

		// Token: 0x040000EC RID: 236
		private const int SEC_RESERVE = 67108864;

		// Token: 0x040000ED RID: 237
		private IntPtr m_fHandle;

		// Token: 0x040000EE RID: 238
		private IntPtr m_hSharedMemoryFile = IntPtr.Zero;

		// Token: 0x040000EF RID: 239
		private IntPtr m_pwData = IntPtr.Zero;

		// Token: 0x040000F0 RID: 240
		private bool m_bAlreadyExist;

		// Token: 0x040000F1 RID: 241
		private bool m_bInit;

		// Token: 0x040000F2 RID: 242
		private uint m_MemSize = 20971520u;

		// Token: 0x040000F3 RID: 243
		private long m_offsetBegin;

		// Token: 0x040000F4 RID: 244
		public long m_FileSize;

		// Token: 0x040000F5 RID: 245
		private FileReader File = new FileReader();

		// Token: 0x0200002E RID: 46
		public struct SYSTEM_INFO
		{
			// Token: 0x040000F6 RID: 246
			public ushort processorArchitecture;

			// Token: 0x040000F7 RID: 247
			private ushort reserved;

			// Token: 0x040000F8 RID: 248
			public uint pageSize;

			// Token: 0x040000F9 RID: 249
			public IntPtr minimumApplicationAddress;

			// Token: 0x040000FA RID: 250
			public IntPtr maximumApplicationAddress;

			// Token: 0x040000FB RID: 251
			public IntPtr activeProcessorMask;

			// Token: 0x040000FC RID: 252
			public uint numberOfProcessors;

			// Token: 0x040000FD RID: 253
			public uint processorType;

			// Token: 0x040000FE RID: 254
			public uint allocationGranularity;

			// Token: 0x040000FF RID: 255
			public ushort processorLevel;

			// Token: 0x04000100 RID: 256
			public ushort processorRevision;
		}
	}
}
