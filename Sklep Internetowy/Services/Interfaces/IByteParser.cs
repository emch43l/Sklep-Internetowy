namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IByteParser
    {
        public long ToBytes(int value, Type size);
    }
}
