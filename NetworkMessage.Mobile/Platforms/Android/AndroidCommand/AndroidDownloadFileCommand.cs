using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using System.Security;
using NetworkMessage.CommandsResults.ConcreteCommandResults;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidDownloadFileCommand : BaseNetworkCommand
    {
        public string Path { get; }

        public AndroidDownloadFileCommand(string path)
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

        public override async Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            BaseNetworkCommandResult loadedFileResult;
            if (!File.Exists(Path))
            {
                loadedFileResult = new DownloadFileResult("File doesn't exist");
                return loadedFileResult;
            }
            
            loadedFileResult = new DownloadFileResult(Path);
            return loadedFileResult;
        }
    }
}
