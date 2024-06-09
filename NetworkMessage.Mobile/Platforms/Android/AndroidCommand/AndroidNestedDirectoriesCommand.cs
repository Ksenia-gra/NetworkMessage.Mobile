using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using System.Security;
using NetworkMessage.CommandsResults.ConcreteCommandResults;
using NetworkMessage.DTO;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidNestedDirectoriesCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidNestedDirectoriesCommand(string path)
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
            BaseNetworkCommandResult nestedDirectoriesInfo;
            const string androidDefaultPath = "/storage/emulated/0/";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            try
            {
                if (!directoryInfo.Exists)
                {
                    nestedDirectoriesInfo = new NestedDirectoriesInfoResult("File doesn't exist");
                    return Task.FromResult(nestedDirectoriesInfo);
                }

                IEnumerable<FileInfoDTO> directoriesInfo = directoryInfo.GetDirectories().Select(d =>
                {
                    string fullName = d.FullName[androidDefaultPath.Length..];
                    return new FileInfoDTO(d.Name, d.CreationTimeUtc, d.LastWriteTimeUtc, null, fullName, FileType.File);
                });
                nestedDirectoriesInfo = new NestedDirectoriesInfoResult(directoriesInfo);
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                nestedDirectoriesInfo =
                    new NestedDirectoriesInfoResult("Недопустимый путь", directoryNotFoundException);
            }
            catch (IOException ioException)
            {
                nestedDirectoriesInfo = new NestedDirectoriesInfoResult("Ошибка при чтении файла", ioException);
            }
            catch (SecurityException securityException)
            {
                nestedDirectoriesInfo =
                    new NestedDirectoriesInfoResult("Отсутствует необходимое разрешение", securityException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                nestedDirectoriesInfo =
                    new NestedDirectoriesInfoResult("Отсутствует необходимое разрешение", unauthorizedAccessException);
            }
            catch (Exception exception)
            {
                nestedDirectoriesInfo = new NestedDirectoriesInfoResult(exception.Message, exception);
            }

            return Task.FromResult(nestedDirectoriesInfo);
        }
    }
}