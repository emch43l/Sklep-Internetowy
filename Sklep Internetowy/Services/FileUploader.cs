using Sklep_Internetowy.Interfaces;

namespace Sklep_Internetowy.Services
{
    public interface IFileUploader
    {
        public string GetDirectory();

        public void SetTargetFolderTo(TargetFolder folder);

        public void SetTargetFolderName(string name);

        public Task<List<UploadedFile>> UploadFiles(ICollection<IFormFile> files);

        public List<FileUploaderError> GetErrors();

        public List<IFile> DeleteFile(ICollection<IFile> files);

    }

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
            _reader = reader;
            _reader.ThrowExceptionWhenParamMissing(true);
            _rootFolderName = _reader.GetFolderName(TargetFolder.Root);
            _enviromentDirectory = environment.WebRootPath;
        }

        public async Task<List<UploadedFile>> UploadFiles(ICollection<IFormFile> files)
        {
            string uniqueFileName;

            List<UploadedFile> uploadedFiles = new List<UploadedFile>();
            if (files == null)
                return uploadedFiles;

            foreach (IFormFile file in files)
            {
                if(file.Length <= 0)
                {
                    _Errors.Add(
                        new FileUploaderError(FileName: file.FileName, Message: "Inalid file !")
                        );
                    continue;
                }
                try
                {
                    CreatDirectoryIfNotExists(GetDirectory());
                    uniqueFileName = CreateUniqueFileName(file);
                    using (Stream stream = System.IO.File.Create(GetDirectory() + "/" + uniqueFileName))
                    {
                        await file.CopyToAsync(stream);
                        uploadedFiles.Add(new UploadedFile(File: file, UploadedFileName: uniqueFileName));
                    }
                }
                catch(Exception e)
                {
                    _Errors.Add(
                        new FileUploaderError(FileName: file.FileName, Message: e.Message)
                        );
                }

            }

            return uploadedFiles;
        }
        public List<IFile> DeleteFile(ICollection<IFile> files)
        {
            string filePath;

            List<IFile> deletedFiles = new List<IFile>();

            if (files.Count == 0)
                return deletedFiles;

            foreach(IFile file in files)
            {
                try
                {
                    filePath = GetDirectory() + "/" + file.GetFileName();
                    if(FileExists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        deletedFiles.Add(file);
                    }
                    else
                    {
                        _Errors.Add(
                            new FileUploaderError(FileName: file.GetFileTitle(), Message: $"File {file.GetFileTitle()} does not exist !")
                            );
                    }
                }
                catch(Exception e)
                {
                    _Errors.Add(
                        new FileUploaderError(FileName: file.GetFileTitle(), Message: e.Message)
                        );
                }
            }

            return deletedFiles;
        }

        private bool FileExists(string filePath)
            => System.IO.File.Exists(filePath);

        private string CreateUniqueFileName(IFormFile file)
         => Guid.NewGuid() + "_" + string.Join('_', file.FileName.Split(Path.GetInvalidFileNameChars()));

        private void CreatDirectoryIfNotExists(string path)
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
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
