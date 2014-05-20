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
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton btnLogin { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ivGravatar { get; set; }

		[Outlet]
		NSRotatingImageView.NSRotatingImageView ivSpinner { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField txtEmail { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSecureTextField txtPassword { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ivSpinner != null) {
				ivSpinner.Dispose ();
				ivSpinner = null;
			}

			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (ivGravatar != null) {
				ivGravatar.Dispose ();
				ivGravatar = null;
			}

			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}

			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
		}
	}

	[Register ("LoginView")]
	partial class LoginView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
