using System;
using System.Drawing;
using System.IO;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace XamarinStore.Mac
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			FileCache.SaveLocation = Path.Combine (NSSearchPath.GetDirectories (NSSearchPathDirectory.ApplicationSupportDirectory, NSSearchPathDomain.User, true) [0], "XamarinStore.Mac");
			if (!Directory.Exists (FileCache.SaveLocation))
				Directory.CreateDirectory (FileCache.SaveLocation);

			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
	}
}

