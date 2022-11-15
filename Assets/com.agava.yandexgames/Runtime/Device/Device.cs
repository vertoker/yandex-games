using System.Runtime.InteropServices;

namespace Agava.YandexGames
{
    public static class Device
    {
        /// <summary>
        /// Proxy for `ysdk.deviceInfo.type` property.
        /// </summary>
        public static DeviceType Type => (DeviceType)GetDeviceType();

        [DllImport("__Internal")]
        private static extern int GetDeviceType();
    }
}
