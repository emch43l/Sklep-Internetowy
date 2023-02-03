using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{
    public class CollectionLengthAttribute : ValidationAttribute
    {
        private readonly int _length;
        public CollectionLengthAttribute(int length) 
        {
            _length = length;
            ErrorMessage = $"Maximum entries number is {_length}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ICollection<object>? collection = value as ICollection<object>;

            if (collection == null)
                return ValidationResult.Success;

            if (collection.Count > _length)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
