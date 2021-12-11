using System.Security.Cryptography;
using System.Text;

namespace ZupTeste.Core.Utils
{
    public static class PasswordUtil
    {
        public static string EncryptNewPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            var salt = GenerateRandom();
            return Encrypt(password + salt) + ";" + salt;
        }

        public static string Encrypt(string password)
        {
            var crypt = SHA256.Create();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool Compare(string encryptedPassword, string password)
        {
            var semiColonIndex = encryptedPassword.LastIndexOf(';');

            if (semiColonIndex == 0)
                return false;

            var salt = encryptedPassword.Substring(semiColonIndex + 1);
            if (string.IsNullOrEmpty(salt))
                return false;

            return encryptedPassword == Encrypt(password + salt) + ";" + salt;
        }

        public static string GenerateRandom(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            var chars = new char[length];
            for (var i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }
    }
}
