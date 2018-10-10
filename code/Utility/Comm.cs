using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using XiaoMiFlash.code.data;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200000E RID: 14
	public class Comm
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00008714 File Offset: 0x00006914
		public Comm()
		{
			this._keepReading = false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00008770 File Offset: 0x00006970
		public bool isKeepReading()
		{
			return this._keepReading;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00008778 File Offset: 0x00006978
		public bool IsOpen
		{
			get
			{
				int num = 20;
				while (num-- > 0 && !this.serialPort.IsOpen)
				{
					Log.w(this.serialPort.PortName, "wait for port open.");
					Thread.Sleep(50);
				}
				return this.serialPort.IsOpen;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000087C6 File Offset: 0x000069C6
		public void StartReading()
		{
			if (!this._keepReading)
			{
				this._keepReading = true;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000087D7 File Offset: 0x000069D7
		public void StopReading()
		{
			if (this._keepReading)
			{
				this._keepReading = false;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000087E8 File Offset: 0x000069E8
		public byte[] ReadPortData()
		{
			byte[] array = null;
			if (this.serialPort.IsOpen)
			{
				int bytesToRead = this.serialPort.BytesToRead;
				if (bytesToRead > 0)
				{
					array = new byte[bytesToRead];
					try
					{
						this.serialPort.Read(array, 0, bytesToRead);
					}
					catch (TimeoutException ex)
					{
						Log.w(this.serialPort.PortName, ex, false);
					}
				}
			}
			return array;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008854 File Offset: 0x00006A54
		public byte[] ReadPortData(int offset, int count)
		{
			byte[] array = new byte[count];
			try
			{
				this.serialPort.Read(array, offset, count);
			}
			catch (TimeoutException ex)
			{
				Log.w(this.serialPort.PortName, ex, false);
			}
			return array;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000088A0 File Offset: 0x00006AA0
		public void Open()
		{
			this.Close();
			this.serialPort.Open();
			if (this.serialPort.IsOpen)
			{
				return;
			}
			string text = "open serial port failed!";
			Log.w(this.serialPort.PortName, text);
			FlashingDevice.UpdateDeviceStatus(this.serialPort.PortName, null, text, "error", true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00008904 File Offset: 0x00006B04
		private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			int bytesToRead = this.serialPort.BytesToRead;
			this.recData = new byte[bytesToRead];
			this.received_count += (long)bytesToRead;
			this.serialPort.Read(this.recData, 0, bytesToRead);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000894C File Offset: 0x00006B4C
		public void Close()
		{
			this.serialPort.Close();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008959 File Offset: 0x00006B59
		public void CleanBuffer()
		{
			this.serialPort.DiscardOutBuffer();
			this.serialPort.DiscardInBuffer();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00008974 File Offset: 0x00006B74
		public void WritePort(byte[] send, int offSet, int count)
		{
			if (this.IsOpen)
			{
				bool keepReading = this._keepReading;
				int num = 0;
				Exception ex = new TimeoutException();
				bool flag = false;
				while (num++ <= 6 && ex != null && ex.GetType() == typeof(TimeoutException))
				{
					try
					{
						this.serialPort.WriteTimeout = 2000;
						this.serialPort.Write(send, offSet, count);
						flag = true;
						if (this.isWriteDump)
						{
							Log.w(this.serialPort.PortName, "write to port:");
							this.Dump(send);
						}
						ex = null;
					}
					catch (TimeoutException ex2)
					{
						ex = ex2;
						Log.w(this.serialPort.PortName, "write time out try agian " + num);
						Thread.Sleep(500);
					}
					catch (Exception ex3)
					{
						Log.w(this.serialPort.PortName, "write failed:" + ex3.Message);
					}
				}
				if (!flag)
				{
					Log.w(this.serialPort.PortName, ex, true);
					throw new Exception("write time out,maybe device was disconnected.");
				}
			}
			this.writeCount++;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008AAC File Offset: 0x00006CAC
		public bool SendCommand(string command)
		{
			return this.SendCommand(command, false);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public bool SendCommand(string command, bool checkAck)
		{
			byte[] bytes = Encoding.Default.GetBytes(command);
			if (this.isReadDump || checkAck)
			{
				Log.w(this.serialPort.PortName, "send command:" + command);
			}
			this.WritePort(bytes, 0, bytes.Length);
			return checkAck && this.GetResponse(checkAck);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00008B10 File Offset: 0x00006D10
		private int SubstringCount(string str, string substring)
		{
			if (str.Contains(substring))
			{
				string text = str.Replace(substring, "");
				return (str.Length - text.Length) / substring.Length;
			}
			return 0;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00008B4C File Offset: 0x00006D4C
		public bool chkRspAck(out string msg)
		{
			msg = null;
			byte[] binary = this.ReadDataFromPort();
			string[] array = this.Dump(binary, true);
			string value = "<response value=\"ACK\"";
			int num = 10;
			while ((array.Length != 2 || array[1].IndexOf(value) < 0) && num-- >= 0)
			{
				Thread.Sleep(10);
				binary = this.ReadDataFromPort();
				array = this.Dump(binary, true);
			}
			if (array.Length == 2 && array[1].IndexOf(value) >= 0)
			{
				this.CleanBuffer();
				return true;
			}
			msg = "did not detect ACK from target.";
			return false;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00008BCC File Offset: 0x00006DCC
		public bool chkRspAck(out string msg, int chunkCount)
		{
			msg = null;
			byte[] binary = this.ReadDataFromPort();
			string[] array = this.Dump(binary, true);
			string text = "<response value=\"ACK\"";
			int num = 10;
			while ((array.Length != 2 || array[1].IndexOf(text) < 0) && num-- >= 0)
			{
				Thread.Sleep(10);
				binary = this.ReadDataFromPort();
				array = this.Dump(binary, true);
			}
			if (array.Length != 2 || array[1].IndexOf(text) < 0)
			{
				msg = array[1];
				return false;
			}
			int num2 = this.SubstringCount(array[1], text);
			num = 10;
			while (num2 < chunkCount * 2 && num-- > 0)
			{
				Thread.Sleep(10);
				binary = this.ReadDataFromPort();
				array = this.Dump(binary, true);
				num2 += this.SubstringCount(array[1], text);
			}
			if (chunkCount * 2 > num2)
			{
				Log.w(this.serialPort.PortName, "ACK count don't match!");
				throw new Exception("ACK count don't match!");
			}
			Log.w(this.serialPort.PortName, string.Format("{0} chunks match {1} ack", chunkCount, num2));
			this.CleanBuffer();
			this.writeCount = 0;
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00008CEC File Offset: 0x00006EEC
		public byte[] getRecDataIgnoreExcep()
		{
			byte[] array = this.ReadDataFromPort();
			if (array != null && array.Length > 0 && this.isReadDump)
			{
				Log.w(this.serialPort.PortName, "read from port:");
				this.Dump(array);
			}
			return array;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00008D30 File Offset: 0x00006F30
		public byte[] getRecData()
		{
			byte[] array = this.ReadDataFromPort();
			if (array == null)
			{
				throw new Exception("can not read from port " + this.serialPort.PortName);
			}
			if (array.Length > 0 && this.isReadDump)
			{
				Log.w(this.serialPort.PortName, "read from port:");
				this.Dump(array);
			}
			return array;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00008D90 File Offset: 0x00006F90
		private byte[] ReadDataFromPort()
		{
			int num = 10;
			this.recData = null;
			this.recData = this.ReadPortData();
			while (num-- >= 0 && this.recData == null)
			{
				Thread.Sleep(50);
				this.recData = this.ReadPortData();
			}
			return this.recData;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00008DE0 File Offset: 0x00006FE0
		private bool WaitForAck()
		{
			bool flag = false;
			int num = 10;
			while (num-- > 0 && !flag)
			{
				byte[] binary = this.ReadDataFromPort();
				string[] array = this.Dump(binary);
				flag = (array.Length == 2 && array[1].IndexOf("<response value=\"ACK\" />") >= 0);
				Thread.Sleep(50);
			}
			return flag;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00008E34 File Offset: 0x00007034
		public bool GetResponse(bool waiteACK)
		{
			bool flag = false;
			Log.w(this.serialPort.PortName, "get response from target");
			if (!waiteACK)
			{
				return this.ReadDataFromPort() != null;
			}
			int num = 2;
			if (waiteACK)
			{
				num = 4;
			}
			while (num-- > 0 && !flag)
			{
				List<XmlDocument> responseXml = this.GetResponseXml(waiteACK);
				int count = responseXml.Count;
				foreach (XmlDocument xmlDocument in responseXml)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode("data");
					XmlNodeList childNodes = xmlNode.ChildNodes;
					foreach (object obj in childNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						XmlElement xmlElement = (XmlElement)xmlNode2;
						if (xmlElement.Name.ToLower() == "sig")
						{
							this.auth = xmlElement.OuterXml.Replace("blob", "sig");
						}
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							if (xmlAttribute.Name.ToLower() == "maxpayloadsizetotargetinbytes")
							{
								this.m_dwBufferSectors = Convert.ToInt32(xmlAttribute.Value) / this.intSectorSize;
							}
							if (xmlAttribute.Value.ToLower() == "ack")
							{
								flag = true;
							}
						}
					}
				}
				if (waiteACK)
				{
					Thread.Sleep(50);
				}
			}
			return flag;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000903C File Offset: 0x0000723C
		private List<XmlDocument> GetResponseXml()
		{
			return this.GetResponseXml(false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00009048 File Offset: 0x00007248
		public List<XmlDocument> GetResponseXml(bool waiteACK)
		{
			List<XmlDocument> list = new List<XmlDocument>();
			byte[] binary = this.ReadDataFromPort();
			string[] array = this.Dump(binary, waiteACK);
			if (array.Length >= 2)
			{
				string[] source = Regex.Split(array[1], "\\<\\?xml");
				foreach (string text in source.ToList<string>())
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (text.ToLower().IndexOf(this.edlAuthErr) >= 0)
						{
							this.needEdlAuth = true;
						}
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml("<?xml " + text);
						list.Add(xmlDocument);
					}
				}
			}
			return list;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000910C File Offset: 0x0000730C
		private string GetResponseXmlStr()
		{
			byte[] binary = this.ReadDataFromPort();
			return this.Dump(binary)[1];
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00009129 File Offset: 0x00007329
		private string[] Dump(byte[] binary)
		{
			return this.Dump(binary, false);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00009134 File Offset: 0x00007334
		private string[] Dump(byte[] binary, bool waitACK)
		{
			if (binary == null)
			{
				Log.w(this.serialPort.PortName, "no Binary dump");
				return new string[]
				{
					"",
					""
				};
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			new StringBuilder();
			new StringBuilder();
			for (int i = 0; i < binary.Length; i++)
			{
				stringBuilder2.Append(Convert.ToChar(binary[i]).ToString());
			}
			if ((this.isReadDump || waitACK) && !this._keepReading)
			{
				Log.w(this.serialPort.PortName, "dump:" + stringBuilder2.ToString() + "\r\n\r\n", false);
			}
			return new string[]
			{
				stringBuilder.ToString(),
				stringBuilder2.ToString()
			};
		}

		// Token: 0x04000035 RID: 53
		public int writeCount;

		// Token: 0x04000036 RID: 54
		public bool isReadDump = true;

		// Token: 0x04000037 RID: 55
		public bool isWriteDump;

		// Token: 0x04000038 RID: 56
		public bool ignoreResponse = true;

		// Token: 0x04000039 RID: 57
		public SerialPort serialPort;

		// Token: 0x0400003A RID: 58
		private bool _keepReading;

		// Token: 0x0400003B RID: 59
		public byte[] recData;

		// Token: 0x0400003C RID: 60
		private long received_count;

		// Token: 0x0400003D RID: 61
		public int MAX_SECTOR_STR_LEN = 20;

		// Token: 0x0400003E RID: 62
		public int SECTOR_SIZE_UFS = 4096;

		// Token: 0x0400003F RID: 63
		public int SECTOR_SIZE_EMMC = 512;

		// Token: 0x04000040 RID: 64
		public int m_dwBufferSectors;

		// Token: 0x04000041 RID: 65
		public int intSectorSize;

		// Token: 0x04000042 RID: 66
		public string auth = "";

		// Token: 0x04000043 RID: 67
		public string edlAuthErr = "error: only nop and sig tag can be recevied before authentication";

		// Token: 0x04000044 RID: 68
		public bool needEdlAuth;
	}
}
