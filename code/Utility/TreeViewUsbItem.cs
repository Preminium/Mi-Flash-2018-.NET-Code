using System;
using System.Collections.Generic;
using MiUSB;

namespace XiaoMiFlash.code.Utility
{
	// Token: 0x02000023 RID: 35
	internal class TreeViewUsbItem
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000B7AC File Offset: 0x000099AC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public string Name { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000B7BD File Offset: 0x000099BD
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000B7C5 File Offset: 0x000099C5
		public object Data { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000B7CE File Offset: 0x000099CE
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000B7D6 File Offset: 0x000099D6
		public List<TreeViewUsbItem> Children { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public static List<TreeViewUsbItem> AllUsbDevices
		{
			get
			{
				TreeViewUsbItem.ConnectedHubs = 0;
				TreeViewUsbItem.ConnectedDevices = 0;
				TreeViewUsbItem treeViewUsbItem = new TreeViewUsbItem();
				treeViewUsbItem.Name = "Computer";
				treeViewUsbItem.Data = "Machine Name:" + System.Environment.MachineName;
				HostControllerInfo[] allHostControllers = USB.AllHostControllers;
				if (allHostControllers != null)
				{
					List<TreeViewUsbItem> list = new List<TreeViewUsbItem>(allHostControllers.Length);
					foreach (HostControllerInfo hostControllerInfo in allHostControllers)
					{
						TreeViewUsbItem treeViewUsbItem2 = new TreeViewUsbItem();
						treeViewUsbItem2.Name = hostControllerInfo.Name;
						treeViewUsbItem2.Data = hostControllerInfo;
						string usbRootHubPath = USB.GetUsbRootHubPath(hostControllerInfo.PNPDeviceID);
						treeViewUsbItem2.Children = TreeViewUsbItem.AddHubNode(usbRootHubPath, "RootHub");
						list.Add(treeViewUsbItem2);
					}
					treeViewUsbItem.Children = list;
				}
				return new List<TreeViewUsbItem>(1)
				{
					treeViewUsbItem
				};
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		private static List<TreeViewUsbItem> AddHubNode(string HubPath, string HubNodeName)
		{
			UsbNodeInformation[] usbNodeInformation = USB.GetUsbNodeInformation(HubPath);
			if (usbNodeInformation != null)
			{
				TreeViewUsbItem treeViewUsbItem = new TreeViewUsbItem();
				if (string.IsNullOrEmpty(usbNodeInformation[0].Name))
				{
					treeViewUsbItem.Name = HubNodeName;
				}
				else
				{
					treeViewUsbItem.Name = usbNodeInformation[0].Name;
				}
				treeViewUsbItem.Data = usbNodeInformation[0];
				if (usbNodeInformation[0].NodeType == USB_HUB_NODE.UsbHub)
				{
					treeViewUsbItem.Children = TreeViewUsbItem.AddPortNode(HubPath, usbNodeInformation[0].NumberOfPorts);
				}
				else
				{
					treeViewUsbItem.Children = null;
				}
				return new List<TreeViewUsbItem>(1)
				{
					treeViewUsbItem
				};
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000B968 File Offset: 0x00009B68
		private static List<TreeViewUsbItem> AddPortNode(string HubPath, int NumberOfPorts)
		{
			UsbNodeConnectionInformation[] usbNodeConnectionInformation = USB.GetUsbNodeConnectionInformation(HubPath, NumberOfPorts);
			if (usbNodeConnectionInformation != null)
			{
				List<TreeViewUsbItem> list = new List<TreeViewUsbItem>(NumberOfPorts);
				foreach (UsbNodeConnectionInformation usbNodeConnectionInformation2 in usbNodeConnectionInformation)
				{
					TreeViewUsbItem treeViewUsbItem = new TreeViewUsbItem();
					treeViewUsbItem.Name = string.Concat(new object[]
					{
						"[Port",
						usbNodeConnectionInformation2.ConnectionIndex,
						"]",
						usbNodeConnectionInformation2.ConnectionStatus
					});
					treeViewUsbItem.Data = usbNodeConnectionInformation2;
					treeViewUsbItem.Children = null;
					if (usbNodeConnectionInformation2.ConnectionStatus == USB_CONNECTION_STATUS.DeviceConnected)
					{
						TreeViewUsbItem.ConnectedDevices++;
						if (!string.IsNullOrEmpty(usbNodeConnectionInformation2.DeviceDescriptor.Product))
						{
							treeViewUsbItem.Name = treeViewUsbItem.Name + ": " + usbNodeConnectionInformation2.DeviceDescriptor.Product;
						}
						if (usbNodeConnectionInformation2.DeviceIsHub)
						{
							string externalHubPath = USB.GetExternalHubPath(usbNodeConnectionInformation2.DevicePath, usbNodeConnectionInformation2.ConnectionIndex);
							UsbNodeInformation[] usbNodeInformation = USB.GetUsbNodeInformation(externalHubPath);
							if (usbNodeInformation != null)
							{
								treeViewUsbItem.Data = new ExternalHubInfo
								{
									NodeInfo = usbNodeInformation[0],
									NodeConnectionInfo = usbNodeConnectionInformation2
								};
								if (usbNodeInformation[0].NodeType == USB_HUB_NODE.UsbHub)
								{
									treeViewUsbItem.Children = TreeViewUsbItem.AddPortNode(externalHubPath, usbNodeInformation[0].NumberOfPorts);
									foreach (TreeViewUsbItem treeViewUsbItem2 in treeViewUsbItem.Children)
									{
										try
										{
											if (treeViewUsbItem2 != null && treeViewUsbItem2.Data != null)
											{
												UsbNodeConnectionInformation usbNodeConnectionInformation3 = (UsbNodeConnectionInformation)treeViewUsbItem2.Data;
												int connectionIndex = usbNodeConnectionInformation2.ConnectionIndex;
												usbNodeConnectionInformation3.ConnectionIndex = Convert.ToInt32(connectionIndex.ToString() + usbNodeConnectionInformation3.ConnectionIndex.ToString());
												treeViewUsbItem2.Data = usbNodeConnectionInformation3;
												treeViewUsbItem2.Name = string.Concat(new object[]
												{
													"[Port",
													usbNodeConnectionInformation3.ConnectionIndex,
													"]",
													usbNodeConnectionInformation2.ConnectionStatus
												});
											}
										}
										catch (Exception ex)
										{
											Log.w(ex.Message + ":" + ex.StackTrace);
										}
									}
								}
								if (string.IsNullOrEmpty(usbNodeConnectionInformation2.DeviceDescriptor.Product) && !string.IsNullOrEmpty(usbNodeInformation[0].Name))
								{
									treeViewUsbItem.Name = treeViewUsbItem.Name + ": " + usbNodeInformation[0].Name;
								}
							}
							TreeViewUsbItem.ConnectedHubs++;
						}
					}
					list.Add(treeViewUsbItem);
				}
				return list;
			}
			return null;
		}

		// Token: 0x040000A9 RID: 169
		public static int ConnectedHubs;

		// Token: 0x040000AA RID: 170
		public static int ConnectedDevices;
	}
}
