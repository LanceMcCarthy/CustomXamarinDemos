using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using CommonHelpers.Common;
using IconAssetGenerator.Uwp.Helpers;
using IconAssetGenerator.Uwp.Models;

namespace IconAssetGenerator.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private StorageFile _sourceIconFile;
        private bool _isGenerationEnabled;
        private ObservableCollection<IconDefinition> _iconDefinitions;

        public MainViewModel()
        {
            // Apple iOS icon definitions from helper class
            foreach (var appleIconDefinition in DefinitionsHelper.AppleIconDefinitions)
            {
                IconDefinitions.Add(appleIconDefinition);
            }

            // Android icon definitions from helper class
            foreach (var androidIconDefinition in DefinitionsHelper.AndroidIconDefinitions)
            {
                IconDefinitions.Add(androidIconDefinition);
            }


            // TODO Add your Icon definitions to IconDefinitions
            //IconDefinitions.Add(new IconDefinition
            //{
            //    PlatformName = "Desktop",
            //    Category = "Taskbar",
            //    Height = 30,
            //    Width = 30
            //});
        }

        public ObservableCollection<IconDefinition> IconDefinitions
        {
            get => _iconDefinitions ?? (_iconDefinitions = new ObservableCollection<IconDefinition>());
            set => _iconDefinitions = value;
        }
        
        public StorageFile SourceIconFile
        {
            get => _sourceIconFile;
            set
            {
                SetProperty(ref _sourceIconFile, value);

                IsGenerationEnabled = _sourceIconFile != null;
            }
        }

        public bool IsGenerationEnabled
        {
            get => _isGenerationEnabled;
            set => SetProperty(ref _isGenerationEnabled, value);
        }

        public async void SelectFileButton_OnClicked(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                // Clean out any previous temp copies
                var tempFile = await ApplicationData.Current.TemporaryFolder.TryGetItemAsync(file.Name);
                if (tempFile != null)
                {
                    await tempFile.DeleteAsync();
                }

                SourceIconFile = await file.CopyAsync(ApplicationData.Current.TemporaryFolder);
            }
        }

        public async void GenerateButton_OnClicked(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                await GenerateIconsAsync(folder);
            }
        }

        public async Task GenerateIconsAsync(StorageFolder targetFolder)
        {
            if (SourceIconFile == null)
            {
                await new MessageDialog("You must pick an image file first.").ShowAsync();
                return;
            }

            try
            {
                IsBusy = true;
                
                foreach (var iconDef in IconDefinitions)
                {
                    IsBusyMessage = $"Generating {iconDef.PlatformName} icons...";

                    var filePath = await iconDef.GenerateIconAsync(SourceIconFile, targetFolder);

                    iconDef.ImagePath = filePath;
                }



                await new MessageDialog($"Your icons have been generated and saved to {targetFolder.Path}", "Complete!").ShowAsync();

                foreach (var iconDef in IconDefinitions)
                {
                    OnPropertyChanged(nameof(iconDef.ImagePath));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GenerateIconsAsync {ex.Message}");
            }
            finally
            {
                IsBusyMessage = string.Empty;
                IsBusy = false;
            }
        }
    }
}
