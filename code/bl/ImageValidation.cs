using System;
using System.Collections;
using System.IO;
using System.Xml;
using XiaoMiFlash.code.Utility;

namespace XiaoMiFlash.code.bl
{
	// Token: 0x02000007 RID: 7
	public class ImageValidation
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003D50 File Offset: 0x00001F50
		public static string Validate(string path)
		{
			bool flag = true;
			string result = "md5 validate successfully.";
			new Hashtable();
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				if (directoryInfo2.Name.ToLower().IndexOf("images") >= 0)
				{
					directoryInfo = directoryInfo2;
					break;
				}
			}
			string text = directoryInfo.FullName;
			new Hashtable();
			string text2 = directoryInfo.Parent.FullName + "\\md5sum.xml";
			if (File.Exists(text2))
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlReader reader = XmlReader.Create(text2, new XmlReaderSettings
				{
					IgnoreComments = true
				});
				xmlDocument.Load(reader);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
				XmlNode firstChild = xmlNode.FirstChild;
				XmlNodeList childNodes = firstChild.ChildNodes;
				using (IEnumerator enumerator = childNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlNode xmlNode2 = (XmlNode)obj;
						XmlElement xmlElement = (XmlElement)xmlNode2;
						foreach (object obj2 in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							if (xmlAttribute.Name.ToLower() == "name")
							{
								string arg = xmlAttribute.Value.ToLower();
								string innerText = xmlElement.InnerText;
								text = directoryInfo.FullName + string.Format("\\{0}", arg);
								string md5HashFromFile = Utility.GetMD5HashFromFile(text);
								if (md5HashFromFile != innerText)
								{
									result = string.Format("{0} md5 validate failed!", text);
									flag = false;
									break;
								}
								Log.w(string.Format("{0} md5 valide success.", text));
							}
						}
						if (!flag)
						{
							break;
						}
					}
					return result;
				}
			}
			result = "not found md5sum.xml.";
			return result;
		}
	}
}
