using System;

namespace XiaoMiFlash.code.module
{
	// Token: 0x02000036 RID: 54
	public class CommandPacket
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00014668 File Offset: 0x00012868
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00014670 File Offset: 0x00012870
		public int Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00014679 File Offset: 0x00012879
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00014681 File Offset: 0x00012881
		public int Length
		{
			get
			{
				return this._Lengthgth;
			}
			set
			{
				this._Lengthgth = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0001468A File Offset: 0x0001288A
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00014692 File Offset: 0x00012892
		public int VersionNumber
		{
			get
			{
				return this._Versionnumber;
			}
			set
			{
				this._Versionnumber = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0001469B File Offset: 0x0001289B
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000146A3 File Offset: 0x000128A3
		public int VersionCompatible
		{
			get
			{
				return this._Versioncompatible;
			}
			set
			{
				this._Versioncompatible = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000146AC File Offset: 0x000128AC
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000146B4 File Offset: 0x000128B4
		public int CommandPacketLengthgth
		{
			get
			{
				return this._commandpacketLengthgth;
			}
			set
			{
				this._commandpacketLengthgth = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000146BD File Offset: 0x000128BD
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000146C5 File Offset: 0x000128C5
		public int Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				this._Mode = value;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000146CE File Offset: 0x000128CE
		public CommandPacket()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000146D8 File Offset: 0x000128D8
		public CommandPacket(byte[] arr)
		{
			if (arr.Length >= 48)
			{
				for (int i = 0; i < arr.Length; i++)
				{
					if (i % 4 == 0)
					{
						int num = i;
						if (num <= 8)
						{
							if (num != 0)
							{
								if (num != 4)
								{
									if (num == 8)
									{
										this._Versionnumber = (int)arr[i];
									}
								}
								else
								{
									this._Lengthgth = (int)arr[i];
								}
							}
							else
							{
								this._command = (int)arr[i];
							}
						}
						else if (num != 12)
						{
							if (num != 16)
							{
								if (num == 20)
								{
									this._Mode = (int)arr[i];
								}
							}
							else
							{
								this._commandpacketLengthgth = (int)arr[i];
							}
						}
						else
						{
							this._Versioncompatible = (int)arr[i];
						}
					}
				}
			}
		}

		// Token: 0x0400015D RID: 349
		private int _command;

		// Token: 0x0400015E RID: 350
		private int _Lengthgth;

		// Token: 0x0400015F RID: 351
		private int _Versionnumber;

		// Token: 0x04000160 RID: 352
		private int _Versioncompatible;

		// Token: 0x04000161 RID: 353
		private int _commandpacketLengthgth;

		// Token: 0x04000162 RID: 354
		private int _Mode;
	}
}
