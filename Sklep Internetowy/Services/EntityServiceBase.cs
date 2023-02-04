using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sklep_Internetowy.Services
{
    public abstract class EntityServiceBase
    {
        private Dictionary<string,string> _errors = new Dictionary<string, string>();

        public void AddError(string key, string message) => _errors.Add(key,message);

        public Dictionary<string,string> GetErrors() => _errors;

        public int GetErrorsCount() => _errors.Count;
    }
}
