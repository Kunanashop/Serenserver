using System.Collections.Generic;
using GTANetworkAPI;
using Alyx.Core;
using AlyxSDK;
using System;
using Alyx.GUI;
using static Alyx.Core.Quests;

namespace Alyx.Fractions
{
    class Ems : Script
    {
        private static nLog Log = new nLog("EMS");
        public static int HumanMedkitsLefts = 100;
        public static int PriceForHealth = 100;

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                var emsped = NAPI.ColShape.CreateCylinderColShape(new Vector3(308.86157, -592.5301, 42.164062), 1, 2, 0);
                emsped.OnEntityEnterColShape += (shape, player) =>
                {
                    try
                    {
                        player.SetData("INTERACTIONCHECK", 914);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityEnterColShape: " + ex.Message); }
                };
                emsped.OnEntityExitColShape += (shape, player) =>
                {
                    try
                    {
                        player.SetData("INTERACTIONCHECK", 0);
                    }
                    catch (Exception ex) { Console.WriteLine("shape.OnEntityExitColShape: " + ex.Message); }
                };

                #region cols
                // enter ems LS
                var col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[0], 1, 2, 0);
                col.SetData("INTERACT", 15);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
               // NAPI.Marker.CreateMarker(21, emsCheckpoints[0] + new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 0.8f, new Color(255, 255, 255, 60));

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[1], 1, 2, 0); // exit ems
                col.SetData("INTERACT", 16);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
               // NAPI.Marker.CreateMarker(21, emsCheckpoints[1] + new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 0.8f, new Color(255, 255, 255, 60));
                for (int i = 3; i < emsCheckpoints.Count; i++)
                {
                    Marker marker = NAPI.Marker.CreateMarker(1, emsCheckpoints[i] - new Vector3(0, 0, 0), new Vector3(), new Vector3(), 0.8f, new Color(67, 140, 239, 200), false, 0);
                }
                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[3], 1, 2, 0); // open hospital stock
                col.SetData("INTERACT", 17);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
               // NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~Открыть склад"), new Vector3(emsCheckpoints[3].X, emsCheckpoints[3].Y, emsCheckpoints[3].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[4], 1, 2, 0); // duty change
                col.SetData("INTERACT", 18);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                //NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~Переодеться"), new Vector3(emsCheckpoints[4].X, emsCheckpoints[4].Y, emsCheckpoints[4].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[5], 1, 2, 0); // start heal course
                col.SetData("INTERACT", 19);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                //NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~Начать лечение"), new Vector3(emsCheckpoints[5].X, emsCheckpoints[5].Y, emsCheckpoints[5].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[6], 1, 2, 0); // tattoo delete
                col.SetData("INTERACT", 51);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                //NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~Удаление татуировок"), new Vector3(emsCheckpoints[6].X, emsCheckpoints[6].Y, emsCheckpoints[6].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                col = NAPI.ColShape.CreateCylinderColShape(new Vector3(261.7316, -1352.62, 23.4178), 53, 7, 0); // start heal course
                col.OnEntityEnterColShape += (s, e) =>
                {
                    try
                    {
                        e.SetData("IN_HOSPITAL", true);
                    }
                    catch { }
                };

                #region Load Medkits
                col = NAPI.ColShape.CreateCylinderColShape(new Vector3(3595.796, 3661.733, 32.75175), 4, 5, 0); // take meds
                col.SetData("INTERACT", 58);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                NAPI.Marker.CreateMarker(1, new Vector3(3595.796, 3661.733, 29.75175), new Vector3(), new Vector3(), 4, new Color(255, 0, 0));

                col = NAPI.ColShape.CreateCylinderColShape(new Vector3(3597.154, 3670.129, 32.75175), 1, 2, 0); // take meds
                col.SetData("INTERACT", 58);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                NAPI.Marker.CreateMarker(1, new Vector3(3597.154, 3670.129, 29.75175), new Vector3(), new Vector3(), 4, new Color(255, 0, 0));
                NAPI.Blip.CreateBlip(305, new Vector3(3588.917, 3661.756, 41.48687), 1, 3, "Склад аптечек", 255, 0, true);
                #endregion

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[7], 1, 2, 0); // roof
                col.SetData("INTERACT", 63);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
               // NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~В больницу"), new Vector3(emsCheckpoints[7].X, emsCheckpoints[7].Y, emsCheckpoints[7].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                col = NAPI.ColShape.CreateCylinderColShape(emsCheckpoints[8], 1, 2, 0); // to roof
                col.SetData("INTERACT", 63);
                col.OnEntityEnterColShape += emsShape_onEntityEnterColShape;
                col.OnEntityExitColShape += emsShape_onEntityExitColShape;
                //NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~b~На крышу"), new Vector3(emsCheckpoints[8].X, emsCheckpoints[8].Y, emsCheckpoints[8].Z + 0.3), 5F, 0.3F, 0, new Color(255, 255, 255));

                #endregion
            }
            catch (Exception e) { Log.Write("ResourceStart: " + e.Message, nLog.Type.Error); }
        }

        public static List<Vector3> emsCheckpoints = new List<Vector3>()
        {
            new Vector3(355.9243, -596.5783, 27.65636), // enter ems                0
            new Vector3(275.446, -1361.11, 23.5378),    // exit ems                 1
            new Vector3(322.88574, -582.2573, 43.284097),  // spawn after death        2
            new Vector3(304.3107, -600.3539, 42.164097),  // open hospital stock      3
            new Vector3(298.6097, -598.26526, 42.164093),   // duty change              4
            new Vector3(260.4829, -1358.456, 23.5378),  // start heal course        5
            new Vector3(257.4013, -1344.476, 23.42937), // tattoo delete            6
            new Vector3(-10000,-1000,-10000), // roof     fake                7
            new Vector3(339.0912, -583.9084, -703.04566), // roof               8
        };
        public static void OpenMenuEMS(Player player)
        {
            Trigger.ClientEvent(player, "NPC.cameraOn", "EMS", 1000);
            Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Привет ты в главной больнице города Los Santos! Чем могу помочь?", (new QuestAnswer("Как здесь работать?", 80), new QuestAnswer("Купить медицинскую карту", 84), new QuestAnswer("Лечение", 82), new QuestAnswer("Спасибо, я сам", 2)));
        }
        public static void GetHealthEms(Player player)
        {
            if (NAPI.Player.GetPlayerHealth(player) > 99)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не нуждаетесь в курсе лечения", 3000);
                return;
            }
            if (player.HasData("HEAL_TIMER"))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы уже брали курс лечения", 3000);
                return;
            }
            if (Main.Players[player].Money < PriceForHealth)
            {
                Notify.Error(player, "Недостаточно средств на оплату лекарства");
                return;
            }
            MoneySystem.Wallet.Change(player, -PriceForHealth);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы начали курс лечения", 3000);
            player.SetData("HEAL_TIMER", Timers.Start(3750, () => healTimer(player)));
            return;
        }
        public static void callEms(Player player, bool death = false)
        {
            if (!death)
            {
                if (Manager.countOfFractionMembers(8) == 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Нет медиков в Вашем районе. Попробуйте позже", 3000);
                    return;
                }
                if (player.HasData("NEXTCALL_EMS") && DateTime.Now < player.GetData<DateTime>("NEXTCALL_EMS"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Вы уже вызвали медиков, попробуйте позже", 3000);
                    return;
                }
                player.SetData("NEXTCALL_EMS", DateTime.Now.AddMinutes(7));
            }

            if (death && (Main.Players[player].InsideHouseID != -1 || Main.Players[player].InsideGarageID != -1)) return;

            if (player.HasData("CALLEMS_BLIP"))
                NAPI.Task.Run(() => { try { NAPI.Entity.DeleteEntity(player.GetData<Blip>("CALLEMS_BLIP")); } catch { } });

            var Blip = NAPI.Blip.CreateBlip(0, player.Position, 1, 70, $"Call from player ({player.Value})", 0, 0, true, 0, NAPI.GlobalDimension);
            NAPI.Blip.SetBlipTransparency(Blip, 0);
            foreach (var p in NAPI.Pools.GetAllPlayers())
            {
                if (!Main.Players.ContainsKey(p) || Main.Players[p].FractionID != 8) continue;
                Trigger.ClientEvent(p, "changeBlipAlpha", Blip, 255);
            }
            player.SetData("CALLEMS_BLIP", Blip);

            var colshape = NAPI.ColShape.CreateCylinderColShape(player.Position, 70, 4, 0);
            colshape.OnEntityExitColShape += (s, e) =>
            {
                if (e == player)
                {
                    try
                    {
                        if (Blip != null) Blip.Delete();
                        e.ResetData("CALLEMS_BLIP");

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                colshape.Delete();
                            }
                            catch { }
                        }, 20);
                        e.ResetData("CALLEMS_COL");
                        e.ResetData("IS_CALLEMS");
                    }
                    catch (Exception ex) { Log.Write("EnterEmsCall: " + ex.Message); }
                }
            };
            player.SetData("CALLEMS_COL", colshape);

            player.SetData("IS_CALLEMS", true);
            Manager.sendFractionMessage(8, $"Поступил вызов от игрока ({player.Value})");
            Manager.sendFractionMessage(8, $"~b~Поступил вызов от игрока ({player.Value})", true);
        }

        public static void acceptCall(Player player, Player target)
        {
            int where = -1;
            try
            {
                where = 0;
                if (!Manager.canUseCommand(player, "ems")) return;
                where = 1;
                if (!target.HasData("IS_CALLEMS"))
                {
                    where = 2;
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Игрок не вызывал EMS, или этот вызов уже кто-то принял", 3000);
                    return;
                }
                where = 3;
                Blip blip = target.GetData<Blip>("CALLEMS_BLIP");

                where = 4;
                Trigger.ClientEvent(player, "changeBlipColor", blip, 38);
                where = 5;
                Trigger.ClientEvent(player, "createWaypoint", blip.Position.X, blip.Position.Y);
                where = 6;

                ColShape colshape = target.GetData<ColShape>("CALLEMS_COL");
                where = 7;
                colshape.OnEntityEnterColShape += (s, e) =>
                {
                    if (e == player)
                    {
                        try
                        {
                            NAPI.Entity.DeleteEntity(target.GetData<Blip>("CALLEMS_BLIP"));
                            target.ResetData("CALLEMS_BLIP");
                            NAPI.Task.Run(() =>
                            {
                                try
                                {
                                    colshape.Delete();
                                }
                                catch { }
                            }, 20);
                        }
                        catch (Exception ex) { Log.Write("EnterEmsCall: " + ex.Message); }
                    }
                };
                where = 8;

                Manager.sendFractionMessage(7, $"{player.Name.Replace('_', ' ')} принял вызов от игрока ({target.Value})");
                where = 9;
                Manager.sendFractionMessage(7, $"~b~{player.Name.Replace('_', ' ')} принял вызов от игрока ({target.Value})", true);
                where = 10;
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"Игрок ({player.Value}) принял Ваш вызов", 3000);
                where = 11;
            }
            catch (Exception e) { Log.Write($"acceptCall/{where}/: {e.ToString()}"); }
        }

        public static void onPlayerDisconnectedhandler(Player player, DisconnectionType type, string reason)
        {
            try
            {
                if (player.HasData("HEAL_TIMER"))
                {
                    //Main.StopT(player.GetData("HEAL_TIMER"), "timer_7");
                    Timers.Stop(player.GetData<string>("HEAL_TIMER"));
                }

                if (player.HasData("DYING_TIMER"))
                {
                    //Main.StopT(player.GetData("DYING_TIMER"), "timer_8");
                    Timers.Stop(player.GetData<string>("DYING_TIMER"));
                }

                if (player.HasData("CALLEMS_BLIP"))
                {
                    NAPI.Entity.DeleteEntity(player.GetData<Blip>("CALLEMS_BLIP"));

                    Manager.sendFractionMessage(8, $"{player.Name.Replace('_', ' ')} отменил вызов");
                }
                if (player.HasData("CALLEMS_COL"))
                {
                    NAPI.ColShape.DeleteColShape(player.GetData<ColShape>("CALLEMS_COL"));
                }
            }
            catch (Exception e) { Log.Write("PlayerDisconnected: " + e.Message, nLog.Type.Error); }
        }

        private static List<string> deadAnims = new List<string>() { "dead_a", "dead_b", "dead_c", "dead_d", "dead_e", "dead_f", "dead_g", "dead_h" };
        [ServerEvent(Event.PlayerDeath)]
        public void onPlayerDeathHandler(Player player, Player entityKiller, uint weapon)
        {
            try
            {
                if (!Main.Players.ContainsKey(player)) return;
                if (player.HasData("ARENA")) return;
                if (player.HasData("CARROOMID") && player.HasData("LAST_HP"))
                {
                    player.Health = player.GetData<int>("LAST_HP");
                    return;
                }
                Log.Debug($"{player.Name} is died by {weapon} {entityKiller}");

                FractionCommands.onPlayerDeathHandler(player, entityKiller, weapon);
                SafeMain.onPlayerDeathHandler(player, entityKiller, weapon);
                Weapons.Event_PlayerDeath(player, entityKiller, weapon);
                Army.Event_PlayerDeath(player, entityKiller, weapon);
                Police.Event_PlayerDeath(player, entityKiller, weapon);
                Houses.HouseManager.Event_OnPlayerDeath(player, entityKiller, weapon);

                Jobs.Collector.Event_PlayerDeath(player, entityKiller, weapon);
                Jobs.Gopostal.Event_PlayerDeath(player, entityKiller, weapon);

                if (player.HasData("Phone")) MenuManager.Close(player);

                //VehicleManager.WarpPlayerOutOfVehicle(player);
                Main.Players[player].IsAlive = false;
                if (player.HasData("AdminSkin"))
                {
                    player.ResetData("AdminSkin");
                    player.SetSkin((Main.Players[player].Gender) ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);
                    Customization.ApplyCharacter(player);
                }
                Trigger.ClientEvent(player, "screenFadeOut", 2000);

                var dimension = player.Dimension;

                if (Main.Players[player].DemorganTime != 0 || Main.Players[player].ArrestTime != 0)
                    player.SetData("IS_DYING", true);
                player.SetSharedData("IS_DYING", true);

                if (!player.HasData("IS_DYING"))
                {
                    player.SetSharedData("InDeath", true);
                    player.SetData("IS_STATE", true);
                    player.SetData("DYING_POS", player.Position);
                    var medics = 0;
                    Main.Players[player].IsAlive = false;
                    foreach (var m in Manager.Members) if (m.Value.FractionID == 8) medics++;

                    string text = "";
                    if (entityKiller != null && entityKiller != player && Main.Players.ContainsKey(player))
                        text = $"Вас убил ({entityKiller.Id}) #{entityKiller.GetSharedData<int>("PERSON_ID")}";

                    Trigger.ClientEvent(player, "openDialogMED", $"Вы хотите вызвать медиков ({medics} в сети)?", text);
                }
                else
                {
                    NAPI.Task.Run(() => {
                        try
                        {
                            if (!Main.Players.ContainsKey(player)) return;

                            if (player.HasData("DYING_TIMER"))
                            {
                                //Main.StopT(player.GetData("DYING_TIMER"), "timer_9");
                                Timers.Stop(player.GetData<string>("DYING_TIMER"));
                                player.ResetData("DYING_TIMER");
                            }

                            if (player.HasData("CALLEMS_BLIP"))
                            {
                                NAPI.Entity.DeleteEntity(player.GetData<Blip>("CALLEMS_BLIP"));
                                player.ResetData("CALLEMS_BLIP");
                            }

                            if (player.HasData("CALLEMS_COL"))
                            {
                                NAPI.ColShape.DeleteColShape(player.GetData<ColShape>("CALLEMS_COL"));
                                player.ResetData("CALLEMS_COL");
                            }

                            Trigger.ClientEvent(player, "DeathTimer", false);
                            player.SetSharedData("InDeath", false);
                            var spawnPos = new Vector3();

                            if (Main.Players[player].DemorganTime != 0)
                            {
                                spawnPos = Admin.DemorganPosition + new Vector3(0, 0, 1.12);
                                dimension = 1337;
                            }
                            else if (Main.Players[player].ArrestTime != 0)
                                spawnPos = Police.policeCheckpoints[4];
                            else if (Main.Players[player].FractionID == 14)
                                spawnPos = Fractions.Manager.FractionSpawns[14] + new Vector3(0, 0, 1.12);
                            else
                            {
                                player.SetData("IN_HOSPITAL", true);
                                spawnPos = emsCheckpoints[2];
                            }

                            NAPI.Player.SpawnPlayer(player, spawnPos);
                            NAPI.Player.SetPlayerHealth(player, 20);
                            player.ResetData("IS_DYING");
                            player.ResetData("IS_STATE");
                            player.ResetSharedData("IS_DYING");
                            Main.Players[player].IsAlive = true;
                            Main.OffAntiAnim(player);
                            NAPI.Entity.SetEntityDimension(player, dimension);
                            Trigger.ClientEvent(player, "screenDeath");
                        }
                        catch { }
                    }, 4000);
                }
            }
            catch (Exception e) { Log.Write("PlayerDeath: " + e.Message, nLog.Type.Error); }
        }

        public static void DeathConfirm(Player player, bool call)
        {

            if (Main.Players[player].DemorganTime != 0)
            {
                var spawnPos = Admin.DemorganPosition + new Vector3(0, 0, 1.12);
                uint dimension = 1337;
                NAPI.Player.SpawnPlayer(player, spawnPos);
                NAPI.Player.SetPlayerHealth(player, 20);
                player.ResetData("IS_DYING");
                player.ResetData("IS_STATE");
                Main.Players[player].IsAlive = true;
                Main.OffAntiAnim(player);
                Trigger.ClientEvent(player, "screenDeath");
                NAPI.Entity.SetEntityDimension(player, dimension);
                Trigger.ClientEvent(player, "closeDialogMED");
                return;
            }
            NAPI.Player.SpawnPlayer(player, player.Position);
            NAPI.Entity.SetEntityDimension(player, 0);

            Main.OnAntiAnim(player);
            player.SetData("IS_DYING", true);
            player.SetData("DYING_POS", player.Position);

            if (call) callEms(player, true);
            Voice.Voice.PhoneHCommand(player);

            NAPI.Player.SetPlayerHealth(player, 999);
            var time = (call) ? 240000 : 120000;
            Trigger.ClientEvent(player, "DeathTimer", time);
            var timeMsg = "";
            //player.SetData("DYING_TIMER", Main.StartT(time, time, (o) => { player.Health = 0; }, "DYING_TIMER"));
            NAPI.Task.Run(() => { try { timeMsg = (call) ? "4 минут Вас не вылечит медик или кто-нибудь другой" : "2 минут Вас никто не вылечит"; player.SetData("DYING_TIMER", Timers.StartOnce(time, () => DeathTimer(player))); } catch { } });

            var deadAnimName = deadAnims[Main.rnd.Next(deadAnims.Count)];
            NAPI.Task.Run(() => { try { player.PlayAnimation("dead", deadAnimName, 39); } catch { } }, 500);

            Notify.Send(player, NotifyType.Alert, NotifyPosition.BottomCenter, $"Если в течение {timeMsg}, то Вы попадёте в больницу", 3000);
        }

        public static void DeathTimer(Player player)
        {
            NAPI.Task.Run(() => {
                player.Health = 0;


                if (player.HasData("CALLEMS_BLIP"))
                {
                    NAPI.Entity.DeleteEntity(player.GetData<Blip>("CALLEMS_BLIP"));
                    Fractions.Manager.sendFractionMessage(8, $"{player.Name.Replace('_', ' ')} умер. Вы не успели его спасти!");
                }
                if (player.HasData("CALLEMS_COL"))
                {
                    NAPI.ColShape.DeleteColShape(player.GetData<ColShape>("CALLEMS_COL"));
                }
            });
        }

        public static void payMedkit(Player player)
        {
            if (Main.Players[player].Money < player.GetData<int>("PRICE"))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас нет столько денег", 3000);
                return;
            }
            Player seller = player.GetData<Player>("SELLER");
            if (player.Position.DistanceTo(seller.Position) > 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы слишком далеко от продавца", 3000);
                return;
            }
            var item = nInventory.Find(Main.Players[seller].UUID, ItemType.HealthKit);
            if (item == null || item.Count < 1)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У продавца не осталось аптечек", 3000);
                return;
            }
            var tryAdd = nInventory.TryAdd(player, new nItem(ItemType.HealthKit));
            if (tryAdd == -1 || tryAdd > 0)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Недостаточно места в инвентаре", 3000);
                return;
            }

            nInventory.Add(player, new nItem(ItemType.HealthKit));
            nInventory.Remove(seller, ItemType.HealthKit, 1);

            Fractions.Stocks.fracStocks[6].Money += Convert.ToInt32(player.GetData<int>("PRICE") * 0.85);
            MoneySystem.Wallet.Change(player, -player.GetData<int>("PRICE"));
            MoneySystem.Wallet.Change(seller, Convert.ToInt32(player.GetData<int>("PRICE") * 0.15));

            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы купили аптечку", 3000);
            Notify.Send(seller, NotifyType.Info, NotifyPosition.BottomCenter, $"Игрок ({player.Value}) купил у Вас аптечку", 3000);
        }

        public static void payHeal(Player player)
        {
            if (Main.Players[player].Money < player.GetData<int>("PRICE"))
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас нет столько денег", 3000);
                return;
            }   
            var seller = player.GetData<Player>("SELLER");
            if (player.Position.DistanceTo(seller.Position) > 2)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы слишком далеко от врача", 3000);
                return;
            }
            if (NAPI.Player.IsPlayerInAnyVehicle(seller) && NAPI.Player.IsPlayerInAnyVehicle(player))
            {
                var pveh = seller.Vehicle;
                var tveh = player.Vehicle;
                Vehicle veh = NAPI.Entity.GetEntityFromHandle<Vehicle>(pveh);
                if (veh.GetData<string>("ACCESS") != "FRACTION" || veh.GetData<string>("TYPE") != "EMS" || !veh.HasData("CANMEDKITS"))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы сидите не в карете EMS", 3000);
                    return;
                }
                if (pveh != tveh)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Игрок сидит в другой машине", 3000);
                    return;
                }
                Notify.Send(seller, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы вылечили игрока ({player.Value})", 3000);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Игрок ({seller.Value}) вылечил Вас", 3000);
                Trigger.ClientEvent(player, "stopScreenEffect", "PPFilter");
                NAPI.Player.SetPlayerHealth(player, 100);
                MoneySystem.Wallet.Change(player, -player.GetData<int>("PRICE"));
                MoneySystem.Wallet.Change(seller, player.GetData<int>("PRICE"));
                GameLog.Money($"player({Main.Players[player].UUID})", $"player({Main.Players[seller].UUID})", player.GetData<int>("PRICE"), $"payHeal");
                return;
            }
            else if (seller.GetData<bool>("IN_HOSPITAL") && player.GetData<bool>("IN_HOSPITAL"))
            {
                Notify.Send(seller, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы вылечили игрока ({player.Value})", 3000);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Игрок ({seller.Value}) вылечил Вас", 3000);
                NAPI.Player.SetPlayerHealth(player, 100);
                MoneySystem.Wallet.Change(player, -player.GetData<int>("PRICE"));
                MoneySystem.Wallet.Change(seller, player.GetData<int>("PRICE"));
                GameLog.Money($"player({Main.Players[player].UUID})", $"player({Main.Players[seller].UUID})", player.GetData<int>("PRICE"), $"payHeal");
                Trigger.ClientEvent(player, "stopScreenEffect", "PPFilter");
                return;
            }
            else
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы должны быть в больнице или корете скорой помощи", 3000);
                return;
            }
        }

        public static void interactPressed(Player player, int interact)
        {
            switch (interact)
            {
                case 15:
                    if (player.IsInVehicle) return;
                    if (player.HasData("FOLLOWING"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вас кто-то тащит за собой", 3000);
                        return;
                    }
                    player.SetData("IN_HOSPITAL", true);
                    NAPI.Entity.SetEntityPosition(player, emsCheckpoints[1] + new Vector3(0, 0, 1.12));
                    Main.PlayerEnterInterior(player, emsCheckpoints[1] + new Vector3(0, 0, 1.12));
                    return;
                case 16:
                    if (player.HasData("FOLLOWING"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вас кто-то тащит за собой", 3000);
                        return;
                    }
                    if (NAPI.Player.GetPlayerHealth(player) < 100)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы должны сначала закончить лечение", 3000);
                        break;
                    }
                    /*if (player.HasData("HEAL_TIMER"))
                    {
                        Main.StopT(player.GetData("HEAL_TIMER"));
                        player.ResetData("HEAL_TIMER");
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Ваше лечение закончено", 3000);
                    }*/
                    player.SetData("IN_HOSPITAL", false);
                    NAPI.Entity.SetEntityPosition(player, emsCheckpoints[0] + new Vector3(0, 0, 1.12));
                    Main.PlayerEnterInterior(player, emsCheckpoints[0] + new Vector3(0, 0, 1.12));
                    return;
                case 17:
                    if (Main.Players[player].FractionID != 8)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник EMS", 3000);
                        return;
                    }
                    if (!Stocks.fracStocks[8].IsOpen)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Склад закрыт", 3000);
                        return;
                    }
                    OpenHospitalStockMenu(player);
                    return;
                case 18:
                    if (Main.Players[player].FractionID == 8)
                    {
                       /* if (!NAPI.Data.GetEntityData(player, "ON_DUTY"))
                        {
                            Notify.Error(player, "Вы должны начать рабочий день у стойки с информацией");
                            return;
                        }*/
                        /*else
                        {*/
                            Trigger.ClientEvent(player, "OpenClothesChange", ("Копмлект EMS #1", "Копмлект EMS #2", "Копмлект EMS #3", "Копмлект EMS #4"), "EMS", 8);
                            NAPI.Entity.SetEntityRotation(player, new Vector3(0, 0, 255));
                            NAPI.Entity.SetEntityDimension(player, (uint)(2000 + player.Value));
                        /*}*/
                    }
                    else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник EMS", 3000);
                    return;
                case 19:
                    if (NAPI.Player.GetPlayerHealth(player) > 99)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не нуждаетесь в лечении", 3000);
                        break;
                    }
                    if (player.HasData("HEAL_TIMER"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы уже лечитесь", 3000);
                        break;
                    }
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы начали лечение", 3000);
                    //player.SetData("HEAL_TIMER", Main.StartT(3750, 3750, (o) => healTimer(player), "HEAL_TIMER"));
                    player.SetData("HEAL_TIMER", Timers.Start(3750, () => healTimer(player)));
                    return;
                case 51:
                    OpenTattooDeleteMenu(player);
                    return;
                case 58:
                    if (Main.Players[player].FractionID != 8)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник EMS", 3000);
                        break;
                    }
                    if (!player.IsInVehicle || !player.Vehicle.HasData("CANMEDKITS"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не в машине или Ваша машина не может перевозить аптечки", 3000);
                        break;
                    }

                    var medCount = VehicleInventory.GetCountOfType(player.Vehicle, ItemType.HealthKit);
                    if (medCount >= 50)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"В машине максимум аптечек", 3000);
                        break;
                    }
                    if (HumanMedkitsLefts <= 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Аптечки закончились. Приезжайте за новыми через час", 3000);
                        break;
                    }
                    var toAdd = (HumanMedkitsLefts > 50 - medCount) ? 50 - medCount : HumanMedkitsLefts;
                    HumanMedkitsLefts = toAdd;

                    VehicleInventory.Add(player.Vehicle, new nItem(ItemType.HealthKit, toAdd));
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы заполнили машину аптечками", 3000);
                    return;
                case 63:
                    if (Main.Players[player].FractionID != 8)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник EMS", 3000);
                        break;
                    }
                    if (player.IsInVehicle) return;
                    if (player.Position.Z > 50)
                    {
                        if (player.HasData("FOLLOWING"))
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вас кто-то тащит за собой", 3000);
                            return;
                        }
                        player.SetData("IN_HOSPITAL", true);
                        NAPI.Entity.SetEntityPosition(player, emsCheckpoints[8] + new Vector3(0, 0, 1.12));
                        Main.PlayerEnterInterior(player, emsCheckpoints[8] + new Vector3(0, 0, 1.12));
                    }
                    else
                    {
                        if (player.HasData("FOLLOWING"))
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вас кто-то тащит за собой", 3000);
                            return;
                        }
                        player.SetData("IN_HOSPITAL", false);
                        NAPI.Entity.SetEntityPosition(player, emsCheckpoints[7] + new Vector3(0, 0, 1.12));
                        Main.PlayerEnterInterior(player, emsCheckpoints[7] + new Vector3(0, 0, 1.12));
                    }
                    return;
            }
        }

        private static void healTimer(Player player)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (player.Health == 100)
                    {
                        //Main.StopT(player.GetData("HEAL_TIMER"), "timer_10");
                        Timers.Stop(player.GetData<string>("HEAL_TIMER"));
                        player.ResetData("HEAL_TIMER");
                        Trigger.ClientEvent(player, "stopScreenEffect", "PPFilter");
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Ваше лечение закончено", 3000);
                        return;
                    }
                    player.Health = player.Health + 1;
                }
                catch { }
            });
        }

        private void emsShape_onEntityEnterColShape(ColShape shape, Player entity)
        {
            try
            {
                NAPI.Data.SetEntityData(entity, "INTERACTIONCHECK", shape.GetData<int>("INTERACT"));
            }
            catch (Exception ex) { Log.Write("emsShape_onEntityEnterColShape: " + ex.Message, nLog.Type.Error); }
        }

        private void emsShape_onEntityExitColShape(ColShape shape, Player entity)
        {
            try
            {
                NAPI.Data.SetEntityData(entity, "INTERACTIONCHECK", 0);
            }
            catch (Exception ex) { Log.Write("emsShape_onEntityExitColShape: " + ex.Message, nLog.Type.Error); }
        }

        #region menus
        public static void OpenHospitalStockMenu(Player player)
        {
            Trigger.ClientEvent(player, "OpenStock_EMS");
            Trigger.ClientEvent(player, "NPC.cameraOn", "EMSStock", 1500);
        }
        [RemoteEvent("SetStockBuyEms")]
        public static void callback_stockems(Player player, int id)
        {
            switch (id)
            {
                case 0:
                    if (!Main.Players.ContainsKey(player)) return;
                    if (Stocks.fracStocks[8].Medkits < 10)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"На складе не осталось медицинских материалов", 3000);
                        return;
                    }
                    if (Main.Players[player].FractionLVL < 3)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к электрошокеру", 3000);
                        return;
                    }
                    var tryAdd = nInventory.TryAdd(player, new nItem(ItemType.StunGun, 1));
                    if (tryAdd == -1 || tryAdd > 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                        return;
                    }
                    Fractions.Stocks.fracStocks[8].Medkits -= 10;
                    Fractions.Stocks.fracStocks[8].UpdateLabel();
                    Weapons.GiveWeapon(player, ItemType.StunGun, Weapons.GetSerialFrac(true, 8));
                    Trigger.ClientEvent(player, "acguns");
                    Notify.Succ(player, "Вы взяли тайзер");
                    return;
                case 1:
                    if (!Manager.canGetWeapon(player, "Medkits")) return;
                    if (Main.Players[player].FractionLVL < 3)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа к электрошокеру", 3000);
                        return;
                    }
                    if (Stocks.fracStocks[8].Medkits <= 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"На складе не осталось медицинских материалов", 3000);
                        return;
                    }
                    var tryAdd1 = nInventory.TryAdd(player, new nItem(ItemType.HealthKit, 1));
                    if (tryAdd1 == -1 || tryAdd1 > 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                        return;
                    }
                    nInventory.Add(player, new nItem(ItemType.HealthKit));
                    var itemInv = nInventory.Find(Main.Players[player].UUID, ItemType.HealthKit);
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы взяли эпинефрин, У Вас {itemInv.Count} штук", 3000);
                    Stocks.fracStocks[8].Medkits--;
                    Fractions.Stocks.fracStocks[8].UpdateLabel();
                    GameLog.Stock(Main.Players[player].FractionID, Main.Players[player].UUID, "medkit", 1, false);
                    return;
                case 2:
                    if (Stocks.fracStocks[8].Medkits <= 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"На складе не осталось медицинских материалов", 3000);
                        return;
                    }
                    var tryAdd2 = nInventory.TryAdd(player, new nItem(ItemType.Pill, 10));
                    if (tryAdd2 == -1 || tryAdd2 > 0)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomLeft, $"Недостаточно места в инвентаре", 2000);
                        return;
                    }
                    nInventory.Add(player, new nItem(ItemType.Pill, 10));
                    var itemInvPill = nInventory.Find(Main.Players[player].UUID, ItemType.Pill);
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы взяли таблетки, У Вас {itemInvPill.Count} штук", 3000);
                    Stocks.fracStocks[8].Medkits--;
                    Fractions.Stocks.fracStocks[8].UpdateLabel();
                    GameLog.Stock(Main.Players[player].FractionID, Main.Players[player].UUID, "medkit", 1, false);
                    return;

            }
        }

        public static void OpenTattooDeleteMenu(Player player)
        {
            Menu menu = new Menu("tattoodelete", false, false);
            menu.Callback = callback_tattoodelete;

            Menu.Item menuItem = new Menu.Item("header", Menu.MenuItem.Header);
            menuItem.Text = $"Сведение татуировок";
            menu.Add(menuItem);

            menuItem = new Menu.Item("header", Menu.MenuItem.Card);
            menuItem.Text = $"Выберите зону, в которой хотите свести все татуировки. Стоимость сведения в одной зоне - 3000$";
            menu.Add(menuItem);

            menuItem = new Menu.Item("Torso", Menu.MenuItem.Button);
            menuItem.Text = "Торс";
            menu.Add(menuItem);

            menuItem = new Menu.Item("Head", Menu.MenuItem.Button);
            menuItem.Text = "Голова";
            menu.Add(menuItem);

            menuItem = new Menu.Item("LeftArm", Menu.MenuItem.Button);
            menuItem.Text = "Левая рука";
            menu.Add(menuItem);

            menuItem = new Menu.Item("RightArm", Menu.MenuItem.Button);
            menuItem.Text = "Правая рука";
            menu.Add(menuItem);

            menuItem = new Menu.Item("LeftLeg", Menu.MenuItem.Button);
            menuItem.Text = "Левая нога";
            menu.Add(menuItem);

            menuItem = new Menu.Item("RightLeg", Menu.MenuItem.Button);
            menuItem.Text = "Правая нога";
            menu.Add(menuItem);

            menuItem = new Menu.Item("close", Menu.MenuItem.Button);
            menuItem.Text = "Закрыть";
            menu.Add(menuItem);

            menu.Open(player);
        }

        private static List<string> TattooZonesNames = new List<string>() { "торса", "головы", "левой руки", "правой руки", "левой ноги", "правой ноги" };
        private static void callback_tattoodelete(Player client, Menu menu, Menu.Item item, string eventName, dynamic data)
        {
            if (item.ID == "close")
            {
                MenuManager.Close(client);
                return;
            }
            var zone = Enum.Parse<TattooZones>(item.ID);
            if (Customization.CustomPlayerData[Main.Players[client].UUID].Tattoos[Convert.ToInt32(zone)].Count == 0)
            {
                Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "У Вас нет татуировок в этой зоне", 3000);
                return;
            }
            if (!MoneySystem.Wallet.Change(client, -600))
            {
                Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно средств", 3000);
                return;
            }
            GameLog.Money($"player({Main.Players[client].UUID})", $"server", 600, $"tattooRemove");
            Fractions.Stocks.fracStocks[6].Money += 600;

            client.ClearDecorations();
            Customization.CustomPlayerData[Main.Players[client].UUID].Tattoos[Convert.ToInt32(zone)] = new List<Tattoo>();
            foreach (var list in Customization.CustomPlayerData[Main.Players[client].UUID].Tattoos.Values)
            {
                foreach (var t in list)
                {
                    if (t == null) continue;
                    var decoration = new Decoration();
                    decoration.Collection = NAPI.Util.GetHashKey(t.Dictionary);
                    decoration.Overlay = NAPI.Util.GetHashKey(t.Hash);
                    client.SetDecoration(decoration);
                }
            }
            client.SetSharedData("TATTOOS", Newtonsoft.Json.JsonConvert.SerializeObject(Customization.CustomPlayerData[Main.Players[client].UUID].Tattoos));

            Notify.Send(client, NotifyType.Success, NotifyPosition.BottomCenter, "Вы свели татуировки с " + TattooZonesNames[Convert.ToInt32(zone)], 3000);
        }
        #endregion
    }
}
