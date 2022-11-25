using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common;

/// <summary>
/// base64加密解密
/// </summary>
public class Base64 : EncryptBase, IEncryption
{
    /// <summary>
    /// base64解密
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Decryption(string content)
    {
        var buffer = Convert.FromBase64String(content);
        return Encoding.UTF8.GetString(buffer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string Encryption(string content)
    {
        var buffer = Encoding.UTF8.GetBytes(content);
        return Convert.ToBase64String(buffer);
    }
}
