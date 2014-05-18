
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.ObjCRuntime;
using MonoMac.CoreFoundation;

namespace XamarinStore.Mac
{
	public partial class ProcessingViewController : MonoMac.AppKit.NSViewController
	{
		private User user;
		public event EventHandler OrderPlaced;
		private CABasicAnimation _rotationAnimation;
		private NSDictionary _RotationAnimations;
		private static NSDictionary _NoAnimations = new NSDictionary();

		#region Constructors

		// Called when created from unmanaged code
		public ProcessingViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ProcessingViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ProcessingViewController (User user) : base ("ProcessingView", NSBundle.MainBundle)
		{
			this.user = user;
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Processing";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new ProcessingView View
		{
			get {
				return (ProcessingView)base.View;
			}
		}

		public override void AwakeFromNib ()
		{
			ivStatusImage.WantsLayer = true;
			View.Gear = ivStatusImage;

			btnTryAgain.Hidden = true;
			btnTryAgain.Activated += (object sender, EventArgs e) => ProcessOrder();

			// Change button text color to white and centered (lame that you can't do this in IB)
			var coloredTitle = new NSMutableAttributedString (btnTryAgain.Title);
			var titleRange = new NSRange (0, coloredTitle.Length);
			coloredTitle.AddAttribute (NSAttributedString.ForegroundColorAttributeName, NSColor.White, titleRange);
			var centeredAttribute = new NSMutableParagraphStyle ();
			centeredAttribute.Alignment = NSTextAlignment.Center;
			coloredTitle.AddAttribute (NSAttributedString.ParagraphStyleAttributeName, centeredAttribute, titleRange);
			btnTryAgain.AttributedTitle = coloredTitle;

			ProcessOrder ();
		}

		private async void ProcessOrder ()
		{
			ivStatusImage.Image = NSImage.ImageNamed ("gear");
			btnTryAgain.Hidden = true;
			txtStatusMessage.StringValue = "Processing...";
			StartAnimation ();
			var result = await WebService.Shared.PlaceOrder (user);
			txtStatusMessage.StringValue = result.Success ? "Your order has been placed!" : result.Message;
			StopAnimation ();
			if (result.Success)
			{
				ShowSuccess ();
				if (OrderPlaced != null)
					OrderPlaced (this, EventArgs.Empty);
			}
			else
			{
				ShowTryAgain ();
			}
		}

		private void StartAnimation()
		{
			ivStatusImage.Layer.AddAnimation (GetRotationAnimationTransformRotationZ (), "transform.rotation.z");
		}

		private void StopAnimation()
		{
			ivStatusImage.Layer.RemoveAllAnimations ();
		}

		private CABasicAnimation GetRotationAnimationTransformRotationZ()
		{
			if (_rotationAnimation == null)
			{
				_rotationAnimation = CABasicAnimation.FromKeyPath ("transform.rotation.z");
				_rotationAnimation.To = NSNumber.FromDouble (-Math.PI * 2d);
				_rotationAnimation.Duration = 2f;
				_rotationAnimation.RepeatCount = float.PositiveInfinity;
				_rotationAnimation.Cumulative = true;
				_rotationAnimation.FillMode = CAFillMode.Forwards;
				_rotationAnimation.RemovedOnCompletion = false;
			}
			return _rotationAnimation;
		}

		private void ShowSuccess()
		{
			ivStatusImage.Image = NSImage.ImageNamed ("success");
		}

		private void ShowTryAgain()
		{
			ivStatusImage.Image = NSImage.ImageNamed ("warning");
			btnTryAgain.Hidden = false;
		}
	}
}