// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace UberBlablaCar2Go
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIImageView BackgroundImageView { get; set; }

		[Outlet]
		UIKit.UIButton DriveButton { get; set; }

		[Outlet]
		UIKit.UIButton RideButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DriveButton != null) {
				DriveButton.Dispose ();
				DriveButton = null;
			}

			if (RideButton != null) {
				RideButton.Dispose ();
				RideButton = null;
			}

			if (BackgroundImageView != null) {
				BackgroundImageView.Dispose ();
				BackgroundImageView = null;
			}
		}
	}
}
