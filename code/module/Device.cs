using System;
using System.Collections.Generic;
using System.Threading;
using XiaoMiFlash.code.bl;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.module
{
	// Token: 0x0200005D RID: 93
	public class Device
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000151B6 File Offset: 0x000133B6
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000151BE File Offset: 0x000133BE
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000151C7 File Offset: 0x000133C7
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000151CF File Offset: 0x000133CF
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000151D8 File Offset: 0x000133D8
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000151E0 File Offset: 0x000133E0
		public float Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				this._progress = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000151E9 File Offset: 0x000133E9
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000151F1 File Offset: 0x000133F1
		public DateTime StartTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000151FA File Offset: 0x000133FA
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00015202 File Offset: 0x00013402
		public float Elapse
		{
			get
			{
				return this._elapse;
			}
			set
			{
				this._elapse = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0001520B File Offset: 0x0001340B
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00015213 File Offset: 0x00013413
		public string Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0001521C File Offset: 0x0001341C
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00015224 File Offset: 0x00013424
		public List<string> StatusList
		{
			get
			{
				return this._statuslist;
			}
			set
			{
				this._statuslist = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0001522D File Offset: 0x0001342D
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00015235 File Offset: 0x00013435
		public string Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0001523E File Offset: 0x0001343E
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00015246 File Offset: 0x00013446
		public bool? IsDone
		{
			get
			{
				return this._isdone;
			}
			set
			{
				this._isdone = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0001524F File Offset: 0x0001344F
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00015257 File Offset: 0x00013457
		public bool IsUpdate
		{
			get
			{
				return this._isupdate;
			}
			set
			{
				this._isupdate = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00015260 File Offset: 0x00013460
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00015268 File Offset: 0x00013468
		public DeviceCtrl DeviceCtrl
		{
			get
			{
				return this._devicectrl;
			}
			set
			{
				this._devicectrl = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00015271 File Offset: 0x00013471
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00015279 File Offset: 0x00013479
		public Thread DThread
		{
			get
			{
				return this._thread;
			}
			set
			{
				this._thread = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00015282 File Offset: 0x00013482
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0001528A File Offset: 0x0001348A
		public Cmd DCmd
		{
			get
			{
				return this._cmd;
			}
			set
			{
				this._cmd = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00015293 File Offset: 0x00013493
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0001529B File Offset: 0x0001349B
		public Comm DComm
		{
			get
			{
				return this._comm;
			}
			set
			{
				this._comm = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000152A4 File Offset: 0x000134A4
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000152AC File Offset: 0x000134AC
		public bool CheckCPUID
		{
			get
			{
				return this._checkcpuid;
			}
			set
			{
				this._checkcpuid = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000152B5 File Offset: 0x000134B5
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000152BD File Offset: 0x000134BD
		public string DeviceType
		{
			get
			{
				return this._devicetype;
			}
			set
			{
				this._devicetype = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000152C6 File Offset: 0x000134C6
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000152CE File Offset: 0x000134CE
		public ushort IdProduct
		{
			get
			{
				return this._idproduct;
			}
			set
			{
				this._idproduct = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000152D7 File Offset: 0x000134D7
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000152DF File Offset: 0x000134DF
		public ushort IdVendor
		{
			get
			{
				return this._idvendor;
			}
			set
			{
				this._idvendor = value;
			}
		}

		// Token: 0x040001F7 RID: 503
		private int _index;

		// Token: 0x040001F8 RID: 504
		private string _name;

		// Token: 0x040001F9 RID: 505
		private float _progress;

		// Token: 0x040001FA RID: 506
		private DateTime _startTime;

		// Token: 0x040001FB RID: 507
		private float _elapse;

		// Token: 0x040001FC RID: 508
		private string _status;

		// Token: 0x040001FD RID: 509
		private List<string> _statuslist = new List<string>();

		// Token: 0x040001FE RID: 510
		private string _result = "";

		// Token: 0x040001FF RID: 511
		private bool? _isdone;

		// Token: 0x04000200 RID: 512
		private bool _isupdate;

		// Token: 0x04000201 RID: 513
		private DeviceCtrl _devicectrl;

		// Token: 0x04000202 RID: 514
		private Thread _thread;

		// Token: 0x04000203 RID: 515
		private Cmd _cmd;

		// Token: 0x04000204 RID: 516
		private Comm _comm;

		// Token: 0x04000205 RID: 517
		private bool _checkcpuid;

		// Token: 0x04000206 RID: 518
		private string _devicetype;

		// Token: 0x04000207 RID: 519
		private ushort _idproduct;

		// Token: 0x04000208 RID: 520
		private ushort _idvendor;
	}
}
