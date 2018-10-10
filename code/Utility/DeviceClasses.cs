using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000052 RID: 82
	public class DeviceClasses
	{
		// Token: 0x060001AD RID: 429
		[DllImport("cfgmgr32.dll")]
		private static extern uint CM_Enumerate_Classes(uint ClassIndex, ref Guid ClassGuid, uint Params);

		// Token: 0x060001AE RID: 430
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiClassNameFromGuidA(ref Guid ClassGuid, StringBuilder ClassName, uint ClassNameSize, ref uint RequiredSize);

		// Token: 0x060001AF RID: 431
		[DllImport("setupapi.dll")]
		private static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, uint Enumerator, IntPtr hwndParent, uint Flags);

		// Token: 0x060001B0 RID: 432
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		// Token: 0x060001B1 RID: 433
		[DllImport("setupapi.dll")]
		private static extern IntPtr SetupDiOpenClassRegKeyExA(ref Guid ClassGuid, uint samDesired, int Flags, IntPtr MachineName, uint Reserved);

		// Token: 0x060001B2 RID: 434
		[DllImport("setupapi.dll")]
		private static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, DeviceClasses.SP_DEVINFO_DATA DeviceInfoData);

		// Token: 0x060001B3 RID: 435
		[DllImport("advapi32.dll")]
		private static extern uint RegQueryValueA(IntPtr KeyClass, uint SubKey, StringBuilder ClassDescription, ref uint sizeB);

		// Token: 0x060001B4 RID: 436
		[DllImport("user32.dll")]
		public static extern int LoadBitmapW(int hInstance, ulong Reserved);

		// Token: 0x060001B5 RID: 437
		[DllImport("setupapi.dll")]
		public static extern bool SetupDiGetClassImageList(out DeviceClasses.SP_CLASSIMAGELIST_DATA ClassImageListData);

		// Token: 0x060001B6 RID: 438
		[DllImport("setupapi.dll")]
		public static extern int SetupDiDrawMiniIcon(Graphics hdc, DeviceClasses.RECT rc, int MiniIconIndex, int Flags);

		// Token: 0x060001B7 RID: 439
		[DllImport("setupapi.dll")]
		public static extern bool SetupDiGetClassBitmapIndex(Guid ClassGuid, out int MiniIconIndex);

		// Token: 0x060001B8 RID: 440
		[DllImport("setupapi.dll")]
		public static extern int SetupDiLoadClassIcon(ref Guid classGuid, out IntPtr hIcon, out int index);

		// Token: 0x060001B9 RID: 441 RVA: 0x00014AEC File Offset: 0x00012CEC
		public static int EnumerateClasses(uint ClassIndex, StringBuilder ClassName, StringBuilder ClassDescription, ref bool DevicePresent)
		{
			Guid empty = Guid.Empty;
			DeviceClasses.SP_DEVINFO_DATA sp_DEVINFO_DATA = new DeviceClasses.SP_DEVINFO_DATA();
			uint num = 0u;
			uint num2 = DeviceClasses.CM_Enumerate_Classes(ClassIndex, ref empty, 0u);
			DevicePresent = false;
			new DeviceClasses.SP_CLASSIMAGELIST_DATA();
			if (num2 != 0u)
			{
				return (int)num2;
			}
			DeviceClasses.SetupDiClassNameFromGuidA(ref empty, ClassName, num, ref num);
			if (num > 0u)
			{
				ClassName.Capacity = (int)num;
				DeviceClasses.SetupDiClassNameFromGuidA(ref empty, ClassName, num, ref num);
			}
			IntPtr deviceInfoSet = DeviceClasses.SetupDiGetClassDevs(ref empty, 0u, IntPtr.Zero, 2u);
			if (deviceInfoSet.ToInt32() == -1)
			{
				DevicePresent = false;
				return 0;
			}
			uint memberIndex = 0u;
			sp_DEVINFO_DATA.cbSize = 28;
			sp_DEVINFO_DATA.DevInst = 0;
			sp_DEVINFO_DATA.ClassGuid = Guid.Empty;
			sp_DEVINFO_DATA.Reserved = 0UL;
			if (!DeviceClasses.SetupDiEnumDeviceInfo(deviceInfoSet, memberIndex, sp_DEVINFO_DATA))
			{
				DevicePresent = false;
				return 0;
			}
			DeviceClasses.SetupDiDestroyDeviceInfoList(deviceInfoSet);
			IntPtr keyClass = DeviceClasses.SetupDiOpenClassRegKeyExA(ref empty, 33554432u, 1, IntPtr.Zero, 0u);
			if (keyClass.ToInt32() == -1)
			{
				DevicePresent = false;
				return 0;
			}
			uint num3 = 1000u;
			ClassDescription.Capacity = 1000;
			uint num4 = DeviceClasses.RegQueryValueA(keyClass, 0u, ClassDescription, ref num3);
			if (num4 != 0u)
			{
				ClassDescription = new StringBuilder("");
			}
			DevicePresent = true;
			DeviceClasses.ClassesGuid = sp_DEVINFO_DATA.ClassGuid;
			return 0;
		}

		// Token: 0x040001C5 RID: 453
		public const int MAX_SIZE_DEVICE_DESCRIPTION = 1000;

		// Token: 0x040001C6 RID: 454
		public const int CR_SUCCESS = 0;

		// Token: 0x040001C7 RID: 455
		public const int CR_NO_SUCH_VALUE = 37;

		// Token: 0x040001C8 RID: 456
		public const int CR_INVALID_DATA = 31;

		// Token: 0x040001C9 RID: 457
		private const int DIGCF_PRESENT = 2;

		// Token: 0x040001CA RID: 458
		private const int DIOCR_INSTALLER = 1;

		// Token: 0x040001CB RID: 459
		private const int MAXIMUM_ALLOWED = 33554432;

		// Token: 0x040001CC RID: 460
		public const int DMI_MASK = 1;

		// Token: 0x040001CD RID: 461
		public const int DMI_BKCOLOR = 2;

		// Token: 0x040001CE RID: 462
		public const int DMI_USERECT = 4;

		// Token: 0x040001CF RID: 463
		public const int DIGCF_DEFAULT = 1;

		// Token: 0x040001D0 RID: 464
		public const int DIGCF_ALLCLASSES = 4;

		// Token: 0x040001D1 RID: 465
		public const int DIGCF_PROFILE = 8;

		// Token: 0x040001D2 RID: 466
		public const int DIGCF_DEVICEINTERFACE = 16;

		// Token: 0x040001D3 RID: 467
		public static Guid ClassesGuid;

		// Token: 0x02000053 RID: 83
		[StructLayout(LayoutKind.Sequential)]
		private class SP_DEVINFO_DATA
		{
			// Token: 0x040001D4 RID: 468
			public int cbSize;

			// Token: 0x040001D5 RID: 469
			public Guid ClassGuid;

			// Token: 0x040001D6 RID: 470
			public int DevInst;

			// Token: 0x040001D7 RID: 471
			public ulong Reserved;
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Sequential)]
		public class SP_CLASSIMAGELIST_DATA
		{
			// Token: 0x040001D8 RID: 472
			public int cbSize;

			// Token: 0x040001D9 RID: 473
			public ImageList ImageList;

			// Token: 0x040001DA RID: 474
			public ulong Reserved;
		}

		// Token: 0x02000055 RID: 85
		public struct RECT
		{
			// Token: 0x040001DB RID: 475
			private long left;

			// Token: 0x040001DC RID: 476
			private long top;

			// Token: 0x040001DD RID: 477
			private long right;

			// Token: 0x040001DE RID: 478
			private long bottom;
		}
	}
}
