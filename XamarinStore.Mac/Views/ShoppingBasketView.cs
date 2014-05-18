
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace XamarinStore.Mac
{
	public partial class ShoppingBasketView : MonoMac.AppKit.NSView
	{
		private ShoppingBasketViewController _controller;

		#region Constructors

		// Called when created from unmanaged code
		public ShoppingBasketView (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ShoppingBasketView (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		public ShoppingBasketViewController ViewController
		{
			get { return _controller; }
			set { _controller = value; }
		}

		public override void ViewWillMoveToSuperview (NSView newSuperview)
		{
			if (newSuperview != null)
			{
				base.ViewWillMoveToSuperview (newSuperview);

				if (_controller != null)
					_controller.RefreshBasket ();
			}
		}
	}
}

