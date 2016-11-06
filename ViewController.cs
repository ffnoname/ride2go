using System;

using UIKit;
using System.Resources;

namespace UberBlablaCar2Go
{
	public partial class ViewController : UIViewController
	{
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.DriveButton.Layer.CornerRadius = 80;
			this.RideButton.Layer.CornerRadius = 80;
			this.BackgroundImageView.Image = UIImage.FromFile ("map2.png");
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			DestinationController target = (DestinationController)segue.DestinationViewController;
			if (segue.Identifier == "Driver") 
			{
				target.Mode = "Driver";
			} else if(segue.Identifier == "Rider")
			{
				target.Mode = "Rider";
			}
			base.PrepareForSegue (segue, sender);
		}
	}
}
