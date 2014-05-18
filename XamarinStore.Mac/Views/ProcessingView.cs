
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;

namespace XamarinStore.Mac
{
	public partial class ProcessingView : MonoMac.AppKit.NSView
	{
		public NSImageView Gear { get; set; }

		#region Constructors

		// Called when created from unmanaged code
		public ProcessingView (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ProcessingView (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		public override void ViewWillDraw ()
		{
			base.ViewWillDraw ();
			// Hacky: reset status image anchor point and position (it keeps changing)
			Gear.Layer.AnchorPoint = new PointF (0.5f, 0.5f);
			Gear.Layer.Position = new PointF (Gear.Frame.GetMidX (), Gear.Frame.GetMidY ());
		}
	}
}

