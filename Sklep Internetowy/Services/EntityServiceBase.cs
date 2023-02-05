using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Sklep_Internetowy.Services
{
    public enum ErrorType
    {
        Minor,
        Major
    }

    public record ServiceError(ErrorType type, string key, string message);

    public abstract class EntityServiceBase
    {
        private List<ServiceError> _errors = new List<ServiceError>();

        public void AddError(ServiceError error) 
            => _errors.Add(error);

        public List<ServiceError> GetErrors(ErrorType? type = null) 
            => (type == null) ? _errors : _errors.Where(c => c.type == type).ToList();

        public int GetErrorsCount() 
            => _errors.Count;

        public bool IsMajorError() 
            => _errors.Where(c => c.type == ErrorType.Major).FirstOrDefault() != null;

        public void AddErrorsToModelState(ModelStateDictionary modelState)
            => _errors.ForEach(e => modelState.AddModelError(e.key, e.message));

    }
}
