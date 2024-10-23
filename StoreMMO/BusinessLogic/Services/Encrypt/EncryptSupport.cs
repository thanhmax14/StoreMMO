using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Encrypt
{
    public class EncryptSupport
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                return JsonSerializer.SerializeToUtf8Bytes(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization failed: {ex.Message}");
                return null;
            }
        }
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            if (arrBytes == null || arrBytes.Length == 0)
            {
                return default(T);
            }
            try
            {
                return JsonSerializer.Deserialize<T>(arrBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization failed: {ex.Message}");
                return default(T);
            }
        }
		public static byte[] ObjectListToByteArray<T>(List<T> objList)
		{
			if (objList == null || objList.Count == 0)
			{
				return null;
			}
			try
			{
				// Serialize danh sách thành mảng byte
				return JsonSerializer.SerializeToUtf8Bytes(objList);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Serialization failed: {ex.Message}");
				return null;
			}
		}
		public static List<T> ByteArrayToObjectList<T>(byte[] arrBytes)
		{
			if (arrBytes == null || arrBytes.Length == 0)
			{
				return default(List<T>);
			}
			try
			{
				// Deserialize mảng byte thành danh sách object
				return JsonSerializer.Deserialize<List<T>>(arrBytes);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Deserialization failed: {ex.Message}");
				return default(List<T>);
			}
		}


		public static bool EncryptQuestions_SaveToFile(string fname, byte[] data, string key)
        {
            bool result;
            try
            {
                FileStream fileStream = new FileStream(fname, FileMode.Create, FileAccess.Write);
                CryptoStream cryptoStream = new CryptoStream(fileStream, new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key),
                    IV = Encoding.ASCII.GetBytes(key)
                }.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.Close();
                fileStream.Close();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        // Token: 0x06000004 RID: 4 RVA: 0x00002154 File Offset: 0x00001154
        public static byte[] DecryptQuestions_FromFile(string fname, string key)
        {
            byte[] result;
            try
            {
                FileStream fileStream = new FileStream(fname, FileMode.Open, FileAccess.Read);
                CryptoStream cryptoStream = new CryptoStream(fileStream, new DESCryptoServiceProvider
                {
                    Key = Encoding.ASCII.GetBytes(key),
                    IV = Encoding.ASCII.GetBytes(key)
                }.CreateDecryptor(), CryptoStreamMode.Read);
                byte[] array = new byte[fileStream.Length];
                int num = cryptoStream.Read(array, 0, (int)fileStream.Length);
                cryptoStream.Close();
                fileStream.Close();
                result = array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
     
        //key mus have 8 charactor
        public static string Encryption(byte[] data, string key)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = des.Key;
            des.Padding = PaddingMode.PKCS7;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform transform = des.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Position = 0L;
            string result = string.Empty;
            result = Convert.ToBase64String(memoryStream.ToArray());
            cryptoStream.Close();
            return result;
        }

     
        public static string Decryption(byte[] data, string key)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = des.Key;
            des.Padding = PaddingMode.PKCS7;
            MemoryStream stream = new MemoryStream(data);
            CryptoStream cryptoStream = new CryptoStream(stream, des.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] array = new byte[data.Length];
            cryptoStream.Read(array, 0, array.Length);
            cryptoStream.Close();
            return Encoding.Unicode.GetString(array);
        }

    
        public static string GetMD5(string msg)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = md.ComputeHash(Encoding.Unicode.GetBytes(msg));
            return Encoding.Unicode.GetString(bytes);
        }

        public static string EncodeBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        // Giải mã tham số
        public static string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

		public static string GenerateRandomString(int length)
		{
			const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			StringBuilder randomString = new StringBuilder();
			Random random = new Random();

			for (int i = 0; i < length; i++)
			{
				int index = random.Next(characters.Length);
				char randomChar = characters[index];
				randomString.Append(randomChar);
			}

			return randomString.ToString().ToUpper();
		}

	}
}
