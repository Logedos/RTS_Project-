namespace Systems.SaveSystem
{
    using System;
    using System.Text;
    using System.IO;
    using System.Security.Cryptography;

    internal static class AesEncryptor
    {
        private const string _PASSWORD = "DFg%KzaUpf@k#H*FaJ8s";
        private static readonly byte[] _SALT = { 0x52, 0x41, 0x16, 0x79, 0x86, 0x64, 0x97, 0x22 };
        
        internal static string Encrypt(string input, string password = null, byte[] salt = null)
        {
            password ??= _PASSWORD;
            salt ??= _SALT;

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(input), password, salt));
        }
        
        internal static byte[] Encrypt(byte[] input, string password = null, byte[] salt = null)
        {
            password ??= _PASSWORD;
            salt ??= _SALT;

            var pdb = new Rfc2898DeriveBytes(password, salt);

            var ms = new MemoryStream();
            var aes = Aes.Create();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);

            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();

            return ms.ToArray();
        }
        
        internal static string Decrypt(string input, string password = null, byte[] salt = null)
        {
            password ??= _PASSWORD;
            salt ??= _SALT;

            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input), password, salt));
        }
        
        internal static byte[] Decrypt(byte[] input, string password = null, byte[] salt = null)
        {
            password ??= _PASSWORD;
            salt ??= _SALT;

            var pdb = new Rfc2898DeriveBytes(password, salt);
            var ms = new MemoryStream();
            var aes = Aes.Create();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}