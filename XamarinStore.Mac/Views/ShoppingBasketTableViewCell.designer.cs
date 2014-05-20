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
	[Register ("ShoppingBasketTableViewCellController")]
	partial class ShoppingBasketTableViewCellController
	{
		[Outlet]
		MonoMac.AppKit.NSImageView ivProductImage { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductColor { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductName { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductPrice { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductSize { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ivProductImage != null) {
				ivProductImage.Dispose ();
				ivProductImage = null;
			}

			if (lblProductName != null) {
				lblProductName.Dispose ();
				lblProductName = null;
			}

			if (lblProductSize != null) {
				lblProductSize.Dispose ();
				lblProductSize = null;
			}

			if (lblProductColor != null) {
				lblProductColor.Dispose ();
				lblProductColor = null;
			}

			if (lblProductPrice != null) {
				lblProductPrice.Dispose ();
				lblProductPrice = null;
			}
		}
	}

	[Register ("ShoppingBasketTableViewCell")]
	partial class ShoppingBasketTableViewCell
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
