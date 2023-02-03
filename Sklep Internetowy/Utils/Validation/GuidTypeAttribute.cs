using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{
    public class GuidTypeAttribute : ValidationAttribute
    {
        public GuidTypeAttribute() 
        {
            ErrorMessage = "This value is not valid GUID string !";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? val = value as string;

            if (val == null)
                return ValidationResult.Success;

            if (!Guid.TryParse(val, out _))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;

        }
    }
}
