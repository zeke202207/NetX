using System;
using System.Collections.Generic;
using System.Text;

namespace NetX.Common
{
    public abstract class EncryptBase
    {
        private string _key = "asdfghjklzxcvbnmqwertyui";       //加密的密钥
        private string _iv = "Aweke148";                 //加密的偏移量，提高破解难度

        /// <summary>
        /// 描  述：加密密钥
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        /// <summary>
        /// 描  述：加密偏移量
        /// </summary>
        public string IV
        {
            get { return this._iv; }
            set { this._iv = value; }
        }

        /// <summary>
        /// 描  述：字符串加密
        /// </summary>
        /// <param name="strInput">待加密字符串</param>
        /// <returns>加密串</returns>
        public virtual string EncryptStr(string strInput)
        {
            try
            {
                //实施加密串
                return Encoding.UTF8.GetString(this.Encrypt(Encoding.UTF8.GetBytes(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 描  述：字符串解密
        /// </summary>
        /// <param name="strInput">待解密字符串</param>
        /// <returns>解密串</returns>
        public virtual string DecryptStr(string strInput)
        {
            try
            {
                //实施解密串
                return Encoding.UTF8.GetString(this.Decrypt(Encoding.UTF8.GetBytes(strInput)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 描  述：文件及流加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual byte[] Encrypt(byte[] input)
        {
            try
            {
                //默认直接返回结果串
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 描  述：文件及流解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual byte[] Decrypt(byte[] input)
        {
            try
            {
                //默认直接返回结果串
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
