using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using System.Security;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidDownloadFileCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidDownloadFileCommand(string path)
        {
            path = "/storage/emulated/0" + path.Substring(4);
            Path = path;
        }

        public override async Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            BaseNetworkCommandResult loadedFileResult;
            if (!File.Exists(Path))
            {
                loadedFileResult = new DownloadFileResult("File doesn't exist");
                return loadedFileResult;
            }

            try
            {
                byte[] file = await File.ReadAllBytesAsync(Path);
                loadedFileResult = new DownloadFileResult(file);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                loadedFileResult = new DownloadFileResult("Недопустимый путь", directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                loadedFileResult = new DownloadFileResult("Ошибка при чтении файла", ioException);
            }
            catch (SecurityException securityException)
            {
                loadedFileResult = new DownloadFileResult("Отсутствует необходимое разрешение", securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                loadedFileResult = new DownloadFileResult("Эта операция не поддерживается на текущей платформе", unauthorizedAccessException);
            }
            catch (Exception exception)
            {
                loadedFileResult = new DownloadFileResult(exception.Message, exception);
            }


            return loadedFileResult;
        }
    }
}
