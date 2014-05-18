using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using MonoMac.ImageKit;

namespace XamarinStore.Mac
{
	public class ProductBrowserItem : IKImageBrowserItem
	{
		private NSUrl _url;
		private readonly Product _product;

		public ProductBrowserItem (Product product)
		{
			_url = NSUrl.FromString (product.ImageUrl);
			_product = product;

			GetUrl ();
		}

		private async void GetUrl()
		{
			var imagePath = await FileCache.Download (_product.ImageForSize (220 * NSScreen.MainScreen.BackingScaleFactor));
			_url = NSUrl.FromFilename(imagePath);
		}

		public override string ImageUID
		{
			get {
				return _url.Path;
			}
		}

		public override NSObject ImageRepresentation
		{
			get {
				return _url;
			}
		}

		public override NSString ImageRepresentationType
		{
			get {
				return IKImageBrowserItem.NSURLRepresentationType;
			}
		}

		public override string ImageTitle
		{
			get {
				return  _product.Name;
			}
		}

		public Product ImageProduct
		{
			get {
				return _product;
			}
		}
	}
}

