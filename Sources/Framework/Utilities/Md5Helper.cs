namespace Pingvinius.Framework.Utilities
{
    using System.Security.Cryptography;
    using System.Text;

    public static class Md5Helper
    {
        public static string Calculate(string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);

            using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())
            {
                bytes = md5Provider.ComputeHash(bytes);
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}