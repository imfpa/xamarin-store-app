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
	[Register ("ShippingAddressViewController")]
	partial class ShippingAddressViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnPlaceOrder { get; set; }

		[Outlet]
		MonoMac.AppKit.NSComboBox cbCountry { get; set; }

		[Outlet]
		MonoMac.AppKit.NSComboBox cbState { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtAddress1 { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtAddress2 { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtCity { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtFirstName { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtLastName { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtPhoneNumber { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtPostalCode { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtFirstName != null) {
				txtFirstName.Dispose ();
				txtFirstName = null;
			}

			if (txtLastName != null) {
				txtLastName.Dispose ();
				txtLastName = null;
			}

			if (txtAddress1 != null) {
				txtAddress1.Dispose ();
				txtAddress1 = null;
			}

			if (txtAddress2 != null) {
				txtAddress2.Dispose ();
				txtAddress2 = null;
			}

			if (cbCountry != null) {
				cbCountry.Dispose ();
				cbCountry = null;
			}

			if (txtPostalCode != null) {
				txtPostalCode.Dispose ();
				txtPostalCode = null;
			}

			if (txtCity != null) {
				txtCity.Dispose ();
				txtCity = null;
			}

			if (cbState != null) {
				cbState.Dispose ();
				cbState = null;
			}

			if (btnPlaceOrder != null) {
				btnPlaceOrder.Dispose ();
				btnPlaceOrder = null;
			}

			if (txtPhoneNumber != null) {
				txtPhoneNumber.Dispose ();
				txtPhoneNumber = null;
			}
		}
	}

	[Register ("ShippingAddressView")]
	partial class ShippingAddressView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
