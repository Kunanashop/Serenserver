using System;
using System.Collections.Generic;
using GTANetworkAPI;
using AlyxSDK;

namespace Alyx.Core
{
    class Walkie : Script
    {
        private static nLog Log = new nLog("Walkie-Talkie");
        private static List<Player> Listeners = new List<Player>();
        private static Random rnd = new Random();
        private static Dictionary<int, int> FractionsFrequency = new Dictionary<int, int>()
        {
            {6, 0 },
            {7, 0 },
            {8, 0 },
            {9, 0 },
            {14, 0 },
            {15, 0 },
            {18, 0 },
        };

        [ServerEvent(Event.ResourceStart)]
        public static void ResourceStart()
        {
            FractionsFrequency[6] = rnd.Next(1, 950);
            FractionsFrequency[7] = rnd.Next(1, 950);
            FractionsFrequency[8] = rnd.Next(1, 950);
            FractionsFrequency[9] = rnd.Next(1, 950);
            FractionsFrequency[14] = rnd.Next(1, 950);
            FractionsFrequency[15] = rnd.Next(1, 950);
            FractionsFrequency[18] = rnd.Next(1, 950);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public static void PlayerDisc(Player player, DisconnectionType type, string reason)
        {
            if (player.GetData<bool>("LISTENER_WALKIE") == true)
            {
                Listeners.Remove(player);
            }
        }
        [ServerEvent(Event.PlayerDeath)]
        public static void PlayerDic(Player player, Player entityKiller, uint weapon)
        {
            if (player.GetData<bool>("LISTENER_WALKIE") == true)
            {
                Listeners.Remove(player);
                CloseWalkieMenu(player);
            }
        }

        public static void OpenWalkie(Player player)
        {
            player.ResetData("Frequency");
            switch (Main.Players[player].FractionID)
            {
                default:
                    player.SetData("Frequency", 1);
                    player.SetData("LISTENER_WALKIE", true);
                    player.SetSharedData("LISTENER_RADIO", true);
                    Listeners.Add(player);
                    NAPI.ClientEvent.TriggerClientEvent(player, "walkie.open", 1);
                    break;
            }
        }

        [RemoteEvent("ChangeFrequency")]
        public static void ChangeFrequency(Player player, int Frequency)
        {
            if (Convert.ToString(Frequency) == null)
            {
                Notify.Succ(player, $"Введите радиоволну");
                return;
            }
            if (Convert.ToString(Frequency).Length < 6)
            {
                Notify.Succ(player, $"Вы не полностью ввели радиоволну");
                return;
            }
            if (Frequency > 99999 && Frequency < 119999)
            {
                Notify.Error(player, $"Вы не можете подключиться к этой радиоволне");
                return;
            }
            player.SetData("Frequency", Frequency);
            Notify.Succ(player, $"Вы подключились к волне");
        }

        public static void CloseWalkie(Player player)
        {
            player.SetData("LISTENER_WALKIE", false);
            player.SetSharedData("LISTENER_RADIO", false);
            Listeners.Remove(player);
            NAPI.ClientEvent.TriggerClientEvent(player, "walkie.close");
        }     
        
        [RemoteEvent("closeWalkie")]
        public static void CloseWalkieMenu(Player player)
        {
            player.SetData("LISTENER_WALKIE", false);
            player.SetSharedData("LISTENER_RADIO", false);
            Listeners.Remove(player);
            player.SetData("Walkie_open", false);
            NAPI.ClientEvent.TriggerClientEvent(player, "walkie.close");
        }

        [RemoteEvent("talkingInWalkie")]
        public static void TalkingWalkie(Player player)
        {
            try
            {
                if (!player.IsInVehicle)
                {
                    player.PlayAnimation("random@arrests", "generic_radio_chatter", 49);
                }
                NAPI.ClientEvent.TriggerClientEvent(player, "walkie.playSound");
                player.SetData("TalkingInWalkie", true);
                foreach (Player p in Listeners)
                {
                    if (p == player) continue;
                    if (player.GetData<int>("Frequency") != p.GetData<int>("Frequency")) continue;
                    NAPI.ClientEvent.TriggerClientEvent(p, "walkie.talking", player);
                    NAPI.ClientEvent.TriggerClientEvent(player, "walkie.talking", p);
                    NAPI.ClientEvent.TriggerClientEvent(p, "walkie.playSound");
                    player.EnableVoiceTo(p);
                }
            }
            catch (Exception e)
            {
                Log.Write($"TalkingWalkie: {e.Message}", nLog.Type.Error);
            }
        }

        [RemoteEvent("DisableTalkingWalkie")]
        public static void DisableTalkingWalkie(Player player)
        {
            try
            {
                if (!player.IsInVehicle)
                {
                    player.PlayAnimation("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 33);
                }
                NAPI.ClientEvent.TriggerClientEvent(player, "walkie.playSound");
                foreach (Player p in Listeners)
                {
                    if (p == player) continue;
                    if (player.GetData<int>("Frequency") != p.GetData<int>("Frequency")) continue;
                    if (p.GetData<bool>("TalkingInWalkie") == true) continue;
                    NAPI.ClientEvent.TriggerClientEvent(p, "walkie.playSound");
                    p.SetData("TalkingInWalkie", false);
                    NAPI.ClientEvent.TriggerClientEvent(p, "walkie.disableTalking", player);
                    player.DisableVoiceTo(p);
                }
            }
            catch (Exception e)
            {
                Log.Write($"DisableTalkingWalkie: {e.Message}", nLog.Type.Error);
            }
        }
    }
}