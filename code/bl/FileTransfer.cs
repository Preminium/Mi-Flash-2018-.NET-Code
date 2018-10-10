using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using XiaoMiFlash.code.data;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x0200002C RID: 44
	public class FileTransfer
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0000D970 File Offset: 0x0000BB70
		public FileTransfer(string port, string filePath)
		{
			this.portName = port;
			this.filePath = filePath;
			FlashingDevice.UpdateDeviceStatus(this.portName, new float?(0f), "flashing " + filePath, "flashing", false);
			this.openFile(filePath);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		private bool openFile(string filePath)
		{
			this.filePath = filePath;
			bool result = false;
			if (MemImg.isHighSpeed && filePath.ToLower().IndexOf(MiAppConfig.Get("noquick")) < 0)
			{
				this.fileLength = MemImg.mapImg(filePath);
				Log.w(this.portName, string.Format("Image {0} ,quick transfer", filePath));
			}
			try
			{
				FileInfo fileInfo = new FileInfo(filePath);
				this.fileLength = fileInfo.Length;
				this.fileStream = File.OpenRead(filePath);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
				throw new Exception(ex.Message);
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public int transfer(SerialPort port, int offset, int size)
		{
			if (!port.IsOpen)
			{
				string.Format("{0} is not open", port.PortName);
				return 0;
			}
			int result = 0;
			byte[] bytesFromFile = this.GetBytesFromFile((long)offset, size, out result);
			port.Write(bytesFromFile, 0, size);
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		public void WriteFile(SerialPortDevice portCnn, string strPartitionStartSector, string strPartitionSectorNumber, string pszImageFile, string strFileStartSector, string strFileSectorOffset, string sector_size, string physical_partition_number, string addtionalFirehose, bool chkAck, int? chunkCount)
		{
			long num = Convert.ToInt64(strPartitionSectorNumber);
			if (num == 0L)
			{
				num = 2147483647L;
			}
			long num2 = Convert.ToInt64(strFileStartSector);
			long num3 = Convert.ToInt64(strFileSectorOffset);
			long num4 = Convert.ToInt64(sector_size);
			long num5 = Convert.ToInt64(physical_partition_number);
			Log.w(portCnn.comm.serialPort.PortName, string.Format("write file legnth {0} to partition {1}", this.getFileSize(), strPartitionStartSector));
			long num6 = (this.getFileSize() + num4 - 1L) / num4;
			if (num6 - num2 > num)
			{
				num6 = num2 + num;
			}
			else
			{
				num = num6 - num2;
			}
			string command = string.Format(Firehose.FIREHOSE_PROGRAM, new object[]
			{
				num4,
				num,
				strPartitionStartSector,
				num5,
				addtionalFirehose
			});
			portCnn.comm.SendCommand(command);
			for (long num7 = num2; num7 < num6; num7 += (long)portCnn.comm.m_dwBufferSectors)
			{
				long num8 = num6 - num7;
				num8 = ((num8 < (long)portCnn.comm.m_dwBufferSectors) ? num8 : ((long)portCnn.comm.m_dwBufferSectors));
				Log.w(portCnn.comm.serialPort.PortName, string.Format("WriteFile position {0}, size {1}", (num4 * num7).ToString("X"), num4 * num8));
				long offset = num3 + num4 * num7;
				int size = (int)(num4 * num8);
				int num9 = 0;
				byte[] bytesFromFile = this.GetBytesFromFile(offset, size, out num9);
				portCnn.comm.WritePort(bytesFromFile, 0, bytesFromFile.Length);
			}
			if (chkAck)
			{
				string message = null;
				if (!portCnn.comm.chkRspAck(out message, chunkCount.Value))
				{
					throw new Exception(message);
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public void WriteSparseFileToDevice(SerialPortDevice portCnn, string pszPartitionStartSector, string pszPartitionSectorNumber, string pszImageFile, string pszFileStartSector, string pszSectorSizeInBytes, string pszPhysicalPartitionNumber, string addtionalFirehose)
		{
			int num = Convert.ToInt32(pszPartitionStartSector);
			int num2 = Convert.ToInt32(pszPartitionSectorNumber);
			int num3 = Convert.ToInt32(pszFileStartSector);
			long num4 = 0L;
			int num5 = Convert.ToInt32(pszSectorSizeInBytes);
			Convert.ToInt32(pszPhysicalPartitionNumber);
			SparseImageHeader sparseImageHeader = default(SparseImageHeader);
			string text = "";
			if (num3 != 0)
			{
				text = "ERROR_BAD_FORMAT";
				Log.w(portCnn.comm.serialPort.PortName, text);
			}
			if (num5 == 0)
			{
				text = "ERROR_BAD_FORMAT";
				Log.w(portCnn.comm.serialPort.PortName, "ERROR_BAD_FORMAT");
			}
			int size = Marshal.SizeOf(sparseImageHeader);
			int num6 = 0;
			byte[] bytesFromFile = this.GetBytesFromFile(num4, size, out num6);
			sparseImageHeader = (SparseImageHeader)CommandFormat.BytesToStuct(bytesFromFile, typeof(SparseImageHeader));
			num4 += (long)((ulong)sparseImageHeader.uFileHeaderSize);
			if (sparseImageHeader.uMagic != 3978755898u)
			{
				text = "ERROR_BAD_FORMAT";
				Log.w(portCnn.comm.serialPort.PortName, string.Format("ERROR_BAD_FORMAT {0}", sparseImageHeader.uMagic.ToString()));
			}
			if (sparseImageHeader.uMajorVersion != 1)
			{
				text = "ERROR_UNSUPPORTED_TYPE";
				Log.w(portCnn.comm.serialPort.PortName, string.Format("ERROR_UNSUPPORTED_TYPE {0}", sparseImageHeader.uMajorVersion.ToString()));
			}
			if ((ulong)sparseImageHeader.uBlockSize % (ulong)((long)num5) != 0UL)
			{
				text = "ERROR_BAD_FORMAT";
				Log.w(portCnn.comm.serialPort.PortName, string.Format("ERROR_BAD_FORMAT {0}", sparseImageHeader.uBlockSize.ToString()));
			}
			if (num2 != 0 && (ulong)(sparseImageHeader.uBlockSize * sparseImageHeader.uTotalBlocks) / (ulong)((long)num5) > (ulong)((long)num2))
			{
				text = "ERROR_FILE_TOO_LARGE";
				Log.w(portCnn.comm.serialPort.PortName, string.Format("ERROR_FILE_TOO_LARGE size {0} ullPartitionSectorNumber {1}", ((long)((ulong)(sparseImageHeader.uBlockSize * sparseImageHeader.uTotalBlocks) / (ulong)((long)num5))).ToString(), num2.ToString()));
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new Exception(text);
			}
			int num7 = 0;
			int num8 = 1;
			while ((long)num8 <= (long)((ulong)sparseImageHeader.uTotalChunks))
			{
				Log.w(portCnn.comm.serialPort.PortName, string.Format("total chunks {0}, current chunk {1}", sparseImageHeader.uTotalChunks, num8));
				SparseChunkHeader sparseChunkHeader = default(SparseChunkHeader);
				size = Marshal.SizeOf(sparseChunkHeader);
				float num9 = 0f;
				bytesFromFile = this.GetBytesFromFile(num4, size, out num6, out num9);
				sparseChunkHeader = (SparseChunkHeader)CommandFormat.BytesToStuct(bytesFromFile, typeof(SparseChunkHeader));
				num4 += (long)((ulong)sparseImageHeader.uChunkHeaderSize);
				int num10 = (int)(sparseImageHeader.uBlockSize * sparseChunkHeader.uChunkSize);
				int num11 = num10 / num5;
				if (sparseChunkHeader.uChunkType == 51905)
				{
					if ((ulong)sparseChunkHeader.uTotalSize != (ulong)((long)((int)sparseImageHeader.uChunkHeaderSize + num10)))
					{
						Log.w(portCnn.comm.serialPort.PortName, "ERROR_BAD_FORMAT");
						throw new Exception("ERROR_BAD_FORMAT");
					}
					string strPartitionStartSector = num.ToString();
					string strPartitionSectorNumber = num11.ToString();
					string strFileStartSector = (num4 / (long)num5).ToString();
					string strFileSectorOffset = (num4 % (long)num5).ToString();
					num7++;
					bool chkAck = false;
					int value = 0;
					if (sparseImageHeader.uTotalChunks <= 10u)
					{
						if ((long)num8 == (long)((ulong)sparseImageHeader.uTotalChunks))
						{
							value = num7;
							chkAck = true;
						}
					}
					else
					{
						if (num7 % 10 == 0)
						{
							value = 10;
							chkAck = true;
						}
						if ((long)num8 == (long)((ulong)sparseImageHeader.uTotalChunks))
						{
							value = num7 % 10;
							chkAck = true;
						}
					}
					this.WriteFile(portCnn, strPartitionStartSector, strPartitionSectorNumber, pszImageFile, strFileStartSector, strFileSectorOffset, pszSectorSizeInBytes, pszPhysicalPartitionNumber, addtionalFirehose, chkAck, new int?(value));
					num4 += (long)(num5 * num11);
					num += num11;
				}
				else if (sparseChunkHeader.uChunkType == 51907)
				{
					if (sparseChunkHeader.uTotalSize != (uint)sparseImageHeader.uChunkHeaderSize)
					{
						Log.w(portCnn.comm.serialPort.PortName, "ERROR_BAD_FORMAT");
					}
					num += num11;
					if ((long)num8 == (long)((ulong)sparseImageHeader.uTotalChunks))
					{
						int num12 = num7 % 10;
						if (num12 > 0)
						{
							string message = null;
							if (!portCnn.comm.chkRspAck(out message, num12))
							{
								throw new Exception(message);
							}
						}
					}
				}
				else
				{
					Log.w(portCnn.comm.serialPort.PortName, string.Format("ERROR_UNSUPPORTED_TYPE {0}", sparseChunkHeader.uChunkType.ToString()));
				}
				num8++;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		public byte[] GetBytesFromFile(long offset, int size, out int n)
		{
			float value = 0f;
			byte[] array;
			if (MemImg.isHighSpeed && this.filePath.ToLower().IndexOf(MiAppConfig.Get("noquick")) < 0)
			{
				array = MemImg.GetBytesFromFile(this.filePath, offset, size, out value);
				n = array.Length;
			}
			else
			{
				long length = this.fileStream.Length;
				array = new byte[size];
				this.fileStream.Seek(offset, SeekOrigin.Begin);
				n = this.fileStream.Read(array, 0, size);
				value = (float)offset / (float)length;
			}
			FlashingDevice.UpdateDeviceStatus(this.portName, new float?(value), null, "flashing", false);
			return array;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000E160 File Offset: 0x0000C360
		public byte[] GetBytesFromFile(long offset, int size, out int n, out float percent)
		{
			byte[] array;
			if (MemImg.isHighSpeed && this.filePath.ToLower().IndexOf(MiAppConfig.Get("noquick")) < 0)
			{
				array = MemImg.GetBytesFromFile(this.filePath, offset, size, out percent);
				n = array.Length;
			}
			else
			{
				long length = this.fileStream.Length;
				array = new byte[size];
				this.fileStream.Seek(offset, SeekOrigin.Begin);
				n = this.fileStream.Read(array, 0, size);
				percent = (float)offset / (float)length;
			}
			FlashingDevice.UpdateDeviceStatus(this.portName, new float?(percent), null, "flashing", false);
			return array;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		public long getFileSize()
		{
			if (this.fileLength != 0L)
			{
				return this.fileLength;
			}
			FileInfo fileInfo = new FileInfo(this.filePath);
			return fileInfo.Length;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000E22C File Offset: 0x0000C42C
		public void closeTransfer()
		{
			if (this.fileStream != null)
			{
				this.fileStream.Close();
				this.fileStream.Dispose();
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000E24C File Offset: 0x0000C44C
		~FileTransfer()
		{
			if (this.fileStream != null)
			{
				this.fileStream.Close();
				this.fileStream.Dispose();
			}
		}

		// Token: 0x040000D9 RID: 217
		protected FileStream fileStream;

		// Token: 0x040000DA RID: 218
		public string filePath;

		// Token: 0x040000DB RID: 219
		public string portName;

		// Token: 0x040000DC RID: 220
		private long fileLength;

		// Token: 0x040000DD RID: 221
		private List<ShareMemory> shareMemList = new List<ShareMemory>();
	}
}
