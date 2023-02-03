using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{
    public class MaxFilesAttribute : ValidationAttribute
    {
        private readonly int _maxFilesNumber;
        public MaxFilesAttribute(int maxFilesNum = 1) 
        {
            if (maxFilesNum <= 0)
                throw new ArgumentException("Given argument can not be lower than 1 !");
            _maxFilesNumber = maxFilesNum;
            ErrorMessage = $"Maximum number of files is {_maxFilesNumber} !";
        }

        protected override ValidationResult? IsValid(object? data, ValidationContext validationContext)
        {
            ICollection<IFormFile>? files = data as ICollection<IFormFile>;

            if (files == null)
                return ValidationResult.Success;

            if (files.Count > _maxFilesNumber)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
