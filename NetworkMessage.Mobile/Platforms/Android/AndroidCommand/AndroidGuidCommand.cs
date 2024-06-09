using Android.Content;
using Android.Provider;
using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.CommandsResults.ConcreteCommandResults;
using AndroidApp = Android.App.Application;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidGuidCommand : BaseNetworkCommand
    {
        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            Context context = AndroidApp.Context;
            string guid = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
            BaseNetworkCommandResult result = new DeviceGuidResult(guid);
            return Task.FromResult(result);
        }
    }
}
