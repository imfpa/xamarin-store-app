using System;
using System.Collections.Generic;
using System.Drawing;
using MonoMac.ImageKit;
using MonoMac.AppKit;

namespace XamarinStore.Mac
{
	public class ProductBrowserDataSource : IKImageBrowserDataSource
	{
		private List<ProductBrowserItem> _products = new List<ProductBrowserItem>();

		public ProductBrowserDataSource ()
		{
		}

		public override int ItemCount (IKImageBrowserView aBrowser)
		{
			return _products.Count;
		}

		public override IKImageBrowserItem GetItem (IKImageBrowserView aBrowser, int index)
		{
			return _products [index];
		}

		public void AddProducts(List<Product> products)
		{
			_products.AddRange (products.ConvertAll<ProductBrowserItem> (product => new ProductBrowserItem (product)));
		}
	}
}

