using System;
using System.Collections;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000064 RID: 100
	public class SoftwareImage
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00016143 File Offset: 0x00014343
		public static string ProgrammerPattern
		{
			get
			{
				return "MPRG.*.hex|MPRG.*.mbn|prog_.*_firehose_.*.*|prog_.*firehose_.*.*";
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0001614A File Offset: 0x0001434A
		public static string BootImage
		{
			get
			{
				return "*_msimage.mbn";
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00016151 File Offset: 0x00014351
		public static string ProvisionPattern
		{
			get
			{
				return "provision.*\\.xml";
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00016158 File Offset: 0x00014358
		public static string RawProgramPattern
		{
			get
			{
				return "rawprogram[0-9]{1,20}\\.xml";
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0001615F File Offset: 0x0001435F
		public static string PatchPattern
		{
			get
			{
				return "patch[0-9]{1,20}\\.xml";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00016168 File Offset: 0x00014368
		public static Hashtable DummyProgress
		{
			get
			{
				return new Hashtable
				{
					{
						"xbl",
						1
					},
					{
						"tz",
						2
					},
					{
						"hyp",
						3
					},
					{
						"rpm",
						4
					},
					{
						"emmc_appsboot",
						5
					},
					{
						"pmic",
						6
					},
					{
						"devcfg",
						7
					},
					{
						"BTFM",
						8
					},
					{
						"cmnlib",
						9
					},
					{
						"cmnlib64",
						10
					},
					{
						"NON-HLOS",
						11
					},
					{
						"adspso",
						12
					},
					{
						"mdtp",
						13
					},
					{
						"keymaster",
						14
					},
					{
						"misc",
						15
					},
					{
						"system",
						16
					},
					{
						"cache",
						30
					},
					{
						"userdata",
						34
					},
					{
						"recovery",
						35
					},
					{
						"splash",
						36
					},
					{
						"logo",
						37
					},
					{
						"boot",
						38
					},
					{
						"cust",
						45
					}
				};
			}
		}
	}
}
