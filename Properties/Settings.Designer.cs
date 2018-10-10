using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XiaoMiFlash.Properties
{
	// Token: 0x02000067 RID: 103
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000163F4 File Offset: 0x000145F4
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000163FB File Offset: 0x000145FB
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0001640D File Offset: 0x0001460D
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string txtPath
		{
			get
			{
				return (string)this["txtPath"];
			}
			set
			{
				this["txtPath"] = value;
			}
		}

		// Token: 0x0400022A RID: 554
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
