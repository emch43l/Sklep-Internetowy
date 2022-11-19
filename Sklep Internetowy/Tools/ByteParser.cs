using System.Text.RegularExpressions;

namespace Sklep_Internetowy.Tools
{

    public class ByteParser
    {
        public static long ToBytes(int value, Type size)
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
