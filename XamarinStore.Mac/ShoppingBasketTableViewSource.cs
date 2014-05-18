using System;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace XamarinStore.Mac
{
	[Register ("ShoppingBasketTableViewSource")]
	public class ShoppingBasketTableViewSource : NSTableViewSource
	{
		public ShoppingBasketTableViewSource ()
		{
		}

		public override int GetRowCount (NSTableView tableView)
		{
			return WebService.Shared.CurrentOrder.Products.Count;
		}

		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, int row)
		{
			var product = WebService.Shared.CurrentOrder.Products [row];
			ShoppingBasketTableViewCellController controller = new ShoppingBasketTableViewCellController ();
			controller.Product = product;
			return (ShoppingBasketTableViewCell)controller.View;
		}
	}
}
