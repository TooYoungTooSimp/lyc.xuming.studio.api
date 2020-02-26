using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyc.xuming.studio.api.Utilities
{
    public static class EncodingExtensions
    {
        public static byte[] EncodeToByteArray(this string s, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetBytes(s);
        public static string DecodeToString(this byte[] byteArray, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetString(byteArray);
    }
}
