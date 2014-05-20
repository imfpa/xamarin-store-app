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
	[Register ("ProductsViewController")]
	partial class ProductsViewController
	{
		[Outlet]
		MonoMac.ImageKit.IKImageBrowserView viewImageBrowser { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewImageBrowser != null) {
				viewImageBrowser.Dispose ();
				viewImageBrowser = null;
			}
		}
	}

	[Register ("ProductsView")]
	partial class ProductsView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
