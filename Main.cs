using System;
using System.Collections.Generic;
using CoreLocation;
using MapKit;
using UIKit;

namespace UberBlablaCar2Go
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string [] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}

		public static void CenterMap (MKMapView map, double lat, double lon, double radiusInKm)
		{
			var coords = new CLLocationCoordinate2D (lat, lon);
			var span = new MKCoordinateSpan (KilometerToLatitudeDegrees (radiusInKm), KilometerToLongitudeDegrees (radiusInKm, coords.Latitude));
			map.Region = new MKCoordinateRegion (coords, span);
		}

		private static double KilometerToLatitudeDegrees (double kilometer)
		{
			const double earthRadius = 6371.0;
			const double radiansToDegrees = 180.0 / Math.PI;
			return (kilometer / earthRadius) * radiansToDegrees;
		}

		private static double KilometerToLongitudeDegrees (double kilometer, double atLatitude)
		{
			const double earthRadius = 6371.0;
			const double degreesToRadians = Math.PI / 180.0;
			const double radiansToDegrees = 180.0 / Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos (atLatitude * degreesToRadians);
			return (kilometer / radiusAtLatitude) * radiansToDegrees;
		}

		public static void PresentOKAlert (string title, string description, UIViewController controller, Action okAction)
		{
			controller.InvokeOnMainThread (() => {
				// No, inform the user that they must create a home first
				UIAlertController alert = UIAlertController.Create (title, description, UIAlertControllerStyle.Alert);

				// Configure the alert
				alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, (action) => okAction ()));

				controller.PresentViewController (alert, true, null);
			});
		}

		public static CLLocationCoordinate2D [] GetSecondRoute ()
		{
			List<CLLocationCoordinate2D> points = new List<CLLocationCoordinate2D> ();

			points.Add (new CLLocationCoordinate2D (48.8143778, 9.213246700000001));
			points.Add (new CLLocationCoordinate2D (48.8122422, 9.211273));
			points.Add (new CLLocationCoordinate2D (48.8117511, 9.210163999999999));
			points.Add (new CLLocationCoordinate2D (48.8094428, 9.2097341));
			points.Add (new CLLocationCoordinate2D (48.8064134, 9.212074099999999));
			points.Add (new CLLocationCoordinate2D (48.8059265, 9.212907299999999));
			points.Add (new CLLocationCoordinate2D (48.8068592, 9.214938199999999));
			points.Add (new CLLocationCoordinate2D (48.8076951, 9.217729499999999));
			points.Add (new CLLocationCoordinate2D (48.8080468, 9.2182265));
			points.Add (new CLLocationCoordinate2D (48.8055191, 9.2189683));
			points.Add (new CLLocationCoordinate2D (48.8054716, 9.2189815));
			points.Add (new CLLocationCoordinate2D (48.80145700000001, 9.220177));
			points.Add (new CLLocationCoordinate2D (48.8020511, 9.2173435));

			return points.ToArray ();
		}
	}
}
