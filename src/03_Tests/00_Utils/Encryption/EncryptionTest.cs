using NetX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tests._00_Utils.Encryption
{
    public class EncryptionTest
    {
        [Fact]
        public void AESTest()
        {
            string content = "zeke";
            IEncryption encryption = new AES();
            var enc = encryption.Encryption(content);
            var dec = encryption.Decryption(enc);
            Assert.Equal(content, dec);
        }

        [Fact]
        public void AESEmptyTest()
        {
            string content = "";
            IEncryption encryption = new AES();
            var enc = encryption.Encryption(content);
            var dec = encryption.Decryption(enc);
            Assert.Equal(content, dec);
        }

        [Fact]
        public void AESNotEquelUpTest()
        {
            string content = "zeke";
            string result = "Zeke";
            IEncryption encryption = new AES();
            var enc = encryption.Encryption(content);
            var dec = encryption.Decryption(enc);
            Assert.NotEqual(result, dec);
        }

        [Fact]
        public void DESTest()
        {
            string content = "111111";
            IEncryption encryption = new DES();
            var enc = encryption.Encryption(content);
            var dec = encryption.Decryption(enc);
            Assert.Equal(content, dec);
        }


        [Fact]
        public void MD5Test()
        {
            string content = "zeke"; 
            string result = "E0B5E1A3E86A399112B9EB893DAEACFD";// 32位 大写 : E0B5E1A3E86A399112B9EB893DAEACFD
            IEncryption encryption = new MD5();
            var enc = encryption.Encryption(content);
            Assert.Equal(enc, result);
        }

        [Fact]
        public void Base64Test()
        {
            string content = "zeke";
            IEncryption encryption = new Base64();
            var enc = encryption.Encryption(content);
            var dec = encryption.Decryption(enc);
            Assert.Equal(content, dec);
        }
    }
}
