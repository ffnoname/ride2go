using System;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace UberBlablaCar2Go
{
	public static class LocationService
	{
		public async static Task<Position> GetLocation ()
		{
			CrossGeolocator.Current.DesiredAccuracy = 10;
			try {

				var position = await CrossGeolocator.Current.GetPositionAsync (2000);
				return position;
			}
			catch (Exception e) 
			{
				return new Position ();
			}

		}
	}
}
