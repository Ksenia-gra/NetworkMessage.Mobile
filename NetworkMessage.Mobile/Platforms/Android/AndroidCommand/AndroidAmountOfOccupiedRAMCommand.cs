using Android.App;
using Android.Content;
using Android.OS.Storage;
using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidAmountOfOccupiedRAMCommand : BaseNetworkCommand
    {
        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            StorageManager storageManager =
                global::Android.App.Application.Context.GetSystemService(Context.StorageService) as StorageManager;
            if (storageManager != null)
            {
                //storageManager.
            }

            ActivityManager activityManager = (ActivityManager)global::Android.App.Application.Context.GetSystemService(Context.ActivityService);
            ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();
            activityManager.GetMemoryInfo(memoryInfo);



            long totalMemory = memoryInfo.TotalMem;
            long availableMemory = memoryInfo.AvailMem;
            long usedMemory = totalMemory - availableMemory;

            throw new NotImplementedException();
            return default;
        }
    }
}
