using System;
using System.Collections.ObjectModel;
using System.IO;
using CallDetector.Portable.Common;
using CallDetector.Portable.DependencyServices;
using CallDetector.Portable.Helpers;
using CallDetector.Portable.Models;
using CommonHelpers.Common;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace CallDetector.Portable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isDrawerOpen;
        private bool _isServiceRunning;
        private string _status;
        private Color _serviceButtonColor;

        public MainViewModel()
        {
            DependencyService.Get<ICallManager>().CallStatedChanged += OnCallStatedChanged;
            
            GoToViewCommand = new Command(LoadView);
            ToggleDrawerCommand = new Command(ToggleDrawer);
            DeleteLogFileCommand = new Command<ItemTapCommandContext>(DeleteLogFile);

            StartStopServiceCommand = new Command(StartStopService);
            DeclineCallCommand = new Command(DeclineCall);
        }

        public ObservableCollection<LogFile> Logs { get; set; } = new ObservableCollection<LogFile>();

        public ObservableCollection<LogFile> SelectedLogs { get; set; } = new ObservableCollection<LogFile>();

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

        private void OnCallStatedChanged(object sender, CallStateChangedEventArgs e)
        {
            Status += $"CallState {e.CallState}, PhoneNumber: {e.PhoneNumber}{Environment.NewLine}";
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public Color ServiceButtonColor
        {
            get => _serviceButtonColor;
            set => SetProperty(ref _serviceButtonColor, value);
        }

        public Command GoToViewCommand { get; set; }

        public Command ToggleDrawerCommand { get; set; }

        public Command DeleteLogFileCommand { get; }
        

        public Command StartStopServiceCommand { get; }

        public Command DeclineCallCommand { get; }

        public INavigationHandler NavigationHandler { private get; set; }

        private void LoadView(object viewType)
        {
            if ((ViewType)viewType == ViewType.CallLog)
            {
                LoadFiles("*.log");
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
                ServiceButtonColor = Color.Green;

                Status += $"Service Stopped{Environment.NewLine}";
            }
            else
            {
                DependencyService.Get<ICallManager>().StartService();
                ServiceButtonColor = Color.DarkRed;

                Status += $"Service Started{Environment.NewLine}";
            }

            IsServiceRunning = !IsServiceRunning;
        }

        private void DeclineCall()
        {
            DependencyService.Get<ICallManager>().DeclineCall();
        }

        private void DeleteLogFile(ItemTapCommandContext context)
        {
            // TODO show dialog for confirmation
            if(context?.Item is LogFile logFile)
            {
                if (File.Exists(logFile.FilePath))
                {
                    File.Delete(logFile.FilePath);
                }
            }
        }

        public void LoadFiles(string fileTypeFilter)
        {
            var filePaths = FileHelpers.GetLocalFolderFilePaths(fileTypeFilter);

            Logs.Clear();

            foreach (var filePath in filePaths)
            {
                Logs.Add(new LogFile
                {
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath
                });
            }
        }
    }
}
