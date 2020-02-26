using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyc.xuming.studio.api.Utilities
{
    public static class Base64Extensions
    {
        public static byte[] Base64DecodeToByteArray(this string s) => Convert.FromBase64String(s);
        public static string Base64DecodeToString(this string s, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetString(s.Base64DecodeToByteArray());
        public static string Base64Encode(this byte[] byteArray) => Convert.ToBase64String(byteArray);
    }
}
