using NetworkMessage.Commands;
using NetworkMessage.CommandsResults;
using NetworkMessage.CommandsResults.ConcreteCommandResults;

namespace NetworkMessage.Mobile.Platforms.Android.AndroidCommand
{
    public class AndroidBatteryChargePersentageCommand : BaseNetworkCommand
    {
        public override Task<BaseNetworkCommandResult> ExecuteAsync(CancellationToken token = default, params object[] objects)
        {
            BaseNetworkCommandResult result;
            try
            {
                byte percent = (byte)(Battery.Default.ChargeLevel * 100);
                result = new BatteryChargeResult(percent);
            }
            catch (PermissionException perEx)
            {
                result = new BatteryChargeResult("Разрешение не выдано", perEx);
            }

            return Task.FromResult(result);
        }
    }
}
