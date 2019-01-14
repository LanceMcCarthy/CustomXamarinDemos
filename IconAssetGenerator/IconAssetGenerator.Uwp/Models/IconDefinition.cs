using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using CommonHelpers.Common;

namespace IconAssetGenerator.Uwp.Models
{
    public class IconDefinition : BindableBase
    {
        private StorageFile _imageFile;
        private bool _isGenerating;

        /// <summary>
        /// Specify the platform (e.g. "iPhone: iOS 9 & 10", "iPad: iOS 7 & 8", "iPad: iOS 9 & 10", 
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// Specific what the icon will be used for (e.g. Application Icon, Spotlight, Settings)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The scale for the icon set (e.g. 3x, 2x, 1x)
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// The Icon's required Width for the Target Use
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// The Icon's required Height for the Target Use
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// When the asset is generated, this property will be updated. Useful for binding and copy/paste operations
        /// </summary>
        public StorageFile ImageFile
        {
            get => _imageFile;
            set => SetProperty(ref _imageFile, value);
        }

        /// <summary>
        /// Toggled when the image is being generated. Useful for display current status of overall generation procedure.
        /// </summary>
        public bool IsGenerating
        {
            get => _isGenerating;
            set => SetProperty(ref _isGenerating, value);
        }

        /// <summary>
        /// Generates the final icon file. 
        /// </summary>
        /// <param name="originalFile">The source image file</param>
        /// <param name="targetFolder">The target folder to save the icons in</param>
        public async Task GenerateIconAsync(StorageFile originalFile, StorageFolder targetFolder)
        {
            // File name with size first to allow easier sorting
            var fileName = $"{Width}x{Height} ({Scale}) {Category} {PlatformName}.png";

            // Create new file for resized image
            var targetFile = await targetFolder.CreateFileAsync(fileName);

            try
            {
                IsGenerating = true;

                using(var originalImageStream = await originalFile.OpenStreamForReadAsync())
                {
                    var bitmapDecoder = await BitmapDecoder.CreateAsync(originalImageStream.AsRandomAccessStream());
                    
                    // Open the target stream
                    using (var resizedStream = await targetFile.OpenStreamForWriteAsync())
                    {
                        var bitmapEncoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream.AsRandomAccessStream(), bitmapDecoder);

                        // Simple resize transform
                        // TODO Determine if another interpolation works better
                        bitmapEncoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                        bitmapEncoder.BitmapTransform.ScaledWidth = Width;
                        bitmapEncoder.BitmapTransform.ScaledHeight = Height;

                        await bitmapEncoder.FlushAsync();
                    }

                    // Set the IconDefinition's file property
                    ImageFile = targetFile;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception generating {fileName} - {ex.Message}");
            }
            finally
            {
                IsGenerating = false;
            }
        }
    }
}