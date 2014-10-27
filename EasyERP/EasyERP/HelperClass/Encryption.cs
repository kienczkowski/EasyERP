using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EasyERP.HelperClass
{
    public class Encryption
    {
        public static string GetSaltedHash(string password, string userName)
        {
            var salt = System.Text.Encoding.UTF8.GetBytes(userName);
            var pass = System.Text.Encoding.UTF8.GetBytes(password);
            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(pass);
            return Convert.ToBase64String(saltedHash);
        }
    }
}