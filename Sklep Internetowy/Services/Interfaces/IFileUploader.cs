using Sklep_Internetowy.Interfaces;

namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IFileUploader
    {
        public string GetDirectory();

        public void SetTargetFolderTo(TargetFolder folder);

        public void SetTargetFolderName(string name);

        public Task<UploadedFile> UploadFile(IFormFile file);

        public List<FileUploaderError> GetErrors();

        public IFile DeleteFile(IFile file);

    }
}
