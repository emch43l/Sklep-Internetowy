using Sklep_Internetowy.Services;

namespace SklepInternetowyTest
{
    public class DirectoryConfigurationReaderTest : IDirectoryConfigurationReader
    {
        public string GetDirectory(TargetFolder folder)
        {
            return string.Empty;
        }

        public string GetFolderName(TargetFolder folder)
        {
            return string.Empty;
        }

        public void SetSection(string sectionName)
        {
            throw new NotImplementedException();
        }

        public void ThrowExceptionWhenParamMissing(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
