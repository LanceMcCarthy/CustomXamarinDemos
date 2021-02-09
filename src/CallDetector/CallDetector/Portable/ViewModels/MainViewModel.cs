using System;
using System.Collections.ObjectModel;
using CallDetector.Portable.Common;
using CallDetector.Portable.DependencyServices;
using CallDetector.Portable.Helpers;
using CallDetector.Portable.Models;
using CommonHelpers.Common;
using Telerik.XamarinForms.DataControls.ListView;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace CallDetector.Portable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isDrawerOpen;
        private bool _isServiceRunning;
        private string _status;

        public MainViewModel()
        {
            DependencyService.Get<ICallManager>().CallStatedChanged += OnCallStatedChanged;
            
            GoToViewCommand = new Command(LoadView);
            ToggleDrawerCommand = new Command(ToggleDrawer);
            DeleteLogFileCommand = new Command<ItemTapCommandContext>(RemoveCall);

            StartStopServiceCommand = new Command(StartStopService);
            DeclineCallCommand = new Command(DeclineCall);
        }

        #region Properties

        public ObservableCollection<Caller> Calls { get; set; } = new ObservableCollection<Caller>();

        public ObservableCollection<Caller> SelectedCalls { get; set; } = new ObservableCollection<Caller>();

        public bool IsDrawerOpen
        {
            get => _isDrawerOpen;
            set => SetProperty(ref _isDrawerOpen, value);
        }

        public bool IsServiceRunning
        {
            get => _isServiceRunning;
            set => SetProperty(ref _isServiceRunning, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public SelectionMode CallListSelectionMode { get; set; }

        #endregion

        #region Commmands

        public Command GoToViewCommand { get; set; }

        public Command ToggleDrawerCommand { get; set; }

        public Command DeleteLogFileCommand { get; }
        

        public Command StartStopServiceCommand { get; }

        public Command DeclineCallCommand { get; }

        #endregion

        #region Events and Methods

        public INavigationHandler NavigationHandler { private get; set; }
        
        private async void OnCallStatedChanged(object sender, CallStateChangedEventArgs e)
        {
            if (e == null)
                return;

            if(string.IsNullOrEmpty(e.PhoneNumber))
            {
                Status = $"[{e.CallState}]";
            }
            else
            {
                Status = $"[{e.CallState}] PhoneNumber: {e.PhoneNumber}";

                var caller = new Caller(e.PhoneNumber);
                Calls.Add(caller);

                await caller.ValidateCallerAsync();
            }
        }
        
        private void LoadView(object viewType)
        {
            if ((ViewType)viewType == ViewType.CallLog)
            {
                Calls.LoadFromCache();
            }

            NavigationHandler.LoadView((ViewType)viewType);
        }

        private void ToggleDrawer()
        {
            IsDrawerOpen = !IsDrawerOpen;
        }

        private void StartStopService()
        {
            if (IsServiceRunning)
            {
                DependencyService.Get<ICallManager>().StopService();
                Status = "Service Stopped";
            }
            else
            {
                DependencyService.Get<ICallManager>().StartService();
                Status = "Service Started";
            }

            IsServiceRunning = !IsServiceRunning;
        }

        private void DeclineCall()
        {
            DependencyService.Get<ICallManager>().DeclineCall();
        }

        private async void RemoveCall(ItemTapCommandContext context)
        {
            if (CallListSelectionMode == SelectionMode.Single)
            {
                if (context?.Item is Caller call)
                {
                    var confirmed = await Application.Current.MainPage.DisplayAlert("Delete Call?", "This will permanently remove the selected call from the log.", "DELETE", "cancel");

                    if (confirmed)
                    {
                        Calls.Remove(call);
                        Calls.SaveToCache();
                    }
                }
            }
            else if(CallListSelectionMode == SelectionMode.Multiple)
            {
                var confirmed = await Application.Current.MainPage.DisplayAlert("Delete Call?", "This will permanently remove the selected call from the log.", "DELETE", "cancel");

                if (confirmed)
                {
                    foreach (var call in SelectedCalls)
                    {
                        Calls.Remove(call);
                    }

                    Calls.SaveToCache();
                }
            }
        }
        

        #endregion
    }
}
