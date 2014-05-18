
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace XamarinStore.Mac
{
	public partial class PrefillXamarinAccountInstructionsView : MonoMac.AppKit.NSView
	{
		#region Constructors

		// Called when created from unmanaged code
		public PrefillXamarinAccountInstructionsView (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public PrefillXamarinAccountInstructionsView (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion
	}
}

