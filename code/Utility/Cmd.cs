using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000004 RID: 4
	public class Cmd
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000027C8 File Offset: 0x000009C8
		public Cmd(string _deivcename, string _scriptPath)
		{
			this.devicename = _deivcename;
			this.scriptPath = _scriptPath;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002815 File Offset: 0x00000A15
		public string Execute(string deviceName, string dosCommand)
		{
			return this.Execute(deviceName, dosCommand, 0);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002820 File Offset: 0x00000A20
		public string Execute_returnLine(string deviceName, string dosCommand)
		{
			return this.Execute_returnLine(deviceName, dosCommand, 1);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000282C File Offset: 0x00000A2C
		public string Execute(string deviceName, string command, int seconds)
		{
			string text = "";
			if (command != null && !command.Equals(""))
			{
				Process process = this.initCmdProcess(command);
				try
				{
					if (process.Start())
					{
						if (seconds == 0)
						{
							process.WaitForExit();
						}
						else
						{
							process.WaitForExit(seconds);
						}
						text = process.StandardOutput.ReadToEnd();
						text += process.StandardError.ReadToEnd();
					}
				}
				catch (Exception ex)
				{
					Log.w(deviceName, ex.Message);
				}
				finally
				{
					if (process != null)
					{
						process.Close();
						process.Dispose();
					}
				}
			}
			return text;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028D0 File Offset: 0x00000AD0
		public string Exec_returnLine(string deviceName, string command, int seconds)
		{
			this.devicename = deviceName;
			StringBuilder stringBuilder = new StringBuilder();
			if (command != null && !command.Equals(""))
			{
				try
				{
					Process process = this.initCmdProcess(command);
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardInput = true;
					process.StartInfo.Arguments = "/C " + command;
					this.mprocess = process;
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.Name == this.devicename)
						{
							device.DCmd = this;
						}
					}
					process.Refresh();
					if (process.Start())
					{
						StreamReader standardOutput = process.StandardOutput;
						StreamReader standardError = process.StandardError;
						string text = standardOutput.ReadLine();
						string text2 = standardError.ReadLine();
						while (!standardOutput.EndOfStream && !standardError.EndOfStream)
						{
							if (text.Length > 0)
							{
								Log.w(this.devicename, text);
								if ((!string.IsNullOrEmpty(this.errMsg) || text.ToLower().IndexOf("error") >= 0 || text.ToLower().IndexOf("fail") >= 0 || text.ToLower() == "missmatching image and device" || text.ToLower() == "missmatching board version") && string.IsNullOrEmpty(this.errMsg))
								{
									this.errMsg = text;
								}
							}
							if (text2.Length > 0)
							{
								Log.w(this.devicename, text2);
								if ((!string.IsNullOrEmpty(this.errMsg) || text2.ToLower().IndexOf("error") >= 0 || text2.ToLower().IndexOf("fail") >= 0 || text2.ToLower() == "missmatching image and device" || text2.ToLower() == "missmatching board version") && string.IsNullOrEmpty(this.errMsg))
								{
									this.errMsg = text2;
								}
							}
							text = standardOutput.ReadLine();
							text2 = standardError.ReadLine();
							float? percent = this.GetPercent(text);
							FlashingDevice.UpdateDeviceStatus(this.devicename, percent, text, "flashing", false);
						}
					}
				}
				catch (Exception ex)
				{
					Log.w(deviceName, ex, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, null, ex.Message, "error", true);
				}
				finally
				{
					this.FlashDone();
					if (this.mprocess != null)
					{
						this.mprocess.Close();
						this.mprocess.Dispose();
					}
					Log.w(this.devicename, "process exit.");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public string Execute_returnLine(string deviceName, string command, int seconds)
		{
			this.devicename = deviceName;
			StringBuilder stringBuilder = new StringBuilder();
			if (command != null && !command.Equals(""))
			{
				try
				{
					Process process = this.initCmdProcess(command);
					process.StartInfo.Arguments = "/C " + command;
					this.mprocess = process;
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.Name == this.devicename)
						{
							device.DCmd = this;
						}
					}
					process.EnableRaisingEvents = true;
					process.OutputDataReceived += this.process_OutputDataReceived;
					process.ErrorDataReceived += this.process_ErrorDataReceived;
					if (process.Start())
					{
						Log.w(this.devicename, string.Format("Physical Memory Usage:{0} Byte", process.WorkingSet64.ToString()));
						Log.w(this.devicename, string.Format("start process id {0} name {1}", process.Id.ToString(), process.ProcessName));
						Log.w(string.Format("start process id {0} name {1}", process.Id.ToString(), process.ProcessName));
						process.BeginOutputReadLine();
						process.BeginErrorReadLine();
						process.WaitForExit();
					}
				}
				catch (Exception ex)
				{
					Log.w(deviceName, ex, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, null, ex.Message, "error", true);
				}
				finally
				{
					this.FlashDone();
					if (this.mprocess != null)
					{
						this.mprocess.Close();
						this.mprocess.Dispose();
					}
					Log.w(this.devicename, "process exit.");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private void process_Exited(object sender, EventArgs e)
		{
			try
			{
				Process process = (Process)sender;
				Log.w(this.devicename, string.Format("process exit id {0} name {1}", process.Id.ToString(), process.ProcessName));
				Log.w(string.Format("process exit id {0} name {1}", process.Id.ToString(), process.ProcessName));
				process.CancelErrorRead();
				process.CancelOutputRead();
				process.Refresh();
			}
			catch (Exception ex)
			{
				Log.w(this.devicename, ex.Message, false);
			}
			finally
			{
				this.FlashDone();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002E90 File Offset: 0x00001090
		private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				try
				{
					string data = e.Data;
					Log.w(this.devicename, "err:" + e.Data, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, null, data, "flashing", false);
					if ((!string.IsNullOrEmpty(this.errMsg) || data.ToLower().IndexOf("error") >= 0 || data.ToLower().IndexOf("fail") >= 0 || data.ToLower() == "missmatching image and device") && string.IsNullOrEmpty(this.errMsg))
					{
						this.errMsg = data;
					}
				}
				catch (Exception ex)
				{
					Log.w(this.devicename, ex.Message);
					try
					{
						Log.w(this.devicename, "exit process");
						Process process = (Process)sender;
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex.Message + "\r\n" + ex.StackTrace;
						}
					}
					catch (Exception ex2)
					{
						Log.w(this.devicename, ex2.Message);
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex2.Message + "\r\n" + ex2.StackTrace;
						}
					}
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002FF8 File Offset: 0x000011F8
		private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				try
				{
					string data = e.Data;
					float? percent = this.GetPercent(data);
					Log.w(this.devicename, "info:" + e.Data, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, percent, data, "flashing", false);
					if ((!string.IsNullOrEmpty(this.errMsg) || data.ToLower().IndexOf("error") >= 0 || data.ToLower().IndexOf("fail") >= 0 || data.ToLower() == "missmatching image and device" || data.ToLower() == "missmatching board version") && string.IsNullOrEmpty(this.errMsg))
					{
						this.errMsg = data;
					}
				}
				catch (Exception ex)
				{
					Log.w(this.devicename, ex.Message);
					try
					{
						Log.w(this.devicename, "exit process");
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex.Message + "\r\n" + ex.StackTrace;
						}
					}
					catch (Exception ex2)
					{
						Log.w(this.devicename, ex2.Message);
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex2.Message + "\r\n" + ex2.StackTrace;
						}
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000316C File Offset: 0x0000136C
		public string consoleMode_Execute_returnLine(string deviceName, string command, int seconds)
		{
			this.devicename = deviceName;
			StringBuilder stringBuilder = new StringBuilder();
			if (command != null && !command.Equals(""))
			{
				try
				{
					Process process = this.initCmdProcess(command);
					this.mprocess = process;
					foreach (Device device in FlashingDevice.flashDeviceList)
					{
						if (device.Name == this.devicename)
						{
							device.DCmd = this;
						}
					}
					process.OutputDataReceived += this.consoleMode_process_OutputDataReceived;
					process.ErrorDataReceived += this.consoleMode_process_ErrorDataReceived;
					process.EnableRaisingEvents = true;
					process.Exited += this.consoleMode_process_Exited;
					if (process.Start())
					{
						Log.w(this.devicename, string.Format("Physical Memory Usage:{0} Byte", process.WorkingSet64.ToString()));
						process.BeginOutputReadLine();
						process.BeginErrorReadLine();
						process.WaitForExit();
					}
				}
				catch (Exception ex)
				{
					Log.w(deviceName, ex, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, null, ex.Message, "error", true);
				}
				finally
				{
					if (this.mprocess != null)
					{
						this.mprocess.Close();
						this.mprocess.Dispose();
						Log.w(this.devicename, "process exit.");
					}
					this.FlashDone();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003328 File Offset: 0x00001528
		private void consoleMode_process_Exited(object sender, EventArgs e)
		{
			try
			{
				Process process = (Process)sender;
				process.CancelErrorRead();
				process.CancelOutputRead();
				process.Refresh();
			}
			catch (Exception ex)
			{
				Log.w(this.devicename, ex.Message, false);
			}
			finally
			{
				this.FlashDone();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000338C File Offset: 0x0000158C
		private void consoleMode_process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				try
				{
					string data = e.Data;
					if ((!string.IsNullOrEmpty(this.errMsg) || data.ToLower().IndexOf("error") >= 0 || data.ToLower().IndexOf("fail") >= 0 || data == "Missmatching image and device") && string.IsNullOrEmpty(this.errMsg))
					{
						this.errMsg = data;
					}
					Log.w(this.devicename, e.Data, false);
					FlashingDevice.UpdateDeviceStatus(this.devicename, null, e.Data, "flashing", false);
				}
				catch (Exception ex)
				{
					try
					{
						Process process = (Process)sender;
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex.Message + "\r\n" + ex.StackTrace;
						}
					}
					catch (Exception ex2)
					{
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex2.Message + "\r\n" + ex2.StackTrace;
						}
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000034B8 File Offset: 0x000016B8
		private void consoleMode_process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				try
				{
					string data = e.Data;
					if (data.ToLower().IndexOf("no insert comport") >= 0)
					{
						try
						{
							Process process = (Process)sender;
							this.errMsg = "no device insert";
						}
						catch (Exception ex)
						{
							Log.w(ex.Message, true);
						}
					}
					if (!string.IsNullOrEmpty(this.errMsg) || data.ToLower().IndexOf("error") >= 0 || data.ToLower().IndexOf("fail") >= 0)
					{
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = data;
						}
						Process process2 = (Process)sender;
						Log.w(this.devicename, data, false);
					}
					else
					{
						if (data.ToLower().IndexOf("insert comport: com") >= 0)
						{
							string text = data.ToLower().Replace("insert comport:", "").Trim();
							Device device = new Device();
							device.Index = int.Parse(text.Replace("com", ""));
							device.Name = text;
							device.StartTime = DateTime.Now;
							this.devicename = device.Name;
							if (MiAppConfig.Get("chip") == "MTK")
							{
								device.Status = "wait for device insert";
							}
							else
							{
								device.Status = "flashing";
							}
							device.Progress = 0f;
							device.IsDone = new bool?(false);
							device.IsUpdate = true;
							if (this.devicename.ToLower() == device.Name.ToLower() && UsbDevice.MtkDevice.IndexOf(device.Name) < 0)
							{
								UsbDevice.MtkDevice.Add(device.Name);
								FlashingDevice.consoleMode_UsbInserted = true;
							}
							Log.w(this.devicename, data);
						}
						else if (data.ToLower().IndexOf("da usb vcom") >= 0)
						{
							Regex regex = new Regex("\\(com.*\\)");
							Match match = regex.Match(data.ToLower());
							if (match.Groups.Count > 0)
							{
								string s = match.Groups[0].Value.Replace("com", "").Replace("(", "").Replace(")", "").Trim();
								if (this.ini.GetIniInt("bootrom", this.devicename, 0) == 0 && !this.ini.WriteIniInt("bootrom", this.devicename, int.Parse(s)))
								{
									MessageBox.Show("couldn't write vcom");
								}
							}
						}
						else if (data.ToLower().IndexOf("download success") >= 0)
						{
							try
							{
								Log.w(this.devicename, e.Data, false);
								this.FlashDone();
							}
							catch (Exception ex2)
							{
								Log.w(this.devicename, ex2.Message, false);
							}
							return;
						}
						string status = data;
						float? progress = null;
						if (data.ToLower().IndexOf("percent") >= 0)
						{
							string text2 = data.ToLower();
							string text3 = "percent";
							int num = text2.IndexOf(text3);
							status = text2.Substring(0, num);
							string value = text2.Substring(num + text3.Length, text2.Length - text3.Length - num).Trim();
							progress = new float?((float)Convert.ToInt32(value) / 100f);
						}
						else
						{
							Log.w(this.devicename, e.Data, true);
						}
						FlashingDevice.UpdateDeviceStatus(this.devicename, progress, status, "flashing", false);
					}
				}
				catch (Exception ex3)
				{
					try
					{
						if (string.IsNullOrEmpty(this.errMsg))
						{
							this.errMsg = ex3.Message + "\r\n" + ex3.StackTrace;
						}
						Process process3 = (Process)sender;
					}
					catch (Exception ex4)
					{
						Log.w(ex4.Message, true);
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000038F8 File Offset: 0x00001AF8
		private void FlashDone()
		{
			if (string.IsNullOrEmpty(this.errMsg))
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(MiAppConfig.Get("checkPoint")))
				{
					using (List<Device>.Enumerator enumerator = FlashingDevice.flashDeviceList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Device device = enumerator.Current;
							if (device.Name == this.devicename)
							{
								List<string> list = new List<string>(device.StatusList);
								using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										string text = enumerator2.Current;
										Regex regex = new Regex(MiAppConfig.Get("checkPoint"));
										if (regex.IsMatch(text.ToLower()) && text.ToLower().IndexOf("bootloader") < 0)
										{
											flag = true;
											Log.w(this.devicename, string.Format("catch checkpoint ({0})", MiAppConfig.Get("checkPoint")));
											break;
										}
									}
									break;
								}
							}
						}
						goto IL_100;
					}
				}
				flag = true;
				IL_100:
				if (flag)
				{
					FlashingDevice.UpdateDeviceStatus(this.devicename, new float?(1f), "flash done", "success", true);
					Log.w(this.devicename, "flash done");
				}
				else
				{
					if (string.IsNullOrEmpty(this.errMsg))
					{
						this.errMsg = "Not catch checkpoint (" + MiAppConfig.Get("checkPoint") + "),flash is not done";
					}
					FlashingDevice.UpdateDeviceStatus(this.devicename, new float?(1f), this.errMsg, "error", true);
					Log.w(this.devicename, "error:" + this.errMsg, false);
				}
			}
			else
			{
				FlashingDevice.UpdateDeviceStatus(this.devicename, new float?(1f), this.errMsg, "error", true);
				Log.w(this.devicename, "error:" + this.errMsg, false);
			}
			UsbDevice.MtkDevice.Remove(this.devicename);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003B14 File Offset: 0x00001D14
		private Process initCmdProcess(string command)
		{
			return new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = "/C (" + command + ")",
					UseShellExecute = false,
					RedirectStandardInput = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true
				}
			};
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003B7C File Offset: 0x00001D7C
		private float? GetPercent(string line)
		{
			Hashtable dummyProgress = SoftwareImage.DummyProgress;
			float? result = null;
			foreach (object obj in dummyProgress.Keys)
			{
				string text = (string)obj;
				if (line.IndexOf(text) >= 0)
				{
					result = new float?((float)Convert.ToInt32(dummyProgress[text]) / 50f);
					break;
				}
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003C08 File Offset: 0x00001E08
		public void KillProcessAndChildrens(string devicename, int pid)
		{
			Log.w(devicename, string.Format("Kill Process {0} And Child Process", pid.ToString()));
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			try
			{
				Process processById = Process.GetProcessById(pid);
				if (!processById.HasExited)
				{
					processById.Kill();
				}
			}
			catch (ArgumentException)
			{
			}
			if (managementObjectCollection != null)
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					this.KillProcessAndChildrens(devicename, Convert.ToInt32(managementObject["ProcessID"]));
				}
			}
		}

		// Token: 0x04000010 RID: 16
		private string devicename;

		// Token: 0x04000011 RID: 17
		public string errMsg = "";

		// Token: 0x04000012 RID: 18
		private string scriptPath = "";

		// Token: 0x04000013 RID: 19
		public Process mprocess;

		// Token: 0x04000014 RID: 20
		public string logType = "";

		// Token: 0x04000015 RID: 21
		private IniFile ini = new IniFile();
	}
}
