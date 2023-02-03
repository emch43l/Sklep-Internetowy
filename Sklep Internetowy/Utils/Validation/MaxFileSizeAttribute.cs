using Sklep_Internetowy.Services;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        private readonly ByteParser _byteParser = new ByteParser();
        public MaxFileSizeAttribute(int value, Services.Type type)
        {
            _maxFileSize = _byteParser.ToBytes(value, type);
            ErrorMessage = $"File size is too big ! Max file size: {value}{type.ToString()}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile>? files = value as List<IFormFile>;
            if (files == null)
                return ValidationResult.Success;

            foreach (IFormFile file in files)
                if (!IsValidSize(file))
                    return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;

        }

        protected bool IsValidSize(IFormFile file) => _maxFileSize >= file.Length;


    }
}
