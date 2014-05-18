
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.CoreGraphics;

namespace XamarinStore.Mac
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		private ProductsViewController _productsViewController = null;
		private ShoppingBasketViewController _shoppingBasketViewController = null;
		private NSView _currentSubview = null;

		private ProductsViewController ProductsViewController
		{
			get {
				if (_productsViewController == null)
					_productsViewController = new ProductsViewController ();
				return _productsViewController;
			}
		}

		private ShoppingBasketViewController ShoppingBasketViewController
		{
			get {
				if (_shoppingBasketViewController == null)
					_shoppingBasketViewController = new ShoppingBasketViewController ();
				return _shoppingBasketViewController;
			}
		}

		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			// We don't want a title since we have the "Xamarin Store" image in the Toolbar
			Window.Title = "";
			// Make the background reeeeeal nice, Clark
			Window.BackgroundColor = NSColor.White;
			// Let's keep it simple and no zoom for now
			Window.StandardWindowButton (NSWindowButton.ZoomButton).Hidden = true;

			// Center the main window in the screen
			var windowSize = Window.Frame.Size;
			Window.SetFrame (new RectangleF (
				new PointF ((Window.Screen.Frame.Width / 2f) - (windowSize.Width / 2f), (Window.Screen.Frame.Height / 2f) - (windowSize.Height / 2f)),
				windowSize), true);
				
			// Setup events for some buttons
			btnNavigationProducts.Activated += OnNavigationButtonActivated;
			btnNavigationShoppingBag.Activated += OnNavigationButtonActivated;
			ShoppingBasketViewController.CheckoutInitiated += OnCheckoutInitiated;

			// Setup main view animation for switching views
			viewCurrent.WantsLayer = true;	// make sure we get a CoreLayer for the transition
			var viewTrans = new CATransition ();
			viewTrans.Type = CATransition.TransitionMoveIn;
			viewTrans.Subtype = CATransition.TransitionFromRight;
			viewTrans.Speed = 1.0f;	// 1.0x default speed
			viewCurrent.Animations = NSDictionary.FromObjectAndKey (viewTrans, (NSString)"subviews");

			// Setup handler for product selection
			ProductsViewController.ProductSelected += OnProductSelected;

			// Set the intial view and its size
			ProductsViewController.View.Frame = viewCurrent.Bounds;
			viewCurrent.AddSubview (ProductsViewController.View);
			lblViewTitle.StringValue = ProductsViewController.Title;
			_currentSubview = ProductsViewController.View;
		}

		private void ShowView(NSView view, string title)
		{
			// Set the current view title
			lblViewTitle.StringValue = title;

			// Resize the view to match the current view size
			view.Frame = viewCurrent.Bounds;

			// Make sure our navigation buttons are selected properly if we've programmatically changed views (instead of user interaction)
			if (view == ProductsViewController.View)
			{
				btnNavigationProducts.State = NSCellStateValue.On;
				btnNavigationShoppingBag.State = NSCellStateValue.Off;
			}
			else if (view == ShoppingBasketViewController.View)
			{
				btnNavigationShoppingBag.State = NSCellStateValue.On;
				btnNavigationProducts.State = NSCellStateValue.Off;
			}

			ivFlyingShirt.WantsLayer = true;

			// Show the view (animated)
			((NSView)viewCurrent.Animator).ReplaceSubviewWith (_currentSubview, view);
			_currentSubview = view;
		}

		private void OnNavigationButtonActivated(object sender, EventArgs e)
		{
			if (btnNavigationProducts.State == NSCellStateValue.On)
			{
				// Reset selection to none
				ProductsViewController.ResetSelection ();
				// Show Products view
				ShowView (ProductsViewController.View, ProductsViewController.Title);
			}
			else if (btnNavigationShoppingBag.State == NSCellStateValue.On)
			{
				// Show Shopping Basket view
				ShowView (ShoppingBasketViewController.View, ShoppingBasketViewController.Title);
			}
		}

		private void OnCheckoutInitiated()
		{
			var loginViewController = new LoginViewController ();
			if (loginViewController.ShouldShowInstructions)
			{
				PrefillXamarinAccountInstructionsViewController prefillController = new PrefillXamarinAccountInstructionsViewController ();
				ShowView (prefillController.View, prefillController.Title);
			}
			else
			{
				loginViewController.LoginSucceeded += ShowAddress;
				ShowView (loginViewController.View, loginViewController.Title);
			}
		}

		private void ShowAddress()
		{
			var shippingAddressViewController = new ShippingAddressViewController (WebService.Shared.CurrentUser);
			shippingAddressViewController.ShippingComplete += (object sender, EventArgs e) => ProcessOrder();
			ShowView (shippingAddressViewController.View, shippingAddressViewController.Title);
		}

		private void ProcessOrder()
		{
			var processingViewController = new ProcessingViewController (WebService.Shared.CurrentUser);
			processingViewController.OrderPlaced += (object sender, EventArgs e) => OrderCompleted();
			ShowView (processingViewController.View, processingViewController.Title);
		}

		private void OrderCompleted ()
		{
			//ShowView (ProductsViewController.View, ProductsViewController.Title);
		}

		private void OnProductSelected(Product product)
		{
			// Show product detail view with this product
			var pvc = new ProductViewController (product);
			pvc.ProductAddedToBasket += (Product productAdded, PointF startPoint) => {
				// Fly the shirt from "Add" button to the basket icon in navigation
				FlyTheShirt(startPoint);

				// Add the product to the current order
				WebService.Shared.CurrentOrder.Add (productAdded);
			};
			ShowView (pvc.View, pvc.Title);
		}

		private void FlyTheShirt(PointF startPoint)
		{
			// Reset starting point of the ImageView
			ivFlyingShirt.Frame = new RectangleF(startPoint.X, startPoint.Y, ivFlyingShirt.Frame.Width, ivFlyingShirt.Frame.Height);

			NSMatrix matrix = (NSMatrix)btnNavigationProducts.ControlView;
			int lastRowIndex = matrix.Rows - 1;
			// Assuming the shopping basket button is always the last in the list...
			var btnFrame = matrix.CellFrameAtRowColumn (lastRowIndex, 0);
			var btnCenter = new PointF (btnFrame.Left + (btnFrame.Width / 2), btnFrame.Top + (btnFrame.Height / 2));
			var btnCenterCompensated = new PointF (btnCenter.X - (ivFlyingShirt.Frame.Width / 2), btnCenter.Y + (ivFlyingShirt.Frame.Height / 2));
			var endPoint = matrix.ConvertPointToView (btnCenterCompensated, matrix.Superview);

			// Create quadratic curve path
			var path = new CGPath ();
			path.MoveToPoint (startPoint);
			// Use the view title label as the control point for the curve
			var controlPoint = lblViewTitle.Superview.ConvertPointToView(new PointF(lblViewTitle.Frame.Left + (lblViewTitle.Frame.Width/3), lblViewTitle.Frame.Bottom), null);
			path.AddQuadCurveToPoint (controlPoint.X, controlPoint.Y, endPoint.X, endPoint.Y);

			// Create animation based on the path and add it to the image view
			var pathAnimation = CAKeyFrameAnimation.GetFromKeyPath ("frameOrigin");
			pathAnimation.Path = path;
			pathAnimation.RepeatCount = 0f;
			pathAnimation.Duration = 0.7f;
			pathAnimation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
			pathAnimation.AnimationStarted += delegate {
				// Show the ImageView
				ivFlyingShirt.Hidden = false;
			};
			pathAnimation.AnimationStopped += delegate {
				// Hide the ImageView
				ivFlyingShirt.Hidden = true;
				badgeView.BadgeNumber = WebService.Shared.CurrentOrder.Products.Count;
			};

			ivFlyingShirt.Animations = NSDictionary.FromObjectAndKey (pathAnimation, (NSString)"frameOrigin");

			// Change the frame location via animation
			((NSImageView) ivFlyingShirt.Animator).Frame = new RectangleF(endPoint, ivFlyingShirt.Frame.Size);
		}
	}
}

