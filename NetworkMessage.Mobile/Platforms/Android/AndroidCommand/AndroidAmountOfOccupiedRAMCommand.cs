using Android.App;
using Android.Content;
using Android.OS.Storage;
using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.CommandsResults.ConcreteCommandResults;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices;
using System.Diagnostics;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidAmountOfOccupiedRAMCommand : BaseNetworkCommand
    {
        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
			BaseNetworkCommandResult result;
            try
            {
                StorageManager storageManager =
                    global::Android.App.Application.Context.GetSystemService(Context.StorageService) as StorageManager;
                if (storageManager != null)
                {
                    //storageManager.
                }

                Process process = Process.GetCurrentProcess();

                ActivityManager activityManager = (ActivityManager)global::Android.App.Application.Context.GetSystemService(Context.ActivityService);
                ActivityManager.MemoryInfo memoryInfo = new ActivityManager.MemoryInfo();
                activityManager.GetMemoryInfo(memoryInfo);

                long totalMemory = memoryInfo.TotalMem;
                long availableMemory = memoryInfo.AvailMem;
                long usedMemory = totalMemory - availableMemory;
                result = new AmountOfOccupiedRAMResult(process.WorkingSet64);



				/*using Xamarin.Essentials;*//*

				long memoryUsed = Memory.AppMemoryUsage;
				long memoryAvailable = Memory.AppMemoryUsageLimit;
*/
				Console.WriteLine($"Memory Used: {memoryUsed} bytes");
				Console.WriteLine($"Memory Available: {memoryAvailable} bytes");

			}
			catch (Exception ex)            
            {
                result = new AmountOfOccupiedRAMResult(ex.Message, ex);
            }

            return Task.FromResult(result);
        }
    }
}
