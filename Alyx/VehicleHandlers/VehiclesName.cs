using GTANetworkAPI;
using System.Collections.Generic;
using System.Linq;

namespace Alyx.VehicleHandlers
{
    public static class VehiclesName
    {
        //реальные названия авто
        public static Dictionary<string, string> ModelList = new Dictionary<string, string>()
        {

              {"deluxo", "Alyx-Mode model" },
              {"eqg", "Mercedes-Benz EQG" },
              {"lex570", "Lexus LX570" },
              {"g63", "Mercedes-Benz G63 2021" },
              {"panamera17turbo", "Porsche Panamera Turbo" },
              {"kiastinger", "kiastinger" },
              {"vclass", "Mercedes-Benz V-Class" },
              {"e63s", "Mercedes-Benz E63S" },
              {"cls63s", "Mercedes-Benz CLS63S" },
              {"bmwe39", "BMW E39" },
              {"x5g05", "BMW X5M G05" },
              {"urus", "Lamborghini Urus" },
              {"msprinter", "Mercedes-Benz Sprinter" },
              {"modelx", "Tesla Model X" },
              {"m5comp", "BMW M5 F90 Competition" },
              {"emsnspeedo", "Speedo EMS" },
              {"cullinan", "Rolls-Royce Cullinan" },
              {"chiron19", "Bugatti Chiron" },
              {"bmwx7", "BMW X7" },
              {"a90", "Toyota Supra A90 2019" },
              {"63gls", "Mercedes-Benz GLS63" },

              //modelname  //Realname
        };

        public static string GetRealVehicleName(string model)
        {
            if (ModelList.ContainsKey(model))
            {
                return ModelList[model];
            }
            else
            {
                return model;
            }
        }

        public static string GetVehicleModelName(string name)
        {
            if (ModelList.ContainsValue(name))
            {
                return ModelList.FirstOrDefault(x => x.Value == name).Key;
            }
            else
            {
                return name;
            }
        }
        public static string GetRealVehicleNameHash(VehicleHash model)
        {
            if (ModelList2.ContainsKey(model))
            {
                return ModelList2[model];
            }
            else
            {
                return "null";
            }
        }
        public static Dictionary<VehicleHash, string> ModelList2 = new Dictionary<VehicleHash, string>()
        {

              { (VehicleHash)NAPI.Util.GetHashKey("a6"), "Audi A6" },
              { (VehicleHash)NAPI.Util.GetHashKey("a45"), "Mercedes-Benz A45" },
              { (VehicleHash)NAPI.Util.GetHashKey("c32"), "Mercedes-Benz C32" },
              { (VehicleHash)NAPI.Util.GetHashKey("s600w220"), "Mercedes-Benz s600 w220" },
              //modelname  //Realname
        };
    }
}
