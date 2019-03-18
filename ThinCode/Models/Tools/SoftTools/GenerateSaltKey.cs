using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ThinCode.Models.Tools
{
    public static class GenerateSaltKey
    {
        private static int saltLengthLimit = 32;
        public static int _SaltLengthLimit { get => saltLengthLimit; set => saltLengthLimit = value; }

 
        public static byte[] GenerateSALT(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];

            //Require NameSpace: using System.Security.Cryptography;
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}