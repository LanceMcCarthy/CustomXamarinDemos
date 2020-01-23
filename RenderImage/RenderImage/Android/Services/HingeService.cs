using System;
using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.Views;
using Microsoft.Device.Display;
using RenderImage.Android.Services;
using RenderImage.Portable.Controls.HingeService;
using RenderImage.Portable.Controls.LayoutService;
using Xamarin.Forms;

[assembly: Dependency(typeof(HingeService))]
namespace RenderImage.Android.Services
{
	public class HingeService : IHingeService, IDisposable
	{
		private static ScreenHelper _helper;
        private readonly bool isDuo;
        private readonly HingeSensor hingeSensor;
        private int hingeAngle;
		private Rectangle hingeLocation;

        public static Activity MainActivity { get; set; }

        ILayoutService LayoutService => DependencyService.Get<ILayoutService>();

		public HingeService()
		{
			if (_helper == null)
            {
                _helper = new ScreenHelper();
            }

			isDuo = _helper.Initialize(MainActivity);

            if (!isDuo)
            {
                return;
            }

            hingeSensor = new HingeSensor(MainActivity);
            hingeSensor.OnSensorChanged += OnSensorChanged;
            hingeSensor.StartListening();
        }

        private void OnSensorChanged(object sender, HingeSensor.HingeSensorChangedEventArgs e)
		{
			if (hingeLocation != GetHinge())
			{
				hingeLocation = GetHinge();
				LayoutService.AddLayoutGuide("Hinge", hingeLocation);
			}

			if (hingeAngle != e.HingeAngle)
            {
                OnHingeUpdated?.Invoke(this, new HingeEventArgs(e.HingeAngle));
            }

			hingeAngle = e.HingeAngle;
		}

		public void Dispose()
		{
            if (hingeSensor == null)
            {
                return;
            }

            hingeSensor.OnSensorChanged -= OnSensorChanged;
            hingeSensor.StopListening();
        }

		public bool IsSpanned => isDuo && (_helper?.IsDualMode ?? false);

		public Rectangle GetHinge()
		{
			if (!isDuo || _helper == null)
            {
                return Rectangle.Zero;
            }

			var rotation = ScreenHelper.GetRotation(_helper.Activity);
			var hinge = _helper.DisplayMask.GetBoundingRectsForRotation(rotation).FirstOrDefault();

            if (hinge == null)
            {
                return Rectangle.Zero;
            }

			var hingeDp = new Rectangle(PixelsToDp(hinge.Left), PixelsToDp(hinge.Top), PixelsToDp(hinge.Width()), PixelsToDp(hinge.Height()));

			return hingeDp;
		}

		public bool IsLandscape
		{
			get
			{
				if (!isDuo || _helper == null)
                {
                    return false;
                }

				var rotation = ScreenHelper.GetRotation(_helper.Activity);

				return (rotation == SurfaceOrientation.Rotation270 || rotation == SurfaceOrientation.Rotation90);
			}
		}

		private static double PixelsToDp(double px) => px / MainActivity.Resources.DisplayMetrics.Density;

		public event EventHandler<HingeEventArgs> OnHingeUpdated;
		public event PropertyChangedEventHandler PropertyChanged;
	}
}