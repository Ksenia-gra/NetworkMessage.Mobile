using NetworkMessage.CommandFactory;
using NetworkMessage.Commands;
using NetworkMessage.Mobile.Platforms.Android.AndroidCommand;

namespace NetworkMessage.Mobile.Platforms.Android
{
    public class AndroidCommandFactory : ICommandFactory
    {
        public BaseNetworkCommand CreateAmountOfOccupiedRAMCommand()
        {
            throw new NotImplementedException();
        }

        public BaseNetworkCommand CreateAmountOfRAMCommand()
        {
            throw new NotImplementedException();
        }

        public BaseNetworkCommand CreateBatteryChargePersentageCommand()
        {
            return new AndroidBatteryChargePersentageCommand();
        }

        public BaseNetworkCommand CreateDirectoryInfoCommand(string path)
        {
            throw new NotImplementedException();
        }

        public BaseNetworkCommand CreateDownloadDirectoryCommand(string path)
        {
            throw new NotImplementedException();
        }        

        public BaseNetworkCommand CreateDownloadFileCommand(string path)
        {
            return new AndroidDownloadFileCommand(path);
        }

        public BaseNetworkCommand CreateFileInfoCommand(string path)
        {
            return new AndroidFileInfoCommand(path);
        }

        public BaseNetworkCommand CreateGuidCommand()
        {
            return new AndroidGuidCommand();
        }

        public BaseNetworkCommand CreateMacAddressCommand()
        {
            throw new NotImplementedException();
        }

        public BaseNetworkCommand CreateNestedDirectoriesInfoCommand(string path)
        {
            return new AndroidNestedDirectoriesCommand(path);
        }

        public BaseNetworkCommand CreateNestedFilesInfoCommand(string path)
        {
            return new AndroidNestedFilesInfoCommand(path);
        }

        public BaseNetworkCommand CreatePercentageOfCPUUsageCommand()
        {
            throw new NotImplementedException();
        }

        public BaseNetworkCommand CreateScreenshotCommand()
        {
            throw new NotImplementedException();
        }
    }
}
