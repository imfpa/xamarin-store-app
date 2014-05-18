
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;

namespace XamarinStore.Mac
{
	public partial class PrefillXamarinAccountInstructionsViewController : MonoMac.AppKit.NSViewController
	{
		#region Constructors

		// Called when created from unmanaged code
		public PrefillXamarinAccountInstructionsViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public PrefillXamarinAccountInstructionsViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public PrefillXamarinAccountInstructionsViewController () : base ("PrefillXamarinAccountInstructionsView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Prefill Account";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new PrefillXamarinAccountInstructionsView View
		{
			get {
				return (PrefillXamarinAccountInstructionsView)base.View;
			}
		}
	}
}

