using System;
using Alyx;
using AlyxSDK;
using GTANetworkAPI;

namespace Alyx.Core
{
    public class Quests : Script
    {
        private static nLog Log = new nLog("Quests");
        public static Vector3 positionFirst = new Vector3(-495.19247, -684.2751, 32.09198);
        public static Vector3 positionSecond = new Vector3(-58.036095, -800.47437, 43.10729);
        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                NAPI.Blip.CreateBlip(480, positionFirst, 0.8f, 5, Main.StringToU16("Гарри Скотленд"), 255, 0, true, 0, 0);
                NAPI.Blip.CreateBlip(480, positionSecond, 0.8f, 57, Main.StringToU16("Эндрю Стивенс"), 255, 0, true, 0, 0);

                var shape = NAPI.ColShape.CreateCylinderColShape(positionFirst, 1.2f, 2, 0); shape.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 1000); } catch (Exception e) { Log.Write(e.ToString(), nLog.Type.Error); } }; shape.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception e) { Log.Write(e.ToString(), nLog.Type.Error); } };
                var shape2 = NAPI.ColShape.CreateCylinderColShape(positionSecond, 1.2f, 2, 0); shape2.OnEntityEnterColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 1001); } catch (Exception e) { Log.Write(e.ToString(), nLog.Type.Error); } }; shape2.OnEntityExitColShape += (shape, player) => { try { player.SetData("INTERACTIONCHECK", 0); } catch (Exception e) { Log.Write(e.ToString(), nLog.Type.Error); } };
            }
            catch { }
        }

        public static void Interactions(Player player, int id)
        {
            switch (id)
            {
                case 1000:
                    if (Main.Players[player].Achievements[0] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Салам, выглядишь ты конечно не очень, как будто только что на тебя насрали 3 лошади, мне аж противно смотреть. Смотри тебе все равно делать что-то нужно, а я помочь могу. Короче сгоняй в ближайший магазин 24/7 и купи мне пачку чипсов.", (new QuestAnswer("Ну ладно...", 1), new QuestAnswer("Да пошел ты!", 2)));
                    }
                    if (Main.Players[player].Achievements[0] == true && Main.Players[player].Achievements[1] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Я тебя попросил чипсы мне принести, или ты не понимаешь?", (new QuestAnswer("Ну ладно...", 2), 0));
                    }
                    if (Main.Players[player].Achievements[1] == true && Main.Players[player].Achievements[2] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "О это опять ты, ну что там чипсы принес? Вот спасибо, ладно смотри, на тебе пока пару тысяч зелёных возьми в аренду скутер у парнишки напротив, их кстати много и они не очень и разговорчивые.", (new QuestAnswer("Ясно", 3), 0));
                    }
                    if (Main.Players[player].Achievements[2] == true && Main.Players[player].Achievements[3] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "По твоему виду не видно что ты уже поработал на стройке, если ты хочешь нормально развиться сходи туда, не позорься", (new QuestAnswer("Окей...", 2), 0));
                    }
                    if (Main.Players[player].Achievements[3] == true && Main.Players[player].Achievements[4] == false && Main.Players[player].Licenses[1] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Ты почему не сказал мне что ты взял скутер без прав, я уже наскребал денег на оплату штрафа, получи себе лицензию на транспорт в центре лицензирования.", (new QuestAnswer("Прости...", 5), 0));
                    }
                    else if (Main.Players[player].Achievements[3] == true && Main.Players[player].Achievements[4] == false && Main.Players[player].Licenses[1] == true)
                    {
                        Main.Players[player].Achievements[4] = true;
                        Main.Players[player].Achievements[5] = true;
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Деньги есть, лицензия есть, паспорт... Ах да, паспорт. Удостоверься что у тебя есть документы", (new QuestAnswer("Как?", 6), 0));
                    }
                    if (Main.Players[player].Achievements[5] == true && Main.Players[player].Achievements[6] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Деньги есть, лицензия есть, паспорт... Ах да, паспорт. Удостоверься что у тебя есть документы", (new QuestAnswer("Как?", 6), 0));
                    }
                    if (Main.Players[player].Achievements[6] == true && Main.Players[player].Achievements[7] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Деньги есть, лицензия есть, паспорт... Ах да, паспорт. Удостоверься что у тебя есть документы", (new QuestAnswer("Как?", 6), 0));
                    }
                    if (Main.Players[player].Achievements[7] == true && Main.Players[player].Achievements[8] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "GarryScotland", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Есть паспорт, это хорошо. Спасибо можешь не говорить, за то, что помог. Сейчас я тебе дам координаты одного парниши, он может помочь, его зовут Эндрю кстати.", (new QuestAnswer("Ладно", 8), 0));
                    }
                    return;
                case 1001:
                    if (Main.Players[player].Achievements[8] == true && Main.Players[player].Achievements[9] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "EndryStivens", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эндрю Стивент", "Предприниматель", "Приветствую, Гарри уже сообщил мне что ты приедешь. Мне нет времени с тобой возиться, поэтому возьми 100.000.000$, должно хватить на все", (new QuestAnswer("О! Спасибо", 9), new QuestAnswer("А что с ними делать?", 10)));
                    }
                    if (Main.Players[player].Achievements[9] == true && Main.Players[player].Achievements[10] == false)
                    {
                        Trigger.ClientEvent(player, "NPC.cameraOn", "EndryStivens", 1000);
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эндрю Стивент", "Предприниматель", "Опять ты! Не мешай мне работать, я уже дал тебе денег", (new QuestAnswer("Прости, хотел спросить...", 11), 0));
                    }
                    return;
            }
        }
        public class QuestAnswer
        {
            public string Text { get; set; }
            public int Event { get; set; }
            public QuestAnswer(string text, int eventn)
            {
                Text = text;
                Event = eventn;
            }
        }
    }
}
