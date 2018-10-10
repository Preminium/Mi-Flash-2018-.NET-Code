using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x0200000D RID: 13
	public class MiAppConfig
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000085F8 File Offset: 0x000067F8
		public static void Add(string key, string value)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings.Add(key, value);
			configuration.Save();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00008624 File Offset: 0x00006824
		public static void SetValue(string AppKey, string AppValue)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings[AppKey].Value = AppValue;
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00008660 File Offset: 0x00006860
		public static string GetAppConfig(string appKey)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(Application.ExecutablePath + ".config");
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
			XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + appKey + "']");
			if (xmlElement != null)
			{
				return xmlElement.Attributes["value"].Value;
			}
			return string.Empty;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000086CE File Offset: 0x000068CE
		public static string Get(string key)
		{
			if (ConfigurationManager.AppSettings[key] == null)
			{
				MiAppConfig.Add(key, "");
			}
			if (ConfigurationManager.AppSettings[key] != null)
			{
				return ConfigurationManager.AppSettings[key].ToString();
			}
			return "";
		}
	}
}
