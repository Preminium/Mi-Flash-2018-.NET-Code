using System;
using System.Runtime.InteropServices;
using MiUSB;

namespace XiaoMiFlash
{
	// Token: 0x02000050 RID: 80
	public class Test
	{
		// Token: 0x060001A2 RID: 418
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool DeviceIoControl(IntPtr hFile, int dwIoControlCode, ref byte[] lpInBuffer, int nInBufferSize, ref byte[] lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x060001A3 RID: 419 RVA: 0x00014964 File Offset: 0x00012B64
		public static UsbNodeInformation[] GetUsbNodeInformation(string DevicePath)
		{
			if (string.IsNullOrEmpty(DevicePath))
			{
				return null;
			}
			IntPtr intPtr = Kernel32.CreateFile("\\\\.\\" + DevicePath, NativeFileAccess.GENERIC_WRITE, NativeFileShare.FILE_SHARE_WRITE, IntPtr.Zero, NativeFileMode.OPEN_EXISTING, IntPtr.Zero, IntPtr.Zero);
			if (intPtr == Kernel32.INVALID_HANDLE_VALUE)
			{
				return null;
			}
			byte[] structure = new byte[76];
			int num;
			bool flag = Test.DeviceIoControl(intPtr, 2229256, ref structure, Marshal.SizeOf(structure), ref structure, Marshal.SizeOf(structure), out num, IntPtr.Zero);
			Marshal.GetLastWin32Error();
			Kernel32.CloseHandle(intPtr);
			if (!flag)
			{
				return null;
			}
			return null;
		}
	}
}
