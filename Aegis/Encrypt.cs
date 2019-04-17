using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Aegis
{
    public class Encrypt
    {
        public string Convert(string input)
        {
            string ret = "";
            var crypt = new SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte b in crypto)
            {
                hash.Append(b.ToString("x2"));
            }
            ret = hash.ToString();
            return ret;
        }
    }
}