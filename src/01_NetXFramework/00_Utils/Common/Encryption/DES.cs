using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetX.Common;

/// <summary>
/// 数据加密标准（DES，Data Encryption Standard）是一种对称加密算法，
/// 很可能是使用最广泛的密钥系统，特别是在保护金融数据的安全中，是安全性比较高的一种算法，
/// 目前只有一种方法可以破解该算法，那就是穷举法。
/// </summary>
public class DES : EncryptBase, IEncryption
{
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Decryption(string content)
    {
        try
        {
            byte[] inputArray = Convert.FromBase64String(content);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(this.Key);
            byte[] allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform cTransform = tripleDES.CreateDecryptor())
            {
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("解密失败", ex);
        }
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Encryption(string content)
    {
        byte[] inputArray = Encoding.UTF8.GetBytes(content);
        var tripleDES = TripleDES.Create();
        var byteKey = Encoding.UTF8.GetBytes(this.Key);
        byte[] allKey = new byte[24];
        Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
        Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
        tripleDES.Key = allKey;
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        using (ICryptoTransform cTransform = tripleDES.CreateEncryptor())
        {
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}
