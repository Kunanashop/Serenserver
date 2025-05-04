using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GTANetworkAPI;
using Alyx.Core;
using AlyxSDK;
using Alyx.GUI;

namespace Alyx.Fractions
{
    class GangsCapture : Script
    {
        private static nLog Log = new nLog("GangCapture");
        public static Dictionary<int, GangPoint> gangPoints = new Dictionary<int, GangPoint>();
        public static bool captureIsGoing = false;
        public static bool captureStarting = false;
        private static string captureTimer;
        private static string toStartCaptureTimer;
        private static int attackersFracID = -1;
        private static int defendersFracID = -1;
        private static int timerCount = 0;
        private static int timerExitCountDef = 0;
        private static int timerExitCountAt = 0;
        private static int attackersSt = 0;
        private static int defendersSt = 0;
        private static bool defendersWas = false;
        private static bool attackersWas = false;
        private static bool smbTryCapture = false;

        public static int FamiliesPoint = 0;
        public static int BallasPoint = 0;
        public static int VagosPoint = 0;
        public static int MarabuntaPoint = 0;
        public static int BloodsPoint = 0;

        public static Vector3 captureRegion = new Vector3();
        public static Dictionary<int, int> gangPointsColor = new Dictionary<int, int>
        {
            { 1, 25 }, // families
            { 2, 50 }, // ballas
            { 3, 66 }, // vagos
            { 4, 42 }, // marabunta
            { 5, 75 }, // blood street
        };
        private static Dictionary<int, string> pictureNotif = new Dictionary<int, string>
        {
            { 1, "CHAR_MP_FAM_BOSS" }, // families
            { 2, "CHAR_MP_GERALD" }, // ballas
            { 3, "CHAR_ORTEGA" }, // vagos
            { 4, "CHAR_MP_ROBERTO" }, // marabunta
            { 5, "CHAR_MP_SNITCH" }, // blood street
        };
        private static Dictionary<int, DateTime> nextCaptDate = new Dictionary<int, DateTime>
        {
            { 1, DateTime.Now },
            { 2, DateTime.Now },
            { 3, DateTime.Now },
            { 4, DateTime.Now },
            { 5, DateTime.Now },
        };
        private static Dictionary<int, DateTime> protectDate = new Dictionary<int, DateTime>
        {
            { 1, DateTime.Now },
            { 2, DateTime.Now },
            { 3, DateTime.Now },
            { 4, DateTime.Now },
            { 5, DateTime.Now },
        };
        public static List<Vector3> gangZones = new List<Vector3>()
        {
           new Vector3(1481.6602, -1435.6931, 67.9863),// МЕРОР респа / ТУпик мерора
            new Vector3(1481.6602, -1575.6931, 67.9863),//МЕРОР Слево от квадрата снизу
            new Vector3(1341.6602, -1575.6931, 67.9863),//Мерор Правый крайний квадрат
            new Vector3(1061.6602, -1715.6931, 67.9863),// МЕРОР Титульник / ТУпик мерора
            new Vector3(1201.6602, -1715.6931, 67.9863),// мерор
            new Vector3(1341, -1715, 67.9863),// meror Park
            new Vector3(1201.6602, -1575.6931, 67.9863),
            new Vector3(1201, -2415, 67.9863),
            new Vector3(1341, -1855.6931, 67.9863),
            new Vector3(1341, -1995, 67.9863),// мерор
            new Vector3(1341, -2135.6931, 67.9863),// meror Park
            new Vector3(1201.6602, -1855.6931, 67.9863),
            new Vector3(1201.6602, -1995.6931, 67.9863),
            new Vector3(1201.6602, -2135.6931, 30.18104),
            new Vector3(1061.6602, -1855.6931, 30.18104),
            new Vector3(1061.6602, -1995.6931, 30.18104),
            new Vector3(1061.6602, -2275.6931, 30.36028),
            new Vector3(1201.6602, -2275.8256, 30.36028),
            new Vector3(1061.6602, -2135.6931, 30.18104),
            new Vector3(1061.6602, -2415.6931, 30.36028),
            new Vector3(921.6602, -2415.6931, 30.36028),
            new Vector3(921.6602, -2135.6931, 30.36028),
            new Vector3(921.6602, -1855.6931, 30.36028),
            new Vector3(921.6602, -1995.6931, 30.36028),
            new Vector3(921.6602, -1715.6931, 30.36028),
            new Vector3(921.6602, -1575.6931, 30.36028),
            new Vector3(921.6602, -2275.6931, 30.36028),
            new Vector3(921.6602, -1435.6931, 30.36028),
            new Vector3(781.6602, -2415.6931, 67.30145),
            new Vector3(781.6602, -2135.6931, 67.30145),
            new Vector3(781.6602, -1855.6931, 67.30145),
            new Vector3(781.6602, -1995.6931, 67.30145),
            new Vector3(781.6602, -1715.6931, 67.30145),
            new Vector3(781.6602, -1575.6931, 67.30145),
            new Vector3(781.6602, -2275.6931, 67.30145),
            new Vector3(781.6602, -1435.6931, 67.30145),
            new Vector3(511, -1315, 28.146452),
            new Vector3(511, -1455, 28.146452),
            new Vector3(511, -1595, 28.146452),
            new Vector3(511, -1735, 28.146452),
            new Vector3(511, -1875, 28.146452),
            new Vector3(511, -2015, 28.146452),
            new Vector3(511, -2155, 28.146452),
            new Vector3(371, -1315, 28.146452),
            new Vector3(371, -1455, 28.146452),
            new Vector3(371, -1595, 28.146452),
            new Vector3(371, -1735, 28.146452),
            new Vector3(371, -1875, 28.146452),
            new Vector3(371, -2015, 28.146452),
            new Vector3(371, -2155, 28.146452),
            new Vector3(231, -1315, 28.146452),
            new Vector3(231, -1455, 28.146452),
            new Vector3(231, -1595, 28.146452),
            new Vector3(231, -1735, 28.146452),
            new Vector3(231, -1875, 28.146452),
            new Vector3(231, -2015, 28.146452),
            new Vector3(231, -2155, 28.146452),
            new Vector3(91, -1455, 28.146452),
            new Vector3(91, -1595, 28.146452),
            new Vector3(91, -1735, 28.146452),
            new Vector3(91, -1875, 28.146452),
            new Vector3(91, -2015, 28.146452),
            new Vector3(91, -2155, 28.146452),
            new Vector3(-49, -1455, 28.146452),
            new Vector3(-49, -1595, 28.146452),
            new Vector3(-49, -1735, 28.146452),
            new Vector3(-49, -1875, 28.146452),
            new Vector3(-189, -1455, 28.146452),
            new Vector3(-189, -1595, 28.146452),
            new Vector3(-189, -1735, 28.146452),
        };

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                var result = MySQL.QueryRead("SELECT * FROM gangspoints");
                if (result == null || result.Rows.Count == 0) return;
                foreach (DataRow Row in result.Rows)
                {
                    var data = new GangPoint();
                    data.ID = Convert.ToInt32(Row["id"]);
                    data.GangOwner = Convert.ToInt32(Row["gangid"]);
                    data.IsCapture = false;

                    if (data.ID >= gangZones.Count) break;
                    gangPoints.Add(data.ID, data);
                }
                foreach (var gangpoint in gangPoints.Values)
                {
                    var colShape = NAPI.ColShape.Create2DColShape(gangZones[gangpoint.ID].X, gangZones[gangpoint.ID].Y, 100, 100, 0);
                    colShape.OnEntityEnterColShape += onPlayerEnterGangPoint;
                    colShape.OnEntityExitColShape += onPlayerExitGangPoint;
                    colShape.SetData("ID", gangpoint.ID);
                }
                CheckPoints();
            }
            catch (Exception e)
            {
                Log.Write("EXCEPTION AT\"FRACTIONS_CAPTURE\":\n" + e.ToString(), nLog.Type.Error);
            }
        }

        public static void CheckPoints()
        {
            foreach (var g in gangPoints.Values)
            {
                if (g.GangOwner == 1)
                {
                    FamiliesPoint++;
                }
                if (g.GangOwner == 2)
                {
                    BallasPoint++;
                }
                if (g.GangOwner == 3)
                {
                    VagosPoint++;
                }
                if (g.GangOwner == 4)
                {
                    MarabuntaPoint++;
                }
                if (g.GangOwner == 5)
                {
                    BloodsPoint++;
                }
            }
        }

        public static void CMD_startCapture(Player player)
        {
            if (!Manager.canUseCommand(player, "capture")) return;
            if (player.GetData<int>("GANGPOINT") == -1)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не находитесь ни на одном из регионов", 3000);
                return;
            }
            GangPoint region = gangPoints[player.GetData<int>("GANGPOINT")];
            if (region.GangOwner == Main.Players[player].FractionID)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не можете напасть на свою территорию", 3000);
                return;
            }
            if (DateTime.Now.Hour < 13 || DateTime.Now.Hour > 23)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы можете напасть только с 13:00 до 23:00", 3000);
                return;
            }
            if (DateTime.Now < nextCaptDate[Main.Players[player].FractionID])
            {
                DateTime g = new DateTime((nextCaptDate[Main.Players[player].FractionID] - DateTime.Now).Ticks);
                var min = g.Minute;
                var sec = g.Second;
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы сможете начать захват только через {min}:{sec}", 3000);
                return;
            }
            if (DateTime.Now < protectDate[region.GangOwner])
            {
                DateTime g = new DateTime((protectDate[region.GangOwner] - DateTime.Now).Ticks);
                var min = g.Minute;
                var sec = g.Second;
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы сможете начать захват территории этой банды только через {min}:{sec}", 3000);
                return;
            }
            if (Manager.countOfFractionMembers(region.GangOwner) < 5)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Недостаточный онлайн в банде противников", 3000);
                return;
            }
            if (smbTryCapture) return;
            smbTryCapture = true;
            if (captureStarting || captureIsGoing)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Захват территории уже идёт", 3000);
                smbTryCapture = false;
                return;
            }

            timerCount = 0;
            timerExitCountDef = 0;
            timerExitCountAt = 0;
            region.IsCapture = true;
            attackersFracID = Main.Players[player].FractionID;
            defendersFracID = region.GangOwner;
            captureRegion = new Vector3(gangZones[region.ID].X, gangZones[region.ID].Y, gangZones[region.ID].Z);

            //toStartCaptureTimer = Main.StartT(900000, 9999999, (o) => timerStartCapture(region), "CAPTURESTART_TIMER");
            //toStartCaptureTimer = Timers.StartOnce(900000, () => timerStartCapture(region));
            toStartCaptureTimer = Timers.StartOnce(10000, () => timerStartCapture(region));
            Main.ClientEventToAll("setZoneFlash", region.ID, true, gangPointsColor[region.GangOwner]);

            captureStarting = true;
            smbTryCapture = false;

            Manager.sendFractionMessage(region.GangOwner, $"{Manager.getName(attackersFracID)} решили отхватить нашу территорию. У вас 15 минут на сборы");
            Manager.sendFractionMessageChat(region.GangOwner, $"{Manager.getName(attackersFracID)} решили отхватить нашу территорию. У вас 15 минут на сборы");
            Manager.sendFractionMessage(attackersFracID, "Вы начали войну за Территорию. У вас 15 минут на подготовку");
            Manager.sendFractionMessageChat(attackersFracID, "Вы начали войну за Территорию. У вас 15 минут на подготовку");
        }

        private static void timerStartCapture(GangPoint region)
        {
            var attackers = 0;
            var defenders = 0;

            foreach (var p in NAPI.Pools.GetAllPlayers())
            {
                if (!Main.Players.ContainsKey(p) || !p.HasData("GANGPOINT") || p.GetData<int>("GANGPOINT") != region.ID) continue;
                if (Main.Players[p].FractionID == region.GangOwner) defenders++;
                if (Main.Players[p].FractionID == attackersFracID) attackers++;
            }
            foreach (var p in NAPI.Pools.GetAllPlayers())
            {
                if (!Main.Players.ContainsKey(p) || !p.HasData("GANGPOINT") || p.GetData<int>("GANGPOINT") != region.ID) continue;
                if (Main.Players[p].FractionID == region.GangOwner || Main.Players[p].FractionID == attackersFracID)
                {
                    Trigger.ClientEvent(p, "sendCaptureInformation", attackers, defenders, 0, 0, region.GangOwner, attackersFracID);
                    Trigger.ClientEvent(p, "captureHud", true);
                    p.SetData("CAPTURE", true);
                }
            }

            captureIsGoing = true;
            captureStarting = false;

            captureTimer = Timers.Start(1000, () => timerUpdate(region, region.ID));

            Manager.sendFractionMessage(region.GangOwner, $"На вашу территорию напали {Manager.getName(attackersFracID)}");
            Manager.sendFractionMessageChat(region.GangOwner, $"На вашу территорию напали {Manager.getName(attackersFracID)}");
            Manager.sendFractionMessageChat(attackersFracID, "Вы начали войну за территорию");
            Manager.sendFractionMessage(attackersFracID, "Вы начали войну за территорию");
        }
        [ServerEvent(Event.PlayerDeath)]
        public void onPlayerDeathHandler(Player player, Player killer, uint weapon)
        {
            try
            {
                if (captureIsGoing)
                {
                    if (Convert.ToString(killer) != "")
                    {
                        if (Main.Players[player].FractionID == attackersFracID && Main.Players[killer].FractionID == defendersFracID || Main.Players[player].FractionID == defendersFracID && Main.Players[killer].FractionID == attackersFracID)
                        {
                            if (player.Position.DistanceTo(captureRegion) < 60 && killer.Position.DistanceTo(captureRegion) < 60)
                            {
                                var deadplayerName = $"{Main.Players[player].FirstName}_{Main.Players[player].LastName}";
                                var killerName = $"{Main.Players[killer].FirstName}_{Main.Players[killer].LastName}";
                                string frac1 = "fraction" + Main.Players[killer].FractionID;
                                string frac2 = "fraction" + Main.Players[player].FractionID;
                                object data = new
                                {
                                    killer = killerName,
                                    frac1 = frac1,
                                    deadplayerName = deadplayerName,
                                    frac2 = frac2,
                                    weapon = weapon
                                };
                                //Console.WriteLine(weapon);
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (!Main.Players.ContainsKey(p) || !p.HasData("GANGPOINT")) continue;
                                    Trigger.ClientEvent(p, "sendkillinfo", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Main.Players[player].FractionID == attackersFracID || Main.Players[player].FractionID == defendersFracID)
                        {
                            if (player.Position.DistanceTo(captureRegion) < 60)
                            {
                                string frac2 = "fraction" + Main.Players[player].FractionID;
                                var deadplayerName = $"{Main.Players[player].FirstName}_{Main.Players[player].LastName}";
                                object data = new
                                {
                                    killer = deadplayerName,
                                    frac1 = frac2,
                                    deadplayerName = "null",
                                    frac2 = "null",
                                    weapon = weapon
                                };
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (!Main.Players.ContainsKey(p) || !p.HasData("GANGPOINT")) continue;
                                    Trigger.ClientEvent(p, "sendkillinfo", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) { Log.Write("captureDeath: " + e.Message, nLog.Type.Error); }
        }
        private static void timerUpdate(GangPoint region, int id)
        {
            try
            {
                var attackers = 0;
                var defenders = 0;

                var allplayers = Main.Players.Keys.ToList();
                foreach (var p in allplayers)
                {
                    if (!Main.Players.ContainsKey(p) || !p.HasData("CAPTURE") || !p.GetData<bool>("CAPTURE") || !p.HasData("GANGPOINT") || p.GetData<int>("GANGPOINT") != region.ID) continue;
                    if (Main.Players[p].FractionID == region.GangOwner) defenders++;
                    if (Main.Players[p].FractionID == attackersFracID) attackers++;
                }

                attackersSt = attackers;
                defendersSt = defenders;
                if (!defendersWas && defenders != 0)
                    defendersWas = true;
                if (!attackersWas && defendersWas)
                    attackersWas = true;

                if (defenders != 0) timerExitCountDef = 0;
                if (attackers != 0) timerExitCountAt = 0;

                if (defendersWas && defenders == 0)
                {
                    timerExitCountDef++;
                    if (timerExitCountDef >= 60)
                    {
                        endCapture(region, 0, 1);
                        return;
                    }
                }
                if (attackersWas && attackers == 0)
                {
                    timerExitCountAt++;
                    if (timerExitCountAt >= 60)
                    {
                        endCapture(region, 1, 0);
                        return;
                    }
                }

                if (timerCount >= 600 && !defendersWas)
                    endCapture(region, defenders, attackers);

                timerCount++;
                foreach (var p in NAPI.Pools.GetAllPlayers())
                {
                    if (!Main.Players.ContainsKey(p) || !p.HasData("GANGPOINT") || p.GetData<int>("GANGPOINT") != region.ID) continue;
                    if (Main.Players[p].FractionID == region.GangOwner || Main.Players[p].FractionID == attackersFracID)
                    {
                        int minutes = timerCount / 60;
                        int seconds = timerCount % 60;
                        Trigger.ClientEvent(p, "sendCaptureInformation", attackers, defenders, minutes, seconds, region.GangOwner, attackersFracID);
                    }
                }
            }
            catch (Exception e) { Log.Write("GangCapture: " + e.Message, nLog.Type.Error); }
        }

        private static void endCapture(GangPoint region, int defenders, int attackers)
        {
            //Main.StopT(captureTimer, "endCapture_gangcapture");
            Timers.Stop(captureTimer);
            NAPI.Task.Run(() => Main.ClientEventToAll("captureHud", false));
            protectDate[region.GangOwner] = DateTime.Now.AddMinutes(20);
            protectDate[attackersFracID] = DateTime.Now.AddMinutes(20);
            if (attackers <= defenders)
            {
                Manager.sendFractionMessage(region.GangOwner, $"Вы захватили территорию");
                Manager.sendFractionMessageChat(region.GangOwner, $"Вы захватили территорию");
                Manager.sendFractionSound(region.GangOwner, "wincapt");
                Manager.sendFractionMessageChat(attackersFracID, "Вы проиграли войну за Территорию");
                Manager.sendFractionSound(attackersFracID, "losecapt");
                Manager.sendFractionMessage(attackersFracID, "Вы проиграли войну за Территорию");
                foreach (var m in Manager.Members.Keys)
                {
                    if (Main.Players[m].FractionID == region.GangOwner)
                    {
                        MoneySystem.Wallet.Change(m, 10000000);
                        GameLog.Money($"server", $"player({Main.Players[m].UUID})", 300, $"winCapture");
                    }
                }
            }
            else if (attackers > defenders)
            {
                Manager.sendFractionMessage(region.GangOwner, $"Вы проиграли войну за Территорию");
                Manager.sendFractionMessageChat(region.GangOwner, $"Вы проиграли войну за Территорию");
                Manager.sendFractionSound(region.GangOwner, "losecapt");
                Manager.sendFractionMessageChat(attackersFracID, "Вы захватили территорию");
                Manager.sendFractionMessage(attackersFracID, "Вы захватили территорию");
                Manager.sendFractionSound(attackersFracID, "wincapt");
                region.GangOwner = attackersFracID;
                foreach (var m in Manager.Members.Keys)
                {
                    if (Main.Players[m].FractionID == attackersFracID)
                    {
                        MoneySystem.Wallet.Change(m, 10000000);
                        GameLog.Money($"server", $"player({Main.Players[m].UUID})", 300, $"winCapture");
                    }
                }
            }
            DateTime nextcapt = DateTime.Now.AddMinutes(120);
            nextCaptDate[attackersFracID] = nextcapt;
            region.IsCapture = false;
            captureIsGoing = false;
            NAPI.Task.Run(() =>
            {
                Main.ClientEventToAll("setZoneFlash", region.ID, false);
                Main.ClientEventToAll("setZoneColor", region.ID, gangPointsColor[region.GangOwner]);
            });
            CheckPoints();
        }

        private static void onPlayerEnterGangPoint(ColShape shape, Player player)
        {
            try
            {
                if (!Main.Players.ContainsKey(player)) return;
                if ((Main.Players[player].FractionID >= 1 && Main.Players[player].FractionID <= 5) || Main.Players[player].AdminLVL > 0)
                {
                    player.SetData("GANGPOINT", (int)shape.GetData<int>("ID"));
                    GangPoint region = gangPoints[(int)shape.GetData<int>("ID")];
                    if (region.IsCapture && captureIsGoing && (Main.Players[player].FractionID == attackersFracID || Main.Players[player].FractionID == region.GangOwner))
                    {
                        int minutes = timerCount / 60;
                        int seconds = timerCount % 60;
                        Trigger.ClientEvent(player, "sendCaptureInformation", attackersSt, defendersSt, minutes, seconds, region.GangOwner, attackersFracID);
                        Trigger.ClientEvent(player, "captureHud", true);
                        player.SetData("CAPTURE", true);
                    }
                }
            }
            catch (Exception ex) { Log.Write("onPlayerEnterGangPoint: " + ex.Message, nLog.Type.Error); }
        }

        private static void onPlayerExitGangPoint(ColShape shape, Player player)
        {
            try
            {
                if (!Main.Players.ContainsKey(player)) return;
                if ((Main.Players[player].FractionID >= 1 && Main.Players[player].FractionID <= 5) || Main.Players[player].AdminLVL > 0)
                {
                    //Log.Write($"Gangsta {player.Name} exited gangPoint");
                    if (shape.GetData<int>("ID") == player.GetData<int>("GANGPOINT"))
                        player.SetData("GANGPOINT", -1);

                    GangPoint region = gangPoints[(int)shape.GetData<int>("ID")];
                    if (region.IsCapture && (Main.Players[player].FractionID == attackersFracID || Main.Players[player].FractionID == region.GangOwner))
                        Trigger.ClientEvent(player, "captureHud", false);
                    player.ResetData("CAPTURE");
                }
            }
            catch (Exception ex) { Log.Write("onPlayerExitGangPoint: " + ex.Message, nLog.Type.Error); }
        }

        public static void SavingRegions()
        {
            foreach (var region in gangPoints.Values)
                MySQL.Query($"UPDATE gangspoints SET gangid={region.GangOwner} WHERE id={region.ID}");
            Log.Write("Gang Regions has been saved to DB", nLog.Type.Success);
        }

        public static void LoadBlips(Player player)
        {
            if (Manager.FractionTypes[Main.Players[player].FractionID] != 1 && Main.Players[player].AdminLVL <= 0) return;
            var colors = new List<int>();
            foreach (var g in gangPoints.Values)
                colors.Add(gangPointsColor[g.GangOwner]);

            Trigger.ClientEvent(player, "loadCaptureBlips", Newtonsoft.Json.JsonConvert.SerializeObject(colors));

            if (captureIsGoing || captureStarting) Trigger.ClientEvent(player, "setZoneFlash", gangPoints.FirstOrDefault(g => g.Value.IsCapture == true).Value.ID, true);
        }
        public static void UnLoadBlips(Player player)
        {
            if (Main.Players[player].AdminLVL > 0 || Manager.FractionTypes[Main.Players[player].FractionID] == 1) return;
            Trigger.ClientEvent(player, "unloadCaptureBlips");
        }

        [ServerEvent(Event.ResourceStop)]
        public void OnResourceStop()
        {
            try
            {
                SavingRegions();
            }
            catch (Exception e) { Log.Write("ResourceStop: " + e.Message, nLog.Type.Error); }
        }

        public class GangPoint
        {
            public int ID { get; set; }
            public int GangOwner { get; set; }
            public bool IsCapture { get; set; }
        }
    }
}
