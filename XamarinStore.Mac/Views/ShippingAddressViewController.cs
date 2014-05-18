
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;

namespace XamarinStore.Mac
{
	public partial class ShippingAddressViewController : MonoMac.AppKit.NSViewController
	{
		public event EventHandler ShippingComplete;
		private User _user;

		#region Constructors

		// Called when created from unmanaged code
		public ShippingAddressViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ShippingAddressViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ShippingAddressViewController (User user) : base ("ShippingAddressView", NSBundle.MainBundle)
		{
			_user = user;
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Shipping Details";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new ShippingAddressView View
		{
			get {
				return (ShippingAddressView)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			// Change button text color to white and centered (lame that you can't do this in IB)
			var coloredTitle = new NSMutableAttributedString (btnPlaceOrder.Title);
			var titleRange = new NSRange (0, coloredTitle.Length);
			coloredTitle.AddAttribute (NSAttributedString.ForegroundColorAttributeName, NSColor.White, titleRange);
			var centeredAttribute = new NSMutableParagraphStyle ();
			centeredAttribute.Alignment = NSTextAlignment.Center;
			coloredTitle.AddAttribute (NSAttributedString.ParagraphStyleAttributeName, centeredAttribute, titleRange);
			btnPlaceOrder.AttributedTitle = coloredTitle;

			txtFirstName.StringValue = _user.FirstName;
			txtLastName.StringValue = _user.LastName;

			LoadCountries ();
			cbCountry.Changed += (object sender, EventArgs e) => {
				LoadStates();
			};

			txtAddress1.SelectText (this);
			txtAddress1.BecomeFirstResponder ();

			btnPlaceOrder.Activated += OnBtnPlaceOrder;
		}

		private async void LoadCountries()
		{
			var countries = await WebService.Shared.GetCountries ();
			if(countries != null && countries.Count > 0)
				cbCountry.Add (countries.Select (c => (NSString)c.Name).ToArray ());
		}

		private async void LoadStates()
		{
			var selectedCountry = cbCountry.StringValue;
			if (!string.IsNullOrEmpty (selectedCountry))
			{
				var states = await WebService.Shared.GetStates (selectedCountry);
				if (states != null && states.Count > 0)
					cbState.Add (states.ConvertAll (s => (NSString)s).ToArray ());
			}
		}

		private async void OnBtnPlaceOrder(object sender, EventArgs e)
		{
			_user.FirstName = txtFirstName.StringValue;
			_user.LastName = txtLastName.StringValue;
			_user.Address = txtAddress1.StringValue;
			_user.Address2 = txtAddress2.StringValue;
			_user.City = txtCity.StringValue;
			_user.Country = await WebService.Shared.GetCountryCode(cbCountry.StringValue);
			_user.Phone = txtPhoneNumber.StringValue;
			_user.State = cbState.StringValue;
			_user.ZipCode = txtPostalCode.StringValue;
			var isValid = await _user.IsInformationValid ();
			if (!isValid.Item1) {
				var alert = new NSAlert ();
				alert.Window.Title = "Error";
				alert.MessageText = isValid.Item2;
				alert.RunModal ();
				return;
			}
			if (ShippingComplete != null)
				ShippingComplete (this, EventArgs.Empty);
		}
	}
}

