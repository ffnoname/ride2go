using System;
using System.Timers;
using Plugin.Geolocator.Abstractions;
using UIKit;

namespace UberBlablaCar2Go
{
	public partial class RiderWaitingController : UIViewController, IDestination
	{
		Position position; 
		string destination;
		public void SetDestination (Position position, string destination)
		{
			this.position = position;
			this.destination = destination;
		}

		Timer timer;
		public RiderWaitingController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Ride2GoService.RequestRide ("Marcel", this.position.Latitude, this.position.Longitude, this.destination);

			this.DestinationLabel.Text = this.destination;
			this.timer = new Timer ();
			timer.Interval = 3000;
			timer.Start ();
			timer.Elapsed += this.TimerTick;
			this.LoadingIndicator.StartAnimating ();
		}

		void TimerTick (object sender, ElapsedEventArgs e)
		{
			this.TimerHasTicked ();
		}

		Driver driver;
		private async void TimerHasTicked ()
		{
			var driver = await Ride2GoService.GetDriverNearby ();
			if (driver != null) 
			{
				this.timer.Elapsed -= TimerTick;
				this.driver = driver;
				Application.PresentOKAlert ("Driver found", "You will drive with " + driver.Name, this, () => this.PerformSegue ("DriverFoundSegue", this));
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (segue.Identifier == "DriverFoundSegue") 
			{
				var target = (RideIsComingController)segue.DestinationViewController;
				target.SetDriver (this.driver, this.position, this.destination);
			}
			base.PrepareForSegue (segue, sender);
		}
	}
}
