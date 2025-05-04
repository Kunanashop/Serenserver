using System;
using Alyx;
using AlyxSDK;
using GTANetworkAPI;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Alyx.Houses;

namespace Alyx.Core
{
    class Exter
    {
        public static int LASTID = 0;
        public string Model;
        public int ID;
        public int UUID;
        public int Time;
        public Vector3 Position;
        public Vector3 Rotation;

        [JsonIgnore]
        public GTANetworkAPI.Object obj { get; private set; }

        public Exter(int uuid, string model, Vector3 pos, Vector3 rot, int time)
        {
            UUID = uuid;
            Model = model;
            Position = pos;
            Rotation = rot;
            Time = time;
            ID = LASTID;
            Create(0);
        }

        public GTANetworkAPI.Object Create(uint Dimension)
        {
            obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(Model), Position, Rotation, 255, Dimension);
            obj.SetData("UUID", UUID);
            obj.SetData("TIME", Time);
            obj.SetData("IDS", ID);
            obj.SetData("MODEL", Model);
            obj.SetSharedData("TYPE", "DROPPED");
            obj.SetData("SNOWMAKE", true);
            return obj;
        }
    }
    class Exteriors : Script
    {
        private static nLog Log = new nLog("Exteriors");
        public static List<Exter> ExtersList = new List<Exter>();
        [ServerEvent(Event.ResourceStart)]
        public void Exteriorss()
        {
            var result = MySQL.QueryRead($"SELECT * FROM `exteriors`");
            if (result == null || result.Rows.Count == 0)
            {
                Log.Write("DB return null result.", nLog.Type.Warn);
                return;
            }
            foreach (DataRow Row in result.Rows)
            {
                Exter employment = new Exter(
                        Convert.ToInt32(Row["UUID"]),
                        Convert.ToString(Row["model"]),
                        JsonConvert.DeserializeObject<Vector3>(Row["pos"].ToString()),
                        JsonConvert.DeserializeObject<Vector3>(Row["rot"].ToString()),
                        Convert.ToInt32(Row["time"])
                );
                ExtersList.Add(employment);
                Exter.LASTID++;
            }
            Log.Write($"Loaded players exters.", nLog.Type.Success);
        }

        [RemoteEvent("acceptEditE")]
        public void ClientEvent_acceptEditE(Player player, float X, float Y, float Z, float XX, float YY, float ZZ)
        {
            try
            {
                if (!player.HasData("IS_EDITINGE")) return;
                int id = player.GetData<int>("EDIT_IDE");
                House house = HouseManager.GetHouse(player, true);
                Vector3 pos = new Vector3(X, Y, Z);
                Vector3 rot = new Vector3(XX, YY, ZZ);
                player.ResetData("IS_EDITINGE");
                if (pos.Z < player.Position.Z-1.12 || pos.Z > player.Position.Z)
                {
                    Notify.Error(player, "Предмет расположен слишком высоко или слишком низко");
                    return;
                }
                if (pos.DistanceTo(house.Position) > 15)
                {
                    Notify.Error(player, "Вы далеко от дома");
                    return;
                }

                MySQL.Query($"INSERT INTO `exteriors` (`UUID`,`model`,`pos`,`rot`,`time`,`id`) VALUES ('{Main.Players[player].UUID}','{player.GetData<string>("EDIT_MODELE")}','{JsonConvert.SerializeObject(pos)}','{JsonConvert.SerializeObject(rot)}','30','{Exter.LASTID}')");
                Exter employment = new Exter(
                         Convert.ToInt32(Main.Players[player].UUID),
                         Convert.ToString(player.GetData<string>("EDIT_MODELE")),
                         pos,
                         rot,
                         Convert.ToInt32(30)
                 );
                if (player.GetData<string>("EDIT_MODELE") == "grand_prop_xmas_snowman")
                {
                    nInventory.Remove(player, new nItem(ItemType.Snowman, 1));
                }
                if (player.GetData<string>("EDIT_MODELE") == "grand_prop_xmas_snowman2")
                {
                    nInventory.Remove(player, new nItem(ItemType.Snowman2, 1));
                }
                if (player.GetData<string>("EDIT_MODELE") == "grand_prop_xmas_igloo")
                {
                    nInventory.Remove(player, new nItem(ItemType.Igla, 1));
                }
                ExtersList.Add(employment);
                Exter.LASTID++;
                return;
            }
            catch (Exception e) { Log.Write("acceptEditE: " + e.Message, nLog.Type.Error); }
        }

        [RemoteEvent("cancelEditE")]
        public void ClientEvent_cancelEditE(Player player, params object[] arguments)
        {
            try
            {
                player.ResetData("IS_EDITINGE");
                return;
            }
            catch (Exception e) { Log.Write("cancelEditE: " + e.Message, nLog.Type.Error); }
        }
    }
}