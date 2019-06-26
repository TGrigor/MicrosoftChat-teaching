using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat.PresentationLayer.Utilities
{
    public static class HashingUtilities
    {
        public static string GetHash512(string text)
        {
            string hash;
            using (var sha512 = SHA512.Create())
            {
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            return hash;
        } 
    }
}
