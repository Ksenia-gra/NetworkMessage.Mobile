using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.Models;
using System.Security;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidFileInfoCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidFileInfoCommand(string path)
        {
            path = "/storage/emulated/0" + path.Substring(4);
            Path = path;
        }

        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            FileInfo fileInfo = new FileInfo(Path);
            BaseNetworkCommandResult fileInfoResult;
            if (!fileInfo.Exists)
            {
                fileInfoResult = new FileInfoResult("File doesn't exist");
                return Task.FromResult(fileInfoResult);
            }

            try
            {
                string fileName = fileInfo.Name;
                long fileLength = fileInfo.Length;                
                DateTime creationTime = fileInfo.CreationTimeUtc;
                DateTime changingDate = fileInfo.LastWriteTimeUtc;
                string fullName = fileInfo.FullName;
                fileInfoResult = new FileInfoResult(new MyFileInfo(fileName, creationTime, changingDate, fileLength, fullName));
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
