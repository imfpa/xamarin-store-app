// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace XamarinStore.Mac
{
	[Register ("ProcessingViewController")]
	partial class ProcessingViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnTryAgain { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ivStatusImage { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtStatusMessage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTryAgain != null) {
				btnTryAgain.Dispose ();
				btnTryAgain = null;
			}

			if (ivStatusImage != null) {
				ivStatusImage.Dispose ();
				ivStatusImage = null;
			}

			if (txtStatusMessage != null) {
				txtStatusMessage.Dispose ();
				txtStatusMessage = null;
			}
		}
	}

	[Register ("ProcessingView")]
	partial class ProcessingView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
