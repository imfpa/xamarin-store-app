using System;
using System.Collections.Generic;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ImageKit;

namespace XamarinStore.Mac
{
	public partial class ProductsViewController : NSViewController
	{
		public event Action<Product> ProductSelected = delegate {};

		#region Constructors

		// Called when created from unmanaged code
		public ProductsViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ProductsViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ProductsViewController () : base ("ProductsView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Products";
		}

		#endregion

		//strongly typed view accessor
		public new ProductsView View
		{
			get {
				return (ProductsView)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			viewImageBrowser.CellSize = new SizeF (220, 220);
			viewImageBrowser.SelectionDidChange += ImageBrowserSelectionDidChange;

			GetData ();
		}

		private async void GetData ()
		{
			var imagesDataSource = new ProductBrowserDataSource ();
			var products = await WebService.Shared.GetProducts ();
			//Kicking off a task no need to await
			#pragma warning disable 4014
			WebService.Shared.PreloadImages (viewImageBrowser.CellSize.Width * NSScreen.MainScreen.BackingScaleFactor);
			#pragma warning restore 4014
			imagesDataSource.AddProducts (products);

			viewImageBrowser.DataSource = imagesDataSource;
			viewImageBrowser.ReloadData ();
		}

		public void ResetSelection()
		{
			// Pass an empty index set to deselect all items
			viewImageBrowser.SelectItemsAt (new NSIndexSet (), false);
		}

		private void ImageBrowserSelectionDidChange(object sender, EventArgs e)
		{
			var selectedIndices = viewImageBrowser.SelectionIndexes;
			if (selectedIndices != null && selectedIndices.Count == 1)
			{
				var selectedProduct = ((ProductBrowserItem)viewImageBrowser.GetCellAt ((int)selectedIndices.FirstIndex).RepresentedItem).ImageProduct;
				ProductSelected (selectedProduct);
			}
		}
	}
}

