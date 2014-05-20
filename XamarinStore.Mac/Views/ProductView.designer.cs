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
	[Register ("ProductViewController")]
	partial class ProductViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnAddToBasket { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton comboColors { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton comboSizes { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ivProductImage { get; set; }

		[Outlet]
		NSRotatingImageView.NSRotatingImageView ivSpinner { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductDescription { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductName { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField lblProductPrice { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView viewLoading { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ivSpinner != null) {
				ivSpinner.Dispose ();
				ivSpinner = null;
			}

			if (btnAddToBasket != null) {
				btnAddToBasket.Dispose ();
				btnAddToBasket = null;
			}

			if (comboColors != null) {
				comboColors.Dispose ();
				comboColors = null;
			}

			if (comboSizes != null) {
				comboSizes.Dispose ();
				comboSizes = null;
			}

			if (ivProductImage != null) {
				ivProductImage.Dispose ();
				ivProductImage = null;
			}

			if (lblProductDescription != null) {
				lblProductDescription.Dispose ();
				lblProductDescription = null;
			}

			if (lblProductName != null) {
				lblProductName.Dispose ();
				lblProductName = null;
			}

			if (lblProductPrice != null) {
				lblProductPrice.Dispose ();
				lblProductPrice = null;
			}

			if (viewLoading != null) {
				viewLoading.Dispose ();
				viewLoading = null;
			}
		}
	}

	[Register ("ProductView")]
	partial class ProductView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
