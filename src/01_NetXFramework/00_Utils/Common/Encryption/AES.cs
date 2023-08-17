using System.Security.Cryptography;
using System.Text;

namespace NetX.Common;

/// <summary>
/// 高级加密标准（英语：Advanced Encryption Standard，缩写：AES）
/// 在密码学中又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。
/// 这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。
/// AES先进加密算法是一向被认为牢不可破的加密算法，针对这项加密算法的攻击是异常复杂的，
/// 事实上想要完全破解AES花费的时间要以数十亿年计，极大的保证了数据的安全性。
/// </summary>
public class AES : EncryptBase, IEncryption
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
            var decBuffer = Convert.FromBase64String(content);
            var des = Aes.Create();
            des.Key = Encoding.Default.GetBytes(base.Key);
            //des.IV = Encoding.Default.GetBytes(base.IV);
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform cTransform = des.CreateDecryptor())
            {
                byte[] resultArray = cTransform.TransformFinalBlock(decBuffer, 0, decBuffer.Length);
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
        var encBuffer = Encoding.Default.GetBytes(content);
        var des = Aes.Create();
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.PKCS7;
        des.Key = Encoding.Default.GetBytes(base.Key);
        //des.IV = Encoding.Default.GetBytes(base.IV);
        using (ICryptoTransform cTransform = des.CreateEncryptor())
        {
            byte[] resultArray = cTransform.TransformFinalBlock(encBuffer, 0, encBuffer.Length);
            return Convert.ToBase64String(resultArray);
        }
    }
}
