using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NetX.Common
{
    /// <summary>
    ///  RSA是第一个既能用于数据加密也能用于数字签名的算法。
    ///  它易于理解和操作，也很流行。
    ///  算法的名字以发明者的名字命名：Ron Rivest, Adi Shamir 和Leonard Adleman。
    ///  但RSA的安全性一直未能得到理论上的证明。
    ///  它经历了各种攻击，至今未被完全攻破。今天只有短的RSA钥匙才可能被强力方式解破。
    ///  到2008年为止，世界上还没有任何可靠的攻击RSA算法的方式。
    ///  只要其钥匙的长度足够长，用RSA加密的信息实际上是不能被解破的。
    ///  但在分布式计算和量子计算机理论日趋成熟的今天，RSA加密安全性受到了挑战。
    /// </summary>
    public class RSAcs : IEncryption
    {
        public string Decryption(string content)
        {
            try
            {
                var bytes = Convert.FromBase64String(content);
                var DecryptBytes = new RSACryptoServiceProvider(new CspParameters()).Decrypt(bytes, false);
                return Encoding.Default.GetString(DecryptBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Encryption(string content)
        {
            var bytes = Encoding.Default.GetBytes(content);
            var encryptBytes = new RSACryptoServiceProvider(new CspParameters()).Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }
    }
}
