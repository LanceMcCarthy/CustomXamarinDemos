using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.XamarinForms.Input;
using Telerik.Zip;
using Xamarin.Forms;

namespace PdfViewerWithSignaturePad
{
    public static class HelperMethods
    {
        public static RadFixedDocument ImportPdfFileToFixedDocument(string pathOrResourceName, bool isEmbeddedResource = true)
        {
            Debug.WriteLine($"Preparing to load original PDF...", "File Load");

            if (isEmbeddedResource)
            {
                Debug.WriteLine($"Using embedded resource file.", "File Load");

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
                Debug.WriteLine($"Loading AppData folder PDF file.", "File Load");

                using (var stream = File.OpenRead(pathOrResourceName))
                {
                    var provider = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.PdfFormatProvider();
                    return provider.Import(stream);
                }
            }
        }

        public static async Task<byte[]> GetSignatureImageAsync(RadSignaturePad pad, Telerik.XamarinForms.Input.ImageFormat imgFormat)
        {
            Debug.WriteLine($"Getting signature Image...", "Signature Image Save");

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

                imgStream.Position = 0;

                imgBytes = imgStream.ToArray();
            }
            
            Debug.WriteLine($"Signature Image Created! In memory as a {imgFormat} byte[] - Length: {imgBytes.Length}", "SignAndSave");

            return imgBytes;
        }

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

        // UWP uses ARGB for bitmaps, for other operating systems, use the RGBA pixel format method above)
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
