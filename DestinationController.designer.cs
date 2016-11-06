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
	[Register ("DestinationController")]
	partial class DestinationController
	{
		[Outlet]
		UIKit.UITextField DestinationField { get; set; }

		[Outlet]
		MapKit.MKMapView MapView { get; set; }

		[Outlet]
		UIKit.UIButton OkButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DestinationField != null) {
				DestinationField.Dispose ();
				DestinationField = null;
			}

			if (MapView != null) {
				MapView.Dispose ();
				MapView = null;
			}

			if (OkButton != null) {
				OkButton.Dispose ();
				OkButton = null;
			}
		}
	}
}
