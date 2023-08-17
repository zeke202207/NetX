namespace NetX.Common
{
    /// <summary>
    ///  加密方式         加密向量            是否可逆    **
    ///  MD5、SHA         不需要              不可逆     **
    ///  RSA              不需要               可逆      **
    ///  AES、DES          需要                可逆      **
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string Encryption(string content);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        string Decryption(string content);
    }
}
