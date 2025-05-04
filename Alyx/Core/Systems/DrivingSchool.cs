using System.Collections.Generic;
using System;
using GTANetworkAPI;
using Newtonsoft.Json;
using Alyx.GUI;
using AlyxSDK;
using static Alyx.Core.Quests;

namespace Alyx.Core
{
    class DrivingSchool : Script
    {
        // мотоциклы, легковые машины, грузовые, водный, вертолёты, самолёты
        private static List<int> LicPrices = new List<int>() { 1000, 3500, 10000, 12000, 20000, 21000 };
        private static Vector3 enterSchool = new Vector3(-711.3695, -1305.2975, 3.992632);
        private static List<Vector3> startCourseCoord = new List<Vector3>()
        {
            new Vector3(-707.0867, -1275.8596, 4.881519),
        };
        private static List<Vector3> startCourseRot = new List<Vector3>()
        {
            new Vector3(0.104611255, 0.019487236, 141.83179),
            new Vector3(0.104611255, 0.019487236, 141.83179),
            new Vector3(0.104611255, 0.019487236, 141.83179),
        };
        private static List<Vector3> drivingCoords = new List<Vector3>()
        {
            new Vector3(-743.29065, -1297.7557, 4.8826632),     //as1
            new Vector3(-721.6567, -1264.4321, 8.120333),     //as2
            new Vector3(-694.88525, -1240.9398, 10.445287),     //as3
            new Vector3(-659.7931, -1314.9128, 10.4506035),     //as4
            new Vector3(-659.5859, -1438.2676, 10.464868),     //as5
            new Vector3(-707.8045, -1509.1527, 12.126915),     //as6
            new Vector3(-751.8379, -1589.1432, 14.294757),     //as7
            new Vector3(-814.85455, -1668.3447, 17.078615),     //as8
            new Vector3(-972.41785, -1832.2412, 19.573656),     //as9
            new Vector3(-1089.5142, -2020.5852, 12.996085),     //as10
            new Vector3(-967.31323, -2150.9453, 8.670086),     //as11
            new Vector3(-947.7292, -2144.3386, 8.845933),     //as12
            new Vector3(-942.781, -2122.7502, 9.210661),     //as13
            new Vector3(-918.0939, -2067.7847, 9.181835),     //as14
            new Vector3(-891.56586, -2051.6838, 9.180723),     //as15
            new Vector3(-944.291, -2105.3604, 9.181939),     //as16
            new Vector3(-952.09595, -2140.8408, 8.829953),     //as17
            new Vector3(-1049.8688, -2058.6707, 13.078414),     //as18
            new Vector3(-973.9227, -1841.0247, 19.36009),     //as19
            new Vector3(-833.73773, -1696.0359, 18.403006),     //as20
            new Vector3(-728.14795, -1574.0854, 14.234413),     //as21
            new Vector3(-673.41345, -1470.7544, 10.417674),     //as22
            new Vector3(-645.2232, -1400.5048, 10.539662),     //as23
            new Vector3(-632.62177, -1314.5555, 10.537052),     //as24
            new Vector3(-640.1948, -1275.1984, 10.47842),     //as25
            new Vector3(-679.41455, -1231.6116, 10.54893),     //as26
            new Vector3(-707.18567, -1240.5889, 10.269665),     //as27
            new Vector3(-762.9907, -1287.5219, 4.8821507),     //as28
            new Vector3(-724.15, -1286.1101, 4.8817043),     //as29
        };

        private static nLog Log = new nLog("DrivingSc");

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                var shape = NAPI.ColShape.CreateCylinderColShape(enterSchool, 1, 2, 0);
                shape.OnEntityEnterColShape += onPlayerEnterSchool;
                shape.OnEntityExitColShape += onPlayerExitSchool;

                var blip = NAPI.Blip.CreateBlip(enterSchool, 0);
                blip.ShortRange = true;
                blip.Name = Main.StringToU16("Центр лицензирования");
                blip.Scale = 1f;
                blip.Sprite = 545;
                blip.Color = 4;
                for (int i = 0; i < drivingCoords.Count; i++)
                {
                    var colshape = NAPI.ColShape.CreateCylinderColShape(drivingCoords[i], 4, 5, 0);
                    colshape.OnEntityEnterColShape += onPlayerEnterDrive;
                    colshape.SetData("NUMBER", i);
                }
            }
            catch (Exception e) { Log.Write("ResourceStart: " + e.Message, nLog.Type.Error); }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void Event_OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            try
            {
                if (player.HasData("SCHOOLVEH") && player.GetData<Vehicle>("SCHOOLVEH") == vehicle)
                {
                    //player.SetData("SCHOOL_TIMER", Main.StartT(60000, 99999999, (o) => timer_exitVehicle(player), "SCHOOL_TIMER"));
                    player.SetData("SCHOOL_TIMER", Timers.StartOnce(60000, () => timer_exitVehicle(player)));

                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"Если вы не сядете в машину в течение 60 секунд, то провалите экзамен", 3000);
                    return;
                }
            }
            catch (Exception e) { Log.Write("PlayerExitVehicle: " + e.Message, nLog.Type.Error); }
        }

        private void timer_exitVehicle(Player player)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (!Main.Players.ContainsKey(player)) return;
                    if (!player.HasData("SCHOOLVEH")) return;
                    if (player.IsInVehicle && player.Vehicle == player.GetData<Vehicle>("SCHOOLVEH")) return;
                    NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("SCHOOLVEH"));
                    Trigger.ClientEvent(player, "deleteCheckpoint", 12, 0);
                    player.ResetData("IS_DRIVING");
                    player.ResetData("SCHOOLVEH");
                    //Main.StopT(player.GetData<string>("SCHOOL_TIMER"), "timer_36");
                    Timers.Stop(player.GetData<string>("SCHOOL_TIMER"));
                    player.ResetData("SCHOOL_TIMER");
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"Вы провалили экзмен", 3000);
                }
                catch (Exception e) { Log.Write("TimerDrivingSchool: " + e.Message, nLog.Type.Error); }
            });
        }

        public static void onPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (player.HasData("SCHOOLVEH")) NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("SCHOOLVEH"));
                }
                catch (Exception e) { Log.Write("PlayerDisconnected: " + e.Message, nLog.Type.Error); }
            }, 0);
        }
        [RemoteEvent("server::StartdriveCourse")]
        public static void startDrivingCourse(Player player, int index)
        {
            if (player.HasData("IS_DRIVING") || player.GetData<bool>("ON_WORK"))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не можете сделать это сейчас", 3000);
                return;
            }
            if (Main.Players[player].Licenses[index])
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас уже есть эта лицензия", 3000);
                return;
            }
            switch (index)
            {
                case 0:
                    if (Main.Players[player].Money < LicPrices[0])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    var vehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Bati, startCourseCoord[0], startCourseRot[0], 30, 30);
                    vehicle.NumberPlate = "SCHOOL";
                    player.SetIntoVehicle(vehicle, 0);
                    player.SetData("SCHOOLVEH", vehicle);
                    vehicle.SetData("ACCESS", "SCHOOL");
                    vehicle.SetData("DRIVER", player);
                    player.SetData("IS_DRIVING", true);
                    player.SetData("LICENSE", 0);
                    Trigger.ClientEvent(player, "createCheckpoint", 12, 1, drivingCoords[0] - new Vector3(0, 0, 2), 4, 0, 255, 0, 0);
                    Trigger.ClientEvent(player, "createWaypoint", drivingCoords[0].X, drivingCoords[0].Y);
                    player.SetData("CHECK", 0);
                    MoneySystem.Wallet.Change(player, -LicPrices[0]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[0];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[0], $"buyLic");
                    Core.VehicleStreaming.SetEngineState(vehicle, false);
                    return;
                case 1:
                    if (Main.Players[player].Money < LicPrices[1])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    vehicle = NAPI.Vehicle.CreateVehicle((VehicleHash)NAPI.Util.GetHashKey("a45"), startCourseCoord[0], startCourseRot[0], 30, 30);
                    player.SetIntoVehicle(vehicle, 0);
                    vehicle.NumberPlate = "SCHOOL";
                    player.SetData("SCHOOLVEH", vehicle);
                    vehicle.SetData("ACCESS", "SCHOOL");
                    vehicle.SetData("DRIVER", player);
                    player.SetData("IS_DRIVING", true);
                    player.SetData("LICENSE", 1);
                    Trigger.ClientEvent(player, "createCheckpoint", 12, 1, drivingCoords[0] - new Vector3(0, 0, 2), 4, 0, 255, 0, 0);
                    Trigger.ClientEvent(player, "createWaypoint", drivingCoords[0].X, drivingCoords[0].Y);
                    player.SetData("CHECK", 0);
                    MoneySystem.Wallet.Change(player, -LicPrices[1]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[1];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[1], $"buyLic");
                    Core.VehicleStreaming.SetEngineState(vehicle, false);
                    return;
                case 2:
                    if (Main.Players[player].Money < LicPrices[2])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    vehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Bison, startCourseCoord[0], startCourseRot[0], 30, 30);
                    player.SetIntoVehicle(vehicle, 0);
                    vehicle.NumberPlate = "SCHOOL";
                    player.SetData("SCHOOLVEH", vehicle);
                    vehicle.SetData("ACCESS", "SCHOOL");
                    vehicle.SetData("DRIVER", player);
                    player.SetData("IS_DRIVING", true);
                    player.SetData("LICENSE", 2);
                    Trigger.ClientEvent(player, "createCheckpoint", 12, 1, drivingCoords[0] - new Vector3(0, 0, 2), 4, 0, 255, 0, 0);
                    Trigger.ClientEvent(player, "createWaypoint", drivingCoords[0].X, drivingCoords[0].Y);
                    player.SetData("CHECK", 0);
                    MoneySystem.Wallet.Change(player, -LicPrices[2]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[2];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[2], $"buyLic");
                    Core.VehicleStreaming.SetEngineState(vehicle, false);
                    return;
                case 3:
                    if (Main.Players[player].Money < LicPrices[3])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    Main.Players[player].Licenses[3] = true;
                    MoneySystem.Wallet.Change(player, -LicPrices[3]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[3];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[3], $"buyLic");
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы успешно купили лицензию на водный транспорт", 3000);
                    Dashboard.sendStats(player);
                    return;
                case 4:
                    if (Main.Players[player].Money < LicPrices[4])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"", 3000);
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    Main.Players[player].Licenses[4] = true;
                    MoneySystem.Wallet.Change(player, -LicPrices[4]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[4];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[4], $"buyLic");
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы успешно купили лицензию управление вертолётами", 3000);
                    Dashboard.sendStats(player);
                    return;
                case 5:
                    if (Main.Players[player].Money < LicPrices[5])
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас недостаточно денег, чтобы купить эту лицензию", 3000);
                        return;
                    }
                    Main.Players[player].Licenses[5] = true;
                    MoneySystem.Wallet.Change(player, -LicPrices[5]);
                    Fractions.Stocks.fracStocks[6].Money += LicPrices[5];
                    GameLog.Money($"player({Main.Players[player].UUID})", $"frac(6)", LicPrices[5], $"buyLic");
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы успешно купили лицензию управление самолётами", 3000);
                    Dashboard.sendStats(player);
                    return;
            }
        }
        private void onPlayerEnterSchool(ColShape shape, Player entity)
        {
            try
            {
                NAPI.Data.SetEntityData(entity, "INTERACTIONCHECK", 39);
            }
            catch (Exception e) { Log.Write("onPlayerEnterSchool: " + e.ToString(), nLog.Type.Error); }
        }
        private void onPlayerExitSchool(ColShape shape, Player player)
        {
            NAPI.Data.SetEntityData(player, "INTERACTIONCHECK", 0);
        }
        private void onPlayerEnterDrive(ColShape shape, Player player)
        {
            try
            {
                if (!player.IsInVehicle || player.VehicleSeat != 0) return;
                if (!player.Vehicle.HasData("ACCESS") || player.Vehicle.GetData<string>("ACCESS") != "SCHOOL") return;
                if (!player.HasData("IS_DRIVING")) return;
                if (player.Vehicle != player.GetData<Vehicle>("SCHOOLVEH")) return;
                if (shape.GetData<int>("NUMBER") != player.GetData<int>("CHECK")) return;
                //Trigger.ClientEvent(player, "deleteCheckpoint", 12, 0);
                var check = player.GetData<int>("CHECK");
                if (check == drivingCoords.Count - 1)
                {
                    player.ResetData("IS_DRIVING");
                    var vehHP = player.Vehicle.Health;
                    NAPI.Task.Run(() =>
                    {
                        try
                        {
                            NAPI.Entity.DeleteEntity(player.Vehicle);
                        }
                        catch { }
                    });
                    player.ResetData("SCHOOLVEH");
                    if (vehHP < 500)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы провалили экзамен", 3000);
                        return;
                    }
                    Main.Players[player].Licenses[player.GetData<int>("LICENSE")] = true;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы успешно сдали экзамен", 3000);
                    if (player.GetData<int>("LICENSE") == 1)
                    {
                        if (Main.Players[player].Achievements[4] == true && Main.Players[player].Achievements[5] == false)
                        {
                            Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Первые шаги", 100, "Вернитесь к Гарри и поговорите с ним");
                            Main.Players[player].Achievements[5] = true;
                        }
                    }
                    Dashboard.sendStats(player);
                    Trigger.ClientEvent(player, "deleteCheckpoint", 12, 0);
                    return;
                }

                player.SetData("CHECK", check + 1);
                if (check + 2 < drivingCoords.Count)
                    Trigger.ClientEvent(player, "createCheckpoint", 12, 1, drivingCoords[check + 1] - new Vector3(0, 0, 2), 4, 0, 255, 0, 0, drivingCoords[check + 2] - new Vector3(0, 0, 1.12));
                else
                    Trigger.ClientEvent(player, "createCheckpoint", 12, 1, drivingCoords[check + 1] - new Vector3(0, 0, 2), 4, 0, 255, 0, 0);
                Trigger.ClientEvent(player, "createWaypoint", drivingCoords[check + 1].X, drivingCoords[check + 1].Y);
            }
            catch (Exception e)
            {
                Log.Write("ENTERDRIVE:\n" + e.ToString(), nLog.Type.Error);
            }
        }

        #region menu
        public static void OpenDriveSchoolMenu(Player player)
        {
            Trigger.ClientEvent(player, "NPC.cameraOn", "AutoschoolPED", 1000);
            Trigger.ClientEvent(player, "client::opendialogmenu", true, "Артур Шторм", "Местный", "Привет, ты в здании лицензирования. Здесь ты можешь получить лицензию на вождение", (new QuestAnswer("Купить лицензию DRIVE A", 54), new QuestAnswer("Купить лицензию DRIVE D", 55), new QuestAnswer("Купить лицензию CDL B", 56), new QuestAnswer("В следующий раз", 2)));
        }
        #endregion
    }
}