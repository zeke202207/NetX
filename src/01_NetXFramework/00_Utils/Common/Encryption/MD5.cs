using System.Text;

namespace NetX.Common;

/// <summary>
/// MD5加密提供器
/// </summary>
public class MD5 : IEncryption
{
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public string Decryption(string content)
    {
        throw new NotSupportedException("MD5不支持解密操作");
    }

    /// <summary>
    /// 加密字符串
    /// 32位大写
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Encryption(string content)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
