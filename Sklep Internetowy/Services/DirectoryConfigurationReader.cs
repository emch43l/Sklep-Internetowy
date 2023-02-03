using Sklep_Internetowy.Services.Interfaces;
using System.Configuration;

namespace Sklep_Internetowy.Services
{
    public class DirectoryConfigurationReader : IDirectoryConfigurationReader
    {
        private readonly IConfiguration _configuration;

        private string _missingParamPlaceholder = "All";

        private string _section = "Uploads";

        private bool _throwException = false;

        private Configuration configuration;

        public DirectoryConfigurationReader(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(_section) ?? configuration;
        }

        public DirectoryConfigurationReader(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void ThrowExceptionWhenParamMissing(bool value)
            => _throwException = value;

        public string GetFolderName(TargetFolder folder)
        {
            return _configuration.GetValue<string?>(folder.ToString()) ?? (
                (_throwException) ? throw new ArgumentNullException() : _missingParamPlaceholder);
        }

        public string GetDirectory(TargetFolder folder)
            => GetFolderName(TargetFolder.Root) + "/" + GetFolderName(folder);

        public void SetSection(string sectionName)
        {
            _section = sectionName;
        }
    }
}
