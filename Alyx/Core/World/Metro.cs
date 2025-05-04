using System;
using GTANetworkAPI;
using AlyxSDK;

namespace Alyx.Core
{
    class Metro : Script
    {
        private static nLog Log = new nLog("Metro");
        #region Musor
        private static ColShape colShape;
        private static ColShape colShape2;
        private static ColShape colShape3;
        private static ColShape colShape4;
        private static ColShape colShape5;
        private static ColShape colShape6;
        private static ColShape colShape7;
        private static ColShape colShape8;
        private static ColShape colShape9;
        private static ColShape colShape10;
        private static int price = 50;
        public static Vector3[] MetroPositions = new Vector3[]
        {
            new Vector3(-1083.1515, -2721.0405, -8.530131),  //1
            new Vector3(-880.7977, -2311.3948, -12.852799),     //2
            new Vector3(-533.6171, -1267.0789, 25.78159),          //3
            new Vector3(228.90434, -1204.3397, 37.782658),            //4
            new Vector3(-298.34442, -332.07004, 8.943094),               //6
            new Vector3(-815.8392, -134.33536, 18.8303),          //7
            new Vector3(-1355.7189, -465.03162, 13.925318),          //8
            new Vector3(-502.79358, -676.7152, 10.688962),               //9
            new Vector3(-207.57571, -1017.8509, 29.018295),                 //10
            new Vector3(102.25202, -1714.7284, 28.992487),                     //11
        };
        public static Vector3[] MetroPositions2 = new Vector3[]
        {
            new Vector3(-1083.1515, -2721.0405, -8.530131),  //1
            new Vector3(-880.7977, -2311.3948, -12.852799),     //2
            new Vector3(-533.6171, -1267.0789, 25.78159),          //3
            new Vector3(228.90434, -1204.3397, 37.782658),            //4
            new Vector3(228.90434, -1204.3397, -10000.782658),            //5
            new Vector3(-298.34442, -332.07004, 8.943094),               //6
            new Vector3(-815.8392, -134.33536, 18.8303),          //7
            new Vector3(-1355.7189, -465.03162, 13.925318),          //8
            new Vector3(-502.79358, -676.7152, 10.688962),               //9
            new Vector3(-207.57571, -1017.8509, 29.018295),                 //10
            new Vector3(102.25202, -1714.7284, 28.992487),                     //11
        };

        public static Vector3[] MetroPositionTrain = new Vector3[]
       {
            new Vector3(-1081.309, -2725.259, -7.137033),
            new Vector3(-876.7512, -2323.808, -11.45609),
            new Vector3(-536.8082, -1286.096, 27.08238),
            new Vector3(270.2029, -1210.818, 39.25398),
            new Vector3(228.90434, -1204.3397, -10000.782658),            //4
            new Vector3(-287.01, -319.07, 8.06),
            new Vector3(-818.40, -130.52, 17.93),
            new Vector3(-1359.0338, -467.11923, 13.034843),
            new Vector3(-502.91434, -680.62, 9.79835),
            new Vector3(-217.25, -1030.57, 28.20),
            new Vector3(111.31, -1727.83, 27.93),
       };
        [ServerEvent(Event.ResourceStart)]
        public static void OnResourceStart()
        {
            try
            {
                foreach (Vector3 vec in MetroPositions)
                {
                    NAPI.Marker.CreateMarker(1, vec + new Vector3(0, 0, 0), new Vector3(), new Vector3(), 0.9f, new Color(67, 140, 239, 200), false, 0);
                }
                //NAPI.Blip.CreateBlip(36, MetroPositions[2], 0.7f, 4, "Станция метро: Puerto Del Sol", 255, 0, true, 0, 0);
                //NAPI.Blip.CreateBlip(36, MetroPositions[3], 0.7f, 4, "Станция метро: Strawberry", 255, 0, true, 0, 0);
                //NAPI.Blip.CreateBlip(36, MetroPositions[8], 0.7f, 4, "Станция метро: Pillbox South", 255, 0, true, 0, 0);
                //NAPI.Blip.CreateBlip(36, MetroPositions[9], 0.7f, 4, "Станция метро: Davis", 255, 0, true, 0, 0);
                var colShapeTrain = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[0], 5, 3);
                var colShapeTrain1 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[1], 5, 3);
                var colShapeTrain2 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[2], 5, 3);
                var colShapeTrain3 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[3], 5, 3);
                var colShapeTrain5 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[5], 5, 3);
                var colShapeTrain6 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[6], 5, 3);
                var colShapeTrain7 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[7], 5, 3);
                var colShapeTrain8 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[8], 5, 3);
                var colShapeTrain9 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[9], 5, 3);
                var colShapeTrain10 = NAPI.ColShape.CreateCylinderColShape(MetroPositionTrain[10], 5, 3);

                colShapeTrain.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 0)
                        {
                            MetroCutsceneStop(ent, 0);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain1.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 1)
                        {
                            MetroCutsceneStop(ent, 1);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain2.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 2)
                        {
                            MetroCutsceneStop(ent, 2);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain3.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 3)
                        {
                            MetroCutsceneStop(ent, 3);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain5.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 5)
                        {
                            MetroCutsceneStop(ent, 5);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain6.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 6)
                        {
                            MetroCutsceneStop(ent, 6);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain7.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 7)
                        {
                            MetroCutsceneStop(ent, 7);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain8.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 8)
                        {
                            MetroCutsceneStop(ent, 8);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain9.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 9)
                        {
                            MetroCutsceneStop(ent, 9);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShapeTrain10.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        if (ent.GetData<int>("GoToMetro") == 10)
                        {
                            MetroCutsceneStop(ent, 10);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };

                colShape = NAPI.ColShape.CreateCylinderColShape(MetroPositions[0], 2, 2, 0);
                colShape.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 921);
                        NAPI.Data.SetEntityData(ent, "InMetro", 0);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape2 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[1], 2, 2, 0);
                colShape2.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 922);
                        NAPI.Data.SetEntityData(ent, "InMetro", 1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape2.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape3 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[2], 2, 2, 0);
                colShape3.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 923);
                        NAPI.Data.SetEntityData(ent, "InMetro", 2);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape3.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape4 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[3], 2, 2, 0);
                colShape4.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 924);
                        NAPI.Data.SetEntityData(ent, "InMetro", 3);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape4.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape5 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[4], 2, 2, 0);
                colShape5.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 925);
                        NAPI.Data.SetEntityData(ent, "InMetro", 5);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape5.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape6 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[5], 2, 2, 0);
                colShape6.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 926);
                        NAPI.Data.SetEntityData(ent, "InMetro", 6);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape6.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape7 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[6], 2, 2, 0);
                colShape7.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 927);
                        NAPI.Data.SetEntityData(ent, "InMetro", 7);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape7.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape8 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[7], 2, 2, 0);
                colShape8.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 928);
                        NAPI.Data.SetEntityData(ent, "InMetro", 8);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape8.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape9 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[8], 2, 2, 0);
                colShape9.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 929);
                        NAPI.Data.SetEntityData(ent, "InMetro", 9);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape9.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                colShape10 = NAPI.ColShape.CreateCylinderColShape(MetroPositions[9], 2, 2, 0);
                colShape10.OnEntityEnterColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 930);
                        NAPI.Data.SetEntityData(ent, "InMetro", 10);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                colShape10.OnEntityExitColShape += (s, ent) =>
                {
                    try
                    {
                        NAPI.Data.SetEntityData(ent, "INTERACTIONCHECK", 0);
                        NAPI.Data.SetEntityData(ent, "InMetro", -1);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                Log.Write("Loaded", nLog.Type.Info);
            }
            catch (Exception e) { Log.Write(e.ToString(), nLog.Type.Error); }
        }
        #endregion

        [RemoteEvent("server::buymetro")]
        public static void BuyMetro(Player player, int travel)
        {
            if (travel == 0)
            {
                Notify.Error(player, "Вы не выбрали станцию");
                return;
            }
            if (player.GetData<int>("GoToMetro") != -1)
            {
                Notify.Error(player, "Вы уже в поезде");
                return;
            }
            if (travel == 5)
            {
                Notify.Error(player, "Эта станция недоступна!");
                return;
            }
            else
            {
                if (Main.Players[player].Money < price)
                {
                    Notify.Error(player, "Недостаточно средств");
                    return;
                }
                if (player.GetData<int>("InMetro") == -1)
                {
                    Notify.Error(player, "Вы не на точке");
                    return;
                }
                MetroCutscene(player, travel - 1);
            }
        }
        public static void MetroCutsceneStop(Player player, int id)
        {
            Trigger.ClientEvent(player, "speedtrain", 0);
            Trigger.ClientEvent(player, "showHUD", true);
            Trigger.ClientEvent(player, "ShowMetroHelp", id);
            Trigger.ClientEvent(player, "MetroStateStopChange", true);
        }

        [RemoteEvent("ExitMetroServer")]
        public static void MetroCutsceneStopEvent(Player player, int id)
        {
            if (player.GetData<int>("GoToMetro") != -1)
            {
                Trigger.ClientEvent(player, "showHUD", false);
                Trigger.ClientEvent(player, "MetroStateStopChange", false);
                Trigger.ClientEvent(player, "ShowMetroHelp", -1);
                Trigger.ClientEvent(player, "screenFadeOut", 1000);
                NAPI.Task.Run(() =>
                {
                    try
                    {
                        if (player != null)
                        {
                            Trigger.ClientEvent(player, "destroytrain");
                            NAPI.Entity.SetEntityPosition(player, MetroPositions2[id] + new Vector3(0, 0, 1.2));
                            player.SetData("GoToMetro", -1);
                        }
                    }
                    catch { }
                }, 1100);
                NAPI.Task.Run(() =>
                {
                    try
                    {
                        if (player != null)
                        {
                            Trigger.ClientEvent(player, "screenFadeIn", 1000);
                            Trigger.ClientEvent(player, "showHUD", true);
                            NAPI.Entity.SetEntityDimension(player, 0);
                            Trigger.ClientEvent(player, "freeze", false);
                        }
                    }
                    catch { }
                }, 1200);
            }
        }
        [ServerEvent(Event.PlayerDisconnected)]
        public void Event_OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            try
            {
                if (player.GetData<int>("GoToMetro") != -1)
                {
                    Trigger.ClientEvent(player, "destroytrain");
                }
            }
            catch (Exception e) { Log.Write("PlayerDisconnected: " + e.Message, nLog.Type.Error); }
        }
        public static void MetroCutscene(Player player, int index)
        {
            Trigger.ClientEvent(player, "showHUD", false);
            Trigger.ClientEvent(player, "screenFadeOut", 1000);
            player.SetData("GoToMetro", index);
            //Notify.Succ(player, $"{index}");
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (player != null)
                    {
                        Trigger.ClientEvent(player, "createtrain", MetroPositionTrain[player.GetData<int>("InMetro")]);
                    }
                }
                catch { }
            }, 800);
            NAPI.Task.Run(() => {
                try
                {
                    if (player != null)
                    {
                        NAPI.Entity.SetEntityPosition(player, MetroPositionTrain[player.GetData<int>("InMetro")] + new Vector3(0, 0, 1));
                        Trigger.ClientEvent(player, "showHUD", false);
                        NAPI.Entity.SetEntityDimension(player, (uint)(5000 + player.Value));
                    }
                }
                catch { }
            }, 1300);
            NAPI.Task.Run(() => {
                try
                {
                    if (player != null)
                    {
                        MoneySystem.Wallet.Change(player, -price);
                        Trigger.ClientEvent(player, "freeze", true);
                    }
                }
                catch { }
            }, 1700);
        }
    }
}
