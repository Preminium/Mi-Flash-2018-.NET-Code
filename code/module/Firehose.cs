using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200005A RID: 90
	public class Firehose
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00014F35 File Offset: 0x00013135
		public static string Reset_To_Edl
		{
			get
			{
				return Firehose._reset_to_edl;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00014F3C File Offset: 0x0001313C
		public static string SetBootPartition
		{
			get
			{
				return Firehose._set_boot_partition;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00014F43 File Offset: 0x00013143
		public static string Nop
		{
			get
			{
				return Firehose._nop;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00014F4A File Offset: 0x0001314A
		public static string Configure
		{
			get
			{
				return Firehose._configure;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00014F51 File Offset: 0x00013151
		public static int payload_size
		{
			get
			{
				return 1048576;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00014F58 File Offset: 0x00013158
		public static int MAX_PATCH_VALUE_LEN
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00014F5C File Offset: 0x0001315C
		public static string FIREHOSE_PROGRAM
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data><program SECTOR_SIZE_IN_BYTES=\"{0}\" num_partition_sectors=\"{1}\" start_sector=\"{2}\" physical_partition_number=\"{3}\" {4}/></data>";
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00014F63 File Offset: 0x00013163
		public static string FIREHOSE_SHA256DIGEST
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data><getsha256digest SECTOR_SIZE_IN_BYTES=\"{0}\" num_partition_sectors=\"{1}\" start_sector=\"{2}\" physical_partition_number=\"{3}\"/></data>";
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00014F6A File Offset: 0x0001316A
		public static string FIREHOSE_PATCH
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data><patch SECTOR_SIZE_IN_BYTES=\"{0}\" byte_offset=\"{1}\" filename=\"DISK\" physical_partition_number=\"{2}\" size_in_bytes=\"{3}\" start_sector=\"{4}\" value=\"{5}\" what=\"Update\" {6}/></data>";
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00014F71 File Offset: 0x00013171
		public static string REQ_AUTH
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data> <sig TargetName=\"req\" verbose=\"1\"/></data>";
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00014F78 File Offset: 0x00013178
		public static string AUTH
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data> <sig TargetName=\"sig\" value=\"{0}\" verbose=\"1\"/></data>";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00014F7F File Offset: 0x0001317F
		public static string AUTHP
		{
			get
			{
				return "<?xml version=\"1.0\" ?><data> <sig TargetName=\"sig\" size_in_bytes=\"{0}\" verbose=\"1\"/></data>";
			}
		}

		// Token: 0x040001F1 RID: 497
		private static string _reset_to_edl = "<?xml version=\"1.0\" ?><data><power verbose=\"{0}\"  value=\"reset_to_edl\"/></data>";

		// Token: 0x040001F2 RID: 498
		private static string _set_boot_partition = "<?xml version=\"1.0\" ?><data><setbootablestoragedrive verbose=\"{0}\"  value=\"1\"/></data>";

		// Token: 0x040001F3 RID: 499
		private static string _nop = "<?xml version=\"1.0\" ?><data><nop verbose=\"{0}\"  value=\"ping\"/></data>";

		// Token: 0x040001F4 RID: 500
		private static string _configure = "<?xml version=\"1.0\" ?><data><configure verbose=\"{0}\" AlwaysValidate=\"0\"  ZlpAwareHost=\"1\"  MaxPayloadSizeToTargetInBytes=\"{1}\" MemoryName=\"{2}\" SkipStorageInit=\"{3}\"/></data>";
	}
}
