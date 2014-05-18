using System;
using System.Drawing;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;
using MonoMac.Foundation;
using MonoMac.CoreGraphics;

namespace NSRotatingImageView
{
	[Register ("NSRotatingImageView")]
	public class NSRotatingImageView : NSImageView
	{
		private CABasicAnimation _rotationAnimation;
		public bool IsAnimating { get; private set; }

		public NSRotatingImageView ()
		{
			Initialize ();
		}

		// Called when created from unmanaged code
		public NSRotatingImageView(IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		private void Initialize()
		{
			WantsLayer = true;
			IsAnimating = false;
		}

		public override void ViewDidMoveToSuperview ()
		{
			base.ViewDidMoveToSuperview ();
			// Ensure the rotation is around the center point
			Layer.AnchorPoint = new PointF (0.5f, 0.5f);
		}

		public override void ViewWillDraw ()
		{
			base.ViewWillDraw ();
			// Ensure the rotation is around the center point
			Layer.AnchorPoint = new PointF (0.5f, 0.5f);
			Layer.Position = new PointF (Frame.GetMidX (), Frame.GetMidY ());
		}

		public void StartAnimation()
		{
			Layer.AddAnimation (GetRotationAnimation (), "transform.rotation.z");
			IsAnimating = !IsAnimating;
		}

		public void StopAnimation()
		{
			Layer.RemoveAnimation("transform.rotation.z");
			IsAnimating = !IsAnimating;
		}

		private CABasicAnimation GetRotationAnimation()
		{
			if (_rotationAnimation == null)
			{
				_rotationAnimation = CABasicAnimation.FromKeyPath ("transform.rotation.z");
				_rotationAnimation.To = NSNumber.FromDouble (-Math.PI * 2d);
				_rotationAnimation.Duration = 1f;
				_rotationAnimation.RepeatCount = float.PositiveInfinity;
				_rotationAnimation.Cumulative = true;
				_rotationAnimation.FillMode = CAFillMode.Forwards;
				_rotationAnimation.RemovedOnCompletion = false;
			}
			return _rotationAnimation;
		}
	}
}

