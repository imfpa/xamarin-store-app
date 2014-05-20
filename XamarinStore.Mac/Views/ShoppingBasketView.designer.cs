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
	[Register ("ShoppingBasketViewController")]
	partial class ShoppingBasketViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnCheckout { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblBasketTotal { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView tvBasketProducts { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView viewBasket { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView viewEmptyBasket { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCheckout != null) {
				btnCheckout.Dispose ();
				btnCheckout = null;
			}

			if (lblBasketTotal != null) {
				lblBasketTotal.Dispose ();
				lblBasketTotal = null;
			}

			if (tvBasketProducts != null) {
				tvBasketProducts.Dispose ();
				tvBasketProducts = null;
			}

			if (viewBasket != null) {
				viewBasket.Dispose ();
				viewBasket = null;
			}

			if (viewEmptyBasket != null) {
				viewEmptyBasket.Dispose ();
				viewEmptyBasket = null;
			}
		}
	}

	[Register ("ShoppingBasketView")]
	partial class ShoppingBasketView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
