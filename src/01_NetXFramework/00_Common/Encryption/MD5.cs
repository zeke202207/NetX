using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NetX.Common
{
    public class MD5 : IEncryption
    {
        public string Decryption(string content)
        {
            throw new NotSupportedException("MD5不支持解密操作");
        }

        public string Encryption(string content)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(content))).Replace("-", "").ToUpper();
        }
    }
}
