using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CoreLocation;
using System.Linq;
using System.Collections.Generic;
using Plugin.Geolocator.Abstractions;
using System.Globalization;

namespace UberBlablaCar2Go
{
	public static class Ride2GoService
	{
		private const string DirectionsApiKey = "empty";
		private const string GeoCodeApiKey = "empty";
		private const string baseAddress = "https://ride2go.herokuapp.com";

		public static async Task<Position> GetPosition (string positionName)
		{
			using (var client = new HttpClient (new NativeMessageHandler ())) 
			{
				positionName = positionName.Replace (" ", "+");
				var address = "https://maps.googleapis.com/maps/api/geocode/json?address=" +
					positionName +
					"&key=" +
					GeoCodeApiKey;
				HttpResponseMessage response = await client.GetAsync (address);
				String responseString = await response.Content.ReadAsStringAsync ();
				var root = JsonConvert.DeserializeObject<GeoCodeRootObject> (responseString);

				var result = root.results.Single ();
				var lat = result.geometry.location.lat;
				var lon = result.geometry.location.lng;

				Position pos = new Position ();
				pos.Latitude = lat;
				pos.Longitude = lon;

				return pos;
			}
		}

		public static async Task<RouteResult> GetRoute (Position start, Position destination, Position wayPoint)
		{
			using (var client = new HttpClient (new NativeMessageHandler ())) 
			{
				try 
				{
					CultureInfo gbInfo = new CultureInfo ("en-GB");
					string startLat = start.Latitude.ToString (gbInfo);
					string startLon = start.Longitude.ToString (gbInfo);

					string destinationLat = destination.Latitude.ToString (gbInfo);
					string destinationLon = destination.Longitude.ToString (gbInfo);

					string waypointString = string.Empty;
					if (wayPoint != null) {
						string waypointLat = wayPoint.Latitude.ToString (gbInfo);
						string waypointLon = wayPoint.Longitude.ToString (gbInfo);

						waypointString = "&waypoints=" + waypointLat + "," + waypointLon;
					}

					var address =
						"https://maps.googleapis.com/maps/api/directions/json?" +
						"origin=" + startLat + "," + startLon +
						"&destination=" + destinationLat + "," + destinationLon +
						waypointString +
						"&key=" + DirectionsApiKey;

					HttpResponseMessage response = await client.GetAsync (address);
					String responseString = await response.Content.ReadAsStringAsync ();
					var root = JsonConvert.DeserializeObject<RootObject> (responseString);

					List<CLLocationCoordinate2D> locations = new List<CLLocationCoordinate2D> ();
					var route = root.routes.Single ();
					foreach (var leg in route.legs) {
						foreach (var step in leg.steps) {
							locations.Add (new CLLocationCoordinate2D (step.start_location.lat, step.start_location.lng));
						}
					}

					RouteResult result = new RouteResult ();
					result.Locations = locations.ToArray ();
					result.Eta = route.legs.First ().duration.text;
					return result;
				} catch (Exception e) 
				{
					Console.WriteLine (e.Message);
					return null;
				}

			}
		}

		public static async Task<Driver> GetDriverNearby ()
		{
			var driver = new Driver ();
			driver.Name = "Hannes";
			driver.Lat = 48.815694;
			driver.Lon = 9.208309;
			return driver;
			/*
			using (var client = new HttpClient (new NativeMessageHandler ())) {
				var address = baseAddress + "/IsRideAvailable?name=marcel";
				HttpResponseMessage response = await client.GetAsync (address);
				if (response.IsSuccessStatusCode) {
					String responseString = await response.Content.ReadAsStringAsync ();
					var rideInfo = JsonConvert.DeserializeObject<RideInfo> (responseString);
					return rideInfo;
				} else {
					return null;
				}
			}*/
		}

		internal static async Task<Rider> GetRiderNearby ()
		{
			Rider rider = new Rider ();
			rider.Name = "Marcel";
			rider.Lat = 48.805499;
			rider.Lon = 9.218828;
			return rider;
		}

		internal static async Task<Driver> GetDriverUpdate (int i)
		{
			Driver driver = new Driver ();
			driver.Name = "Marcel";

			if (i == 0) {
				driver.Lat = 48.814399;
				driver.Lon = 9.209883;
			}

			if (i == 1) {
				driver.Lat = 48.816123;
				driver.Lon = 9.212855;
			}

			if (i == 2) {
				driver.Lat = 48.815459;
				driver.Lon = 9.213810;
			}

			return driver;
		}

		internal static async void MakeTheCarBlink ()
		{
			using (var client = new HttpClient (new NativeMessageHandler ())) {
				var address = "https://cvl.daimler-tss.com/hackathon/vss/vehicles/WME4533421K031586?action=signal";
				HttpContent body = new StringContent(string.Empty);
				body.Headers.ContentType = new MediaTypeHeaderValue ("application/json");
				try {
					HttpResponseMessage response = await client.GetAsync (address); //, body);
					String responseString = await response.Content.ReadAsStringAsync ();
					Console.WriteLine (responseString);
				} catch (Exception e) 
				{
					Console.WriteLine (e.Message);
				}
			}
		}

		public static async Task<bool> RequestRide (string name, double fromLat, double fromLon, string to)
		{
			using (var client = new HttpClient (new NativeMessageHandler ())) {
				var address = baseAddress + "/rider";

				var transferObject = new RiderRequestData (name, fromLat, fromLon, to);
				string transferContent = JsonConvert.SerializeObject (transferObject);
				HttpContent body = new StringContent (transferContent);

				body.Headers.ContentType = new MediaTypeHeaderValue ("application/json");
				try {
					HttpResponseMessage response = await client.PostAsync (address, body);
					String responseString = await response.Content.ReadAsStringAsync ();
					if (!response.IsSuccessStatusCode) {
						return false;
					}
					Console.WriteLine (responseString);
				} catch (Exception e) {
					Console.WriteLine ("shit" + e.Message);
					return false;
				}
				return true;
			}
		}
	}

	public class RouteResult
	{
		public string Eta { get; set; }
		public CLLocationCoordinate2D [] Locations { get; set; }
	}

	public class RiderRequestData
	{
		public string Name { get; set; }
		public double FromLat { get; set; }
		public double FromLon { get; set; }
		public string Address { get; set; }

		public RiderRequestData (string name, double fromLat, double fromLon, string address)
		{
			Address = address;
			FromLat = fromLat;
			FromLon = fromLon;
			Name = name;
		}
	}

	public class Driver
	{
		public double Lat { get; set; }
		public double Lon { get; set; }
		public string Name { get; set; }
	}

	public class Rider
	{
		public double Lat { get; set; }
		public double Lon { get; set; }
		public string Name { get; set; }
	}
}
