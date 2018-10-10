using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MiUSB;
using XiaoMiFlash.code.authFlash;
using XiaoMiFlash.code.bl;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.lan;
using XiaoMiFlash.code.MiControl;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;
using XiaoMiFlash.form;
using XiaoMiFlash.Properties;

namespace XiaoMiFlash
{
	// Token: 0x02000033 RID: 51
	public partial class MainFrm : MiBaseFrm
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public MainFrm()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000EF8B File Offset: 0x0000D18B
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000EF93 File Offset: 0x0000D193
		public string ValidateSpecifyXml
		{
			get
			{
				return this.validateSpecifyXml;
			}
			set
			{
				this.validateSpecifyXml = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public string SwPath
		{
			get
			{
				return this.txtPath.Text;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000EFA9 File Offset: 0x0000D1A9
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		public bool ReadBackVerify
		{
			get
			{
				return this.readbackverify;
			}
			set
			{
				this.readbackverify = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
		public bool OpenWriteDump
		{
			get
			{
				return this.openwritedump;
			}
			set
			{
				this.openwritedump = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000EFCB File Offset: 0x0000D1CB
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000EFD3 File Offset: 0x0000D1D3
		public bool OpenReadDump
		{
			get
			{
				return this.openreaddump;
			}
			set
			{
				this.openreaddump = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public bool Verbose
		{
			get
			{
				return this.verbose;
			}
			set
			{
				this.verbose = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000EFED File Offset: 0x0000D1ED
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000EFF5 File Offset: 0x0000D1F5
		public bool AutoDetectDevice
		{
			get
			{
				return this.autodetectdevice;
			}
			set
			{
				this.autodetectdevice = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000F000 File Offset: 0x0000D200
		public List<string> ComportList
		{
			get
			{
				string item = string.Empty;
				if (!string.IsNullOrEmpty(MiAppConfig.Get("mtkComs")))
				{
					this.comportList = MiAppConfig.Get("mtkComs").Split(new char[]
					{
						','
					}).ToList<string>();
				}
				else
				{
					this.comportList.Clear();
					UsbDevice.MtkDevice.Clear();
				}
				UsbDevice.MtkDevice.Clear();
				foreach (string text in this.comportList)
				{
					if (!string.IsNullOrEmpty(text))
					{
						item = string.Format("com{0}", text);
						if (UsbDevice.MtkDevice.IndexOf(item) < 0)
						{
							UsbDevice.MtkDevice.Add(item);
						}
					}
				}
				return this.comportList;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public void SetFactory(string factory)
		{
			if (!string.IsNullOrEmpty(factory))
			{
				this.statusTab.Text = factory;
				this.isFactory = true;
				this.ForceLockCrc();
			}
			else
			{
				this.statusTab.Text = factory;
				this.isFactory = false;
			}
			MiFlashGlobal.IsFactory = this.isFactory;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000F130 File Offset: 0x0000D330
		public void SetChip(string chip)
		{
			if (!string.IsNullOrEmpty(chip))
			{
				if (chip != null)
				{
					if (chip == "Qualcomm")
					{
						this.pnlMTK.Visible = false;
						this.pnlQcom.Visible = true;
						this.devicelist.Location = new Point(21, 113);
						return;
					}
					if (chip == "MTK")
					{
						this.pnlMTK.Visible = true;
						this.pnlQcom.Visible = false;
						this.devicelist.Location = new Point(21, 154);
						if (!string.IsNullOrEmpty(MiAppConfig.Get("scatter")))
						{
							this.txtScatter.Text = MiAppConfig.Get("scatter");
						}
						if (!string.IsNullOrEmpty(MiAppConfig.Get("da")))
						{
							this.txtDa.Text = MiAppConfig.Get("da").ToString();
						}
						this.cmbDlType.Items.Clear();
						foreach (object obj in MiProperty.DlTable)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.cmbDlType.Items.Add(dictionaryEntry.Key.ToString());
						}
						if (this.cmbDlType.Items.Count > 0)
						{
							if (!string.IsNullOrEmpty(MiAppConfig.Get("dlType")))
							{
								this.cmbDlType.Text = MiAppConfig.Get("dlType");
							}
							else
							{
								this.cmbDlType.Text = this.cmbDlType.Items[0].ToString();
							}
						}
						this.cmbChkSum.Items.Clear();
						foreach (KeyValuePair<string, int> keyValuePair in MiProperty.ChkSumTable)
						{
							this.cmbChkSum.Items.Add(keyValuePair.Key.ToString());
						}
						if (this.cmbChkSum.Items.Count > 0)
						{
							if (!string.IsNullOrEmpty(MiAppConfig.Get("chkSum")))
							{
								this.cmbChkSum.Text = MiAppConfig.Get("chkSum");
							}
							else
							{
								this.cmbChkSum.Text = this.cmbChkSum.Items[0].ToString();
							}
						}
						if (this.ComportList.Count != 0)
						{
							return;
						}
						string text = MiAppConfig.Get("mtkComs");
						if (!string.IsNullOrEmpty(text))
						{
							text.Split(new char[]
							{
								','
							});
							return;
						}
						return;
					}
				}
				this.pnlMTK.Visible = false;
				this.pnlQcom.Visible = true;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000F404 File Offset: 0x0000D604
		private bool IsRunAsAdmin()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
			return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x06000142 RID: 322
		private void MainFrm_Load(object sender, EventArgs e)
		{
			this.Text = "Mi Flash 2018 Modified By Ye Yeint (Nga Yel)";
			string name = "Software\\XiaoMi\\MiFlash\\";
			if (Registry.LocalMachine.OpenSubKey(name, true) == null)
			{
				new DriverFrm
				{
					TopMost = true
				}.Show();
			}
			this.SetLanguage();
			string text = MiAppConfig.Get("swPath");
			this.txtPath.Text = text;
			this.SetFactory(MiAppConfig.Get("factory"));
			this.factory = MiAppConfig.Get("factory");
			if (Directory.Exists(this.txtPath.Text))
			{
				this.SetScriptItems(this.txtPath.Text);
				this.cmbScriptItem.SetText(MiAppConfig.Get("script"));
			}
			this.script = MiAppConfig.Get("script");
			this.SetChip(MiAppConfig.Get("chip"));
			this.txtScatter.Text = MiAppConfig.Get("scatter");
			MiFlashGlobal.Version = this.Text;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000F51C File Offset: 0x0000D71C
		private void checkPython()
		{
			RegistryKey localMachine = Registry.LocalMachine;
			RegistryKey registryKey = localMachine.OpenSubKey("Software\\Python\\");
			registryKey.GetValue("Python").ToString();
			registryKey.Close();
			string dosCommand = string.Format("{0} /i {1}  /qn", Script.msiexec, Script.pythonMsi);
			Cmd cmd = new Cmd("", "");
			cmd.Execute(null, dosCommand);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000F580 File Offset: 0x0000D780
		public void AutoDetectUsb()
		{
			this.RefreshDevice();
			if (this.AutoDetectDevice)
			{
				this.miUsb.AddUSBEventWatcher(new EventArrivedEventHandler(this.USBInsertHandler), null, new TimeSpan(0, 0, 3));
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		private void USBInsertHandler(object sender, EventArrivedEventArgs e)
		{
			if (e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent")
			{
				this.btnAutoFlash.BeginInvoke(new Action<string>(delegate(string msg)
				{
					this.btnAutoFlash_Click(this.btnAutoFlash, new EventArgs());
				}), new object[]
				{
					""
				});
				return;
			}
			if (e.NewEvent.ClassPath.ClassName == "__InstanceDeletionEvent")
			{
				this.btnAutoFlash.BeginInvoke(new Action<string>(delegate(string msg)
				{
					this.btnAutoFlash_Click(this.btnAutoFlash, new EventArgs());
				}), new object[]
				{
					""
				});
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000F67C File Offset: 0x0000D87C
		private void USBRemoveHandler(object sender, EventArrivedEventArgs e)
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000F694 File Offset: 0x0000D894
		private void SetText(string text)
		{
			if (this.btnRefresh.InvokeRequired)
			{
				this.btnRefresh.BeginInvoke(new Action<string>(delegate(string msg)
				{
					this.btnRefresh_Click(this.btnRefresh, new EventArgs());
				}), new object[]
				{
					text
				});
				return;
			}
			this.btnRefresh_Click(this.btnRefresh, new EventArgs());
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000F760 File Offset: 0x0000D960
		private void SetLogText(string text)
		{
			try
			{
				if (this.txtLog.InvokeRequired)
				{
					this.txtLog.BeginInvoke(new Action<string>(delegate(string msg)
					{
						if (this.txtLog.Text.Length >= 575488)
						{
							this.txtLog.Text = "";
						}
						this.txtLog.AppendText(msg);
						this.txtLog.AppendText("\r\n");
						this.txtLog.Select(this.txtLog.TextLength, 0);
						this.txtLog.ScrollToCaret();
					}), new object[]
					{
						text
					});
				}
				else
				{
					if (this.txtLog.Text.Length >= 575488)
					{
						this.txtLog.Text = "";
					}
					this.txtLog.AppendText(text);
					this.txtLog.AppendText("\r\n");
					this.txtLog.Select(this.txtLog.TextLength, 0);
					this.txtLog.ScrollToCaret();
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message + ":" + ex.StackTrace);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000F880 File Offset: 0x0000DA80
		private void Valide()
		{
			try
			{
				string validateMsg = ImageValidation.Validate(this.txtPath.Text);
				if (validateMsg.IndexOf("md5 validate successfully") < 0)
				{
					this.lblMD5.ForeColor = Color.Red;
				}
				base.Invoke(new EventHandler(delegate(object A_1, EventArgs A_2)
				{
					this.lblMD5.Text = validateMsg;
					this.frm.TopMost = false;
					this.frm.Hide();
				}));
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000F980 File Offset: 0x0000DB80
		private void ShowMsg(string msg, bool openOrClose)
		{
			try
			{
				base.Invoke(new EventHandler(delegate(object A_1, EventArgs A_2)
				{
					this.lblMD5.Text = msg;
					if (openOrClose)
					{
						this.frm.TopMost = true;
						this.frm.Show();
						return;
					}
					this.frm.TopMost = false;
					this.frm.Hide();
				}));
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		private void btnBrwDic_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.fbdSelect.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				MemImg.distory();
				this.txtPath.Text = this.fbdSelect.SelectedPath;
				this.SetScriptItems(this.txtPath.Text);
				if (MiAppConfig.GetAppConfig("checkMD5").ToLower() != "true")
				{
					return;
				}
				try
				{
					this.frm.TopMost = true;
					this.frm.Show();
					new Thread(new ThreadStart(this.Valide))
					{
						IsBackground = true
					}.Start();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Log.w(ex.Message + " " + ex.StackTrace);
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		private void btnSelectScatter_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "scatter|*.txt";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.txtScatter.Text = openFileDialog.FileName;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		private void btnDa_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "da|*.bin";
			openFileDialog.InitialDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "da";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.txtDa.Text = openFileDialog.FileName;
				MiAppConfig.SetValue("da", this.txtDa.Text);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000FB64 File Offset: 0x0000DD64
		private void SetScriptItems(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			List<string> list = new List<string>();
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Name.LastIndexOf(".bat") >= 0)
				{
					list.Add(fileInfo.Name);
				}
			}
			if (list.Count<string>() == 0)
			{
				MessageBox.Show("couldn't find flash script.");
				return;
			}
			this.cmbScriptItem.SetItem(list.ToArray());
			if (!Directory.Exists(this.txtPath.Text))
			{
				return;
			}
			string[] array = FileSearcher.SearchFiles(this.txtPath.Text, FlashType.SaveUserData);
			if (this.rdoCleanAll.IsChecked)
			{
				this.script = FlashType.CleanAll;
			}
			else if (this.rdoSaveUserData.IsChecked)
			{
				this.script = Path.GetFileName(array[0]);
			}
			else if (this.rdoCleanAllAndLock.IsChecked)
			{
				this.script = FlashType.CleanAllAndLock;
			}
			this.cmbScriptItem.SetText(this.script);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshDevice();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public void RefreshDevice()
		{
			try
			{
				this.btnRefresh.Enabled = false;
				this.btnFlash.Enabled = false;
				this.btnRefresh.Cursor = Cursors.WaitCursor;
				this.btnFlash.Cursor = Cursors.WaitCursor;
				this.deviceArr = UsbDevice.GetDevice();
				ListView.ListViewItemCollection items = this.devicelist.Items;
				new List<Thread>(this.threads);
				Device d;
				IEnumerable<string> source = from d in FlashingDevice.flashDeviceList
				where d.IsDone == null || (d.IsDone != null && d.IsDone.Value && !d.IsUpdate)
				select d.Name;
				foreach (string text in source.ToList<string>())
				{
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.Name == text.ToString())
						{
							Log.w(string.Format("FlashingDevice.flashDeviceList.Remove {0}", device.Name));
							device.StatusList.Clear();
							FlashingDevice.flashDeviceList.Remove(device);
							break;
						}
					}
					foreach (object obj in items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						string text2 = listViewItem.SubItems[1].Text;
						if (text2 == text.ToString())
						{
							items.Remove(listViewItem);
							break;
						}
					}
					foreach (object obj2 in this.devicelist.Controls)
					{
						Control control = (Control)obj2;
						if (control.Name == text.ToString() + "progressbar")
						{
							this.devicelist.Controls.Remove(control);
							break;
						}
					}
				}
				foreach (Device d2 in this.deviceArr)
				{
					d = d2;
					source = from fd in FlashingDevice.flashDeviceList
					where fd.Name == d.Name
					select fd.Name;
					if (source.Count<string>() == 0)
					{
						d.Progress = 0f;
						d.IsDone = null;
						if (MiAppConfig.Get("chip") == "MTK")
						{
							d.Status = "wait for device insert";
							d.IsUpdate = true;
							d.StartTime = DateTime.Now;
						}
						else
						{
							d.IsUpdate = false;
						}
						if (d.Name.Contains("?"))
						{
							Log.w(d.Name + "device is err Because it contains '?'");
						}
						else
						{
							Log.w("add device " + d.Name);
							FlashingDevice.flashDeviceList.Add(d);
							float num = 0f;
							this.DrawCln(d.Index, d.Name, (double)num);
						}
					}
					else
					{
						Log.w("Dulicate device " + d.Name);
					}
				}
				this.reDrawProgress();
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
				Log.w(ex.StackTrace);
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.btnRefresh.Enabled = true;
				this.btnRefresh.Cursor = Cursors.Default;
				this.btnFlash.Enabled = true;
				this.btnFlash.Cursor = Cursors.Default;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000101F8 File Offset: 0x0000E3F8
		private void DrawCln(int deviceIndex, string deviceName, double progress)
		{
			ListViewItem listViewItem = new ListViewItem(new string[]
			{
				deviceIndex.ToString(),
				deviceName,
				"",
				"0s",
				"",
				""
			});
			this.devicelist.Items.Add(listViewItem);
			Rectangle rectangle = default(Rectangle);
			ProgressBar progressBar = new ProgressBar();
			rectangle = listViewItem.SubItems[2].Bounds;
			rectangle.Width = this.devicelist.Columns[2].Width;
			progressBar.Parent = this.devicelist;
			progressBar.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			progressBar.Value = (int)progress;
			progressBar.Visible = true;
			progressBar.Name = deviceName + "progressbar";
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000102E0 File Offset: 0x0000E4E0
		private void btnFlash_Click(object sender, EventArgs e)
		{
			if (!this.isAutoFlash)
			{
				this.RefreshDevice();
			}
			if (string.IsNullOrEmpty(this.txtPath.Text))
			{
				MessageBox.Show("Please select sw");
				return;
			}
			File.Exists(this.txtPath.Text + "\\" + this.script);
			if (FlashingDevice.flashDeviceList.Count == 0)
			{
				return;
			}
			try
			{
				if (!this.timer_updateStatus.Enabled)
				{
					this.timer_updateStatus.Enabled = true;
				}
				foreach (Device device in FlashingDevice.flashDeviceList)
				{
					if (device.StatusList.Count > 0)
					{
						Log.w(device.Name + " already in flashing");
					}
					else if (device.StartTime > DateTime.MinValue && device.IsDone != null && !device.IsDone.Value && device.IsUpdate)
					{
						Log.w(device.Name + " already in flashing");
					}
					else
					{
						device.StartTime = DateTime.Now;
						device.Status = "flashing";
						device.Progress = 0f;
						device.IsDone = new bool?(false);
						device.IsUpdate = true;
						Log.w(device.Name, MiFlashGlobal.Version);
						FlashingDevice.UpdateDeviceStatus(device.Name, null, "start flash", "flashing", false);
						if (this.isFactory)
						{
							if (device.DeviceCtrl.GetType() == typeof(ScriptDevice))
							{
								this.ForceLockCrc();
								Log.w(device.Name, "factory dll log start");
								CheckCPUIDResult searchPathD = FactoryCtrl.GetSearchPathD(device.Name, this.txtPath.Text.Trim());
								if (!searchPathD.Result)
								{
									FlashingDevice.UpdateDeviceStatus(device.Name, null, string.Format("error:CheckCPUID result {0} status {1}", searchPathD.Result.ToString(), searchPathD.Msg), "factory ev error", true);
									Log.w(device.Name, string.Format("error:device {0} CheckCPUID result {1} status {2}", device.Name, searchPathD.Result.ToString(), searchPathD.Msg), false);
									continue;
								}
								this.txtPath.Text = searchPathD.Path;
								Log.w(device.Name, "factory dll log end");
							}
							else if (device.DeviceCtrl.GetType() == typeof(SerialPortDevice))
							{
								Log.w(device.Name, "factory:" + this.factory);
								if (this.factory == "Inventec")
								{
									this.isAutomate = false;
								}
								if (this.isAutomate && !this.isDaConnect)
								{
									EDL_SA_COMMUNICATION.DaConnect();
									this.isDaConnect = true;
								}
							}
							Log.w(device.Name, "factory env");
						}
						DeviceCtrl deviceCtrl = device.DeviceCtrl;
						deviceCtrl.deviceName = device.Name;
						deviceCtrl.swPath = this.txtPath.Text.Trim();
						deviceCtrl.flashScript = this.script;
						deviceCtrl.readBackVerify = this.ReadBackVerify;
						deviceCtrl.openReadDump = this.OpenReadDump;
						deviceCtrl.openWriteDump = this.OpenWriteDump;
						deviceCtrl.verbose = this.Verbose;
						deviceCtrl.idproduct = device.IdProduct;
						deviceCtrl.idvendor = device.IdVendor;
						Thread thread = new Thread(new ThreadStart(deviceCtrl.flash));
						thread.Name = device.Name;
						thread.IsBackground = true;
						thread.Start();
						device.DThread = thread;
						this.threads.Add(thread);
						Log.w(string.Format("Thread start,thread id {0},thread name {1}", thread.ManagedThreadId, thread.Name));
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Log.w(ex.Message + "  " + ex.StackTrace);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00010724 File Offset: 0x0000E924
		private void ForceLockCrc()
		{
			this.script = FlashType.flash_all_lock_crc;
			this.rdoCleanAll.IsChecked = false;
			this.rdoCleanAll.Enabled = false;
			this.rdoCleanAllAndLock.IsChecked = false;
			this.rdoCleanAllAndLock.Enabled = false;
			this.rdoSaveUserData.IsChecked = false;
			this.rdoSaveUserData.Enabled = false;
			this.cmbScriptItem.OnlyDrop();
			this.cmbScriptItem.SetItem(new string[]
			{
				FlashType.flash_all_lock_crc
			});
			this.cmbScriptItem.SetText(FlashType.flash_all_lock_crc);
			this.cmbScriptItem.Enabled = false;
			this.cmbScriptItem.Enable(false);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000107E8 File Offset: 0x0000E9E8
		private void btnAutoFlash_Click(object sender, EventArgs e)
		{
			this.RefreshDevice();
			this.btnFlash.BeginInvoke(new Action<string>(delegate(string msg)
			{
				this.btnFlash_Click(this.btnFlash, new EventArgs());
			}), new object[]
			{
				""
			});
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00010870 File Offset: 0x0000EA70
		private void btnDownload_Click(object sender, EventArgs e)
		{
			this.timer_updateStatus.Enabled = true;
			this.btnDownload.Enabled = false;
			if (this.ComportList.Count == 0)
			{
				int i;
				for (i = 1; i <= 16; i++)
				{
					IEnumerable<List<Thread>> source = from t in this.threads
					where t.Name == i.ToString()
					select this.threads;
					if (source.Count<List<Thread>>() == 0)
					{
						this.StartConsoleMode(i, 0);
					}
				}
			}
			else
			{
				string item;
				for (int j = 0; j < this.ComportList.Count; j++)
				{
					item = this.ComportList[j];
					IEnumerable<List<Thread>> source2 = from t in this.threads
					where t.Name == item
					select this.threads;
					if (source2.Count<List<Thread>>() == 0)
					{
						this.StartConsoleMode(int.Parse(item), j + 1);
					}
				}
			}
			this.RefreshDevice();
			MiAppConfig.SetValue("scatter", this.txtScatter.Text);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000109C8 File Offset: 0x0000EBC8
		private void btnStop_Click(object sender, EventArgs e)
		{
			MiProcess.KillProcess("SP_download_tool");
			foreach (Thread thread in this.threads)
			{
				if (thread.IsAlive)
				{
					thread.Abort();
				}
			}
			this.threads.Clear();
			foreach (Device device in FlashingDevice.flashDeviceList)
			{
				FlashingDevice.UpdateDeviceStatus(device.Name, new float?(1f), "stopped", "error", true);
			}
			FlashingDevice.flashDeviceList.Clear();
			this.devicelist.Items.Clear();
			this.devicelist.Controls.Clear();
			this.btnDownload.Enabled = true;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		private void StartConsoleMode(int comPort, int sort)
		{
			MTKDevice mtkdevice = new MTKDevice();
			mtkdevice.scatter = this.txtScatter.Text.Trim();
			mtkdevice.da = this.txtDa.Text;
			mtkdevice.dl_type = MiProperty.DlTable[this.cmbDlType.Text].ToString();
			mtkdevice.deviceName = string.Format("com{0}", comPort);
			mtkdevice.comPortIndex = comPort;
			mtkdevice.chkSum = int.Parse(MiProperty.ChkSumTable[this.cmbChkSum.Text].ToString());
			mtkdevice.sort = sort;
			Thread thread = new Thread(new ThreadStart(mtkdevice.flash));
			thread.Name = mtkdevice.comPortIndex.ToString();
			thread.IsBackground = true;
			thread.Start();
			this.threads.Add(thread);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00010BAC File Offset: 0x0000EDAC
		private void timer_updateStatus_Tick(object sender, EventArgs e)
		{
			try
			{
				foreach (Thread thread in this.threads)
				{
				}
				if (FlashingDevice.consoleMode_UsbInserted)
				{
					FlashingDevice.consoleMode_UsbInserted = false;
					this.RefreshDevice();
				}
				bool? flag = null;
				string b = "";
				foreach (object obj in this.devicelist.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (listViewItem.UseItemStyleForSubItems)
					{
						listViewItem.UseItemStyleForSubItems = false;
					}
					b = listViewItem.SubItems[1].Text.ToLower();
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.IsUpdate)
						{
							if (device.DeviceCtrl.GetType() == typeof(ScriptDevice))
							{
								List<string> list = new List<string>(device.StatusList);
								foreach (string value in list)
								{
									string.IsNullOrEmpty(value);
								}
							}
							if (device.Name.ToLower() == b)
							{
								flag = new bool?(true);
								listViewItem.SubItems[2].Text = device.Progress * 100f + "%";
								foreach (object obj2 in this.devicelist.Controls)
								{
									Control control = (Control)obj2;
									if (control.Name == device.Name + "progressbar")
									{
										ProgressBar progressBar = (ProgressBar)control;
										if (progressBar.Value == (int)(device.Progress * 100f) && (int)(device.Progress * 100f) < 100)
										{
											device.Progress += 0.001f;
										}
										progressBar.Value = (int)(device.Progress * 100f);
									}
								}
								if (device.StartTime > DateTime.MinValue)
								{
									int num = (int)DateTime.Now.Subtract(device.StartTime).TotalSeconds;
									listViewItem.SubItems[3].Text = string.Format("{0}s", num.ToString());
								}
								listViewItem.SubItems[4].Text = device.Status;
								listViewItem.SubItems[5].Text = device.Result;
								if (!(device.Status.ToLower() == "wait for device insert"))
								{
									bool? flag2 = null;
									if (device.IsDone != null && device.IsDone.Value && device.Status == "flash done")
									{
										device.IsDone = new bool?(true);
										listViewItem.SubItems[5].BackColor = Color.LightGreen;
										flag2 = new bool?(true);
									}
									if (device.IsDone != null && device.IsDone.Value && device.Result.ToLower() == "success")
									{
										device.IsDone = new bool?(true);
										listViewItem.SubItems[5].BackColor = Color.LightGreen;
										flag2 = new bool?(true);
									}
									else if (device.Result.ToLower().IndexOf("error") >= 0 || device.Result.ToLower().IndexOf("fail") >= 0)
									{
										device.IsDone = new bool?(true);
										listViewItem.SubItems[5].BackColor = Color.Red;
										flag2 = new bool?(false);
									}
									if (flag2 == null)
									{
										listViewItem.SubItems[5].BackColor = Color.White;
									}
									string text = "#USB";
									if (device.IsDone == null || !device.IsDone.Value || flag2 == null)
									{
										break;
									}
									try
									{
										Log.w(device.Name, string.Format("flashSuccess {0}", (flag2 == null) ? "null" : flag2.Value.ToString()), false);
										if (flag2.Value)
										{
											text += string.Format("{0}OK$", device.Index.ToString());
										}
										else
										{
											text += string.Format("{0}NG$", device.Index.ToString());
										}
										TimeSpan timeSpan = DateTime.Now.Subtract(device.StartTime);
										Log.wFlashStatus(string.Format("{0}    {1}     {2}s     {3}", new object[]
										{
											device.Index.ToString(),
											device.Name,
											timeSpan.TotalSeconds.ToString(),
											device.Status
										}));
									}
									catch (Exception ex)
									{
										Log.w(ex.Message + " " + ex.StackTrace);
									}
									Log.w(device.Name, string.Format("isFactory {0} CheckCPUID {1}", this.isFactory.ToString(), device.CheckCPUID), false);
									if (this.isFactory && device.CheckCPUID && flag2 != null && device.DeviceCtrl.GetType() == typeof(ScriptDevice))
									{
										try
										{
											string str = " update flash result to facotry server……";
											ToolStripStatusLabel toolStripStatusLabel = this.statusTab;
											toolStripStatusLabel.Text += str;
											FlashResult flashResult = FactoryCtrl.SetFlashResultD(device.Name, flag2.Value);
											this.statusTab.Text = MiAppConfig.Get("factory");
											if (!flashResult.Result)
											{
												device.IsDone = new bool?(true);
												listViewItem.SubItems[4].Text = flashResult.Msg;
												listViewItem.SubItems[5].Text = "error";
												listViewItem.SubItems[5].BackColor = Color.Red;
											}
											else
											{
												device.IsDone = new bool?(true);
												Log.w(device.Name, "flashResult.Result is true");
											}
										}
										catch (Exception ex2)
										{
											listViewItem.SubItems[4].Text = ex2.Message;
											listViewItem.SubItems[5].Text = "error";
											listViewItem.SubItems[5].BackColor = Color.Red;
											Log.w(ex2.Message + " " + ex2.StackTrace);
										}
									}
									if (this.isAutomate && flag2 != null)
									{
										try
										{
											string str2 = " update flash result to facotry server……";
											ToolStripStatusLabel toolStripStatusLabel2 = this.statusTab;
											toolStripStatusLabel2.Text += str2;
											PInvokeResultArg pinvokeResultArg = EDL_SA_COMMUNICATION.RspEdlResult(device.Name, flag2.Value);
											this.statusTab.Text = MiAppConfig.Get("factory");
											if (pinvokeResultArg.result <= 0)
											{
												device.IsDone = new bool?(true);
												listViewItem.SubItems[4].Text = pinvokeResultArg.lastErrMsg;
												listViewItem.SubItems[5].Text = "error";
												listViewItem.SubItems[5].BackColor = Color.Red;
											}
											else
											{
												device.IsDone = new bool?(true);
												Log.w(device.Name, "flashResult.Result is true");
											}
										}
										catch (Exception ex3)
										{
											listViewItem.SubItems[4].Text = ex3.Message;
											listViewItem.SubItems[5].Text = "error";
											listViewItem.SubItems[5].BackColor = Color.Red;
											Log.w(ex3.Message + " " + ex3.StackTrace);
										}
									}
									if (flag2 != null)
									{
										Log.w(device.Name, string.Format("before:flashSuccess is {0} set IsUpdate:{1} set IsDone {2}", flag2.Value.ToString(), device.IsUpdate.ToString(), device.IsDone.Value.ToString()));
										device.IsUpdate = false;
										device.IsDone = new bool?(true);
										Log.w(device.Name, string.Format("after:flashSuccess is {0} set IsUpdate:false set IsDone true", flag2.Value.ToString()));
									}
									if (this.m_aryClients.Count > 0 && this.m_aryClients[0].Sock != null)
									{
										this.SetLogText(string.Format("send :{0}\r\n", text));
										this.m_aryClients[0].Sock.Send(Encoding.ASCII.GetBytes(text));
										if (FlashingDevice.IsAllDone())
										{
											string text2 = "OPEN\r\n";
											this.m_aryClients[0].Sock.Send(Encoding.ASCII.GetBytes(text2));
											this.SetLogText(text2);
										}
									}
									if (this.clientSocket != null)
									{
										break;
									}
									break;
								}
							}
						}
					}
					if (flag != null && !flag.Value)
					{
						Log.w(string.Format("couldn't find {0} in FlashingDevice.flashDeviceList", listViewItem.SubItems[1].Text.ToLower()));
					}
				}
			}
			catch (Exception ex4)
			{
				Log.w(ex4.Message + " " + ex4.StackTrace);
			}
			this.UpdateThreadStatus();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000116C4 File Offset: 0x0000F8C4
		private void UpdateThreadStatus()
		{
			foreach (Thread thread in new List<Thread>(this.threads))
			{
				if (!thread.IsAlive)
				{
					Log.w(string.Format("Thread stopped, thread id {0}, thread name {1}", thread.ManagedThreadId, thread.Name));
					this.threads.Remove(thread);
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0001174C File Offset: 0x0000F94C
		private void cleanMiFlashTmp()
		{
			try
			{
				if (Directory.Exists(this.txtPath.Text))
				{
					string[] files = Directory.GetFiles(this.txtPath.Text);
					foreach (string text in files)
					{
						if (File.Exists(text))
						{
							FileInfo fileInfo = new FileInfo(text);
							if (fileInfo.Name.IndexOf("miflashTmp_") == 0)
							{
								File.Delete(text);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000117E0 File Offset: 0x0000F9E0
		private void cleanMiFlashTmp(string prefix)
		{
			try
			{
				if (Directory.Exists(this.txtPath.Text))
				{
					string[] files = Directory.GetFiles(this.txtPath.Text);
					foreach (string text in files)
					{
						if (File.Exists(text))
						{
							FileInfo fileInfo = new FileInfo(text);
							if (fileInfo.Name.IndexOf(prefix) == 0)
							{
								File.Delete(text);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00011870 File Offset: 0x0000FA70
		private void devicelist_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			Rectangle rectangle = default(Rectangle);
			int newWidth = e.NewWidth;
			foreach (object obj in this.devicelist.Controls)
			{
				Control control = (Control)obj;
				if (control.Name.IndexOf("progressbar") >= 0)
				{
					ProgressBar progressBar = (ProgressBar)control;
					rectangle = progressBar.Bounds;
					rectangle.Width = this.devicelist.Columns[2].Width;
					progressBar.SetBounds(this.devicelist.Items[0].SubItems[2].Bounds.X, rectangle.Y, rectangle.Width, rectangle.Height);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00011964 File Offset: 0x0000FB64
		private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				MiAppConfig.SetValue("swPath", this.txtPath.Text.ToString());
				if (this.serverSocket != null)
				{
					this.serverSocket.Close();
				}
				if (this.clientSocket != null)
				{
					this.clientSocket.Close();
				}
				if (this.m_aryClients.Count > 0 && this.m_aryClients[0].Sock != null)
				{
					this.m_aryClients[0].Sock.Close();
				}
				if (MiAppConfig.Get("chip") == "MTK")
				{
					MiProcess.KillProcess("SP_download_tool");
				}
				MiProcess.KillProcess("fh_loader");
				this.cleanMiFlashTmp();
				this.miUsb.RemoveUSBEventWatcher();
				MemImg.distory();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Log.w(ex.Message + " " + ex.StackTrace);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00011A64 File Offset: 0x0000FC64
		private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.isAutomate && this.isDaConnect)
			{
				EDL_SA_COMMUNICATION.DaDisConnect();
				this.isDaConnect = false;
			}
			System.Environment.Exit(System.Environment.ExitCode);
			base.Dispose();
			base.Close();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00011A98 File Offset: 0x0000FC98
		public override void SetLanguage()
		{
			base.SetLanguage();
			string name = CultureInfo.InstalledUICulture.Name;
			if (name.ToLower().IndexOf("zh") >= 0)
			{
				base.LanID = LanguageType.chn_s;
			}
			else
			{
				base.LanID = LanguageType.eng;
			}
			LanguageProvider languageProvider = new LanguageProvider(base.LanID);
			this.btnBrwDic.Text = languageProvider.GetLanguage("MainFrm.btnBrwDic");
			this.btnRefresh.Text = languageProvider.GetLanguage("MainFrm.btnRefresh");
			this.btnFlash.Text = languageProvider.GetLanguage("MainFrm.btnFlash");
			this.devicelist.Columns[0].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln0");
			this.devicelist.Columns[1].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln1");
			this.devicelist.Columns[2].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln2");
			this.devicelist.Columns[3].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln3");
			this.devicelist.Columns[4].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln4");
			this.devicelist.Columns[5].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln5");
			this.rdoCleanAll.Text = languageProvider.GetLanguage("MainFrm.rdoCleanAll");
			this.rdoSaveUserData.Text = languageProvider.GetLanguage("MainFrm.rdoSaveUserData");
			this.rdoCleanAllAndLock.Text = languageProvider.GetLanguage("MainFrm.rdoCleanAllAndLock");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00011C38 File Offset: 0x0000FE38
		private void miFlashConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ConfigurationFrm
			{
				Owner = this
			}.Show();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00011C58 File Offset: 0x0000FE58
		private void driverTsmItem_Click(object sender, EventArgs e)
		{
			DriverFrm driverFrm = new DriverFrm();
			driverFrm.Show();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00011C74 File Offset: 0x0000FE74
		private void comportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ComPortConfig
			{
				Owner = this
			}.Show();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00011C94 File Offset: 0x0000FE94
		private void reDrawProgress()
		{
			Rectangle rectangle = default(Rectangle);
			foreach (object obj in this.devicelist.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string text = listViewItem.SubItems[1].Text;
				foreach (object obj2 in this.devicelist.Controls)
				{
					Control control = (Control)obj2;
					if (control.Name.IndexOf("progressbar") >= 0)
					{
						ProgressBar progressBar = (ProgressBar)control;
						if (progressBar.Name == text + "progressbar")
						{
							rectangle = progressBar.Bounds;
							rectangle.Width = this.devicelist.Columns[2].Width;
							rectangle.Y = listViewItem.Bounds.Y;
							progressBar.SetBounds(this.devicelist.Items[0].SubItems[2].Bounds.X, rectangle.Y, rectangle.Width, rectangle.Height);
						}
					}
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00011E38 File Offset: 0x00010038
		private void checkSha256ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ValidationFrm
			{
				Owner = this
			}.Show();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00011E58 File Offset: 0x00010058
		public void CheckSha256()
		{
			this.timer_updateStatus.Enabled = true;
			foreach (Device device in FlashingDevice.flashDeviceList)
			{
				if (device.IsDone == null || device.IsDone.Value)
				{
					device.StartTime = DateTime.Now;
					device.Status = "flashing";
					device.Progress = 0f;
					device.IsDone = new bool?(false);
					device.IsUpdate = true;
					DeviceCtrl deviceCtrl = device.DeviceCtrl;
					deviceCtrl.deviceName = device.Name;
					deviceCtrl.swPath = this.txtPath.Text.Trim();
					deviceCtrl.sha256Path = this.ValidateSpecifyXml;
					new Thread(new ThreadStart(deviceCtrl.CheckSha256))
					{
						IsBackground = true
					}.Start();
				}
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00011F64 File Offset: 0x00010164
		private void flashLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log\\";
			if (Directory.Exists(text))
			{
				Process.Start("Explorer.exe", text);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00011FA0 File Offset: 0x000101A0
		private void fastbootLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.timer_updateStatus.Enabled)
			{
				this.timer_updateStatus.Enabled = true;
			}
			this.deviceArr = UsbDevice.GetDevice();
			foreach (Device device in FlashingDevice.flashDeviceList)
			{
				device.IsDone = new bool?(false);
				device.IsUpdate = true;
				device.Status = "grabbing log";
				device.Progress = 0f;
				device.StartTime = DateTime.Now;
				Thread thread = new Thread(new ThreadStart(new ScriptDevice
				{
					deviceName = device.Name
				}.GrapLog));
				thread.Start();
				thread.IsBackground = true;
				FlashingDevice.UpdateDeviceStatus(device.Name, null, "start grab log", "grabbing log", false);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0001209C File Offset: 0x0001029C
		private void authenticationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MiUserInfo miUserInfo = EDL_SLA_Challenge.authEDl("", out this.canFlash);
			this.lblAccount.Text = miUserInfo.name.Trim();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000120D0 File Offset: 0x000102D0
		private void rdoCleanAll_Click(object sender, EventArgs e)
		{
			if (this.rdoCleanAll.IsChecked)
			{
				this.script = FlashType.CleanAll;
				MiAppConfig.SetValue("script", this.script);
			}
			this.cmbScriptItem.SetText(this.script);
			if (Directory.Exists(this.txtPath.Text))
			{
				this.SetScriptItems(this.txtPath.Text);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0001213C File Offset: 0x0001033C
		private void rdoSaveUserData_Click(object sender, EventArgs e)
		{
			if (this.rdoSaveUserData.IsChecked)
			{
				if (!Directory.Exists(this.txtPath.Text))
				{
					return;
				}
				string[] array = FileSearcher.SearchFiles(this.txtPath.Text, FlashType.SaveUserData);
				if (array.Length == 0)
				{
					MessageBox.Show("couldn't find script.");
					return;
				}
				this.script = Path.GetFileName(array[0]);
				MiAppConfig.SetValue("script", this.script);
			}
			this.cmbScriptItem.SetText(this.script);
			if (Directory.Exists(this.txtPath.Text))
			{
				this.SetScriptItems(this.txtPath.Text);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000121E4 File Offset: 0x000103E4
		private void rdoCleanAllAndLock_Click(object sender, EventArgs e)
		{
			if (this.rdoCleanAllAndLock.IsChecked)
			{
				this.script = FlashType.CleanAllAndLock;
				MiAppConfig.SetValue("script", this.script);
			}
			this.cmbScriptItem.SetText(this.script);
			if (Directory.Exists(this.txtPath.Text))
			{
				this.SetScriptItems(this.txtPath.Text);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00012250 File Offset: 0x00010450
		private void cmbScriptItem_TextChanged(object sender, EventArgs e)
		{
			this.script = this.cmbScriptItem.SelectedText;
			if (this.script == FlashType.CleanAll)
			{
				this.rdoCleanAll.IsChecked = true;
			}
			else if (this.script.IndexOf("flash_all_except") >= 0)
			{
				this.rdoSaveUserData.IsChecked = true;
			}
			else if (this.script == FlashType.CleanAllAndLock)
			{
				this.rdoCleanAllAndLock.IsChecked = true;
			}
			else
			{
				this.rdoCleanAll.IsChecked = false;
				this.rdoSaveUserData.IsChecked = false;
				this.rdoCleanAllAndLock.IsChecked = false;
			}
			MiAppConfig.SetValue("script", this.script);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00012303 File Offset: 0x00010503
		private void txtPath_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00012308 File Offset: 0x00010508
		private void StartAutoFlash()
		{
			try
			{
				IPAddress any = IPAddress.Any;
				this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this.serverSocket.Bind(new IPEndPoint(any, this.myProt));
				this.serverSocket.Listen(10);
				this.SetLogText(string.Format("start listen {0} successfully", this.serverSocket.LocalEndPoint.ToString()));
				this.serverSocket.BeginAccept(new AsyncCallback(this.OnConnectRequest), this.serverSocket);
			}
			catch (Exception ex)
			{
				Log.w(ex.Message);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000123AC File Offset: 0x000105AC
		private void cmbDlType_SelectedValueChanged(object sender, EventArgs e)
		{
			MiAppConfig.SetValue("dlType", this.cmbDlType.SelectedItem.ToString());
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000123C8 File Offset: 0x000105C8
		private void cmbChkSum_SelectedValueChanged(object sender, EventArgs e)
		{
			MiAppConfig.SetValue("chkSum", this.cmbChkSum.SelectedItem.ToString());
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00012420 File Offset: 0x00010620
		private void RecMsg(string recStr)
		{
			string text = recStr.Replace("$", "#");
			foreach (string recStr in text.Split(new char[]
			{
				'#'
			}))
			{
				if (!string.IsNullOrEmpty(recStr))
				{
					if (recStr.IndexOf("READY") >= 0)
					{
						this.SetLogText("start load devices");
						this.btnRefresh.BeginInvoke(new Action<string>(delegate(string msg)
						{
							this.btnRefresh_Click(this.btnRefresh, new EventArgs());
						}), new object[]
						{
							""
						});
					}
					else if (recStr.IndexOf("START") >= 0)
					{
						this.SetLogText("start load devices");
						this.deviceArr = UsbDevice.GetDevice();
						this.btnRefresh.BeginInvoke(new Action<string>(delegate(string msg)
						{
							this.btnRefresh_Click(this.btnRefresh, new EventArgs());
						}), new object[]
						{
							""
						});
						string text2 = recStr.Replace("START", "");
						if (!string.IsNullOrEmpty(text2))
						{
							this.SetLogText("check usb " + text2);
							int num = 0;
							if (!string.IsNullOrEmpty(text2))
							{
								try
								{
									num = int.Parse(text2);
								}
								catch (Exception ex)
								{
									this.SetLogText(ex.Message);
								}
							}
							bool flag = false;
							foreach (Device device in this.deviceArr)
							{
								if (device.Index == num)
								{
									flag = true;
									break;
								}
							}
							string text3 = "#USB" + num.ToString();
							if (flag)
							{
								text3 += "ON$";
							}
							else
							{
								text3 += "OFF$";
							}
							this.SetLogText(string.Format("send :{0}", text3));
							this.m_aryClients[0].Sock.Send(Encoding.ASCII.GetBytes(text3));
						}
					}
					else if (recStr.IndexOf("LOAD") >= 0)
					{
						this.SetLogText("start flash");
						this.btnFlash.BeginInvoke(new Action<string>(delegate(string msg)
						{
							this.btnFlash_Click(this.btnFlash, new EventArgs());
						}), new object[]
						{
							""
						});
					}
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00012698 File Offset: 0x00010898
		private void MainFrm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A)
			{
				this.devicelist.Height = 270;
				this.txtLog.Visible = true;
				if (this.serverSocket == null)
				{
					this.isAutoFlash = true;
					this.StartAutoFlash();
					return;
				}
			}
			else
			{
				if (e.KeyCode == Keys.R && e.Alt)
				{
					e.Handled = true;
					this.btnRefresh.PerformClick();
					return;
				}
				if (e.KeyCode == Keys.F && e.Alt)
				{
					e.Handled = true;
					this.btnFlash.PerformClick();
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0001272C File Offset: 0x0001092C
		public void OnConnectRequest(IAsyncResult ar)
		{
			try
			{
				Socket socket = (Socket)ar.AsyncState;
				this.NewConnection(socket.EndAccept(ar));
				socket.BeginAccept(new AsyncCallback(this.OnConnectRequest), socket);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0001277C File Offset: 0x0001097C
		public void NewConnection(Socket sockClient)
		{
			SocketChatClient socketChatClient = new SocketChatClient(sockClient);
			this.m_aryClients.Add(socketChatClient);
			this.SetLogText(string.Format("Client {0}, joined", socketChatClient.Sock.RemoteEndPoint));
			string text = "Welcome " + DateTime.Now.ToString("G") + "\n\r";
			byte[] bytes = Encoding.ASCII.GetBytes(text.ToCharArray());
			socketChatClient.Sock.Send(bytes, bytes.Length, SocketFlags.None);
			socketChatClient.SetupRecieveCallback(this);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00012804 File Offset: 0x00010A04
		public void OnRecievedData(IAsyncResult ar)
		{
			SocketChatClient socketChatClient = (SocketChatClient)ar.AsyncState;
			byte[] recievedData = socketChatClient.GetRecievedData(ar);
			if (recievedData.Length < 1)
			{
				this.SetLogText(string.Format("Client {0}, disconnected", socketChatClient.Sock.RemoteEndPoint));
				socketChatClient.Sock.Close();
				this.m_aryClients.Remove(socketChatClient);
				return;
			}
			string @string = Encoding.ASCII.GetString(recievedData);
			this.RecMsg(@string);
			socketChatClient.SetupRecieveCallback(this);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0001287C File Offset: 0x00010A7C
		private void checkUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Upgrader.exe");
			Process.Start(fileName);
			System.Environment.Exit(0);
		}

		// Token: 0x04000105 RID: 261
		private USB miUsb = new USB();

		// Token: 0x04000106 RID: 262
		private string validateSpecifyXml;

		// Token: 0x04000107 RID: 263
		private bool readbackverify;

		// Token: 0x04000108 RID: 264
		private bool openwritedump;

		// Token: 0x04000109 RID: 265
		private bool openreaddump;

		// Token: 0x0400010A RID: 266
		private bool verbose;

		// Token: 0x0400010B RID: 267
		public string chip;

		// Token: 0x0400010C RID: 268
		private int downloadEclipse;

		// Token: 0x0400010D RID: 269
		private System.Windows.Forms.Timer eclipseTimer;

		// Token: 0x0400010E RID: 270
		private List<string> comportList = new List<string>();

		// Token: 0x0400010F RID: 271
		private bool autodetectdevice;

		// Token: 0x04000110 RID: 272
		private string script = "";

		// Token: 0x04000111 RID: 273
		private List<Device> deviceArr = new List<Device>();

		// Token: 0x04000112 RID: 274
		private byte[] result = new byte[1024];

		// Token: 0x04000113 RID: 275
		private int myProt = 6002;

		// Token: 0x04000114 RID: 276
		public bool isAutoFlash;

		// Token: 0x04000115 RID: 277
		private bool isFactory;

		// Token: 0x04000116 RID: 278
		public string factory = string.Empty;

		// Token: 0x04000117 RID: 279
		private Socket serverSocket;

		// Token: 0x04000118 RID: 280
		private Socket clientSocket;

		// Token: 0x04000119 RID: 281
		private bool canFlash = true;

		// Token: 0x0400011A RID: 282
		private bool isAutomate;

		// Token: 0x0400011B RID: 283
		private bool isDaConnect;

		// Token: 0x0400011C RID: 284
		private List<Thread> threads = new List<Thread>();

		// Token: 0x0400011D RID: 285
		private List<SocketChatClient> m_aryClients = new List<SocketChatClient>();

		// Token: 0x0400011E RID: 286
		private ProcessFrm frm = new ProcessFrm();
	}
}
