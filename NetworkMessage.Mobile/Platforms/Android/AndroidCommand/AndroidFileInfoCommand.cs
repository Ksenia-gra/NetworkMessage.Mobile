using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using System.Security;
using NetworkMessage.CommandsResults.ConcreteCommandResults;
using NetworkMessage.DTO;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidFileInfoCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidFileInfoCommand(string path)
        {
            string root = "root";
            path = path.Replace('\\', '/');
            int rootIndex = path.IndexOf(root, StringComparison.OrdinalIgnoreCase);
            if (rootIndex == 0)
            {
                path = path[rootIndex..];
            }

            if (path.FirstOrDefault() == '/')
            {
                path = path[1..];
            }

            path = "/storage/emulated/0/" + path;
            Path = path;
        }

        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default,
            params object[] objects)
        {
            BaseNetworkCommandResult fileInfoResult;
            const string androidDefaultPath = "/storage/emulated/0/";
            try
            {
                FileInfo fileInfo = new FileInfo(Path);
                if (!fileInfo.Exists)
                {
                    fileInfoResult = new FileInfoResult("File doesn't exist");
                    return Task.FromResult(fileInfoResult);
                }

                string fileName = fileInfo.Name;
                long fileLength = fileInfo.Length;
                DateTime creationTime = fileInfo.CreationTimeUtc;
                DateTime changingDate = fileInfo.LastWriteTimeUtc;
                string fullName = fileInfo.FullName[androidDefaultPath.Length..];
                fileInfoResult = new FileInfoResult(new FileInfoDTO(fileName, creationTime, changingDate, fileLength, fullName, FileType.File));
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                fileInfoResult = new FileInfoResult("Недопустимый путь", directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                fileInfoResult = new FileInfoResult("Ошибка при чтении файла", ioException);
            }
            catch (SecurityException securityException)
            {
                fileInfoResult = new FileInfoResult("Отсутствует необходимое разрешение", securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                fileInfoResult = new FileInfoResult("Отсутствует необходимое разрешение", unauthorizedAccessException);
            }
            catch (Exception exception)
            {
                fileInfoResult = new FileInfoResult(exception.Message, exception);
            }
            
            return Task.FromResult(fileInfoResult);
        }
    }
}