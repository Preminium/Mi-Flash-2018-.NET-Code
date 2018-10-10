using System;
using System.Net.Sockets;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000069 RID: 105
	internal class SocketChatClient
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00016D1E File Offset: 0x00014F1E
		public SocketChatClient(Socket sock)
		{
			this.m_sock = sock;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00016D3A File Offset: 0x00014F3A
		public Socket Sock
		{
			get
			{
				return this.m_sock;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00016D44 File Offset: 0x00014F44
		public void SetupRecieveCallback(MainFrm app)
		{
			try
			{
				AsyncCallback callback = new AsyncCallback(app.OnRecievedData);
				this.m_sock.BeginReceive(this.m_byBuff, 0, this.m_byBuff.Length, SocketFlags.None, callback, this);
			}
			catch (Exception ex)
			{
				Log.w(string.Format("Recieve callback setup failed! {0}", ex.Message));
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public byte[] GetRecievedData(IAsyncResult ar)
		{
			int num = 0;
			try
			{
				num = this.m_sock.EndReceive(ar);
			}
			catch
			{
			}
			byte[] array = new byte[num];
			Array.Copy(this.m_byBuff, array, num);
			return array;
		}

		// Token: 0x0400022D RID: 557
		private Socket m_sock;

		// Token: 0x0400022E RID: 558
		private byte[] m_byBuff = new byte[50];
	}
}
