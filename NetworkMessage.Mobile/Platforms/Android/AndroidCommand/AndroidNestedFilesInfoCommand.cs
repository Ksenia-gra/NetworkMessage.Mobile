using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.Models;
using System.Security;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    partial class AndroidNestedFilesInfoCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidNestedFilesInfoCommand(string path)
        {
            path = "/storage/emulated/0" + path.Substring(4);
            Path = path;
        }

        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            BaseNetworkCommandResult nestedFilesInfo;
            if (!directoryInfo.Exists)
            {
                nestedFilesInfo = new NestedFilesInfoResult("File doesn't exist");
                return Task.FromResult(nestedFilesInfo);
            }

            try
            {
                IEnumerable<MyFileInfo> filesInfo 
                    = directoryInfo.GetFiles().Select(f => new MyFileInfo(f.Name, f.CreationTimeUtc, f.LastWriteTimeUtc, f.Length, f.FullName));
                nestedFilesInfo = new NestedFilesInfoResult(filesInfo);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                nestedFilesInfo = new NestedFilesInfoResult("Недопустимый путь", directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                nestedFilesInfo = new NestedFilesInfoResult("Ошибка при чтении файла", ioException);
            }
            catch (SecurityException securityException)
            {
                nestedFilesInfo = new NestedFilesInfoResult("Отсутствует необходимое разрешение", securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                nestedFilesInfo = new NestedFilesInfoResult("Отсутствует необходимое разрешение", unauthorizedAccessException);
            }
            catch (Exception exception)
            {
                nestedFilesInfo = new NestedFilesInfoResult(exception.Message, exception);
            }

            return Task.FromResult(nestedFilesInfo);
        }
    }
}
