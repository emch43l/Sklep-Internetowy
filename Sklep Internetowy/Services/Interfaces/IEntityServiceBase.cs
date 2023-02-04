namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IEntityServiceBase
    {
        public void AddError(string key, string message);

        public Dictionary<string, string> GetErrors();

        public int GetErrorsCount();
    }
}
