using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLogic.Services.CreateQR
{
    public class CreateQR
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateQR(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public string GetQR(string text, string BackGroundColor, string qrColor1, int with, int heigh)
        {
            // Tạo QR Code
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    
                    Color qrColor = ColorTranslator.FromHtml(qrColor1); // Màu mã QR
                    Color backgroundColor = ColorTranslator.FromHtml(BackGroundColor); // Màu nền

                   
                    int moduleSize = 10; 
                    Bitmap qrBitmap = qrCode.GetGraphic(moduleSize, qrColor, backgroundColor, true);

                    string imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "assets", "images", "8xbet.jpg");
                    using (Bitmap logo = new Bitmap(imagePath))
                    {
                        int logoSize = qrBitmap.Width / 3;
                        using (Graphics graphics = Graphics.FromImage(qrBitmap))
                        {
                            // Vẽ logo lên mã QR
                            graphics.DrawImage(logo, (qrBitmap.Width - logoSize) / 2, (qrBitmap.Height - logoSize) / 2, logoSize, logoSize);
                        }
                    }

                    // Đặt kích thước theo pixel cho ảnh QR cuối cùng
                    int targetWidth = 300; // Đặt chiều rộng theo pixel
                    int targetHeight = 300; // Đặt chiều cao theo pixel

                    using (Bitmap resizedQrBitmap = new Bitmap(targetWidth, targetHeight))
                    {
                        using (Graphics g = Graphics.FromImage(resizedQrBitmap))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(qrBitmap, 0, 0, targetWidth, targetHeight); // Vẽ mã QR vào bitmap mới
                        }

                        // Chuyển đổi Bitmap thành mảng byte
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
