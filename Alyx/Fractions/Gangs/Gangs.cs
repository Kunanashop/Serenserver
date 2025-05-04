using System.Collections.Generic;
using GTANetworkAPI;
using Alyx.Core;
using AlyxSDK;
using System;
using static Alyx.Core.Quests;

namespace Alyx.Fractions
{
    class Gangs : Script
    {
        private static nLog Log = new nLog("Gangs");

        public static Dictionary<int, Vector3> EnterPoints = new Dictionary<int, Vector3>()
        {
            { 1, new Vector3(-223.069, -1616.918, 33.74932) },
            { 2, new Vector3(85.79318, -1958.851, 20.0017) },
            { 3, new Vector3(1408.579, -1486.897, 59.53736) },
            { 4, new Vector3(892.2407, -2172.888, 31.16626) },
            { 5, new Vector3(485.3334, -1528.692, 28.18008) },
        };
        public static Dictionary<int, Vector3> ExitPoints = new Dictionary<int, Vector3>()
        {
            { 1, new Vector3(-201.7147, -1627.962, -38.664788) },
            { 2, new Vector3(82.57095, -1958.607, -23.41236) },
            { 3, new Vector3(1420.487, -1497.264, -107.8639) },
            { 4, new Vector3(892.4592, -2168.068, 0.921189) },
            { 5, new Vector3(484.9963, -1536.083, -33.22089) },
        };

        public static List<Vector3> DrugPoints = new List<Vector3>()
        {
            new Vector3(8.621573, 3701.914, 39.51624),
            new Vector3(3804.169, 4444.753, 3.977164),
        };

        [ServerEvent(Event.ResourceStart)]
        public void Event_OnResourceStart()
        {
            try
            {
                var Ballasped = NAPI.ColShape.CreateCylinderColShape(new Vector3(-96.057335, -1797.3448, 27.87421), 1, 2, 0);
                Ballasped.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 934); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                Ballasped.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var Groove = NAPI.ColShape.CreateCylinderColShape(new Vector3(-255.18102, -1520.1024, 30.440317), 1, 2, 0);
                Groove.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 935); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                Groove.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var Vagos = NAPI.ColShape.CreateCylinderColShape(new Vector3(976.22015, -1834.1323, 34.987488), 1, 2, 0);
                Vagos.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 936); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                Vagos.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var Crips = NAPI.ColShape.CreateCylinderColShape(new Vector3(1435.455, -1491.4196, 62.4917), 1, 2, 0);
                Crips.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 937); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                Crips.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var Bloods = NAPI.ColShape.CreateCylinderColShape(new Vector3(472.02426, -1308.6912, 28.115564), 1, 2, 0);
                Bloods.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 938); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                Bloods.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var BallasCraft = NAPI.ColShape.CreateCylinderColShape(new Vector3(-76.67501, -1812.6608, 19.68730), 1, 2, 0);
                BallasCraft.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 941); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                BallasCraft.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var GrooveCraft = NAPI.ColShape.CreateCylinderColShape(new Vector3(-240.0167, -1496.9425, 26.852406), 1, 2, 0);
                GrooveCraft.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 940); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                GrooveCraft.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var VagosCraft = NAPI.ColShape.CreateCylinderColShape(new Vector3(961.9523, -1841.8513, 30.159239), 1, 2, 0);
                VagosCraft.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 942); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                VagosCraft.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var CripsCraft = NAPI.ColShape.CreateCylinderColShape(new Vector3(1443.818, -1492.9783, 66.53294), 1, 2, 0);
                CripsCraft.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 943); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                CripsCraft.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };

                var BloodsCraft = NAPI.ColShape.CreateCylinderColShape(new Vector3(515.3959, -1340.065, 28.253294), 1, 2, 0);
                BloodsCraft.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 944); } catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); } };
                BloodsCraft.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); } };



            }
            catch (Exception e) { Log.Write("ResourceStart: " + e.Message, nLog.Type.Error); }
        }

        public static void InteractPressed(Player player, int id)
        {
            if (!Main.Players.ContainsKey(player)) return;
            if (player.IsInVehicle) return;
            switch (id) {
                case 934:
                    Trigger.ClientEvent(player, "NPC.cameraOn", "Ballas", 1000);
                    if (Main.Players[player].FractionID != 2)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дарнелл Баллас", "Гангстер", "Привет, думаю лучше тебе тут не появляться, потому что ты на чужой территори", (new QuestAnswer("Понял", 2), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дарнелл Баллас", "Гангстер", "Привет, как день прошел, чем могу быть полезен для тебя сегодня?", (new QuestAnswer("Взять контракт на авто", 101), new QuestAnswer("Взять контракт на дом", 102), new QuestAnswer("Отмыть деньги", 103), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 935:
                    Trigger.ClientEvent(player, "NPC.cameraOn", "Groove", 1000);
                    if (Main.Players[player].FractionID != 1)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Фрэнк Грув", "Гангстер", "Привет, думаю лучше тебе тут не появляться, потому что ты на чужой территори", (new QuestAnswer("Понял", 2), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Фрэнк Грув", "Гангстер", "Привет, как день прошел, чем могу быть полезен для тебя сегодня?", (new QuestAnswer("Взять контракт на авто", 101), new QuestAnswer("Взять контракт на дом", 102), new QuestAnswer("Отмыть деньги", 103), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 936:
                    Trigger.ClientEvent(player, "NPC.cameraOn", "Vagos", 1000);
                    if (Main.Players[player].FractionID != 3)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эмма Вагос", "Гангстер", "Привет, думаю лучше тебе тут не появляться, потому что ты на чужой территори", (new QuestAnswer("Понял", 2), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эмма Вагос", "Гангстер", "Привет, как день прошел, чем могу быть полезен для тебя сегодня?", (new QuestAnswer("Взять контракт на авто", 101), new QuestAnswer("Взять контракт на дом", 102), new QuestAnswer("Отмыть деньги", 103), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 937:
                    Trigger.ClientEvent(player, "NPC.cameraOn", "Crips", 1000);
                    if (Main.Players[player].FractionID != 4)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Адамай Крипс", "Гангстер", "Привет, думаю лучше тебе тут не появляться, потому что ты на чужой территори", (new QuestAnswer("Понял", 2), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Адамай Крипс", "Гангстер", "Привет, как день прошел, чем могу быть полезен для тебя сегодня?", (new QuestAnswer("Взять контракт на авто", 101), new QuestAnswer("Взять контракт на дом", 102), new QuestAnswer("Отмыть деньги", 103), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 938:
                    Trigger.ClientEvent(player, "NPC.cameraOn", "Bloods", 1000);
                    if (Main.Players[player].FractionID != 5)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Мигель Бладс", "Гангстер", "Привет, думаю лучше тебе тут не появляться, потому что ты на чужой территори", (new QuestAnswer("Понял", 2), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Мигель Бладс", "Гангстер", "Привет, как день прошел, чем могу быть полезен для тебя сегодня?", (new QuestAnswer("Взять контракт на авто", 101), new QuestAnswer("Взять контракт на дом", 102), new QuestAnswer("Отмыть деньги", 103), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
            }
        }
        public static void OpenCraftGang(Player player, int id)
        {
            if (Main.Players[player].FractionID != id)
            {
                Notify.Error(player, "Вы не состоите в этой фракции");
                return;
            }
            if (!Stocks.fracStocks[Main.Players[player].FractionID].IsOpen)
            {
                Notify.Error(player, "Склад закрыт");
                return;
            }
            Trigger.ClientEvent(player, "OpenStock_GANG");

        }
        [RemoteEvent("GangBuyGuns")]
        public static void callback_gangguns(Player player, int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        if (Main.Players[player].FractionLVL < Main.Players[player].FractionID)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 120)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd = nInventory.TryAdd(player, new nItem(ItemType.Pistol, 1));
                        if (tryAdd == -1 || tryAdd > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 120;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        Weapons.GiveWeapon(player, ItemType.Pistol, Weapons.GetSerialFrac(true, Main.Players[player].FractionID));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Пистолет");
                        return;
                    case 1:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 150)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd1 = nInventory.TryAdd(player, new nItem(ItemType.CompactRifle, 1));
                        if (tryAdd1 == -1 || tryAdd1 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 150;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        Weapons.GiveWeapon(player, ItemType.CompactRifle, Weapons.GetSerialFrac(true, Main.Players[player].FractionID));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Компактную винтовку");
                        return;
                    case 2:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 200)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd2 = nInventory.TryAdd(player, new nItem(ItemType.AssaultRifle, 1));
                        if (tryAdd2 == -1 || tryAdd2 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 200;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        Weapons.GiveWeapon(player, ItemType.AssaultRifle, Weapons.GetSerialFrac(true, Main.Players[player].FractionID));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Штурмовую Винтовку");
                        return;
                    case 3:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 50)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd4 = nInventory.TryAdd(player, new nItem(ItemType.PistolAmmo2, 50));
                        if (tryAdd4 == -1 || tryAdd4 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 50;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        nInventory.Add(player, new nItem(ItemType.PistolAmmo2, 50));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Патроны 9x19mm");
                        return;
                    case 4:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 50)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd5 = nInventory.TryAdd(player, new nItem(ItemType.RiflesAmmo, 50));
                        if (tryAdd5 == -1 || tryAdd5 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 50;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        nInventory.Add(player, new nItem(ItemType.RiflesAmmo, 50));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Патроны 62x39mm");
                        return;
                    case 5:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 50)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd7 = nInventory.TryAdd(player, new nItem(ItemType.HeavyRiflesAmmo, 50));
                        if (tryAdd7 == -1 || tryAdd7 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 50;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        nInventory.Add(player, new nItem(ItemType.HeavyRiflesAmmo, 50));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Патроны 12ga Rifles");
                        return;
                    case 6:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 100)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd9 = nInventory.TryAdd(player, new nItem(ItemType.BodyArmor, 1));
                        if (tryAdd9 == -1 || tryAdd9 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 100;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        nInventory.Add(player, new nItem(ItemType.BodyArmor, 1, 50.ToString()));
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы получили бронежилет", 3000);
                        return;
                    case 7:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 200)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd10 = nInventory.TryAdd(player, new nItem(ItemType.Gusenberg, 1));
                        if (tryAdd10 == -1 || tryAdd10 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 200;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        Weapons.GiveWeapon(player, ItemType.Gusenberg, Weapons.GetSerialFrac(true, Main.Players[player].FractionID));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Автомат Томпсона");
                        return;
                    case 8:
                        if (Main.Players[player].FractionLVL < 3)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к этому предмету", 3000);
                            return;
                        }
                        if (Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials < 130)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно материалов на складе", 3000);
                            return;
                        }
                        var tryAdd11 = nInventory.TryAdd(player, new nItem(ItemType.Revolver, 1));
                        if (tryAdd11 == -1 || tryAdd11 > 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials -= 130;
                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].UpdateLabel();
                        Weapons.GiveWeapon(player, ItemType.Revolver, Weapons.GetSerialFrac(true, Main.Players[player].FractionID));
                        Trigger.ClientEvent(player, "acguns");
                        Notify.Succ(player, "Вы взяли Револьвер");
                        return;
                }
            }
            catch (Exception e) { Log.Write("Fbigun: " + e.Message, nLog.Type.Error); }
        }
    }
}
