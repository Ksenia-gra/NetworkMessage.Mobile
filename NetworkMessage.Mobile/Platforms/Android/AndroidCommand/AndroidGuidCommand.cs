using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using static Android.Provider.Settings;
using AndroidMaui = Android;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidGuidCommand : BaseNetworkCommand
    {
        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            var context = AndroidMaui.App.Application.Context;
            string guid = Secure.GetString(context.ContentResolver, Secure.AndroidId);
            BaseNetworkCommandResult result = new DeviceGuidResult(guid);
            return Task.FromResult(result);
        }
    }
}
