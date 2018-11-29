using System;
using CallDetector.Portable.Common;
using CallDetector.Portable.DependencyServices;
using Xamarin.Forms;

namespace CallDetector.Portable
{
    public partial class MainPage : ContentPage
    {
        private bool _isRunning;

        public MainPage()
        {
            InitializeComponent();
            DependencyService.Get<ICallManager>().CallStatedChanged += MainPage_CallStatedChanged;
        }

        private void MainPage_CallStatedChanged(object sender, CallStateChangedEventArgs e)
        {
            OutputLabel.Text += $"CallState {e.CallState}, PhoneNumber: {e.PhoneNumber}{Environment.NewLine}";
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                DependencyService.Get<ICallManager>().StopService();
                StartStopButton.BackgroundColor = Color.Green;
                DeclineCallButton.IsEnabled = false;

                OutputLabel.Text += $"Service Stopped{Environment.NewLine}";
            }
            else
            {
                DependencyService.Get<ICallManager>().StartService();
                StartStopButton.BackgroundColor = Color.DarkRed;
                DeclineCallButton.IsEnabled = true;

                OutputLabel.Text += $"Service Started{Environment.NewLine}";
            }

            _isRunning = !_isRunning;
        }

        private void DeclineCallButton_OnClicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICallManager>().DeclineCall();
        }
    }
}
