using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using XiaoMiFlash.code.bl;

namespace XiaoMiFlash
{
	// Token: 0x0200005C RID: 92
	[RunInstaller(true)]
	public class MiInstaller : Installer
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000150DC File Offset: 0x000132DC
		public MiInstaller()
		{
			this.InitializeComponent();
			base.BeforeInstall += this.MiInstaller_BeforeInstall;
			base.AfterInstall += this.MiInstaller_AfterInstall;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00015110 File Offset: 0x00013310
		private void MiInstaller_AfterInstall(object sender, InstallEventArgs e)
		{
			MiDriver miDriver = new MiDriver();
			miDriver.CopyFiles(base.Context.Parameters["assemblypath"]);
			miDriver.InstallAllDriver(base.Context.Parameters["assemblypath"], false);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001515C File Offset: 0x0001335C
		public override void Install(IDictionary savedState)
		{
			try
			{
				base.Install(savedState);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00015188 File Offset: 0x00013388
		private void MiInstaller_BeforeInstall(object sender, InstallEventArgs e)
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001518A File Offset: 0x0001338A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000151A9 File Offset: 0x000133A9
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x040001F6 RID: 502
		private IContainer components;
	}
}
