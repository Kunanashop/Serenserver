using Alyx;
using AlyxSDK;
using GTANetworkAPI;
using static Alyx.Core.Quests;
using Alyx.GUI;
using System;

namespace Alyx.Core
{
    public class Dialog : Script
    {
        [RemoteEvent("server::dialoganswer")]
        public static void DIALOGANSWER(Player player, int id)
        {
            switch (id)
            {
                case 1:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[0] = true;
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Первые шаги", 100, "Купи в магазине 24/7 пачку чипсов и отнеси их Гарри");
                    Vector3 waypoint = BusinessManager.getNearestBiz(player, 0);
                    Trigger.ClientEvent(player, "createWaypoint", waypoint.X, waypoint.Y);
                    return;
                case 2:
                    Trigger.ClientEvent(player, "client::closedialog");
                    return;
                case 3:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Отправляйся к моему брату на стройку и попроси у него какие-нибудь задания чтобы заработать себе на какую-нибудь одежду, а то выглядишь как бомж честное слово.", (new QuestAnswer("Хорошо, спасибо", 4), 0));
                    return;
                case 4:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[2] = true;
                    player.SetData("JobsBuilderQuestCount", 0);
                    MoneySystem.Wallet.Change(player, 1000);
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Первые деньги", 100, "Заработай на стройке 3 000$. Заработано: " + player.GetData<int>("JobsBuilderQuestCount") + "$ / 3 000$");
                    Trigger.ClientEvent(player, "createWaypoint", -169.25494, -1026.9185);
                    return;
                case 5:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[4] = true;
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Первые шаги", 100, "Получите права категории Drive D в центре лицензирования");
                    Trigger.ClientEvent(player, "createWaypoint", -707.0867, -1275.8596);
                    return;
                case 6:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Гарри Скотленд", "Местный", "Нажми M > Документы", (new QuestAnswer("Сейчас попробую...", 7), 0));
                    return;
                case 7:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[6] = true;
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Первые шаги", 100, "Посмотрите свои документы");
                    return;
                case 8:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[8] = true;
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", true, "Путь к богатсву", 100, "Езжайте к Эндрю, другу Гарри, и поговорите с ним");
                    Trigger.ClientEvent(player, "createWaypoint", -58.036095, -800.47437);
                    return;
                case 9:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Main.Players[player].Achievements[9] = true;
                    MoneySystem.Wallet.Change(player, 15000); // 
                    Trigger.ClientEvent(player, "client::addToMissionsOnHud", false, "", 15000, "");
                    return;
                case 10:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эндрю Стивент", "Предприниматель", "Делай что хочешь, трать на тачки, шмотки, недвижимость, играй в казино и так далее. Только мне не мешай", (new QuestAnswer("О! Спасибо", 9), 0));
                    return;
                case 11:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Эндрю Стивент", "Предприниматель", "Если деньги закончатся, то иди устраивайся на работу или во фракцию.", (new QuestAnswer("Спасибо", 2), 0));
                    return;
                case 20:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Джэймс Фостер", "Работа", "Смотри, я тебе даю транспорт Boxwille и 10 посылок. Потом ты развозишь их, если закончаться приезжай обратно и бери еще", (new QuestAnswer("Понятненько...", 25), 0));
                    return;
                case 25:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Jobs.WorkManager.openGoPostalStart(player);
                    return;
                case 21:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.WorkManager.SetWorkId(player);
                    return;
                case 22:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.WorkManager.callback_gpStartMenu(player, 0);
                    return;
                case 23:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.WorkManager.callback_gpStartMenu(player, 1);
                    return;
                case 24:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.WorkManager.callback_gpStartMenu(player, 2);
                    return;
                case 50:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.Builder.SetWorkId(player);
                    return;
                case 51:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Рон", "Прораб", "Дело простое, нужно всего-лишь таскать мешки по точкам и за это будешь получать немного зеленых", (new QuestAnswer("Понял", 53), 0));
                    return;
                case 52:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Jobs.Builder.SetWorkState(player);
                    return;
                case 53:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Jobs.Builder.OpenMenu(player);
                    return;
                case 54:
                    Trigger.ClientEvent(player, "client::closedialog");
                    DrivingSchool.startDrivingCourse(player, 0);
                    return;
                case 55:
                    Trigger.ClientEvent(player, "client::closedialog");
                    DrivingSchool.startDrivingCourse(player, 1);
                    return;
                case 56:
                    Trigger.ClientEvent(player, "client::closedialog");
                    DrivingSchool.startDrivingCourse(player, 2);
                    return;
                case 60:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Джон Джей", "Сотрудник Government", "Подавайте заявки на портале штата или приходите на наборы.", (new QuestAnswer("Ясно", 62), 0));
                    return;
                case 61:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Джон Джей", "Сотрудник Government", "Вы уверены что хотите купить лицензию на рыбаловную деятельность за 50 000$?", (new QuestAnswer("Да, давайте", 63), new QuestAnswer("Позже", 62)));
                    return;
                case 62:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Джон Джей", "Сотрудник Government", "Привет ты в здании мэрии города Los Santos! Чем могу помочь?", (new QuestAnswer("Как здесь работать?", 60), new QuestAnswer("Купить лицензию на рыбалку", 61), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 63:
                    Trigger.ClientEvent(player, "client::closedialog");
                    if (Main.Players[player].Money < 15000)
                    {
                        Notify.Error(player, "Недостаточно средств");
                        return;
                    }
                    if (Main.Players[player].Licenses[8] == true)
                    {
                        Notify.Error(player, "У вас уже есть лицензия на рыбалку");
                        return;
                    }
                    MoneySystem.Wallet.Change(player, -15000);
                    Main.Players[player].Licenses[8] = true;
                    Fractions.Stocks.fracStocks[6].Materials += 15000;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы купили лицензию на рыбалку", 3000);
                    return;
                case 70:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Нэнси Спунген", "Сотрудник LSPD", "Приходите на наборы в полицию штата, ождиайте новостей от Weazel News!", (new QuestAnswer("Ясно", 73), 0));
                    return;
                case 71:
                    Trigger.ClientEvent(player, "client::closedialog");
                    if (Main.Players[player].Money < 15000)
                    {
                        Notify.Error(player, "Недостаточно средств");
                        return;
                    }
                    if (Main.Players[player].Licenses[6] == true)
                    {
                        Notify.Error(player, "У вас уже есть лицензия на оружие");
                        return;
                    }
                    MoneySystem.Wallet.Change(player, -15000);
                    Main.Players[player].Licenses[6] = true;
                    Fractions.Stocks.fracStocks[7].Materials += 15000;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы купили лицензию на оружие", 3000);
                    return;
                case 74:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Нэнси Спунген", "Сотрудник LSPD", "Вы уверены что хотите купить лицензию на владение оружием за 150 000$?", (new QuestAnswer("Да, давайте", 71), new QuestAnswer("Позже", 73)));
                    return;
                case 72:
                    Trigger.ClientEvent(player, "client::closedialog");
                    if (!player.HasData("HAND_MONEY") && !player.HasData("HEIST_DRILL"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас нет ни сумки с деньгами, ни сумки с дрелью", 3000);
                        return;
                    }
                    if (player.HasData("HAND_MONEY"))
                    {
                        nInventory.Remove(player, ItemType.BagWithMoney, 1);
                        player.SetClothes(5, 0, 0);
                        player.ResetData("HAND_MONEY");
                    }
                    if (player.HasData("HEIST_DRILL"))
                    {
                        nInventory.Remove(player, ItemType.BagWithDrill, 1);
                        player.SetClothes(5, 0, 0);
                        player.ResetData("HEIST_DRILL");
                    }
                    var item = nInventory.Find(Main.Players[player].UUID, ItemType.MoneyHeist);
                    if (item != null)
                    {
                        nInventory.Remove(player, ItemType.MoneyHeist, item.Count);
                        Dashboard.sendItems(player);
                    }
                    MoneySystem.Wallet.Change(player, 20000);
                    GameLog.Money($"server", $"player({Main.Players[player].UUID})", 200, $"policeAward");
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы получили вознаграждение в размере 20.000$", 3000);
                    return;
                case 73:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Нэнси Спунген", "Сотрудник LSPD", "Привет, ты в главном здании Los Santos Police Department, чем могу помочь?", (new QuestAnswer("Как здесь работать?", 70), new QuestAnswer("Купить лицензию на оружие", 74), new QuestAnswer("Сдать вещи", 72), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 80:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Приходите на наборы в полицию штата, ождиайте новостей от Weazel News!", (new QuestAnswer("Ясно", 83), 0));
                    return;
                case 81:
                    Trigger.ClientEvent(player, "client::closedialog");
                    if (Main.Players[player].Money < 15000)
                    {
                        Notify.Error(player, "Недостаточно средств");
                        return;
                    }
                    if (Main.Players[player].Licenses[7] == true)
                    {
                        Notify.Error(player, "У вас уже есть медицинская карта");
                        return;
                    }
                    MoneySystem.Wallet.Change(player, -9000);
                    Main.Players[player].Licenses[7] = true;
                    Fractions.Stocks.fracStocks[8].Materials += 9000;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы купили медицинскую карту", 3000);
                    return;
                case 84:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Вы уверены что хотите купить медицинскую карту за 90 000$?", (new QuestAnswer("Да, давайте", 81), new QuestAnswer("Позже", 83)));
                    return;
                case 82:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    if (player.Health > 99)
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Вы не нуждаетесь в лечении! Надеюсь больше не увидимся", (new QuestAnswer("Ясно", 83), 0));
                    else
                        Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Я выписал вам лечение за 100$, каждую минуту вы будете получать по 20 ХП", (new QuestAnswer("Спасибо!", 85), 0));
                    return;
                case 83:
                    Trigger.ClientEvent(player, "client::closedialog2");
                    Trigger.ClientEvent(player, "client::opendialogmenu", true, "Дэвид Харис", "Доктор", "Привет ты в главной больнице города Los Santos! Чем могу помочь?", (new QuestAnswer("Как здесь работать?", 80), new QuestAnswer("Купить медицинскую карту", 84), new QuestAnswer("Лечение", 82), new QuestAnswer("Спасибо, я сам", 2)));
                    return;
                case 85:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Fractions.Ems.GetHealthEms(player);
                    return;
                case 90:
                    Trigger.ClientEvent(player, "client::closedialog");
                    if (!player.HasData("HAND_MONEY") && !player.HasData("HEIST_DRILL"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"У Вас нет ни сумки с деньгами, ни сумки с дрелью", 3000);
                        return;
                    }
                    if (player.HasData("HAND_MONEY"))
                    {
                        nInventory.Remove(player, ItemType.BagWithMoney, 1);
                        player.SetClothes(5, 0, 0);
                        player.ResetData("HAND_MONEY");
                    }
                    if (player.HasData("HEIST_DRILL"))
                    {
                        nInventory.Remove(player, ItemType.BagWithDrill, 1);
                        player.SetClothes(5, 0, 0);
                        player.ResetData("HEIST_DRILL");
                    }
                    item = nInventory.Find(Main.Players[player].UUID, ItemType.MoneyHeist);
                    if (item != null)
                    {
                        nInventory.Remove(player, ItemType.MoneyHeist, item.Count);
                        Dashboard.sendItems(player);
                    }
                    MoneySystem.Wallet.Change(player, 20000);
                    GameLog.Money($"server", $"player({Main.Players[player].UUID})", 200, $"policeAward");
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы получили вознаграждение в размере 20.000$", 3000);
                    return;
                case 101:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Core.Hijacking.StartHijacking(player);
                    return;
                case 102:
                    Trigger.ClientEvent(player, "client::closedialog");
                    Fractions.HijackingHouse.TakeHouseHijacking(player);
                    return;
                case 103:
                    Trigger.ClientEvent(player, "client::closedialog");
                    item = nInventory.Find(Main.Players[player].UUID, ItemType.MoneyHeist);
                    if (item != null)
                    {
                        nInventory.Remove(player, ItemType.MoneyHeist, item.Count);
                        Dashboard.sendItems(player);
                        var payment = item.Count * 0.85;
                        var payment2 = item.Count * 0.15;

                        Fractions.Stocks.fracStocks[Main.Players[player].FractionID].Materials += Convert.ToInt32(payment2);
                        MoneySystem.Wallet.Change(player, Convert.ToInt32(payment));
                        Notify.Succ(player, $"Вы получили {payment}$");
                        return;
                    }
                    else
                    {
                        Notify.Error(player, "У Вас нет пачек с деньгами");
                    }
                    return;
            }
        }
    }
}
