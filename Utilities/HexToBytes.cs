using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lyc.xuming.studio.api.Utilities
{
    public static class HexToBytes
    {
        static readonly byte[] mp = new byte[128];
        static HexToBytes()
        {
            for (byte i = 00; i < 10; i++) mp['0' + i] = i;
            for (byte i = 10; i < 16; i++) mp['a' + i - 10] = i;
            for (byte i = 10; i < 16; i++) mp['A' + i - 10] = i;
        }
        public static byte[] Convert(string s)
        {
            if ((s.Length & 1) != 0) throw new ArgumentException("Hex string should have even length.");
            byte[] arr = new byte[s.Length >> 1];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (byte)((mp[s[i << 1]] << 4) + mp[s[i << 1 | 1]]);
            return arr;
        }

    }
}
