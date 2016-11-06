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
	[Register ("RiderWaitingController")]
	partial class RiderWaitingController
	{
		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UILabel DestinationLabel { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView LoadingIndicator { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (DestinationLabel != null) {
				DestinationLabel.Dispose ();
				DestinationLabel = null;
			}

			if (LoadingIndicator != null) {
				LoadingIndicator.Dispose ();
				LoadingIndicator = null;
			}
		}
	}
}
