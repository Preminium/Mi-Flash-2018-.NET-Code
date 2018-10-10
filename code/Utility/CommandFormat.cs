using System;
using System.Runtime.InteropServices;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200002B RID: 43
	public class CommandFormat
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000D8BC File Offset: 0x0000BABC
		public static byte[] StructToBytes(object structObj)
		{
			int num = 48;
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(structObj, intPtr, false);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public static byte[] StructToBytes(object structObj, int length)
		{
			byte[] array = new byte[length];
			IntPtr intPtr = Marshal.AllocHGlobal(length);
			Marshal.StructureToPtr(structObj, intPtr, false);
			Marshal.Copy(intPtr, array, 0, length);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000D92C File Offset: 0x0000BB2C
		public static object BytesToStuct(byte[] bytes, Type type)
		{
			int num = Marshal.SizeOf(type);
			if (num > bytes.Length)
			{
				return null;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(bytes, 0, intPtr, num);
			object result = Marshal.PtrToStructure(intPtr, type);
			Marshal.FreeHGlobal(intPtr);
			return result;
		}
	}
}
