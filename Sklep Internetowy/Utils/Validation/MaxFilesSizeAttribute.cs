using Sklep_Internetowy.Services;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Sklep_Internetowy.Utils.Validation
{

    public class MaxFilesSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        private readonly ByteParser _byteParser = new ByteParser();
        public MaxFilesSizeAttribute(int value, Services.Type type)
        {
            _maxFileSize = _byteParser.ToBytes(value, type);
            ErrorMessage = $"Files size is too big! Max files size: {value}{type.ToString()}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile>? files = value as List<IFormFile>;
            if (files == null)
                return ValidationResult.Success;

            long filesSize = files.Sum(s => s.Length);

            if (!IsValidSize(filesSize))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;

        }

        protected bool IsValidSize(long length) => _maxFileSize >= length;


    }
}
