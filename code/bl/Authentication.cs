using System;
using System.IO;
using System.Net;
using System.Text;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000058 RID: 88
	public class Authentication
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00014C60 File Offset: 0x00012E60
		private string SendDataByPost(string Url, string postDataStr, ref CookieContainer cookie)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
			if (cookie.Count == 0)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
				cookie = httpWebRequest.CookieContainer;
			}
			else
			{
				httpWebRequest.CookieContainer = cookie;
			}
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
			httpWebRequest.ContentLength = (long)postDataStr.Length;
			Stream requestStream = httpWebRequest.GetRequestStream();
			StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("gb2312"));
			streamWriter.Write(postDataStr);
			streamWriter.Close();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			Stream responseStream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00014D40 File Offset: 0x00012F40
		private string SendDataByGET(string Url, string postDataStr, ref CookieContainer cookie)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + ((postDataStr == "") ? "" : "?") + postDataStr);
			if (cookie.Count == 0)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
				cookie = httpWebRequest.CookieContainer;
			}
			else
			{
				httpWebRequest.CookieContainer = cookie;
			}
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "text/html;charset=UTF-8";
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			Stream responseStream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00014DF0 File Offset: 0x00012FF0
		public string Login(string username, string pwd)
		{
			string result;
			try
			{
				SignInfo signInfo = default(SignInfo);
				CookieContainer cookieContainer = new CookieContainer();
				signInfo.sid = "passport";
				string url = this.ACCOUNT_LOGIN_URL + "?sid=" + signInfo.sid + "&_json=true&passive=true&hidden=false";
				string text = this.SendDataByGET(url, "", ref cookieContainer);
				url = this.ACCOUNT_LOGIN_AUTH_URL + "?_json=true";
				result = text;
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x040001E5 RID: 485
		private static string ACCOUNT_LOGIN_URL_BASE = "http://account.preview.n.xiaomi.net/";

		// Token: 0x040001E6 RID: 486
		private string ACCOUNT_LOGIN_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/serviceLogin";

		// Token: 0x040001E7 RID: 487
		private string ACCOUNT_LOGIN_AUTH_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/serviceLoginAuth2";

		// Token: 0x040001E8 RID: 488
		private string ACCOUNT_LOGIN_STEP2 = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/loginStep2";

		// Token: 0x040001E9 RID: 489
		private string ACCOUNT_LOGOUT_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/logout";

		// Token: 0x040001EA RID: 490
		private string ACCOUNT_USERCARD_URL = "http://api.account.xiaomi.com/pass/usersCard";

		// Token: 0x040001EB RID: 491
		private string ACCOUNT_REGISTER_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/register";

		// Token: 0x040001EC RID: 492
		private string ACCOUNT_FORGETPASSWORD_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/forgetPassword";

		// Token: 0x040001ED RID: 493
		private string ACCOUT_GET_VERIFY_CODE_URL = Authentication.ACCOUNT_LOGIN_URL_BASE + "pass/getCode?icodeType?=login?";
	}
}
