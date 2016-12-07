using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication
{
    public class HMACAuthenticationHeaderGenerator
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly DateTime _date;

        public HMACAuthenticationHeaderGenerator(string publicKey = "1c0a732b1a33af32b5a32c8941354a11", string privateKey = "54788b43eda7ab2792c1fa6cf01aa7a7")
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
            _date = DateTime.UtcNow;
        }

        public IDictionary<string, string> GenerateHeaders(string url, string method, string requestBody = null)
        {
            var headers = new Dictionary<string, string>();
            var requestBodyMd5 = GenerateContentMd5(requestBody);
            var hmacString = string.Join("_", url, method.ToUpper(), _date.ToString("r"), requestBodyMd5);

            headers.Add("Authorization", string.Format("HMAC {0}:{1}", _publicKey, GenerateHash(hmacString)));
            headers.Add("Date", _date.ToString("r"));

            if (requestBody != null) headers.Add("Content-MD5", GenerateContentMd5(requestBody));

            return headers;
        }

        private static string GenerateContentMd5(string requestBody)
        {
            if (requestBody == null)
                return null;

            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));
            }
        }

        private string GenerateHash(string hmacString)
        {
            using (var hmacSha256 = new HMACSHA256(Encoding.UTF8.GetBytes(_privateKey)))
            {
                var hash = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(hmacString));
                return Convert.ToBase64String(hash);
            }
        }
    }
}