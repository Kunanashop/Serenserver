using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using AlyxSDK;

namespace Alyx.VehicleHandlers
{
    public class AutoPilot
    {
        private static nLog Log = new nLog("AutoPilot");
        public static List<string> accessVehicle = new List<string>()
        {
            "comet2",
            "buffalo",
            "elegy2",
            "modelx",
            "eqg",
            "g63"
        };

        public static bool HasAccessToAutopilot(VehicleHash hash)
        {
            try
            {
                bool access = false;
                foreach (var item in accessVehicle)
                {
                    if ((VehicleHash)NAPI.Util.GetHashKey(item) == hash)
                    {
                        access = true;
                        break;
                    }
                }
                return access;
            }
            catch (Exception e)
            {
                Log.Write(e.Message, nLog.Type.Error);
                return false;
            }
        }
    }
}
