using Sklep_Internetowy.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Sklep_Internetowy.Services
{
    public class ByteParser : IByteParser
    {
        public long ToBytes(int value, Type size)
            => (long)Math.Round(value * Math.Pow(1024, (int)size));
    }

    public enum Type
    {
        B = 0,
        KB = 1,
        MB = 2,
        GB = 3
    }
}
