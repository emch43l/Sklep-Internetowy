using Sklep_Internetowy.Interfaces;
using Sklep_Internetowy.Services.Interfaces;

namespace Sklep_Internetowy.Services
{
    public enum TargetFolder
    {
        Root = 0,
        Images = 1,
        Documents = 2,
        Videos = 3
    }

    public record FileUploaderError(string FileName ,string Message);

    public record UploadedFile(IFormFile File, string UploadedFileName);

    public class FileUploader : IFileUploader
    {
        private readonly string _enviromentDirectory;
        private List<FileUploaderError> _Errors;
        private string _rootFolderName;
        private string _targetFolderName = "";
        private readonly IDirectoryConfigurationReader _reader;

        public FileUploader(IWebHostEnvironment environment, IDirectoryConfigurationReader reader)
        {
            _Errors = new List<FileUploaderError>();
            _reader = reader;
            _reader.ThrowExceptionWhenParamMissing(true);
            _rootFolderName = _reader.GetFolderName(TargetFolder.Root);
            _enviromentDirectory = environment.WebRootPath;
        }

        public async Task<UploadedFile?> UploadFile(IFormFile file)
        {
            string uniqueFileName;

            if(file.Length <= 0)
            {
                _Errors.Add(
                        new FileUploaderError(FileName: file.FileName, Message: "Inalid file !")
                        );
                return null;
            }
            try
            {
                CreatDirectoryIfNotExists(GetDirectory());
                uniqueFileName = CreateUniqueFileName(file);
                using (Stream stream = File.Create(GetDirectory() + "/" + uniqueFileName))
                {
                    await file.CopyToAsync(stream);
                    return new UploadedFile(File: file, UploadedFileName: uniqueFileName);
                }
            }
            catch (Exception e)
            {
                _Errors.Add(
                    new FileUploaderError(FileName: file.FileName, Message: e.Message)
                    );
            }

            return null;
        }
        public IFile? DeleteFile(IFile file)
        {
            string filePath;
            try
            {
                filePath = GetDirectory() + "/" + file.GetFileName();
                if (FileExists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return file;
                }
                else
                {
                    _Errors.Add(
                        new FileUploaderError(FileName: file.GetFileTitle(), Message: $"File {file.GetFileTitle()} does not exist !")
                        );
                    return file;
                }
            }
            catch (Exception e)
            {
                _Errors.Add(
                    new FileUploaderError(FileName: file.GetFileTitle(), Message: e.Message)
                    );
            }

            return null;
        }

        private bool FileExists(string filePath)
            => File.Exists(filePath);

        private string CreateUniqueFileName(IFormFile file)
         => Guid.NewGuid() + "_" + string.Join('_', file.FileName.Split(Path.GetInvalidFileNameChars()));

        private void CreatDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SetTargetFolderName(string name)
        {
            _targetFolderName = name.Trim(System.IO.Path.GetInvalidFileNameChars());
        }

        public void SetTargetFolderTo(TargetFolder folder)
        {
            _targetFolderName = _reader.GetFolderName(folder);
        }

        public string GetDirectory()
            => _enviromentDirectory + "/" + _rootFolderName + "/" + _targetFolderName;

        public List<FileUploaderError> GetErrors()
            => _Errors;

    }
}
