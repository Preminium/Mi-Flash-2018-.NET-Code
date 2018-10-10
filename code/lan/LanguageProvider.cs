using System;
using System.Xml;

namespace XiaoMiFlash.code.lan
{
	// Token: 0x02000014 RID: 20
	public class LanguageProvider
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00009254 File Offset: 0x00007454
		public LanguageProvider(string lanType)
		{
			this.languageType = lanType;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00009270 File Offset: 0x00007470
		public string GetLanguage(string ctrlID)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.IgnoreComments = true;
			XmlReader reader = XmlReader.Create(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Source\\LanguageLibrary.xml");
			xmlDocument.Load(reader);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("LanguageLibrary");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			string result = "";
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				XmlElement xmlElement = (XmlElement)xmlNode2;
				if (!(xmlElement.Name.ToLower() != "lan") && xmlElement.Attributes["CTRLID"].Value == ctrlID)
				{
					result = xmlElement.Attributes[this.languageType].Value;
					break;
				}
			}
			return result;
		}

		// Token: 0x04000061 RID: 97
		private string languageType = "";
	}
}
