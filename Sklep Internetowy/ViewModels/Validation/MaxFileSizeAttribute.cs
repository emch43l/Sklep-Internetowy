using Sklep_Internetowy.Tools;
using Sklep_Internetowy;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Validation
{

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(int value, Tools.Type type) 
        {
            _maxFileSize = ByteParser.ToBytes(value, type);
            this.ErrorMessage = $"File size is too big ! Max file size: {value}{type.ToString()}";
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile>? files = value as List<IFormFile>;
            if (files == null)
                return ValidationResult.Success;

            foreach(IFormFile file in files)
            {
                if (!IsValidSize(file))
                    return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
            
        }

        protected bool IsValidSize(IFormFile file) => _maxFileSize >= file.Length; 


    }
}
