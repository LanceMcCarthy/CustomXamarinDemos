using System;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;

namespace PdfViewerWithSignaturePad
{
    public class CustomJpegImageConverter : JpegImageConverterBase
    {
        public override bool TryConvertToJpegImageData(byte[] imageData, ImageQuality imageQuality, out byte[] jpegImageData)
        {
            var imageSharpImageFormats = new[] { "jpeg", "bmp", "png", "gif" };

            if (this.TryGetImageFormat(imageData, out var imageFormat) && imageSharpImageFormats.Contains(imageFormat.ToLower()))
            {
                // Install the SixLabors.ImageSharp to the class library, see https://docs.sixlabors.com/articles/imagesharp/index.html
                using (SixLabors.ImageSharp.Image imageSharp = SixLabors.ImageSharp.Image.Load(imageData))
                {
                    imageSharp.Mutate(x => x.BackgroundColor(SixLabors.ImageSharp.Color.White));

                    using (var ms = new MemoryStream())
                    {
                        SixLabors.ImageSharp.ImageExtensions.SaveAsJpeg(imageSharp, ms, new JpegEncoder
                        {
                            Quality = (int)imageQuality,
                        });

                        jpegImageData = ms.ToArray();
                    }
                }

                return true;
            }

            jpegImageData = null;

            return false;
        }
    }
}
