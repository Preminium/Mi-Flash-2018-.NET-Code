using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000012 RID: 18
	public class StreamingArg
	{
		// Token: 0x04000046 RID: 70
		public const int HELLO_COMMAND = 1;

		// Token: 0x04000047 RID: 71
		public const int HELLO_RESPONSE = 2;

		// Token: 0x04000048 RID: 72
		public const int READ_PACKET_COMMAND = 3;

		// Token: 0x04000049 RID: 73
		public const int READ_PACKET_RESPONSE = 4;

		// Token: 0x0400004A RID: 74
		public const int STREAM_WRITE_COMMAND = 7;

		// Token: 0x0400004B RID: 75
		public const int STREAM_WRITE_RESPONSE = 8;

		// Token: 0x0400004C RID: 76
		public const int RESET_COMMAND = 11;

		// Token: 0x0400004D RID: 77
		public const int RESET_RESPONSE = 12;

		// Token: 0x0400004E RID: 78
		public const int ERROR_RESPONSE = 13;

		// Token: 0x0400004F RID: 79
		public const int LOG_RESPONSE = 14;

		// Token: 0x04000050 RID: 80
		public const int CLOSE_COMMAND = 21;

		// Token: 0x04000051 RID: 81
		public const int CLOSE_RESPONSE = 22;

		// Token: 0x04000052 RID: 82
		public const int SECURITY_MODE_COMMAND = 23;

		// Token: 0x04000053 RID: 83
		public const int SECURITY_MODE_RESPONSE = 24;

		// Token: 0x04000054 RID: 84
		public const int OPEN_MULTI_IMAGE_COMMAND = 27;

		// Token: 0x04000055 RID: 85
		public const int OPEN_MULTI_IMAGE_RESPONSE = 28;

		// Token: 0x04000056 RID: 86
		public const int ASYNC_HDLC_FLAG = 126;

		// Token: 0x04000057 RID: 87
		public const int ASYNC_HDLC_ESC = 125;

		// Token: 0x04000058 RID: 88
		public const int ASYNC_HDLC_MASK = 32;

		// Token: 0x04000059 RID: 89
		public const int RCV_HUNT_FLAG = 0;

		// Token: 0x0400005A RID: 90
		public const int RCV_GOT_FLAG = 1;

		// Token: 0x0400005B RID: 91
		public const int RCV_GATHER_DATA = 2;

		// Token: 0x0400005C RID: 92
		public const int RCV_GOT_PACKET = 3;

		// Token: 0x0400005D RID: 93
		public const int CRC16_SEED = 0;

		// Token: 0x0400005E RID: 94
		public const int CRC16_OK = 3911;

		// Token: 0x0400005F RID: 95
		public const int CRC32_SEED = 0;
	}
}
