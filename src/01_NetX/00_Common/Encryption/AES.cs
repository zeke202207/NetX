using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetX.Common
{
    /// <summary>
    /// 高级加密标准（英语：Advanced Encryption Standard，缩写：AES）
    /// 在密码学中又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。
    /// 这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。
    /// AES先进加密算法是一向被认为牢不可破的加密算法，针对这项加密算法的攻击是异常复杂的，
    /// 事实上想要完全破解AES花费的时间要以数十亿年计，极大的保证了数据的安全性。
    /// </summary>
    public class AES : EncryptBase, IEncryption
    {
        public string Decryption(string content)
        {
            try
            {
                var bytes = Convert.FromBase64String(content);
                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encoding.Default.GetBytes(base.Key);
                des.IV = Encoding.Default.GetBytes(base.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Encryption(string content)
        {
            var bytes = Encoding.Default.GetBytes(content);
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.Default.GetBytes(base.Key);
            des.IV = Encoding.Default.GetBytes(base.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
