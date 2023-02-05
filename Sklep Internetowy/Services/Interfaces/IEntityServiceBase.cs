using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IEntityServiceBase
    {
        public void AddError(ServiceError error);

        public List<ServiceError> GetErrors(ErrorType? type = null);

        public bool IsMajorError();

        public int GetErrorsCount();

        public void AddErrorsToModelState(ModelStateDictionary modelState);
    }
}
