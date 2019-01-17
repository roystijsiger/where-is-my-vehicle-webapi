using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsMyVehicle.WebApi.Extensions
{
    public static class StringExtensions
    {
        public static string CalculateMd5Hash(this string input)
        {
            var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(input);

            var hash = md5.ComputeHash(inputBytes);

            // Create hash string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
