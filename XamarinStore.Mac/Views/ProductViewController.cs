
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;

namespace XamarinStore.Mac
{
	public partial class ProductViewController : MonoMac.AppKit.NSViewController
	{
		private Product _product;
		public event Action<Product, PointF> ProductAddedToBasket = delegate {};

		#region Constructors

		// Called when created from unmanaged code
		public ProductViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ProductViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ProductViewController (Product product) : base ("ProductView", NSBundle.MainBundle)
		{
			_product = product;
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Product";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new ProductView View
		{
			get {
				return (ProductView)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			lblProductName.StringValue = _product.Name;
			lblProductDescription.StringValue = _product.Description;
			btnAddToBasket.Activated += (object sender, EventArgs e) => {
				_product.Color = _product.Colors[comboColors.IndexOfSelectedItem];
				_product.Size = _product.Sizes[comboSizes.IndexOfSelectedItem];

				// Calculate midpoint of button to use for start of "move to cart" animation
				var buttonMidpoint = new PointF(btnAddToBasket.Frame.Left + (btnAddToBasket.Frame.Width/2), btnAddToBasket.Frame.Top + (btnAddToBasket.Frame.Height/2));
				var buttonMidpointWindow = btnAddToBasket.Superview.ConvertPointToView (buttonMidpoint, btnAddToBasket.Superview.Superview.Superview);
				ProductAddedToBasket (_product, buttonMidpointWindow);
			};
			lblProductPrice.StringValue = _product.PriceDescription;
			if (_product.Price < 0.01)
				lblProductPrice.TextColor = NSColor.Blue;

			comboSizes.AddItems (_product.Sizes.Select (s => s.Name).ToArray());
			comboColors.AddItems (_product.Colors.Select (c => c.Name).ToArray());
			comboColors.Activated += (sender, e) => {
				LoadImage ();
			};
			LoadImage ();
		}

		private async void LoadImage()
		{
			// Using a custom animated spinner because NSProgressIndicator keeps crashing randomly
			ivSpinner.Hidden = false;
			ivSpinner.StartAnimation ();

			var path = string.Empty;

			if (comboColors.ItemCount > 0 &&
				_product.Colors [comboColors.IndexOfSelectedItem].ImageUrls != null &&
				!string.IsNullOrEmpty(_product.Colors [comboColors.IndexOfSelectedItem].ImageUrls[0]))
			{
				// Have a selected color and color image URL, so let's load that one
				var colorImageUrl = _product.Colors [comboColors.IndexOfSelectedItem].ImageUrls [0];
				path = await FileCache.Download(Product.ImageForSize(colorImageUrl, ivProductImage.Frame.Width * NSScreen.MainScreen.BackingScaleFactor));
			}
			else
			{
				// Fall back to whatever image it gives us
				path = await FileCache.Download (_product.ImageForSize (ivProductImage.Frame.Width * NSScreen.MainScreen.BackingScaleFactor));
			}
			// Just in case, make sure we're on the main UI thread for UI updates
			InvokeOnMainThread (() => {
				if(!string.IsNullOrEmpty(path))
					ivProductImage.Image = NSImage.FromStream (File.OpenRead (path));

				ivSpinner.StopAnimation ();
				ivSpinner.Hidden = true;
			});
		}
	}
}

