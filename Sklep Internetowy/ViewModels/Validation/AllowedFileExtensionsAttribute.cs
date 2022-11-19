using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Validation
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedFileExtensions;
        public AllowedFileExtensionsAttribute(params string[] allowedFileExtensions) 
        {
            _allowedFileExtensions = allowedFileExtensions;
            this.ErrorMessage = $"Invalid file ! Supported file extensions: {String.Join(", ",_allowedFileExtensions)}"; 
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<IFormFile> files = value as List<IFormFile>;

            if (files == null)
                return ValidationResult.Success;

            foreach (var file in files)
                if(!FileExtensionIsAllowed(file))
                    return new ValidationResult(this.ErrorMessage);

            return ValidationResult.Success;
        }

        protected bool FileExtensionIsAllowed(IFormFile file)
        {
            return _allowedFileExtensions.Contains(System.IO.Path.GetExtension(file.FileName));
        }
        
    }
}
