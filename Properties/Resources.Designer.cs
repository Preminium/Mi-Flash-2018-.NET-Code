using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace XiaoMiFlash.Properties
{
	// Token: 0x02000020 RID: 32
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	public class Resources
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000B190 File Offset: 0x00009390
		internal Resources()
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000B198 File Offset: 0x00009398
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("XiaoMiFlash.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000B1D7 File Offset: 0x000093D7
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000B1DE File Offset: 0x000093DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000B1E6 File Offset: 0x000093E6
		public static string txtPath
		{
			get
			{
				return Resources.ResourceManager.GetString("txtPath", Resources.resourceCulture);
			}
		}

		// Token: 0x040000A2 RID: 162
		private static ResourceManager resourceMan;

		// Token: 0x040000A3 RID: 163
		private static CultureInfo resourceCulture;
	}
}
