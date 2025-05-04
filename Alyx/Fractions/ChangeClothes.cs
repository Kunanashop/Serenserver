using Alyx;
using Alyx.Core;
using Alyx.Fractions;
using AlyxSDK;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Space.Fractions
{
    class FractionClothes
    {
        public static List<FractionClothes> FractionClothesList = new List<FractionClothes>();
        public int Type { get; set; }
        public int Drawable { get; set; }
        public int Texture { get; set; }
        public string Name { get; set; }
        public FractionClothes(int type, int drawable, int texture, string name)
        {
            Type = type;
            Drawable = drawable;
            Texture = texture;
            Name = name;
            FractionClothesList.Add(this);
        }
    }
    class ChangeClothes : Script
    {
        private static nLog Log = new nLog("ChangeClothes");
        #region Dictionary`s
        private static Dictionary<int, Vector3> FractionColshape = new Dictionary<int, Vector3>
        {
            { 7, new Vector3(463.5568, -999.0581, 29.569565) },
            { 9, new Vector3(2518.5847, -340.4426, 100.773346) },
            { 14, new Vector3(-2358.2505, 3254.0122, 31.690718) },
            { 6, new Vector3(-571.0057, -195.54567, 37.04884) },
        };
        private static Dictionary<int, List<FractionClothes>> Clothes = new Dictionary<int, List<FractionClothes>>
        {
            { 6, new List<FractionClothes> {
                // Type - Тип одежды, Drawable - Айди одежды, Texture - Вариация
                // 12 Type - Шапки
                new FractionClothes(12, 28, 0, "Чёрная шапка"),
                new FractionClothes(12, 4, 0, "Чёрная кепка"),
                new FractionClothes(12, 6, 0, "Армейская кепка"),
                new FractionClothes(13, 3, 0, "Серые очки"),
                // 12 Type - Очки
                new FractionClothes(13, 30, 0, "На тусу"),
                new FractionClothes(13, 21, 0, "Очки со звездочками"),
                // 14 Часы
                new FractionClothes(14, 4, 0, "Золотые часы"),
                new FractionClothes(14, 0, 0, "Серебрянные часы"),

                new FractionClothes(1, 28, 0, "Черная закрытая"),
                new FractionClothes(1, 35, 0, "Балаклава"),
                new FractionClothes(1, 37, 0, "Носок"),
                new FractionClothes(1, 52, 0, "Черная с отверстием для глаз"),
                new FractionClothes(1, 130, 0, "Респиратор"),
                new FractionClothes(1, 132, 0, "Маска с ПНВ"),
                new FractionClothes(4, 23, 0, "Серые штаны"),
                new FractionClothes(4, 59, 0, "Зеленые спортивные штаны"),
                new FractionClothes(4, 33, 0, "Чёрные военные штаны"),
                new FractionClothes(4, 80, 0, "Коричневые штаны"),
                new FractionClothes(4, 86, 0, "Камуфляжные укороченные штаны"),
                new FractionClothes(11, 44, 0, "Салатовая майка"),
                new FractionClothes(11, 51, 0, "Новогодняя майка"),
                new FractionClothes(11, 81, 0, "Черная поло"),
                new FractionClothes(11, 30, 0, "Пиджак красивый"),
                new FractionClothes(11, 234, 0, "Черная рубашка"),
            }},
            { 7, new List<FractionClothes> {
                // Type - Тип одежды, Drawable - Айди одежды, Texture - Вариация
                // 12 Type - Шапки
                new FractionClothes(12, 28, 0, "Чёрная шапка"),
                new FractionClothes(12, 4, 0, "Чёрная кепка"),
                new FractionClothes(12, 6, 0, "Армейская кепка"),
                new FractionClothes(13, 3, 0, "Серые очки"),
                // 12 Type - Очки
                new FractionClothes(13, 30, 0, "На тусу"),
                new FractionClothes(13, 21, 0, "Очки со звездочками"),
                // 14 Часы
                new FractionClothes(14, 4, 0, "Золотые часы"),
                new FractionClothes(14, 0, 0, "Серебрянные часы"),

                new FractionClothes(1, 28, 0, "Черная закрытая"),
                new FractionClothes(1, 35, 0, "Балаклава"),
                new FractionClothes(1, 37, 0, "Носок"),
                new FractionClothes(1, 52, 0, "Черная с отверстием для глаз"),
                new FractionClothes(1, 130, 0, "Респиратор"),
                new FractionClothes(1, 132, 0, "Маска с ПНВ"),
                new FractionClothes(4, 23, 0, "Серые штаны"),
                new FractionClothes(4, 59, 0, "Зеленые спортивные штаны"),
                new FractionClothes(4, 33, 0, "Чёрные военные штаны"),
                new FractionClothes(4, 80, 0, "Коричневые штаны"),
                new FractionClothes(4, 86, 0, "Камуфляжные укороченные штаны"),
                new FractionClothes(11, 44, 0, "Салатовая майка"),
                new FractionClothes(11, 51, 0, "Новогодняя майка"),
                new FractionClothes(11, 81, 0, "Черная поло"),
                new FractionClothes(11, 142, 0, "Черный пиджак"),
                new FractionClothes(11, 234, 0, "Черная рубашка"),
            }},
            { 9, new List<FractionClothes> {
                // Type - Тип одежды, Drawable - Айди одежды, Texture - Вариация
                // 12 Type - Шапки
                new FractionClothes(12, 28, 0, "Чёрная шапка"),
                new FractionClothes(12, 4, 0, "Чёрная кепка"),
                new FractionClothes(12, 6, 0, "Армейская кепка"),
                new FractionClothes(13, 3, 0, "Серые очки"),
                // 12 Type - Очки
                new FractionClothes(13, 30, 0, "На тусу"),
                new FractionClothes(13, 21, 0, "Очки со звездочками"),
                // 14 Часы
                new FractionClothes(14, 4, 0, "Золотые часы"),
                new FractionClothes(14, 0, 0, "Серебрянные часы"),

                new FractionClothes(1, 28, 0, "Черная закрытая"),
                new FractionClothes(1, 35, 0, "Балаклава"),
                new FractionClothes(1, 37, 0, "Носок"),
                new FractionClothes(1, 52, 0, "Черная с отверстием для глаз"),
                new FractionClothes(1, 130, 0, "Респиратор"),
                new FractionClothes(1, 132, 0, "Маска с ПНВ"),
                new FractionClothes(4, 23, 0, "Серые штаны"),
                new FractionClothes(4, 59, 0, "Зеленые спортивные штаны"),
                new FractionClothes(4, 33, 0, "Чёрные военные штаны"),
                new FractionClothes(4, 80, 0, "Коричневые штаны"),
                new FractionClothes(4, 86, 0, "Камуфляжные укороченные штаны"),
                new FractionClothes(11, 44, 0, "Салатовая майка"),
                new FractionClothes(11, 51, 0, "Новогодняя майка"),
                new FractionClothes(11, 81, 0, "Черная поло"),
                new FractionClothes(11, 142, 0, "Черный пиджак"),
                new FractionClothes(11, 234, 0, "Черная рубашка"),
            }},
            { 14, new List<FractionClothes> {
                new FractionClothes(12, 13, 0, "Ковбойская шляпа"),
                new FractionClothes(12, 14, 0, "Белая бандана"),
                new FractionClothes(12, 6, 0, "Армейская кепка"),
                new FractionClothes(13, 24, 0, "Очки летчика"),
                new FractionClothes(13, 25, 0, "Зимние очки"),
                new FractionClothes(13, 21, 0, "Очки со звездочками"),

                new FractionClothes(14, 4, 0, "Золотые часы"),
                new FractionClothes(14, 0, 0, "Серебрянные часы"),

                new FractionClothes(1, 52, 0, "Черная с отверстием для глаз"),
                new FractionClothes(1, 130, 0, "Респиратор"),
                new FractionClothes(1, 132, 0, "Маска с ПНВ"),
                new FractionClothes(4, 24, 0, "Чёрные деловые штаны"),
                new FractionClothes(4, 34, 0, "Чёрные Болотные штаны"),
                new FractionClothes(4, 46, 0, "Серые болотные штаны"),
                new FractionClothes(4, 79, 0, "Чёрные спортивные штаны"),
                new FractionClothes(11, 55, 0, "Полицейская рубашка"),
            }}
        };
        #endregion
        #region Return`s Methods
        private static string NameClothes(int type)
        {
            string text = "";
            switch (type)
            {
                case 1:
                    text = "Маски";
                    break;
                case 4:
                    text = "Штаны";
                    break;
                case 11:
                    text = "Вверх";
                    break;
                case 12:
                    text = "Шапки";
                    break;
                case 13:
                    text = "Очки";
                    break;
                case 14:
                    text = "Часы";
                    break;
            }
            return text;
        }
        #endregion
        [RemoteEvent("ClearPersonFractionChanger:Server")]
        public static void ClearPersonFractionChanger_Server(Player player)
        {
            try
            {
                if (!Main.Players.ContainsKey(player) || Main.Players[player].FractionID == 0) return;
                if (player.HasData("ON_DUTY") && player.GetData<bool>("ON_DUTY"))
                {
                    Customization.ApplyCharacter(player);
                    if (player.HasData("HAND_MONEY"))
                        player.SetClothes(5, 45, 0);
                    else if (player.HasData("HEIST_DRILL"))
                        player.SetClothes(5, 41, 0);
                    player.SetData("ON_DUTY", false);
                    return;
                }
            }
            catch (Exception ex) { Log.Write("ERROR: " + ex.ToString(), nLog.Type.Error); }
        }
        [RemoteEvent("ChangeNowFracClothes:Server")]
        public static void ChangeNowFracClothes_Server(Player player, string name)
        {
            try
            {
                if (!Main.Players.ContainsKey(player) || Main.Players[player].FractionID == 0) return;
                FractionClothes fracClothes = FractionClothes.FractionClothesList.Find(i => i.Name == name);
                if (fracClothes != null)
                {
                    switch (fracClothes.Type)
                    {
                        case 12:
                            Customization.SetHat(player, fracClothes.Drawable, fracClothes.Texture);
                            break;
                        case 13:
                            player.SetAccessories(1, fracClothes.Drawable, fracClothes.Texture);
                            break;
                        case 14:
                            player.SetAccessories(6, fracClothes.Drawable, fracClothes.Texture);
                            break;
                        default:
                            player.SetClothes(fracClothes.Type, fracClothes.Drawable, fracClothes.Texture);
                            break;
                    }
                    player.SetData("ON_DUTY", true);
                }
            }
            catch (Exception ex) { Log.Write("ERROR: " + ex.ToString(), nLog.Type.Error); }
        }
        public static void EnterChangeClothes(Player player)
        {
            try
            {
                if (!Main.Players.ContainsKey(player) || !player.HasData("FractionChangeClothes") || Main.Players[player].FractionID == 0) return;
                if (Main.Players[player].FractionID == player.GetData<int>("FractionChangeClothes") && Clothes.ContainsKey(Main.Players[player].FractionID))
                {
                    Dictionary<string, List<string>> dataClothes = new Dictionary<string, List<string>>();
                    List<string> listCLothes = new List<string>();
                    List<FractionClothes> frac = Clothes[Main.Players[player].FractionID];
                    foreach (var item in frac)
                    {
                        string nameType = NameClothes(item.Type);
                        if (dataClothes.ContainsKey(nameType))
                            dataClothes[nameType].Add(item.Name);
                        else
                            dataClothes.Add(nameType, new List<string> { item.Name });
                    }
                    Alyx.Trigger.ClientEvent(player, "OpenClothesChange", JsonConvert.SerializeObject(dataClothes), Manager.FractionNames[Main.Players[player].FractionID], Main.Players[player].FractionID);
                }
            }
            catch (Exception e) { Log.Write("EnterChangeClothes: " + e.ToString(), nLog.Type.Error); }
        }
        [ServerEvent(Event.ResourceStart)]
        public static void onResourceStart()
        {
            try
            {
                foreach (KeyValuePair<int, Vector3> data in FractionColshape)
                {
                    ColShape shape = NAPI.ColShape.CreateCylinderColShape(data.Value, 1, 2, 0);
                    shape.SetData("INTERACT", 2500);
                    shape.OnEntityEnterColShape += (s, e) =>
                    {
                        if (Main.Players.ContainsKey(e))
                        {
                            e.SetData("INTERACTIONCHECK", 2500);
                            e.SetData("FractionChangeClothes", data.Key);
                        }
                    };
                    shape.OnEntityExitColShape += (s, e) =>
                    {
                        if (Main.Players.ContainsKey(e))
                        {
                            e.ResetData("FractionChangeClothes");
                            e.ResetData("INTERACTIONCHECK");
                        }
                    };
                }
            }
            catch (Exception e) { Log.Write("onResourceStart: " + e.ToString(), nLog.Type.Error); }
        }
    }
}