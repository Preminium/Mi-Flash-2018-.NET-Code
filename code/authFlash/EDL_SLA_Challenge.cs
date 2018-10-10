using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using XiaoMiFlash.code.module;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.authFlash
{
	// Token: 0x0200006B RID: 107
	public class EDL_SLA_Challenge
	{
		// Token: 0x06000246 RID: 582
		[DllImport("SLA_Challenge.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int StartLoginProcess();

		// Token: 0x06000247 RID: 583
		[DllImport("SLA_Challenge.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int get_user_info(IntPtr usr_arg, IntPtr user_info);

		// Token: 0x06000248 RID: 584
		[DllImport("SLA_Challenge.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int free_user_info(IntPtr usr_arg, IntPtr user_info);

		// Token: 0x06000249 RID: 585
		[DllImport("SLA_Challenge.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int SLA_Challenge(IntPtr obj, byte[] challenge_in, int in_len, out IntPtr challenge_out, ref int out_len);

		// Token: 0x0600024A RID: 586
		[DllImport("SLA_Challenge.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int SLA_Challenge_End(IntPtr obj, IntPtr challenge_out);

		// Token: 0x0600024B RID: 587
		[DllImport("SLA_Challenge.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int can_flash(IntPtr obj);

		// Token: 0x0600024C RID: 588 RVA: 0x00016E98 File Offset: 0x00015098
		private static string decodeOut(string str)
		{
			char[] array = str.ToCharArray();
			Encoder encoder = Encoding.Unicode.GetEncoder();
			byte[] array2 = new byte[encoder.GetByteCount(array, 0, array.Length, true)];
			encoder.GetBytes(array, 0, array.Length, array2, 0, true);
			Decoder decoder = Encoding.UTF8.GetDecoder();
			int charCount = decoder.GetCharCount(array2, 0, array2.Length);
			char[] array3 = new char[charCount];
			decoder.GetChars(array2, 0, array2.Length, array3, 0);
			return new string(array3);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00016F10 File Offset: 0x00015110
		public static PInvokeResultArg GetUserInfo(MiUserInfo miUser)
		{
			IntPtr zero = IntPtr.Zero;
			int cb = Marshal.SizeOf(typeof(UserInfo));
			IntPtr intPtr = Marshal.AllocHGlobal(cb);
			int num = -1;
			lock (EDL_SLA_Challenge.locker1)
			{
				num = EDL_SLA_Challenge.get_user_info(zero, intPtr);
			}
			UserInfo userInfo = (UserInfo)Marshal.PtrToStructure(intPtr, typeof(UserInfo));
			miUser.id = Marshal.PtrToStringAnsi(userInfo.user_id);
			string str = Marshal.PtrToStringUni(userInfo.user_name);
			miUser.name = EDL_SLA_Challenge.decodeOut(str);
			PInvokeResultArg pinvokeResultArg = new PInvokeResultArg();
			pinvokeResultArg.result = num;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			if (num != 0)
			{
				return pinvokeResultArg;
			}
			num = EDL_SLA_Challenge.free_user_info(zero, intPtr);
			pinvokeResultArg.result = num;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			return pinvokeResultArg;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00017020 File Offset: 0x00015220
		public static PInvokeResultArg AuthFlash()
		{
			IntPtr zero = IntPtr.Zero;
			int result = -1;
			lock (EDL_SLA_Challenge.locker1)
			{
				result = EDL_SLA_Challenge.can_flash(zero);
			}
			PInvokeResultArg pinvokeResultArg = new PInvokeResultArg();
			pinvokeResultArg.result = result;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			return pinvokeResultArg;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00017090 File Offset: 0x00015290
		public static PInvokeResultArg SignEdl(string orignKey, out string signedKey)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr challenge_out = 0;
			int num = 0;
			int num2 = -1;
			lock (EDL_SLA_Challenge.locker1)
			{
				num2 = EDL_SLA_Challenge.SLA_Challenge(zero, Encoding.Default.GetBytes(orignKey), orignKey.Length, out challenge_out, ref num);
			}
			new StringBuilder();
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				IntPtr ptr = new IntPtr(challenge_out.ToInt64() + (long)(Marshal.SizeOf(typeof(byte)) * i));
				array[i] = Marshal.ReadByte(ptr);
			}
			PInvokeResultArg pinvokeResultArg = new PInvokeResultArg();
			pinvokeResultArg.result = num2;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			if (num2 != 0)
			{
				signedKey = string.Empty;
				return pinvokeResultArg;
			}
			byte[] array2 = array;
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < array2.Length; j++)
			{
				stringBuilder.Append(array2[j].ToString("X2"));
			}
			signedKey = stringBuilder.ToString();
			num2 = EDL_SLA_Challenge.SLA_Challenge_End(zero, challenge_out);
			pinvokeResultArg.result = num2;
			pinvokeResultArg.lastErrCode = Marshal.GetLastWin32Error();
			pinvokeResultArg.lastErrMsg = new Win32Exception(pinvokeResultArg.lastErrCode).Message;
			return pinvokeResultArg;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000171F8 File Offset: 0x000153F8
		public static MiUserInfo authEDl(string devicename, out bool canFlash)
		{
			MiUserInfo miUserInfo = new MiUserInfo();
			canFlash = false;
			try
			{
				PInvokeResultArg pinvokeResultArg = new PInvokeResultArg();
				Log.w(devicename, "GetUserInfo");
				pinvokeResultArg = EDL_SLA_Challenge.GetUserInfo(miUserInfo);
				if (pinvokeResultArg.result == 0)
				{
					Log.w(devicename, "AuthFlash");
					pinvokeResultArg = EDL_SLA_Challenge.AuthFlash();
					if (pinvokeResultArg.result == 1)
					{
						canFlash = true;
					}
					else
					{
						string text = "Authentication required.";
						MessageBox.Show(text);
						Log.w(string.Format("{0} errcode:{1} lasterr:{2}", text, pinvokeResultArg.lastErrCode, pinvokeResultArg.lastErrMsg));
					}
				}
				else
				{
					string text = "Login failed.";
					MessageBox.Show(text);
					Log.w(string.Format("{0} errcode:{1} lasterr:{2}", text, pinvokeResultArg.lastErrCode, pinvokeResultArg.lastErrMsg));
				}
			}
			catch (Exception ex)
			{
				string text = ex.Message;
				Log.w(string.Format("authentication edl error:{0}", text));
			}
			return miUserInfo;
		}

		// Token: 0x04000230 RID: 560
		private static readonly object locker1 = new object();
	}
}
