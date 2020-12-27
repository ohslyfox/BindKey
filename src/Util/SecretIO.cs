using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BindKey.Util
{
    internal class SecretIO
    {
        private IEncryptDecrypt Crypto { get; set; }

        private static SecretIO _singletonInstance = null;
        public static SecretIO GetInstance()
        {
            if (_singletonInstance == null)
            {
                _singletonInstance = new SecretIO();
            }
            return _singletonInstance;
        }

        private SecretIO()
        {
            Crypto = new Cryptography();
        }

        public string Decrypt(string text)
        {
            return Crypto.Decrypt(text, "slyfox");
        }

        public string Encrypt(string text)
        {
            return Crypto.Encrypt(text, "slyfox");
        }

        protected interface IEncryptDecrypt
        {
            public string Encrypt(string value, string key);
            public string Decrypt(string value, string key);
        }

        private class Cryptography : IEncryptDecrypt
        {
            private static int _iterations = 2;
            private static int _keySize = 256;

            private static string _hash = "SHA1";
            private static string _salt = "gtuaxzz233ajnc6f"; // Random
            private static string _vector = "8921opbsl35khasz"; // Random

            public string Encrypt(string value, string password)
            {
                return Encrypt<AesManaged>(value, password);
            }
            public string Encrypt<T>(string value, string password)
                    where T : SymmetricAlgorithm, new()
            {
                byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);

                byte[] encrypted;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes, 0, valueBytes.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                return Convert.ToBase64String(encrypted);
            }

            public string Decrypt(string value, string password)
            {
                return Decrypt<AesManaged>(value, password);
            }
            public string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
            {
                byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = Convert.FromBase64String(value);

                byte[] decrypted;
                int decryptedByteCount = 0;

                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }
        }
    }
}
