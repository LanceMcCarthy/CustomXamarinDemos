using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Annotations;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;
using Telerik.XamarinForms.Input;
using Telerik.Zip;
using Xamarin.Forms;

namespace PdfViewerWithSignaturePad
{
    public static class HelperMethods
    {
        public static RadFixedDocument ImportPdfFileToFixedDocument(string pathOrResourceName, bool isEmbeddedResource = true)
        {
            if (isEmbeddedResource)
            {
                Assembly assembly = typeof(MainPage).Assembly;
                string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(pathOrResourceName));

                using (var stream = assembly.GetManifestResourceStream(fileName))
                {
                    var provider = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider();
                    return provider.Import(stream);
                }
            }
            else
            {
                using (var stream = File.OpenRead(pathOrResourceName))
                {
                    var provider = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider();
                    return provider.Import(stream);
                }
            }
        }

        public static async Task<byte[]> GetSignatureImageAsync(RadSignaturePad pad, ImageFormat imgFormat)
        {
            byte[] imgBytes = null;

            // Create a MemoryStream to load the image into
            using (var imgStream = new MemoryStream())
            {
                // Grab the user's signature image from the SignaturePad
                var saveSettings = new SaveImageSettings()
                {
                    ImageFormat = imgFormat,
                    ScaleFactor = 0.7,
                    ImageQuality = 1,
                    BackgroundColor = Color.White,
                    StrokeColor = Color.DarkBlue,
                    StrokeThickness = 5
                };

                // Save the written signature as an image
                await pad.SaveImageAsync(imgStream, saveSettings);

                if (imgStream.CanSeek)
                {
                    imgStream.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    imgStream.Position = 0;
                }

                imgBytes = imgStream.ToArray();
            }
            
            return imgBytes;
        }

        public static async Task SaveSignatureImageAsync(RadSignaturePad pad, ImageFormat imgFormat, string filePath)
        {
            // Create a MemoryStream to load the image into
            using (var imgStream = new MemoryStream())
            {
                // Grab the user's signature image from the SignaturePad
                var saveSettings = new SaveImageSettings()
                {
                    ImageFormat = imgFormat,
                    ScaleFactor = 0.7,
                    ImageQuality = 1,
                    BackgroundColor = Color.White,
                    StrokeColor = Color.DarkBlue,
                    StrokeThickness = 5
                };

                // Save the written signature as an image
                await pad.SaveImageAsync(imgStream, saveSettings);

                if (imgStream.CanSeek)
                {
                    imgStream.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    imgStream.Position = 0;
                }

                var imgBytes = imgStream.ToArray();

                File.WriteAllBytes(filePath, imgBytes);
            }
        }

        public static RadFixedDocument AddSignatureToDocument(RadFixedDocument originalDocument, byte[] originalImageBytes, ImageFormat imgFormat)
        {
            // We'll use a clone of the original document to perform editing operations with (just in case something goes wrong)
            RadFixedDocument workingDocument = originalDocument.Clone();

            var formFieldsCount = workingDocument.AcroForm.FormFields.Count;

            if (formFieldsCount == 0)
            {
                Trace.WriteLine($"OPERATION SKIPPED. No FormFields discovered within document.", "Document Processing");
                return null;
            }
            
            foreach (var formField in workingDocument.AcroForm.FormFields)
            {
                var widgetsCount = formField.Widgets.Count();
                
                foreach (var widget in formField.Widgets)
                {
                    if (widget.WidgetContentType == WidgetContentType.SignatureContent)
                    {
                        // We've discovered a SignatureWidget in the document.
                        var signatureWidget = widget;

                        // WARNING - this demo has the signature on the first page, if yours is on a different page, you'll need to update this
                        var currentPage = workingDocument.Pages.First();

                        // An easy way to edit the document is to use a FixedContentEditor. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor
                        FixedContentEditor editor = new FixedContentEditor(currentPage);

                        // Move the Editor to the same location on the page that the SignatureWidget was. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/concepts/position
                        editor.Position.Translate(signatureWidget.Rect.X, signatureWidget.Rect.Y);
                        
                        // Get the dimensions of the widget to use for the size of the image
                        var imageHeight = Convert.ToInt32(signatureWidget.Rect.Height);
                        var imageWidth = Convert.ToInt32(signatureWidget.Rect.Width);
                        
                        // Check what the image format is, we use different paths depending on PNG or JPEG to create ImageSource
                        if (imgFormat == ImageFormat.Png)
                        {
                            Trace.WriteLine($"Processing PNG. Separating RGB data and Alpha data...", "Document Processing");
                            EncodedImageData imageData;

                            // Because we're in a .NET Standard 2.0 project type, we need to manually handle the pixels in the byte[] for transparency
                            HelperMethods.GetRawDataFromRgbaSource(originalImageBytes, out var rgbData, out var alphaData);

                            // If the alpha data is null, it means there's no transparency and we can 
                            if (alphaData == null)
                            {
                                Trace.WriteLine($"RGB data length: {rgbData.Length} and NO ALPHA.", "Document Processing");

                                // No alpha channel, so we only have 6-bit chunks
                                imageData = new EncodedImageData(
                                    originalImageBytes,
                                    6,
                                    imageWidth,
                                    imageHeight,
                                    ColorSpaceNames.DeviceRgb,
                                    new[] { PdfFilterNames.FlateDecode });
                            }
                            else
                            {
                                // PNG has an alpha value, get the compressed data
                                byte[] compressedAlphaData = HelperMethods.CompressDataWithDeflate(alphaData);
                                
                                // Because we have an alpha channel, we set an 8-bit wide chunk
                                imageData = new EncodedImageData(
                                    originalImageBytes,
                                    compressedAlphaData,
                                    8,
                                    imageWidth,
                                    imageHeight,
                                    ColorSpaceNames.DeviceRgb,
                                    new[] { PdfFilterNames.FlateDecode });
                            }

                            // Draw the image on the document. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor#inserting-image
                            var imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(imageData);

                            editor.DrawImage(imageSource, imageWidth, imageHeight);
                        }
                        else if (imgFormat == ImageFormat.Jpeg)
                        {
                            // JPEG is significantly easier because there's no transparency and we don't need to separate the alpha channel

                            // Draw the image on the document. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor#inserting-image
                            var imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(new MemoryStream(originalImageBytes));

                            editor.DrawImage(imageSource, imageWidth, imageHeight);
                        }

                        // Finally, remove the SignatureWidget
                        currentPage.Annotations.Remove(signatureWidget);
                        
                        return workingDocument;
                    }
                }
            }

            Trace.WriteLine($"OPERATION SKIPPED. No SignatureField discovered within document.", "Document Processing");
            return null;
        }

        public static RadFixedDocument AddSignatureToDocument(RadFixedDocument originalDocument, string imageFilePath, ImageFormat imgFormat)
        {
            // We'll use a clone of the original document to perform editing operations with (just in case something goes wrong)
            RadFixedDocument workingDocument = originalDocument.Clone();

            var formFieldsCount = workingDocument.AcroForm.FormFields.Count;

            if (formFieldsCount == 0)
            {
                return null;
            }
            
            foreach (var formField in workingDocument.AcroForm.FormFields)
            {
                var widgetsCount = formField.Widgets.Count();
                
                foreach (var widget in formField.Widgets)
                {
                    if (widget.WidgetContentType == WidgetContentType.SignatureContent)
                    {
                        // We've discovered a SignatureWidget in the document.
                        var signatureWidget = widget;

                        // WARNING - this demo has the signature on the first page, if yours is on a different page, you'll need to update this
                        var currentPage = workingDocument.Pages.First();

                        // An easy way to edit the document is to use a FixedContentEditor. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor
                        FixedContentEditor editor = new FixedContentEditor(currentPage);
                        
                        // Move the Editor to the same location on the page that the SignatureWidget was. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/concepts/position
                        editor.Position.Translate(signatureWidget.Rect.X, signatureWidget.Rect.Y);

                        // Get the dimensions of the widget to use for the size of the image
                        var imageHeight = Convert.ToInt32(signatureWidget.Rect.Height);
                        var imageWidth = Convert.ToInt32(signatureWidget.Rect.Width);

                        // Load the image file into a byte[]
                        var fileImageBytes = File.ReadAllBytes(imageFilePath);
                        
                        // Check what the image format is, we use different paths depending on PNG or JPEG to create ImageSource
                        if (imgFormat == ImageFormat.Png)
                        {
                            Trace.WriteLine($"Processing PNG. Separating RGB data and Alpha data...", "Document Processing");
                            EncodedImageData imageData;

                            // Because we're in a .NET Standard 2.0 project type, we need to manually handle the pixels in the byte[] for transparency
                            HelperMethods.GetRawDataFromRgbaSource(fileImageBytes, out var rgbData, out var alphaData);

                            // If the alpha data is null, it means there's no transparency and we can 
                            if (alphaData == null)
                            {
                                Trace.WriteLine($"RGB data length: {rgbData.Length} and NO ALPHA.", "Document Processing");
                                imageData = new EncodedImageData(
                                    fileImageBytes,
                                    6,
                                    imageWidth,
                                    imageHeight,
                                    ColorSpaceNames.DeviceRgb,
                                    new[] { PdfFilterNames.FlateDecode });
                            }
                            else
                            {
                                // PNG has an alpha value, get the compressed data
                                byte[] compressedAlphaData = HelperMethods.CompressDataWithDeflate(alphaData);

                                imageData = new EncodedImageData(
                                    fileImageBytes,
                                    compressedAlphaData,
                                    8,
                                    imageWidth,
                                    imageHeight,
                                    ColorSpaceNames.DeviceRgb,
                                    new[] { PdfFilterNames.FlateDecode });
                            }

                            // Draw the image on the document. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor#inserting-image
                            var imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(imageData);
                            
                            editor.DrawImage(imageSource, imageWidth, imageHeight);
                        }
                        else if (imgFormat == ImageFormat.Jpeg)
                        {
                            // JPEG is significantly easier because there's no transparency and we don't need to separate the alpha channel

                            // Draw the image on the document. See https://docs.telerik.com/devtools/document-processing/libraries/radpdfprocessing/editing/fixedcontenteditor#inserting-image
                            var imageSource = new Telerik.Windows.Documents.Fixed.Model.Resources.ImageSource(new MemoryStream(fileImageBytes));

                            editor.DrawImage(imageSource, imageWidth, imageHeight);
                        }

                        // Finally, remove the SignatureWidget
                        currentPage.Annotations.Remove(signatureWidget);
                        
                        return workingDocument;
                    }
                }
            }

            Trace.WriteLine($"OPERATION SKIPPED. No SignatureField discovered within document.", "Document Processing");
            return null;
        }

        // Method for RGBA bitmaps
        public static void GetRawDataFromRgbaSource(byte[] pixels, out byte[] data, out byte[] alpha)
        {
            data = new byte[pixels.Length * 3];
            alpha = new byte[pixels.Length];
            bool shouldExportAlpha = false;

            for (int i = 0; i < pixels.Length; i++)
            {
                int pixel = pixels[i];

                // Each pixel comes with 4 bytes, read each value as an RGBA pixel value
                byte b = (byte)(pixel & 0xFF);
                byte g = (byte)((pixel >> 8) & 0xFF);
                byte r = (byte)((pixel >> 16) & 0xFF);
                byte a = (byte)((pixel >> 24) & 0xFF);

                // Writing RGB-only bytes
                data[3 * i] = r;
                data[3 * i + 1] = g;
                data[3 * i + 2] = b;

                // Writing alpha channel-only byte[]
                alpha[i] = a;

                // if the alpha value is not 255, then the pixel has transparency and we should export this value
                if (a != 255)
                {
                    shouldExportAlpha = true;
                }
            }

            if (!shouldExportAlpha)
            {
                alpha = null;
            }
        }

        // Method for ARGB bitmaps (UWP uses ARGB)
        public static void GetRawDataFromArgbSource(byte[] pixels, out byte[] data, out byte[] alpha)
        {
            // Create a 3-byte chunk byte[] for just RGB values
            data = new byte[pixels.Length * 3];

            // Create a 1-byte chunk byte[] for alpha values
            alpha = new byte[pixels.Length];

            // USe a flag to reset the alpha byte[] if we are not exporting a transparency channel
            bool shouldExportAlpha = false;

            for (int i = 0; i < pixels.Length; i++)
            {
                int pixel = pixels[i];

                // Each pixel comes with 4 bytes, read each value as an ARGB pixel value
                byte a = (byte)(pixel & 0xFF);
                byte r = (byte)((pixel >> 8) & 0xFF);
                byte g = (byte)((pixel >> 16) & 0xFF);
                byte b = (byte)((pixel >> 24) & 0xFF);

                // Writing RGB-only bytes
                data[3 * i] = r;
                data[3 * i + 1] = g;
                data[3 * i + 2] = b;

                // Writing alpha channel-only byte[]
                alpha[i] = a;

                // if the alpha value is not 255, then the pixel has transparency and we should export this value
                if (a != 255)
                {
                    shouldExportAlpha = true;
                }
            }

            // Null the output for the alpha channel to indicate there is no transparency for this pixel
            if (!shouldExportAlpha)
            {
                alpha = null;
            }
        }

        public static byte[] CompressDataWithDeflate(byte[] data)
        {
            using (var stream = new MemoryStream())
            {
                using (var compressedStream = new CompressedStream(stream, StreamOperationMode.Write, new DeflateSettings()))
                {
                    compressedStream.Write(data, 0, data.Length);
                }

                return stream.ToArray();
            }
        }
    }
}
