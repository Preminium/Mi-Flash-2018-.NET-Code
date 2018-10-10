using System;
using System.Runtime.InteropServices;
using System.Text;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000062 RID: 98
	public class DeviceInfo
	{
		// Token: 0x06000215 RID: 533
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiClassGuidsFromNameA(string ClassN, ref Guid guids, uint ClassNameSize, ref uint ReqSize);

		// Token: 0x06000216 RID: 534
		[DllImport("setupapi.dll")]
		private static extern IntPtr SetupDiGetClassDevsA(ref Guid ClassGuid, uint Enumerator, IntPtr hwndParent, uint Flags);

		// Token: 0x06000217 RID: 535
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, DeviceInfo.SP_DEVINFO_DATA DeviceInfoData);

		// Token: 0x06000218 RID: 536
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		// Token: 0x06000219 RID: 537
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiGetDeviceRegistryPropertyA(IntPtr DeviceInfoSet, DeviceInfo.SP_DEVINFO_DATA DeviceInfoData, uint Property, uint PropertyRegDataType, StringBuilder PropertyBuffer, uint PropertyBufferSize, IntPtr RequiredSize);

		// Token: 0x0600021A RID: 538 RVA: 0x00015FF4 File Offset: 0x000141F4
		public static int EnumerateDevices(uint DeviceIndex, string ClassName, StringBuilder DeviceName)
		{
			uint num = 0u;
			Guid empty = Guid.Empty;
			Guid[] array = new Guid[1];
			DeviceInfo.SP_DEVINFO_DATA sp_DEVINFO_DATA = new DeviceInfo.SP_DEVINFO_DATA();
			bool flag = DeviceInfo.SetupDiClassGuidsFromNameA(ClassName, ref array[0], num, ref num);
			if (num == 0u)
			{
				DeviceName = new StringBuilder("");
				return -2;
			}
			if (!flag)
			{
				array = new Guid[num];
				flag = DeviceInfo.SetupDiClassGuidsFromNameA(ClassName, ref array[0], num, ref num);
				if (!flag || num == 0u)
				{
					DeviceName = new StringBuilder("");
					return -2;
				}
			}
			IntPtr deviceInfoSet = DeviceInfo.SetupDiGetClassDevsA(ref array[0], 0u, IntPtr.Zero, 2u);
			if (deviceInfoSet.ToInt32() == -1)
			{
				DeviceName = new StringBuilder("");
				return -3;
			}
			sp_DEVINFO_DATA.cbSize = 28;
			sp_DEVINFO_DATA.DevInst = 0;
			sp_DEVINFO_DATA.ClassGuid = Guid.Empty;
			sp_DEVINFO_DATA.Reserved = 0UL;
			if (!DeviceInfo.SetupDiEnumDeviceInfo(deviceInfoSet, DeviceIndex, sp_DEVINFO_DATA))
			{
				DeviceInfo.SetupDiDestroyDeviceInfoList(deviceInfoSet);
				DeviceName = new StringBuilder("");
				return -1;
			}
			DeviceName.Capacity = 1000;
			if (!DeviceInfo.SetupDiGetDeviceRegistryPropertyA(deviceInfoSet, sp_DEVINFO_DATA, 12u, 0u, DeviceName, 1000u, IntPtr.Zero) && !DeviceInfo.SetupDiGetDeviceRegistryPropertyA(deviceInfoSet, sp_DEVINFO_DATA, 0u, 0u, DeviceName, 1000u, IntPtr.Zero))
			{
				DeviceInfo.SetupDiDestroyDeviceInfoList(deviceInfoSet);
				DeviceName = new StringBuilder("");
				return -4;
			}
			return 0;
		}

		// Token: 0x0400021D RID: 541
		private const int DIGCF_PRESENT = 2;

		// Token: 0x0400021E RID: 542
		private const int MAX_DEV_LEN = 1000;

		// Token: 0x0400021F RID: 543
		private const int SPDRP_FRIENDLYNAME = 12;

		// Token: 0x04000220 RID: 544
		private const int SPDRP_DEVICEDESC = 0;

		// Token: 0x02000063 RID: 99
		[StructLayout(LayoutKind.Sequential)]
		private class SP_DEVINFO_DATA
		{
			// Token: 0x04000221 RID: 545
			public int cbSize;

			// Token: 0x04000222 RID: 546
			public Guid ClassGuid;

			// Token: 0x04000223 RID: 547
			public int DevInst;

			// Token: 0x04000224 RID: 548
			public ulong Reserved;
		}
	}
}
