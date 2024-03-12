using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Utils
{
    public class MD5Utils
    {
        public static string HashMD5Generate(string password)
        {
            MD5 md5Hash = MD5.Create();
            var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
