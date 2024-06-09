using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using System.Security;
using NetworkMessage.CommandsResults.ConcreteCommandResults;
using NetworkMessage.DTO;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    partial class AndroidNestedFilesInfoCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidNestedFilesInfoCommand(string path)
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

        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            BaseNetworkCommandResult nestedFilesInfo;
            const string androidDefaultPath = "/storage/emulated/0/";
            DirectoryInfo directoryInfo = new DirectoryInfo(Path);
            if (!directoryInfo.Exists)
            {
                nestedFilesInfo = new NestedFilesInfoResult("File doesn't exist");
                return Task.FromResult(nestedFilesInfo);
            }

            try
            {
                IEnumerable<FileInfoDTO> filesInfo = directoryInfo.GetFiles().Select(f =>
                {
                    string fullName = f.FullName[androidDefaultPath.Length..];
                    return new FileInfoDTO(f.Name, f.CreationTimeUtc, f.LastWriteTimeUtc, f.Length, fullName, FileType.File);
                });
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
