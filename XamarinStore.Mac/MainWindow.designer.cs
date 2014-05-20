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
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSToolbar _toolbarMain { get; set; }

		[Outlet]
		BadgeView badgeView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButtonCell btnNavigationProducts { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButtonCell btnNavigationShoppingBag { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ivFlyingShirt { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblViewTitle { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView viewCurrent { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (badgeView != null) {
				badgeView.Dispose ();
				badgeView = null;
			}

			if (_toolbarMain != null) {
				_toolbarMain.Dispose ();
				_toolbarMain = null;
			}

			if (btnNavigationProducts != null) {
				btnNavigationProducts.Dispose ();
				btnNavigationProducts = null;
			}

			if (btnNavigationShoppingBag != null) {
				btnNavigationShoppingBag.Dispose ();
				btnNavigationShoppingBag = null;
			}

			if (ivFlyingShirt != null) {
				ivFlyingShirt.Dispose ();
				ivFlyingShirt = null;
			}

			if (lblViewTitle != null) {
				lblViewTitle.Dispose ();
				lblViewTitle = null;
			}

			if (viewCurrent != null) {
				viewCurrent.Dispose ();
				viewCurrent = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
