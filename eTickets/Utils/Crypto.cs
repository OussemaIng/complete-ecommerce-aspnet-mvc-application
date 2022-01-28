using System;
using System.Text;

namespace eTickets.Utils
{
    public static class Crypto
    {
        public static string Hash(string value)
        {
            var cryptedCode = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
            return cryptedCode;
        }
    }
}
