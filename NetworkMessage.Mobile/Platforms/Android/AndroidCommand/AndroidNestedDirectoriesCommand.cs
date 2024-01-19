using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.Models;
using System.Security;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidNestedDirectoriesCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidNestedDirectoriesCommand(string path)
        {
            path = "/storage/emulated/0" + path.Substring(4);
            Path = path;
        }

        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {            
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            BaseNetworkCommandResult nestedDirectoriesInfo;
            if (!directoryInfo.Exists)
            {
                nestedDirectoriesInfo = new NestedDirectoriesInfoResult("File doesn't exist");
                return Task.FromResult(nestedDirectoriesInfo);
            }

            try
            {                
                IEnumerable<MyDirectoryInfo> directoriesInfo
                    = directoryInfo.GetDirectories().Select(d => new MyDirectoryInfo(d.Name, d.CreationTimeUtc, d.LastWriteTimeUtc, d.FullName));
                nestedDirectoriesInfo = new NestedDirectoriesInfoResult(directoriesInfo);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                nestedDirectoriesInfo = new NestedFilesInfoResult("Недопустимый путь", directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                nestedDirectoriesInfo = new NestedFilesInfoResult("Ошибка при чтении файла", ioException);
            }
            catch (SecurityException securityException)
            {
                nestedDirectoriesInfo = new NestedFilesInfoResult("Отсутствует необходимое разрешение", securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                nestedDirectoriesInfo = new NestedFilesInfoResult("Отсутствует необходимое разрешение", unauthorizedAccessException);
            }
            catch (Exception exception)
            {
                nestedDirectoriesInfo = new NestedFilesInfoResult(exception.Message, exception);
            }

            return Task.FromResult(nestedDirectoriesInfo);
        }
    }
}
