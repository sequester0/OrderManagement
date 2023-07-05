using System.Security.Cryptography;
using System.Text;

namespace OrderManagement.Common.Helpers
{
    public class MD5Helper
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            Console.WriteLine($"MD5 Hash: {sBuilder.ToString()}");
            return sBuilder.ToString();
        }
    }
}
