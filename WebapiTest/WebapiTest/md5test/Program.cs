using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace md5test
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time = DateTime.Now;
            

            MD5 md5 = new MD5CryptoServiceProvider();
            string str = "id=1";
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            string sign = byteToHexStr(bytes);
            Console.WriteLine(sign);

            Console.ReadKey();
        }

        public static string byteToHexStr(byte[] bytes)
        {
            StringBuilder strb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    strb.Append(bytes[i].ToString("x2"));
                }
            }
            return strb.ToString();
        }
    }
}
