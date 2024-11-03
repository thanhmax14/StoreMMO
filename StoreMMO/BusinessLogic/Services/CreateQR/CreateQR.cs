using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Drawing2D;

namespace BusinessLogic.Services.CreateQR
{
    public class CreateQR
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateQR(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


		public string GetQR(string text)
		{
			using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
			{
				QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
				using (QRCode qrCode = new QRCode(qrCodeData))
				{
					// Create a bitmap for the QR code
					int moduleSize = 10; // Size of each module (smaller for lighter QR)
					Bitmap qrBitmap = new Bitmap(qrCodeData.ModuleMatrix.Count * moduleSize, qrCodeData.ModuleMatrix.Count * moduleSize);

					using (Graphics g = Graphics.FromImage(qrBitmap))
					{
						g.Clear(Color.White); // Set the background color to white

						for (int x = 0; x < qrCodeData.ModuleMatrix.Count; x++)
						{
							for (int y = 0; y < qrCodeData.ModuleMatrix.Count; y++)
							{
								if (qrCodeData.ModuleMatrix[x][y])
								{
									// Fill the modules (black squares of the QR code)
									g.FillRectangle(Brushes.Black, x * moduleSize, y * moduleSize, moduleSize, moduleSize);
								}
							}
						}
					}

					// Resize the QR code to a smaller target size if needed
					int targetWidth = 300; // Target width in pixels
					int targetHeight = 300; // Target height in pixels

					using (Bitmap resizedQrBitmap = new Bitmap(targetWidth, targetHeight))
					{
						using (Graphics g = Graphics.FromImage(resizedQrBitmap))
						{
							g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
							g.DrawImage(qrBitmap, 0, 0, targetWidth, targetHeight);
						}

						// Convert Bitmap to byte array
						using (MemoryStream memoryStream = new MemoryStream())
						{
							resizedQrBitmap.Save(memoryStream, ImageFormat.Png);
							byte[] bitmapArray = memoryStream.ToArray();
							string qrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitmapArray));
							return qrUri;
						}
					}
				}
			}
		}







	}
}
