
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace XamarinStore.Mac
{
	public partial class ShoppingBasketTableViewCellController : MonoMac.AppKit.NSViewController
	{
		public Product Product { get; set; }

		#region Constructors

		// Called when created from unmanaged code
		public ShoppingBasketTableViewCellController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ShoppingBasketTableViewCellController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ShoppingBasketTableViewCellController () : base ("ShoppingBasketTableViewCell", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed view accessor
		public new ShoppingBasketTableViewCell View
		{
			get {
				return (ShoppingBasketTableViewCell)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			ivProductImage.Image = NSImage.ImageNamed ("shirt_image");
			lblProductName.StringValue = Product.Name;
			lblProductSize.StringValue = Product.Size.Name;
			lblProductColor.StringValue = Product.Color.Name;
			lblProductPrice.StringValue = Product.PriceDescription;
			if (Product.Price < 0.01)
				lblProductPrice.TextColor = NSColor.Blue;

			LoadImage ();
		}

		private async void LoadImage()
		{
			var path = await FileCache.Download (Product.ImageForSize (Product.Color.ImageUrls [0], ivProductImage.Frame.Width * NSScreen.MainScreen.BackingScaleFactor));
			InvokeOnMainThread (() => ivProductImage.Image = NSImage.FromStream (File.OpenRead (path)));
		}
	}
}

