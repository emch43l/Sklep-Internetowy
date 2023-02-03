namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IDirectoryConfigurationReader
    {
        public void ThrowExceptionWhenParamMissing(bool value);

        public string GetFolderName(TargetFolder folder);

        public void SetSection(string sectionName);

        public string GetDirectory(TargetFolder folder);
    }
}
