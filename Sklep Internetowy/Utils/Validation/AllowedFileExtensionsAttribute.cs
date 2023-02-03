using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Utils.Validation
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedFileExtensions;
        public AllowedFileExtensionsAttribute(params string[] allowedFileExtensions)
        {
            _allowedFileExtensions = allowedFileExtensions;
            ErrorMessage = $"Invalid file ! Supported file extensions: {string.Join(", ", _allowedFileExtensions)}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile>? files = value as List<IFormFile>;

            if (files == null)
                return ValidationResult.Success;

            foreach (var file in files)
                if (!FileExtensionIsAllowed(file))
                    return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

        protected bool FileExtensionIsAllowed(IFormFile file)
            => _allowedFileExtensions.Contains(Path.GetExtension(file.FileName.ToLower()));

    }
}
