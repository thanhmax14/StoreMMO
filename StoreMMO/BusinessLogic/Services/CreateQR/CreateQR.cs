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


        public string GetQR(string text, string BackGroundColor, string qrColor1, int with, int heigh)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    // Màu nền trắng
                    Color backgroundColor = Color.White;

                    // Tạo bitmap QR
                    int moduleSize = 20; // Kích thước mỗi trái tim (tăng kích thước)
                    Bitmap qrBitmap = new Bitmap(qrCodeData.ModuleMatrix.Count * moduleSize, qrCodeData.ModuleMatrix.Count * moduleSize);

                    using (Graphics g = Graphics.FromImage(qrBitmap))
                    {
                        g.Clear(backgroundColor); // Đặt nền trắng

                        // Tạo hiệu ứng gradient từ màu hồng sang đỏ
                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            new Rectangle(0, 0, qrBitmap.Width, qrBitmap.Height),
                            Color.Pink, // Màu bắt đầu (ví dụ: hồng)
                            Color.Red, // Màu kết thúc (ví dụ: đỏ)
                            45f)) // Góc gradient
                        {
                            for (int x = 0; x < qrCodeData.ModuleMatrix.Count; x++)
                            {
                                for (int y = 0; y < qrCodeData.ModuleMatrix.Count; y++)
                                {
                                    if (qrCodeData.ModuleMatrix[x][y])
                                    {
                                        // Vẽ trái tim lớn hơn
                                        DrawHeart(g, brush, x * moduleSize, y * moduleSize, moduleSize * 1.5f, moduleSize * 1.5f); // Tăng kích thước trái tim
                                    }
                                }
                            }
                        }
                        string imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "assets", "images", "8xbet.jpg");
                        // Thêm logo vào giữa QR code
                        using (Bitmap logo = new Bitmap(imagePath)) // Thay đường dẫn logo
                        {
                      //      int logoSize = qrBitmap.Width / 6; // Kích thước logo là 16% của mã QR

                            int logoSize = qrBitmap.Width / 4; // Kích thước logo là 16% của mã QR

                            using (Graphics graphics = Graphics.FromImage(qrBitmap))
                            {
                                // Vẽ logo lên mã QR
                                graphics.DrawImage(logo, (qrBitmap.Width - logoSize) / 2, (qrBitmap.Height - logoSize) / 2, logoSize, logoSize);
                            }
                        }
                    }

                    // Đặt kích thước cho ảnh QR cuối cùng
                    int targetWidth = 10024; // Tăng chiều rộng theo pixel
                    int targetHeight = 10024; // Tăng chiều cao theo pixel

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
      
   

        // Hàm vẽ hình trái tim tại vị trí (x, y) với kích thước width, height
        private void DrawHeart(Graphics g, Brush brush, int x, int y, float width, float height)
        {
            // Định nghĩa hình trái tim với điểm mút
            PointF[] points = new PointF[]
            {
        new PointF(x + width / 2, y + height),  // Điểm đáy của trái tim
        new PointF(x + width, y + height / 3),  // Bên phải
        new PointF(x + 3 * width / 4, y),       // Đỉnh phải
        new PointF(x + width / 2, y + height / 4),  // Trung tâm trên
        new PointF(x + width / 4, y),           // Đỉnh trái
        new PointF(x, y + height / 3),          // Bên trái
            };

            // Vẽ trái tim
            g.FillPolygon(brush, points);
        }








    }
}
