using NetworkMessage.CommandFactory;
using NetworkMessage.Commands;
using NetworkMessage.Mobile.Platforms.Android.AndroidCommand;

namespace NetworkMessage.Mobile.Platforms.Android
{
    public class AndroidCommandFactory : ICommandFactory
    {
        public INetworkCommand CreateAmountOfOccupiedRAMCommand()
        {
            return new AndroidAmountOfOccupiedRAMCommand();
        }

        public INetworkCommand CreateAmountOfRAMCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateBatteryChargePersentageCommand()
        {
            return new AndroidBatteryChargePersentageCommand();
        }

        public INetworkCommand CreateDirectoryInfoCommand(string path)
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateDownloadDirectoryCommand(string path)
        {
            throw new NotImplementedException();
        }        

        public INetworkCommand CreateDownloadFileCommand(string path)
        {
            return new AndroidDownloadFileCommand(path);
        }

        public INetworkCommand CreateDrivesInfoCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateFileInfoCommand(string path)
        {
            return new AndroidFileInfoCommand(path);
        }

        public INetworkCommand CreateGuidCommand()
        {
            return new AndroidGuidCommand();
        }

        public INetworkCommand CreateMacAddressCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateNestedDirectoriesInfoCommand(string path)
        {
            return new AndroidNestedDirectoriesCommand(path);
        }

        public INetworkCommand CreateNestedFilesInfoCommand(string path)
        {
            return new AndroidNestedFilesInfoCommand(path);
        }

        public INetworkCommand CreatePercentageOfCPUUsageCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateScreenshotCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateRunningProgramsCommand()
        {
            throw new NotImplementedException();
        }

        public INetworkCommand CreateStartProgramCommand(string path)
        {
            throw new NotImplementedException();
        }
    }
}
