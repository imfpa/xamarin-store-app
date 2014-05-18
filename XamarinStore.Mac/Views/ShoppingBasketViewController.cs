
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;

namespace XamarinStore.Mac
{
	public partial class ShoppingBasketViewController : MonoMac.AppKit.NSViewController
	{
		public event Action CheckoutInitiated = delegate {};

		#region Constructors

		// Called when created from unmanaged code
		public ShoppingBasketViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ShoppingBasketViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ShoppingBasketViewController () : base ("ShoppingBasketView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Your Basket";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new ShoppingBasketView View
		{
			get {
				return (ShoppingBasketView)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			viewEmptyBasket.WantsLayer = true;
			viewEmptyBasket.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
			viewBasket.WantsLayer = true;
			viewBasket.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};

			// Change button text color to white and centered (lame that you can't do this in IB)
			var coloredTitle = new NSMutableAttributedString (btnCheckout.Title);
			var titleRange = new NSRange (0, coloredTitle.Length);
			coloredTitle.AddAttribute (NSAttributedString.ForegroundColorAttributeName, NSColor.White, titleRange);
			var centeredAttribute = new NSMutableParagraphStyle ();
			centeredAttribute.Alignment = NSTextAlignment.Center;
			coloredTitle.AddAttribute (NSAttributedString.ParagraphStyleAttributeName, centeredAttribute, titleRange);
			btnCheckout.AttributedTitle = coloredTitle;

			tvBasketProducts.Source = new ShoppingBasketTableViewSource ();

			btnCheckout.Activated += (object sender, EventArgs e) => CheckoutInitiated();

			View.ViewController = this;
			RefreshBasket ();
		}

		public void RefreshBasket()
		{
			if (WebService.Shared.CurrentOrder.Products.Count > 0)
			{
				viewEmptyBasket.Hidden = true;
				viewBasket.Hidden = false;
				tvBasketProducts.ReloadData ();
				UpdateTotal ();
			}
			else
			{
				viewEmptyBasket.Hidden = false;
				viewBasket.Hidden = true;
			}
		}

		private void UpdateTotal()
		{
			double total = WebService.Shared.CurrentOrder.Products.Sum (p => p.Price);
			lblBasketTotal.StringValue = "Basket Total: $" + total.ToString ("N2");
		}
	}
}

