using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using XiaoMiFlash.code.authFlash;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x0200000C RID: 12
	public class SerialPortDevice : DeviceCtrl
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000442C File Offset: 0x0000262C
		public override void flash()
		{
			if (string.IsNullOrEmpty(this.deviceName))
			{
				return;
			}
			Log.w(this.deviceName, string.Format("flash in thread name:{0},id:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId.ToString()));
			if (!Directory.Exists(this.swPath))
			{
				throw new Exception("sw path is not valid");
			}
			this.comm.isReadDump = this.openReadDump;
			this.comm.isWriteDump = this.openWriteDump;
			DirectoryInfo directoryInfo = new DirectoryInfo(this.swPath);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				if (directoryInfo2.Name.ToLower().IndexOf("images") >= 0)
				{
					this.swPath = directoryInfo2.FullName;
					Log.w(this.deviceName, "sw in images");
					break;
				}
			}
			if (this.NeedProvision(this.swPath))
			{
				this.m_iSkipStorageInit = 1;
			}
			if (string.IsNullOrEmpty(MiAppConfig.Get("mainProgram")) || MiAppConfig.Get("mainProgram").ToString() == "xiaomi")
			{
				this.XiaomiFlash();
				return;
			}
			this.QcmFlash();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004568 File Offset: 0x00002768
		private void TestFlash()
		{
			if (string.IsNullOrEmpty(this.deviceName))
			{
				return;
			}
			try
			{
				using (SerialPort serialPort = new SerialPort(this.deviceName, 9600))
				{
					try
					{
						FlashingDevice.UpdateDeviceStatus(this.deviceName, null, null, "vboytest11111", false);
						this.registerPort(serialPort);
						int num = 0;
						int num2 = 1;
						int num3 = num2 / num;
						Thread.Sleep(10000);
						FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), "flash done", "success", true);
					}
					catch (Exception ex)
					{
						FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex.Message, "error", true);
						Log.w(this.deviceName, ex + "  " + ex.StackTrace, false);
					}
					finally
					{
						Log.w(serialPort.PortName, "no provision exit:" + serialPort.PortName);
						serialPort.Close();
						serialPort.Dispose();
					}
				}
			}
			catch (Exception ex2)
			{
				FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex2.Message, "error", true);
				Log.w(this.deviceName, ex2 + "  " + ex2.StackTrace, false);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000046E0 File Offset: 0x000028E0
		private void XiaomiFlash()
		{
			if (string.IsNullOrEmpty(this.deviceName))
			{
				return;
			}
			try
			{
				if (!this.NeedProvision(this.swPath))
				{
					using (SerialPort serialPort = new SerialPort(this.deviceName, 9600))
					{
						try
						{
							this.comm.serialPort = serialPort;
							Log.w(this.deviceName, string.Format("flash in thread name:{0},id:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId.ToString()));
							if (!Directory.Exists(this.swPath))
							{
								throw new Exception("sw path is not valid");
							}
							this.comm.isReadDump = this.openReadDump;
							this.comm.isWriteDump = this.openWriteDump;
							DirectoryInfo directoryInfo = new DirectoryInfo(this.swPath);
							DirectoryInfo[] directories = directoryInfo.GetDirectories();
							foreach (DirectoryInfo directoryInfo2 in directories)
							{
								if (directoryInfo2.Name.ToLower().IndexOf("images") >= 0)
								{
									this.swPath = directoryInfo2.FullName;
									Log.w(this.deviceName, "sw in images");
									break;
								}
							}
							if (this.NeedProvision(this.swPath))
							{
								this.m_iSkipStorageInit = 1;
							}
							FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(0f), "start flash", "flashing", false);
							this.registerPort(serialPort);
							this.SaharaDownloadProgrammer();
							Thread.Sleep(1000);
							this.PropareFirehose();
							this.ConfigureDDR(this.comm.intSectorSize, this.BUFFER_SECTORS, this.storageType, this.m_iSkipStorageInit);
							if (this.storageType == Storage.ufs)
							{
								this.SetBootPartition();
							}
							this.comm.StartReading();
							this.FirehoseDownloadImg(this.swPath);
							this.comm.StopReading();
							FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), "flash done", "success", true);
						}
						catch (Exception ex)
						{
							FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex.Message, "error", true);
							Log.w(this.deviceName, ex + "  " + ex.StackTrace, false);
						}
						finally
						{
							Log.w(serialPort.PortName, "no provision exit:" + serialPort.PortName);
							this.comm.serialPort.Close();
							this.comm.serialPort.Dispose();
						}
						goto IL_886;
					}
				}
				bool flag = true;
				using (SerialPort serialPort2 = new SerialPort(this.deviceName, 9600))
				{
					try
					{
						this.comm.serialPort = serialPort2;
						Log.w(this.deviceName, string.Format("flash in thread name:{0},id:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId.ToString()));
						if (!Directory.Exists(this.swPath))
						{
							throw new Exception("sw path is not valid");
						}
						this.comm.isReadDump = this.openReadDump;
						this.comm.isWriteDump = this.openWriteDump;
						DirectoryInfo directoryInfo3 = new DirectoryInfo(this.swPath);
						DirectoryInfo[] directories2 = directoryInfo3.GetDirectories();
						foreach (DirectoryInfo directoryInfo4 in directories2)
						{
							if (directoryInfo4.Name.ToLower().IndexOf("images") >= 0)
							{
								this.swPath = directoryInfo4.FullName;
								Log.w(this.deviceName, "sw in images");
								break;
							}
						}
						if (this.NeedProvision(this.swPath))
						{
							this.m_iSkipStorageInit = 1;
						}
						FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(0f), "start flash", "flashing", false);
						this.registerPort(serialPort2);
						this.SaharaDownloadProgrammer();
						Thread.Sleep(1000);
						this.PropareFirehose();
						this.ConfigureDDR(this.comm.intSectorSize, this.BUFFER_SECTORS, this.storageType, this.m_iSkipStorageInit);
						this.Provision(this.swPath);
						Log.w(this.comm.serialPort.PortName, "restart target");
						FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "restart target", "flashing", false);
						string command = string.Format(Firehose.Reset_To_Edl, this.verbose ? "1" : "0");
						this.comm.SendCommand(command, false);
					}
					catch (Exception ex2)
					{
						flag = false;
						FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex2.Message, "error", true);
						Log.w(this.deviceName, ex2 + "  " + ex2.StackTrace, false);
					}
					finally
					{
						Log.w(this.deviceName, "vboy-test asdasd");
						Log.w(serialPort2.PortName, "before restart close " + serialPort2.PortName);
						serialPort2.Close();
						serialPort2.Dispose();
						this.comm.serialPort.Close();
						this.comm.serialPort.Dispose();
					}
				}
				if (!flag)
				{
					Log.w(this.deviceName, "vboy-test err return");
				}
				else
				{
					using (SerialPort serialPort3 = new SerialPort(this.deviceName, 9600))
					{
						try
						{
							this.comm.serialPort = serialPort3;
							bool flag2 = false;
							Thread.Sleep(5000);
							List<string> list = this.getDevice().ToList<string>();
							int num = 100;
							string text = "";
							while (num-- > 0 && list.IndexOf(this.deviceName) < 0)
							{
								Thread.Sleep(100);
								list = this.getDevice().ToList<string>();
								text = string.Format("waiting for {0} restart", this.deviceName);
								Log.w(this.comm.serialPort.PortName, text);
								FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, text, "restart", false);
							}
							if (list.IndexOf(this.deviceName) < 0)
							{
								text = string.Format("{0} restart failed", this.deviceName);
								Log.w(this.comm.serialPort.PortName, text);
								FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "restart", false);
								flag2 = false;
								throw new Exception(text);
							}
							flag2 = true;
							text = string.Format("{0} restart successfully", this.deviceName);
							Log.w(this.comm.serialPort.PortName, text);
							Thread.Sleep(800);
							bool flag3 = false;
							while (num-- > 0 && !flag3)
							{
								try
								{
									this.comm.serialPort.Open();
									flag3 = true;
									Log.w(this.comm.serialPort.PortName, string.Format(" serial port {0} opend successfully", this.deviceName));
								}
								catch (Exception)
								{
									Log.w(this.comm.serialPort.PortName, string.Format("open serial port {0} ", this.deviceName));
									Thread.Sleep(800);
								}
							}
							flag2 = (flag2 && this.comm.IsOpen);
							FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), text, "restart", false);
							this.SaharaDownloadProgrammer();
							this.PropareFirehose();
							this.m_iSkipStorageInit = 0;
							this.ConfigureDDR(this.comm.intSectorSize, this.BUFFER_SECTORS, this.storageType, this.m_iSkipStorageInit);
							if (this.storageType == Storage.ufs)
							{
								this.SetBootPartition();
							}
							this.comm.StartReading();
							this.FirehoseDownloadImg(this.swPath);
							this.comm.StopReading();
							FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), "flash done", "success", true);
						}
						catch (Exception ex3)
						{
							FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex3.Message, "error", true);
							Log.w(this.deviceName, ex3 + "  " + ex3.StackTrace, false);
						}
						finally
						{
							Log.w(serialPort3.PortName, "after restart close " + serialPort3.PortName);
							serialPort3.Close();
							serialPort3.Dispose();
							this.comm.serialPort.Close();
							this.comm.serialPort.Dispose();
						}
					}
				}
				IL_886:;
			}
			catch (Exception ex4)
			{
				FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex4.Message, "error", true);
				Log.w(this.deviceName, ex4 + "  " + ex4.StackTrace, false);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000050C8 File Offset: 0x000032C8
		private void QcmFlash()
		{
			this.FHloader();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000050D0 File Offset: 0x000032D0
		public override void CheckSha256()
		{
			if (string.IsNullOrEmpty(this.deviceName))
			{
				return;
			}
			try
			{
				if (!Directory.Exists(this.swPath))
				{
					throw new Exception("sw path is not valid");
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(this.swPath);
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					if (directoryInfo2.Name.ToLower().IndexOf("images") >= 0)
					{
						this.swPath = directoryInfo2.FullName;
						break;
					}
				}
				FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(0f), "start flash", "flashing", false);
				this.SaharaDownloadProgrammer();
				this.PropareFirehose();
				this.ConfigureDDR(this.comm.intSectorSize, this.BUFFER_SECTORS, this.storageType, 0);
				this.GetSha256(this.swPath);
				FlashingDevice.UpdateDeviceStatus(this.deviceName, new float?(1f), "check done", "success", true);
			}
			catch (Exception ex)
			{
				FlashingDevice.UpdateDeviceStatus(this.deviceName, null, ex.Message, "error", true);
				Log.w(this.deviceName, ex, true);
			}
			finally
			{
				this.comm.serialPort.Close();
				this.comm.serialPort.Dispose();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005258 File Offset: 0x00003458
		private void registerPort(SerialPort port)
		{
			this.comm.serialPort = port;
			this.comm.serialPort.Open();
			foreach (Device device in FlashingDevice.flashDeviceList)
			{
				if (device.Name == this.deviceName)
				{
					device.DComm = this.comm;
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000052E0 File Offset: 0x000034E0
		private void SaharaDownloadProgrammer()
		{
			if (!this.comm.IsOpen)
			{
				Log.w(this.comm.serialPort.PortName, string.Format("port {0} is not open.", this.comm.serialPort.PortName));
				return;
			}
			string text = string.Format("[{0}]:{1}", this.comm.serialPort.PortName, "start flash.");
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "read hello packet", "flashing", false);
			Log.w(this.comm.serialPort.PortName, text);
			sahara_switch_Mode_packet sahara_switch_Mode_packet = default(sahara_switch_Mode_packet);
			sahara_switch_Mode_packet.Command = 19u;
			sahara_switch_Mode_packet.Length = 8u;
			byte[] array = new byte[12];
			this.comm.getRecDataIgnoreExcep();
			if (this.comm.recData == null || this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			sahara_packet sahara_packet = default(sahara_packet);
			sahara_hello_packet sahara_hello_packet = (sahara_hello_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_hello_packet));
			sahara_hello_packet.Reserved = new uint[6];
			default(sahara_hello_response).Reserved = new uint[6];
			sahara_readdata_packet sahara_readdata_packet = default(sahara_readdata_packet);
			sahara_64b_readdata_packet sahara_64b_readdata_packet = default(sahara_64b_readdata_packet);
			sahara_end_transfer_packet sahara_end_transfer_packet = default(sahara_end_transfer_packet);
			sahara_done_response sahara_done_response = default(sahara_done_response);
			int num = 10;
			byte[] array2;
			while (num-- > 0 && sahara_hello_packet.Command != 1u)
			{
				text = "cannot receive hello packet,MiFlash is trying to reset status!";
				Log.w(this.comm.serialPort.PortName, text);
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "flashing", false);
				this.comm.getRecDataIgnoreExcep();
				if (this.comm.recData == null || this.comm.recData.Length == 0)
				{
					this.comm.recData = new byte[48];
				}
				sahara_hello_packet = (sahara_hello_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_hello_packet));
				Thread.Sleep(500);
				if (sahara_hello_packet.Command != 1u)
				{
					text = "try to reset status.";
					Log.w(this.comm.serialPort.PortName, text);
					FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "flashing", false);
					this.comm.serialPort.DiscardInBuffer();
					this.comm.serialPort.DiscardOutBuffer();
					array2 = CommandFormat.StructToBytes(new sahara_hello_response
					{
						Reserved = new uint[6],
						Command = 2u,
						Length = 48u,
						Version = 2u,
						Version_min = 1u,
						Mode = 3u
					});
					this.comm.WritePort(array2, 0, array2.Length);
					this.comm.getRecDataIgnoreExcep();
					text = "Switch mode back";
					Log.w(this.comm.serialPort.PortName, text);
					FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "flashing", false);
					sahara_switch_Mode_packet.Command = 12u;
					sahara_switch_Mode_packet.Length = 12u;
					sahara_switch_Mode_packet.Mode = 0u;
					array2 = CommandFormat.StructToBytes(sahara_switch_Mode_packet);
					Array.ConstrainedCopy(array2, 0, array, 0, 12);
					this.comm.WritePort(array, 0, array.Length);
				}
			}
			if (sahara_hello_packet.Command != 1u)
			{
				text = "cannot receive hello packet";
				this.comm.serialPort.Close();
				this.comm.serialPort.Dispose();
				throw new Exception(text);
			}
			text = "received hello packet";
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "flashing", false);
			Log.w(this.comm.serialPort.PortName, text);
			array2 = CommandFormat.StructToBytes(new sahara_hello_response
			{
				Reserved = new uint[6],
				Command = 2u,
				Length = 48u,
				Version = 2u,
				Version_min = 1u
			});
			this.comm.WritePort(array2, 0, array2.Length);
			string[] array3 = FileSearcher.SearchFiles(this.swPath, SoftwareImage.ProgrammerPattern);
			if (array3.Length <= 0)
			{
				throw new Exception("can not found programmer file.");
			}
			string text2 = array3[0];
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Name.ToLower().IndexOf("firehose") >= 0)
			{
				this.programmerType = Programmer.firehose;
			}
			if (fileInfo.Name.ToLower().IndexOf("ufs") >= 0)
			{
				this.storageType = Storage.ufs;
			}
			else if (fileInfo.Name.ToLower().IndexOf("emmc") >= 0)
			{
				this.storageType = Storage.emmc;
			}
			if (fileInfo.Name.ToLower().IndexOf("lite") >= 0)
			{
				this.isLite = true;
			}
			this.comm.intSectorSize = ((this.storageType == Storage.ufs) ? this.comm.SECTOR_SIZE_UFS : this.comm.SECTOR_SIZE_EMMC);
			Log.w(this.comm.serialPort.PortName, "download programmer " + text2);
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "download programmer " + text2, "flashing", false);
			FileTransfer fileTransfer = new FileTransfer(this.comm.serialPort.PortName, text2);
			bool flag;
			do
			{
				flag = false;
				this.comm.getRecData();
				byte[] recData = this.comm.recData;
				sahara_packet = (sahara_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_packet));
				uint command = sahara_packet.Command;
				uint num2 = command;
				switch (num2)
				{
				case 3u:
					sahara_readdata_packet = (sahara_readdata_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_readdata_packet));
					text = string.Format("sahara read data:imgID {0}, offset {1},length {2}", sahara_readdata_packet.Image_id, sahara_readdata_packet.Offset, sahara_readdata_packet.SLength);
					fileTransfer.transfer(this.comm.serialPort, (int)sahara_readdata_packet.Offset, (int)sahara_readdata_packet.SLength);
					Log.w(this.comm.serialPort.PortName, text);
					break;
				case 4u:
					sahara_end_transfer_packet = (sahara_end_transfer_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_end_transfer_packet));
					text = string.Format("sahara read end  imgID:{0} status:{1}", sahara_end_transfer_packet.Image_id, sahara_end_transfer_packet.Status);
					if (sahara_end_transfer_packet.Status != 0u)
					{
						Log.w(this.comm.serialPort.PortName, string.Format("sahara read end error with status:{0}", sahara_end_transfer_packet.Status));
					}
					flag = true;
					Log.w(this.comm.serialPort.PortName, text);
					break;
				default:
					if (num2 != 18u)
					{
						text = string.Format("invalid command:{0}", sahara_packet.Command);
						Log.w(this.comm.serialPort.PortName, text);
					}
					else
					{
						sahara_64b_readdata_packet = (sahara_64b_readdata_packet)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_64b_readdata_packet));
						text = string.Format("sahara read 64b data:imgID {0},offset {1},length {2}", sahara_64b_readdata_packet.Image_id, sahara_64b_readdata_packet.Offset, sahara_64b_readdata_packet.SLength);
						fileTransfer.transfer(this.comm.serialPort, (int)sahara_64b_readdata_packet.Offset, (int)sahara_64b_readdata_packet.SLength);
						Log.w(this.comm.serialPort.PortName, text);
					}
					break;
				}
			}
			while (!flag);
			Log.w(this.comm.serialPort.PortName, "Send done packet");
			sahara_packet.Command = 5u;
			sahara_packet.Length = 8u;
			byte[] array4 = CommandFormat.StructToBytes(sahara_packet, 8);
			for (int i = 8; i < array4.Length; i++)
			{
				array4[i] = 0;
			}
			this.comm.WritePort(array4, 0, array4.Length);
			this.comm.getRecData();
			if (this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			sahara_done_response = (sahara_done_response)CommandFormat.BytesToStuct(this.comm.recData, typeof(sahara_done_response));
			if (sahara_done_response.Command == 6u)
			{
				text = string.Format("file {0} transferred successfully", text2);
				Log.w(this.comm.serialPort.PortName, text);
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), text, "flashing", false);
				fileTransfer.closeTransfer();
				Thread.Sleep(1000);
				return;
			}
			text = "programmer transfer error " + sahara_done_response.Command;
			throw new Exception(text);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005C30 File Offset: 0x00003E30
		private void SaharaServer()
		{
			int num = int.Parse(this.deviceName.ToLower().Replace("com", ""));
			string[] array = FileSearcher.SearchFiles(this.swPath, SoftwareImage.ProgrammerPattern);
			if (array.Length <= 0)
			{
				throw new Exception("can not found programmer file.");
			}
			string arg = array[0];
			string dosCommand = string.Format("QSaharaServer.exe  -u {0} -s 13:{1}", num, arg);
			Cmd cmd = new Cmd(this.deviceName, "");
			string text = cmd.Execute(this.deviceName, dosCommand);
			Log.w(this.deviceName, text);
			if (text.ToLower().IndexOf("error") >= 0 || text.ToLower().IndexOf("fail") >= 0)
			{
				throw new Exception(text);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005CFC File Offset: 0x00003EFC
		private void FHloader()
		{
			Cmd cmd = new Cmd(this.deviceName, "");
			int num = int.Parse(this.deviceName.ToLower().Replace("com", ""));
			string[] array = FileSearcher.SearchFiles(this.swPath, SoftwareImage.ProgrammerPattern);
			if (array.Length > 0)
			{
				string text = array[0];
				string text2 = string.Format("QSaharaServer.exe  -u {0} -s 13:{1}", num, text);
				string[] array2 = FileSearcher.SearchFiles(this.swPath, SoftwareImage.RawProgramPattern);
				string[] array3 = FileSearcher.SearchFiles(this.swPath, SoftwareImage.PatchPattern);
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = Path.GetFileName(array2[i]);
				}
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j] = Path.GetFileName(array3[j]);
				}
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Name.ToLower().IndexOf("ufs") >= 0)
				{
					this.storageType = Storage.ufs;
				}
				else if (fileInfo.Name.ToLower().IndexOf("emmc") >= 0)
				{
					this.storageType = Storage.emmc;
				}
				this.verbose = true;
				string text3 = "";
				if (this.verbose)
				{
					text3 = " --verbose";
				}
				string str = string.Format(" & fh_loader.exe --port=\\\\.\\{0} --sendxml={1} --search_path={2} --noprompt --showpercentagecomplete --memoryname={3} {4} --convertprogram2read", new object[]
				{
					this.deviceName,
					string.Join(",", array2),
					this.swPath,
					this.storageType,
					text3
				});
				string.Format(" & fh_loader.exe --port=\\\\.\\{0} --sendxml={1} --search_path={2} --noprompt --showpercentagecomplete --maxpayloadsizeinbytes={3} --zlpawarehost=1 --memoryname={4} {5} {6}", new object[]
				{
					this.deviceName,
					string.Join(",", array3),
					this.swPath,
					this.comm.intSectorSize * this.BUFFER_SECTORS,
					this.storageType,
					(this.m_iSkipStorageInit == 1) ? " --skipstorageinit " : "",
					text3
				});
				string.Format(" & fh_loader.exe --port=\\\\.\\{0} --setactivepartition={1} --noprompt --showpercentagecomplete --maxpayloadsizeinbytes={2} --zlpawarehost=1 --memoryname={3} {4} {5}", new object[]
				{
					this.deviceName,
					(this.storageType.ToLower() == "ufs") ? 1 : 0,
					this.comm.intSectorSize * this.BUFFER_SECTORS,
					(this.m_iSkipStorageInit == 1) ? " --skipstorageinit " : "",
					this.storageType,
					text3
				});
				string.Format(" & fh_loader.exe --port=\\\\.\\{0} --reset --noprompt --showpercentagecomplete --maxpayloadsizeinbytes={1} --zlpawarehost=1 --memoryname={2} {3} {4}", new object[]
				{
					this.deviceName,
					this.comm.intSectorSize * this.BUFFER_SECTORS,
					this.storageType,
					(this.m_iSkipStorageInit == 1) ? " --skipstorageinit " : "",
					text3
				});
				text2 += str;
				Log.w(this.deviceName, text2);
				cmd.Execute_returnLine(this.deviceName, text2, 1);
				return;
			}
			throw new Exception("can not found programmer file.");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00006030 File Offset: 0x00004230
		private void DownloadMiniKernel()
		{
			string[] array = FileSearcher.SearchFiles(this.swPath, SoftwareImage.BootImage);
			if (array.Length <= 0)
			{
				throw new Exception("can not found boot file.");
			}
			string text = array[0];
			string s = "QCOM fast download protocol host";
			string s2 = "QCOM fast download protocol targ";
			byte[] bytes = Encoding.Default.GetBytes(s);
			Encoding.Default.GetBytes(s2);
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), string.Format("Open boot file", text), "flashing", false);
			byte[] sendData = CommandFormat.StructToBytes(new HelloCommand
			{
				uVersionNumber = 3,
				uCompatibleVersion = 3,
				uFeatureBits = 9,
				uMagicNumber = bytes
			});
			int num = 1;
			HelloResponse helloResponse;
			for (;;)
			{
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "send hello command " + num.ToString(), "flashing", false);
				this.TransmitPacket(sendData);
				Thread.Sleep(500);
				this.comm.getRecData();
				if (this.comm.recData.Length == 0)
				{
					this.comm.recData = new byte[48];
				}
				helloResponse = (HelloResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(HelloResponse));
				if (helloResponse.uResponse == 2)
				{
					goto IL_176;
				}
				if (num == 60)
				{
					break;
				}
				num++;
			}
			throw new Exception("target not ready.");
			IL_176:
			int num2 = 1;
			int uMaxBlockSize = (int)helloResponse.uMaxBlockSize;
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "Enable trusted security mode", "flashing", false);
			sendData = CommandFormat.StructToBytes(new SecurityModeCommand
			{
				uCommand = 23,
				uMode = 1
			});
			this.TransmitPacket(sendData);
			Thread.Sleep(500);
			this.comm.getRecData();
			if (this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			if (((SimpleResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(SimpleResponse))).uResponse != 24)
			{
				throw new Exception("invalid state");
			}
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "Open EMMC card USER partition", "flashing", false);
			sendData = CommandFormat.StructToBytes(new OpenMultiImageCommand
			{
				uCommand = 27,
				uType = 33
			});
			this.TransmitPacket(sendData);
			Thread.Sleep(500);
			this.comm.getRecData();
			if (this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			OpenMultiImageResponse openMultiImageResponse = (OpenMultiImageResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(OpenMultiImageResponse));
			if (openMultiImageResponse.uResponse != 28)
			{
				throw new Exception("invalid state");
			}
			if (openMultiImageResponse.uStatus != 0)
			{
				throw new Exception("open failed");
			}
			int num3 = 0;
			StreamWriteCommand streamWriteCommand = default(StreamWriteCommand);
			FileTransfer fileTransfer = new FileTransfer(this.comm.serialPort.PortName, text);
			long num4 = 0L;
			while (num4 < fileTransfer.getFileSize())
			{
				long num5 = fileTransfer.getFileSize() - num4;
				long num6 = (num5 < (long)uMaxBlockSize) ? num5 : ((long)uMaxBlockSize);
				if (num4 % (long)(40 * uMaxBlockSize) == 0L)
				{
					FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), string.Format("StreamWrite address {0}, size {1}", num4, (num5 < (long)(40 * uMaxBlockSize)) ? num5 : ((long)(40 * uMaxBlockSize))), "flashing", false);
				}
				if (num3 == num2)
				{
					StreamWriteResponse streamWriteResponse = default(StreamWriteResponse);
					long num7 = num4 - (long)(num3 * uMaxBlockSize);
					this.comm.getRecData();
					if (this.comm.recData.Length == 0)
					{
						this.comm.recData = new byte[48];
					}
					streamWriteResponse = (StreamWriteResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(StreamWriteResponse));
					if (streamWriteResponse.uResponse != 8)
					{
						throw new Exception("open failed");
					}
					if (num7 != (long)streamWriteResponse.uAddress)
					{
						throw new Exception("open failed");
					}
					num3--;
				}
				streamWriteCommand.uCommand = 7;
				streamWriteCommand.uAddress = (int)num4;
				int num8 = 0;
				byte[] bytesFromFile = fileTransfer.GetBytesFromFile(num4, (int)num6, out num8);
				streamWriteCommand.uData = bytesFromFile;
				sendData = CommandFormat.StructToBytes(streamWriteCommand);
				this.TransmitPacket(sendData);
				num4 += (long)uMaxBlockSize;
				num3++;
			}
			while (num3-- > 0)
			{
				StreamWriteResponse streamWriteResponse2 = default(StreamWriteResponse);
				this.comm.getRecData();
				if (this.comm.recData.Length == 0)
				{
					this.comm.recData = new byte[48];
				}
				if (((StreamWriteResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(StreamWriteResponse))).uResponse != 8)
				{
					throw new Exception("invalid state");
				}
			}
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "Close EMMC card USER partition", "flashing", false);
			SimpleCommand simpleCommand = new SimpleCommand
			{
				uCommand = 21
			};
			sendData = CommandFormat.StructToBytes(simpleCommand);
			this.TransmitPacket(sendData);
			this.comm.getRecData();
			if (this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			if (((SimpleResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(SimpleResponse))).uResponse != 22)
			{
				throw new Exception("invalid state");
			}
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "Reboot to mini kernel", "flashing", false);
			simpleCommand.uCommand = 11;
			sendData = CommandFormat.StructToBytes(simpleCommand);
			this.TransmitPacket(sendData);
			this.comm.getRecData();
			if (this.comm.recData.Length == 0)
			{
				this.comm.recData = new byte[48];
			}
			if (((SimpleResponse)CommandFormat.BytesToStuct(this.comm.recData, typeof(SimpleResponse))).uResponse != 12)
			{
				throw new Exception("invalid state");
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000066DC File Offset: 0x000048DC
		public void TransmitPacket(byte[] sendData)
		{
			int num = 0;
			List<byte> list = new List<byte>();
			list.Add(126);
			foreach (byte b in sendData)
			{
				num = CRC.BuildCRC16(num, (int)b);
				this.AddPacketOctet(list, b);
			}
			this.AddPacketOctet(list, (byte)(num & 255));
			this.AddPacketOctet(list, (byte)(num >> 8));
			list.Add(126);
			this.comm.WritePort(list.ToArray(), 0, list.Count);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00006758 File Offset: 0x00004958
		public void ReceivePacket(byte[] pvPacket, bool bCheckSize)
		{
			int num = 0;
			int uSeed = 0;
			byte[] array = new byte[256];
			int num2 = 0;
			while (num2 != 3)
			{
				byte b = 0;
				this.comm.getRecData();
				bool flag = false;
				switch (num2)
				{
				case 0:
					if (b == 126)
					{
						num = 0;
						uSeed = 0;
						num2 = 1;
					}
					break;
				case 1:
					if (b == 126)
					{
						flag = true;
					}
					else
					{
						num2 = 2;
					}
					break;
				case 2:
					if (b != 126)
					{
						if (b == 125)
						{
							b ^= 32;
						}
						uSeed = CRC.BuildCRC16(uSeed, (int)b);
						if (num < array.Length - 1)
						{
							array[num] = b;
						}
						if (num++ < pvPacket.Length)
						{
							pvPacket[num - 1] = b;
						}
					}
					break;
				}
				if (flag)
				{
					return;
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00006809 File Offset: 0x00004A09
		public void AddPacketOctet(List<byte> arrPacket, byte bOctet)
		{
			if (bOctet == 126 || bOctet == 125)
			{
				arrPacket.Add(125);
				bOctet ^= 32;
			}
			arrPacket.Add(bOctet);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000682B File Offset: 0x00004A2B
		private void PropareFirehose()
		{
			this.ping();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00006834 File Offset: 0x00004A34
		private void ping()
		{
			Log.w(this.comm.serialPort.PortName, "send nop command");
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "ping target via firehose", "flashing", false);
			string command = string.Format(Firehose.Nop, this.verbose ? "1" : "0");
			if (!this.comm.SendCommand(command, true))
			{
				throw new Exception("ping target failed");
			}
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), "ping target via firehose", "flashing", false);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000068E8 File Offset: 0x00004AE8
		private string displace(string myStr, string displaceA, string displaceB)
		{
			string[] array = Regex.Split(myStr, displaceA);
			for (int i = 0; i < array.Length - 1; i++)
			{
				string[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)i)] = array2[(int)intPtr] + displaceB;
			}
			string text = "";
			foreach (string str in array)
			{
				text += str;
			}
			return text;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000694C File Offset: 0x00004B4C
		private string str2Hex(string str)
		{
			char[] array = str.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char value in array)
			{
				int num = Convert.ToInt32(value);
				stringBuilder.Append(string.Format("{0:X}", num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000069A8 File Offset: 0x00004BA8
		private string getsigKey(string sig)
		{
			string text = "http://unlock.update.miui.com/test/rsa?r=0&data=" + sig;
			Log.w(this.comm.serialPort.PortName, "request URL:" + text);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00006A1C File Offset: 0x00004C1C
		private bool dlAuth()
		{
			Log.w(this.comm.serialPort.PortName, "authentication edl.");
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, "edl authentication", "authentication", false);
			string req_AUTH = Firehose.REQ_AUTH;
			string authp = Firehose.AUTHP;
			bool flag = this.comm.SendCommand(req_AUTH, true);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(this.comm.auth);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("sig");
			XmlElement xmlElement = (XmlElement)xmlNode;
			string value = xmlElement.Attributes["value"].Value;
			Log.w(this.comm.serialPort.PortName, string.Format("origin:{0}", value));
			string text = "";
			Log.w(this.comm.serialPort.PortName, "SignEdl");
			if (MiFlashGlobal.IsFactory)
			{
				CheckCPUIDResult checkCPUIDResult = FactoryCtrl.FactorySignEdl(this.deviceName, value, out text);
				if (!checkCPUIDResult.Result)
				{
					Log.w(this.comm.serialPort.PortName, string.Concat(new object[]
					{
						"factory authentication failed result ",
						checkCPUIDResult.Result,
						", msg:",
						checkCPUIDResult.Msg
					}));
					throw new Exception("authentication failed " + checkCPUIDResult.Msg);
				}
			}
			else
			{
				PInvokeResultArg pinvokeResultArg = EDL_SLA_Challenge.SignEdl(value, out text);
				if (pinvokeResultArg.result != 0)
				{
					Log.w(this.comm.serialPort.PortName, "authentication failed result " + pinvokeResultArg.result);
					throw new Exception("authentication failed " + pinvokeResultArg.lastErrMsg);
				}
			}
			Log.w(this.comm.serialPort.PortName, string.Format("siged:{0}", text));
			List<byte> list = new List<byte>();
			for (int i = 0; i < text.Length; i += 2)
			{
				string s = text.Substring(i, 2);
				list.Add(byte.Parse(s, NumberStyles.AllowHexSpecifier));
			}
			byte[] array = list.ToArray();
			string command = string.Format(authp, array.Length);
			flag = this.comm.SendCommand(command, true);
			if (!flag)
			{
				throw new Exception("authentication failed");
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString() + " ");
				stringBuilder2.Append("0x" + b.ToString("X2") + " ");
			}
			this.comm.WritePort(array, 0, array.Length);
			if (!this.comm.GetResponse(true))
			{
				throw new Exception("authentication failed");
			}
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00006D20 File Offset: 0x00004F20
		private void ConfigureDDR(int intSectorSize, int buffer_sectors, string ddrType, int m_iSkipStorageInit)
		{
			Log.w(this.comm.serialPort.PortName, "send configure command");
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "send configure command", "flashing", false);
			string command = string.Format(Firehose.Configure, new object[]
			{
				this.verbose ? "1" : "0",
				intSectorSize * buffer_sectors,
				ddrType,
				m_iSkipStorageInit
			});
			bool flag = this.comm.SendCommand(command, true);
			if (!flag)
			{
				if (!this.comm.needEdlAuth)
				{
					Log.w(this.comm.serialPort.PortName, "configure failed!!!");
					throw new Exception("configure failed!!!");
				}
				foreach (Device device in FlashingDevice.flashDeviceList)
				{
					if (device.Name == this.comm.serialPort.PortName)
					{
						device.IsUpdate = true;
						break;
					}
				}
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, "edl authentication", "flashing", false);
				Log.w(this.comm.serialPort.PortName, "edl authentication");
				if (MiFlashGlobal.IsFactory)
				{
					Log.w(this.comm.serialPort.PortName, "factory need edl authentication.");
				}
				else
				{
					bool flag2 = false;
					EDL_SLA_Challenge.authEDl(this.comm.serialPort.PortName, out flag2);
					if (!flag2)
					{
						Log.w(this.comm.serialPort.PortName, "You are not authorized to Download!!!");
						throw new Exception("You are not authorized to Download!!!");
					}
					Log.w(this.comm.serialPort.PortName, "need edl authentication.");
				}
				if (!this.dlAuth())
				{
					Log.w(this.comm.serialPort.PortName, "authorize failed!!!");
					throw new Exception("authorize failed!!!");
				}
				flag = this.comm.SendCommand(command, true);
			}
			if (Storage.ufs.ToLower() == ddrType && !this.isLite && !flag)
			{
				throw new Exception("send configure command failed");
			}
			Log.w(this.comm.serialPort.PortName, "max buffer sector is " + this.comm.m_dwBufferSectors);
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), "send command command", "flashing", false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00006FEC File Offset: 0x000051EC
		private bool NeedProvision(string swpath)
		{
			bool result = false;
			string[] array = FileSearcher.SearchFiles(swpath, SoftwareImage.ProvisionPattern);
			if (array.Length > 0)
			{
				Log.w(this.deviceName, "need provision");
				result = true;
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00007020 File Offset: 0x00005220
		private bool Provision(string swpath)
		{
			string[] array = FileSearcher.SearchFiles(swpath, SoftwareImage.ProvisionPattern);
			if (array.Length == 0)
			{
				return false;
			}
			string text = array[0];
			string msg = string.Format("start provision:{0}", text);
			Log.w(this.comm.serialPort.PortName, msg);
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = XmlReader.Create(text, new XmlReaderSettings
			{
				IgnoreComments = true
			});
			xmlDocument.Load(xmlReader);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("data");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			int num = 0;
			try
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlElement xmlElement = (XmlElement)xmlNode2;
					if (!(xmlElement.Name.ToLower() != "ufs"))
					{
						StringBuilder stringBuilder = new StringBuilder("<ufs ");
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							if (!(xmlAttribute.Name.ToLower() == "desc"))
							{
								stringBuilder.Append(string.Format("{0}=\"{1}\" ", xmlAttribute.Name, xmlAttribute.Value));
							}
						}
						if (this.verbose)
						{
							stringBuilder.Append(" verbose=\"1\" ");
						}
						stringBuilder.Append("/>");
						string text2 = string.Format("<?xml version=\"1.0\" ?>\n<data>\n{0}\n</data>", stringBuilder.ToString());
						if (!this.comm.SendCommand(text2, true))
						{
							Log.w(this.comm.serialPort.PortName, "Provision failed :" + text2);
						}
						FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?((float)num / (float)childNodes.Count), text, "provisioning", false);
						num++;
					}
				}
				Log.w(this.comm.serialPort.PortName, "Provision done.");
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), "provisiong done", "provisioning", false);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				xmlReader.Close();
			}
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000072FC File Offset: 0x000054FC
		private bool Reboot(string portName)
		{
			bool flag = false;
			Log.w(this.comm.serialPort.PortName, "restart target");
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), "restart target", "flashing", false);
			string command = string.Format(Firehose.Reset_To_Edl, this.verbose ? "1" : "0");
			if (!this.comm.SendCommand(command, true))
			{
				throw new Exception("restart target failed");
			}
			this.comm.serialPort.DiscardInBuffer();
			this.comm.serialPort.DiscardOutBuffer();
			this.comm.serialPort.Close();
			this.comm.serialPort.Dispose();
			Thread.Sleep(5000);
			List<string> list = this.getDevice().ToList<string>();
			int num = 100;
			string text = "";
			while (num-- > 0 && list.IndexOf(portName) < 0)
			{
				Thread.Sleep(100);
				list = this.getDevice().ToList<string>();
				text = string.Format("waiting for {0} restart", portName);
				Log.w(this.comm.serialPort.PortName, text);
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, text, "restart", false);
			}
			if (list.IndexOf(portName) >= 0)
			{
				flag = true;
				text = string.Format("{0} restart successfully", portName);
				Log.w(this.comm.serialPort.PortName, text);
				Thread.Sleep(800);
				bool flag2 = false;
				while (num-- > 0 && !flag2)
				{
					try
					{
						this.comm.serialPort.Open();
						flag2 = true;
						Log.w(this.comm.serialPort.PortName, string.Format(" serial port {0} opend successfully", portName));
					}
					catch (Exception)
					{
						Log.w(this.comm.serialPort.PortName, string.Format("open serial port {0} ", portName));
						Thread.Sleep(800);
					}
				}
				flag = (flag && this.comm.IsOpen);
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), text, "restart", false);
				return flag;
			}
			text = string.Format("{0} restart failed", portName);
			Log.w(this.comm.serialPort.PortName, text);
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "restart", false);
			flag = false;
			throw new Exception(text);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000075AC File Offset: 0x000057AC
		private void SetBootPartition()
		{
			string text = "Set Boot Partition ";
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), text, "flashing", false);
			string command = string.Format(Firehose.SetBootPartition, this.verbose ? "1" : "0");
			if (!this.comm.SendCommand(command, true))
			{
				throw new Exception("set boot partition failed");
			}
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), text, "flashing", false);
			Log.w(this.comm.serialPort.PortName, text);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000765C File Offset: 0x0000585C
		private void FirehoseDownloadImg(string swPath)
		{
			string text = MiAppConfig.Get("rawprogram").ToString();
			string text2 = MiAppConfig.Get("patch").ToString();
			string[] array;
			string[] array2;
			if (string.IsNullOrEmpty(text))
			{
				array = FileSearcher.SearchFiles(swPath, SoftwareImage.RawProgramPattern);
				array2 = FileSearcher.SearchFiles(swPath, SoftwareImage.PatchPattern);
			}
			else
			{
				array = new string[]
				{
					text
				};
				array2 = new string[]
				{
					text2
				};
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (this.WriteFilesToDevice(this.comm.serialPort.PortName, swPath, array[i]))
				{
					this.ApplyPatchesToDevice(this.comm.serialPort.PortName, array2[i]);
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00007718 File Offset: 0x00005918
		private void GetSha256(string swPath)
		{
			if (string.IsNullOrEmpty(this.sha256Path))
			{
				string[] array = FileSearcher.SearchFiles(swPath, SoftwareImage.RawProgramPattern);
				for (int i = 0; i < array.Length; i++)
				{
					Log.w(this.comm.serialPort.PortName, string.Format("sha256 {0}", array[i]));
					this.GetSha256Digest(this.comm.serialPort.PortName, swPath, array[i]);
				}
				return;
			}
			Log.w(this.comm.serialPort.PortName, string.Format("sha256 {0}", this.sha256Path));
			this.GetSha256Digest(this.comm.serialPort.PortName, swPath, this.sha256Path);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000077CC File Offset: 0x000059CC
		private bool WriteFilesToDevice(string portName, string swPath, string rawFilePath)
		{
			bool result = true;
			Log.w(this.comm.serialPort.PortName, string.Format("open program file {0}", rawFilePath));
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, rawFilePath, "flashing", false);
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = XmlReader.Create(rawFilePath, new XmlReaderSettings
			{
				IgnoreComments = true
			});
			xmlDocument.Load(xmlReader);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("data");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			try
			{
				bool flag = false;
				string text = "";
				string text2 = "0";
				string text3 = "0";
				string text4 = "0";
				string text5 = "0";
				string text6 = "512";
				string arg = "";
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlElement xmlElement = (XmlElement)xmlNode2;
					if (!(xmlElement.Name.ToLower() != "program"))
					{
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							string key;
							switch (key = xmlAttribute.Name.ToLower())
							{
							case "file_sector_offset":
								text2 = xmlAttribute.Value;
								break;
							case "filename":
								text = xmlAttribute.Value;
								break;
							case "num_partition_sectors":
								text4 = xmlAttribute.Value;
								break;
							case "start_sector":
								text3 = xmlAttribute.Value;
								break;
							case "sparse":
								flag = (xmlAttribute.Value == "true");
								break;
							case "sector_size_in_bytes":
								text6 = xmlAttribute.Value;
								break;
							case "physical_partition_number":
								text5 = xmlAttribute.Value;
								break;
							case "label":
								arg = xmlAttribute.Value;
								break;
							}
						}
						if (!string.IsNullOrEmpty(text))
						{
							this.comm.writeCount = 0;
							this.comm.CleanBuffer();
							text = swPath + "\\" + text;
							if (!File.Exists(text))
							{
								throw new Exception(string.Format("file {0} not found.", text));
							}
							if (text.IndexOf("gpt_main1") >= 0 || text.IndexOf("gpt_main2") >= 0)
							{
								Thread.Sleep(1000);
							}
							string addtionalFirehose = "";
							if (this.readBackVerify)
							{
								addtionalFirehose = "read_back_verify=\"1\"";
							}
							DateTime now = DateTime.Now;
							if (flag)
							{
								Log.w(this.comm.serialPort.PortName, string.Format("Write sparse file {0} to partition[{1}] sector {2}", text, arg, text3));
								FileTransfer fileTransfer = new FileTransfer(this.comm.serialPort.PortName, text);
								fileTransfer.WriteSparseFileToDevice(this, text3, text4, text, text2, text6, text5, addtionalFirehose);
								fileTransfer.closeTransfer();
							}
							else
							{
								Log.w(this.comm.serialPort.PortName, string.Format("Write file {0} to partition[{1}] sector {2}", text, arg, text3));
								FileTransfer fileTransfer2 = new FileTransfer(this.comm.serialPort.PortName, text);
								fileTransfer2.WriteFile(this, text3, text4, text, text2, "0", text6, text5, addtionalFirehose, true, new int?(1));
								fileTransfer2.closeTransfer();
							}
							string arg2 = (DateTime.Now - now).ToString();
							Log.w(this.comm.serialPort.PortName, string.Format("Image {0} transferred successfully,elapse {1}", text, arg2));
							this.comm.writeCount = 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				xmlReader.Close();
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00007C84 File Offset: 0x00005E84
		private void GetSha256Digest(string portName, string swPath, string rawFilePath)
		{
			Log.w(this.comm.serialPort.PortName, string.Format("open file {0}", rawFilePath));
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, rawFilePath, "flashing", false);
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = XmlReader.Create(rawFilePath, new XmlReaderSettings
			{
				IgnoreComments = true
			});
			xmlDocument.Load(xmlReader);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("data");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			try
			{
				string text = "";
				string value = "0";
				string text2 = "0";
				string value2 = "0";
				string value3 = "0";
				string value4 = "512";
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlElement xmlElement = (XmlElement)xmlNode2;
					if (!(xmlElement.Name.ToLower() != "program"))
					{
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							string key;
							switch (key = xmlAttribute.Name.ToLower())
							{
							case "file_sector_offset":
								value = xmlAttribute.Value;
								break;
							case "filename":
								text = xmlAttribute.Value;
								break;
							case "num_partition_sectors":
								value2 = xmlAttribute.Value;
								break;
							case "start_sector":
								text2 = xmlAttribute.Value;
								break;
							case "sparse":
								xmlAttribute.Value == "true";
								break;
							case "sector_size_in_bytes":
								value4 = xmlAttribute.Value;
								break;
							case "physical_partition_number":
								value3 = xmlAttribute.Value;
								break;
							case "label":
							{
								string value5 = xmlAttribute.Value;
								break;
							}
							}
						}
						if (!string.IsNullOrEmpty(text))
						{
							long num2 = Convert.ToInt64(value2);
							if (num2 == 0L)
							{
								num2 = 2147483647L;
							}
							Convert.ToInt64(value);
							long num3 = Convert.ToInt64(value4);
							long num4 = Convert.ToInt64(value3);
							Log.w(this.comm.serialPort.PortName, string.Format("checking sha256 {0}", text));
							FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, null, string.Format("checking sha256 {0}", text), "checking sha256", false);
							string command = string.Format(Firehose.FIREHOSE_SHA256DIGEST, new object[]
							{
								num3,
								num2,
								text2,
								num4
							});
							if (!this.comm.SendCommand(command, true))
							{
								bool flag = false;
								bool flag2 = false;
								while (!flag2 && !flag)
								{
									List<XmlDocument> responseXml = this.comm.GetResponseXml(true);
									foreach (XmlDocument xmlDocument2 in responseXml)
									{
										XmlNode xmlNode3 = xmlDocument2.SelectSingleNode("data");
										XmlNodeList childNodes2 = xmlNode3.ChildNodes;
										foreach (object obj3 in childNodes2)
										{
											XmlNode xmlNode4 = (XmlNode)obj3;
											XmlElement xmlElement2 = (XmlElement)xmlNode4;
											foreach (object obj4 in xmlElement2.Attributes)
											{
												XmlAttribute xmlAttribute2 = (XmlAttribute)obj4;
												if (xmlAttribute2.Value.ToLower() == "ack")
												{
													flag = true;
												}
												else if (xmlAttribute2.Value.ToLower() == "nak")
												{
													flag2 = true;
												}
											}
										}
									}
									Thread.Sleep(50);
									responseXml = this.comm.GetResponseXml(false);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				xmlReader.Close();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000081F4 File Offset: 0x000063F4
		private bool ApplyPatchesToDevice(string portName, string patchFilePath)
		{
			bool result = true;
			Log.w(this.comm.serialPort.PortName, string.Format("open patch file {0}", patchFilePath));
			FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(0f), patchFilePath, "flashing", false);
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = XmlReader.Create(patchFilePath, new XmlReaderSettings
			{
				IgnoreComments = true
			});
			xmlDocument.Load(xmlReader);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("patches");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			string text = "";
			string pszPatchSize = "0";
			string pszPatchValue = "0";
			string pszDiskOffsetSector = "0";
			string pszSectorOffsetByte = "0";
			string pszPhysicalPartitionNumber = "0";
			string pszSectorSizeInBytes = "512";
			try
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlElement xmlElement = (XmlElement)xmlNode2;
					if (!(xmlElement.Name.ToLower() != "patch"))
					{
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							string key;
							switch (key = xmlAttribute.Name.ToLower())
							{
							case "byte_offset":
								pszSectorOffsetByte = xmlAttribute.Value;
								break;
							case "filename":
								text = xmlAttribute.Value;
								break;
							case "size_in_bytes":
								pszPatchSize = xmlAttribute.Value;
								break;
							case "start_sector":
								pszDiskOffsetSector = xmlAttribute.Value;
								break;
							case "value":
								pszPatchValue = xmlAttribute.Value;
								break;
							case "sector_size_in_bytes":
								pszSectorSizeInBytes = xmlAttribute.Value;
								break;
							case "physical_partition_number":
								pszPhysicalPartitionNumber = xmlAttribute.Value;
								break;
							}
						}
						if (text.ToLower() == "disk")
						{
							this.ApplyPatch(pszDiskOffsetSector, pszSectorOffsetByte, pszPatchValue, pszPatchSize, pszSectorSizeInBytes, pszPhysicalPartitionNumber);
						}
					}
				}
				FlashingDevice.UpdateDeviceStatus(this.comm.serialPort.PortName, new float?(1f), patchFilePath, "flashing", false);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				xmlReader.Close();
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00008528 File Offset: 0x00006728
		private void ApplyPatch(string pszDiskOffsetSector, string pszSectorOffsetByte, string pszPatchValue, string pszPatchSize, string pszSectorSizeInBytes, string pszPhysicalPartitionNumber)
		{
			Log.w(this.comm.serialPort.PortName, string.Format("ApplyPatch sector {0}, offset {1}, value {2}, size {3}", new object[]
			{
				pszDiskOffsetSector,
				pszSectorOffsetByte,
				pszPatchValue,
				pszPatchSize
			}));
			string text = "";
			if (this.readBackVerify)
			{
				text = "read_back_verify=\"1\"";
			}
			string command = string.Format(Firehose.FIREHOSE_PATCH, new object[]
			{
				pszSectorSizeInBytes,
				pszSectorOffsetByte,
				pszPhysicalPartitionNumber,
				pszPatchSize,
				pszDiskOffsetSector,
				pszPatchValue,
				text
			});
			this.comm.SendCommand(command);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000085C0 File Offset: 0x000067C0
		public override string[] getDevice()
		{
			return ComPortCtrl.getDevicesQc();
		}

		// Token: 0x0400002F RID: 47
		public Comm comm = new Comm();

		// Token: 0x04000030 RID: 48
		private int BUFFER_SECTORS = 256;

		// Token: 0x04000031 RID: 49
		private int programmerType = 1;

		// Token: 0x04000032 RID: 50
		private string storageType = "ufs";

		// Token: 0x04000033 RID: 51
		private bool isLite;

		// Token: 0x04000034 RID: 52
		private int m_iSkipStorageInit;
	}
}
