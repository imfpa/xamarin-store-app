
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreAnimation;

namespace XamarinStore.Mac
{
	public partial class LoginViewController : NSViewController
	{
		public event Action LoginSucceeded = delegate {};

		#region Constructors

		// Called when created from unmanaged code
		public LoginViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public LoginViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public LoginViewController () : base ("LoginView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			Title = "Log in";
			View.WantsLayer = true;
			View.Layer = new CALayer () {BackgroundColor = NSColor.White.CGColor};
		}

		#endregion

		//strongly typed view accessor
		public new LoginView View
		{
			get {
				return (LoginView)base.View;
			}
		}

		public bool ShouldShowInstructions {
			get { return string.IsNullOrEmpty (XamarinAccountEmail); }
		}

		public override void AwakeFromNib ()
		{
			ivGravatar.WantsLayer = true;
			ivGravatar.Layer.CornerRadius = ivGravatar.Frame.Width / 2;
			ivGravatar.Layer.MasksToBounds = true;
			DisplayGravatar (XamarinAccountEmail);

			// Change button text color to white and centered (lame that you can't do this in IB)
			var coloredTitle = new NSMutableAttributedString (btnLogin.Title);
			var titleRange = new NSRange (0, coloredTitle.Length);
			coloredTitle.AddAttribute (NSAttributedString.ForegroundColorAttributeName, NSColor.White, titleRange);
			var centeredAttribute = new NSMutableParagraphStyle ();
			centeredAttribute.Alignment = NSTextAlignment.Center;
			coloredTitle.AddAttribute (NSAttributedString.ParagraphStyleAttributeName, centeredAttribute, titleRange);
			btnLogin.AttributedTitle = coloredTitle;

			btnLogin.Activated += (object sender, EventArgs e) => Login();
			var txtDelegate = new CustomNSTextFieldDelegate ();
			txtDelegate.ReturnKeyPressed += () => Login();
			txtPassword.Delegate = txtDelegate;

			txtEmail.StringValue = XamarinAccountEmail;
			txtEmail.FocusRingType = NSFocusRingType.None;
			txtPassword.FocusRingType = NSFocusRingType.None;
			txtPassword.SelectText (this);
			txtPassword.BecomeFirstResponder ();
		}

		private async void DisplayGravatar (string email)
		{
			MemoryStream data;

			try {
				data = await Gravatar.GetImageData (email, (int) ivGravatar.Frame.Width * 2);
			} catch {
				return;
			}

			ivGravatar.Image = NSImage.FromStream (data);
		}

		// TODO: Enter your Xamarin account email address here
		// If you do not have a Xamarin Account please sign up here: https://store.xamarin.com/account/register
		readonly string XamarinAccountEmail = "";
		private async void Login ()
		{
			string username = txtEmail.StringValue;
			string password = txtPassword.StringValue;
			btnLogin.Hidden = true;
			ivSpinner.Hidden = false;
			ivSpinner.StartAnimation ();

			var success = await WebService.Shared.Login (username, password);
			ivSpinner.StopAnimation ();
			ivSpinner.Hidden = true;
			if (success) {
				var canContinue = await WebService.Shared.PlaceOrder (WebService.Shared.CurrentUser, true);
				if (!canContinue.Success) {
					var alert = new NSAlert ();
					alert.Window.Title = "Sorry";
					alert.MessageText = "Only one shirt per person. Edit your cart and try again.";
					alert.RunModal ();
					btnLogin.Hidden = false;
					return;
				}
			}

			if (success) {
				LoginSucceeded ();
			} else {
				var alert = new NSAlert ();
				alert.Window.Title = "Could Not Log In";
				alert.MessageText = "Please verify your Xamarin account credentials and try again";
				alert.RunModal ();

				txtPassword.SelectText (this);
				txtPassword.BecomeFirstResponder ();
				btnLogin.Hidden = false;
			}
		}
	}

	public class CustomNSTextFieldDelegate : NSTextFieldDelegate
	{
		public event Action ReturnKeyPressed = delegate {};
		public CustomNSTextFieldDelegate() {}

		public override void EditingEnded (NSNotification notification)
		{
			var userInfo = notification.UserInfo;
			NSNumber reason = (NSNumber)userInfo.ValueForKey ((NSString)"NSTextMovement");
			var code = (int)reason;
			if (code == 16) // return key
			{
				ReturnKeyPressed ();
			}
		}
	}
}

