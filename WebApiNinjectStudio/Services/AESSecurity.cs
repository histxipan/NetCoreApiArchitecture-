using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApiNinjectStudio.Services
{
    public class AESSecurity
    {

        private readonly IConfiguration configuration;
        private string secretKey;

        public AESSecurity(IConfiguration _configuration)
        {
            configuration = _configuration;
            secretKey = configuration["AppSettings:SecretKeyOfAes"];
        }
        public string AesEncrypt(string clearTxt)
        {
            //string secretKey = "I15TMSLO0KXUWTHO";

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;
                cipher.KeySize = 128;
                cipher.BlockSize = 128;
                cipher.Key = keyBytes;
                cipher.IV = keyBytes;

                byte[] valueBytes = Encoding.UTF8.GetBytes(clearTxt);

                byte[] encrypted;
                using (ICryptoTransform encryptor = cipher.CreateEncryptor())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = ms.ToArray();

                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < encrypted.Length; i++)
                                sb.Append(Convert.ToString(encrypted[i], 16).PadLeft(2, '0'));
                            return sb.ToString().ToUpperInvariant();
                        }
                    }
                }
            }
        }

        public string AesDecypt(string encrypted)
        {
            //string secretKey = "I15TMSLO0KXUWTHO";

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;
                cipher.KeySize = 128;
                cipher.BlockSize = 128;
                cipher.Key = keyBytes;
                cipher.IV = keyBytes;

                List<byte> lstBytes = new List<byte>();
                for (int i = 0; i < encrypted.Length; i += 2)
                    lstBytes.Add(Convert.ToByte(encrypted.Substring(i, 2), 16));

                using (ICryptoTransform decryptor = cipher.CreateDecryptor())
                {
                    using (MemoryStream msDecrypt = new MemoryStream(lstBytes.ToArray()))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
