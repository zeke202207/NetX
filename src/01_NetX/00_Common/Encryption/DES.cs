using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetX.Common
{
    /// <summary>
    /// 数据加密标准（DES，Data Encryption Standard）是一种对称加密算法，
    /// 很可能是使用最广泛的密钥系统，特别是在保护金融数据的安全中，是安全性比较高的一种算法，
    /// 目前只有一种方法可以破解该算法，那就是穷举法。
    /// </summary>
    public class DES : EncryptBase, IEncryption
    {
        public string Decryption(string content)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(this.Key);
                byte[] IV = Encoding.UTF8.GetBytes(this.IV);
                //根据加密后的字节数组创建一个内存流
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //使用传递的私钥、IV 和内存流创建解密流
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, new TripleDESCryptoServiceProvider().CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    //创建一个字节数组保存解密后的数据
                    byte[] input = Encoding.UTF8.GetBytes(content);
                    cryptoStream.Write(input, 0, input.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Encryption(string content)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Key);
            byte[] IV = Encoding.UTF8.GetBytes(this.IV);
            //得到加密后的字节流
            //创建一个内存流
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //使用传递的私钥和IV 创建加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV), CryptoStreamMode.Write);
                //将字节数组写入加密流,并清除缓冲区
                byte[] input = Encoding.UTF8.GetBytes(content);
                cryptoStream.Write(input, 0, input.Length);
                cryptoStream.FlushFinalBlock();
                //得到加密后的字节数组
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}
