using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoMac.CoreGraphics;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace XamarinStore.Mac
{
	[Register("BadgeView")]
	class BadgeView : NSTextField
	{
		const float height = 18;

		public int BadgeNumber {
			get { return badgeNumber; }
			set {
				StringValue = (badgeNumber = value).ToString ();
				CalculateSize ();
				AlphaValue = badgeNumber > 0 ? 1 : 0;
				Hidden = badgeNumber == 0;
				SetNeedsDisplay ();
			}
		}

		SizeF numberSize;
		int badgeNumber;

		public BadgeView (IntPtr handle) : base(handle)
		{
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			Hidden = true;
			BackgroundColor = NSColor.Clear;
			TextColor = NSColor.White;
			Font = NSFont.BoldSystemFontOfSize (12f);;
			Editable = false;
			Selectable = false;
			WantsLayer = true;
			Layer.CornerRadius = height / 2;
			Layer.BackgroundColor = NSColor.Red.CGColor;
			Alignment = NSTextAlignment.Center;
		}

		void CalculateSize ()
		{
			NSString str = new NSString (badgeNumber.ToString());
			NSDictionary attibutedStringAttributed = NSDictionary.FromObjectAndKey(Font, NSAttributedString.FontAttributeName);

			numberSize = str.StringSize (attibutedStringAttributed);
			Frame = new RectangleF (Frame.Location, new SizeF (Math.Max (numberSize.Width, height), height));
		}
	}
}
