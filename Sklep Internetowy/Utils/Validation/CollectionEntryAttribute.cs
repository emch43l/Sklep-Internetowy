using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{
    public class CollectionEntryAttribute : ValidationAttribute
    {
        private readonly ValidationAttribute[] _attributes;
        public CollectionEntryAttribute(params ValidationAttribute[] attributes) 
        {
            _attributes = attributes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ICollection<object>? values = value as ICollection<object>;

            if (values == null)
                return ValidationResult.Success;

            foreach(object val in values)
            {
                foreach(ValidationAttribute attr in _attributes)
                {
                    ValidationResult? validationResult = attr.GetValidationResult(val, validationContext);
                    if (validationResult != ValidationResult.Success)
                        return validationResult;
                }
            }

            return ValidationResult.Success;
        }
    }
}
