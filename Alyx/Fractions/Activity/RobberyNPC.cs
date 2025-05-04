using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Alyx.Core;
using Alyx;
using AlyxSDK;
using System.Diagnostics;

class RobberyNPC : Script
{

    public static TimerEx[] player_robbery_timer { get; set; } = new TimerEx[500];


    public static int MAX_ROBBERY_NPC = 0;
    public class RobberyNPCEnum : IEquatable<RobberyNPCEnum>
    {
        public int id { get; set; }

        public string name { get; set; }
        public string model { get; set; }
        public Vector3 position { get; set; }
        public Vector3 caixa_1 { get; set; }
        public Vector3 caixa_2 { get; set; }

        public float heading { get; set; }
        public int robbery_state { get; set; }
        public int money { get; set; }
        //public Client owned { get; set; }
        public int time_remaining { get; set; }
        public int lastfraction { get; set; }
        public DateTime time_vulnerable { get; set; }
        public int players_aiming { get; set; }

        public int cash_amount { get; set; }
        public bool activeintime { get; set; }
  


        public static void SetRandomActiveHeistBusiness(Player player)
        {
            var rnd = new Random().Next(0, MAX_ROBBERY_NPC + 1);
            var rnd2 = new Random().Next(0, MAX_ROBBERY_NPC + 1);
            robbery_npc[rnd].activeintime = true;
        }
        public override int GetHashCode()
        {
            return id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            RobberyNPCEnum objAsPart = obj as RobberyNPCEnum;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(RobberyNPCEnum other)
        {
            if (other == null) return false;
            return (this.id.Equals(other.id));
        }
    }
    public static List<RobberyNPCEnum> robbery_npc = new List<RobberyNPCEnum>();


    public static void CreateRobberyNPC(string name, string model, Vector3 position, float heading, Vector3 caixa_1, Vector3 caixa_2)
    {
        robbery_npc.Add(new RobberyNPCEnum { id = MAX_ROBBERY_NPC, name = name, model = model, position = position, heading = heading, robbery_state = 0, players_aiming = 0, caixa_1 = caixa_1, caixa_2 = caixa_2, activeintime = true});

        MAX_ROBBERY_NPC++;
    }

    public RobberyNPC()
    {
        CreateRobberyNPC("Магазин 24/7 #2", "mp_m_shopkeep_01", new Vector3(24.47675, -1347.312, 29.49702), 266.0985f, new Vector3(24.47675, -1347.312, 29.49702), new Vector3(24.47675, -1347.312, 29.49702));
        CreateRobberyNPC("Магазин 24/7 #15", "mp_m_shopkeep_01", new Vector3(-1221.522, -907.9908, 12.32635), 31f, new Vector3(-1221.522, -907.9908, 12.32635), new Vector3(-1221.522, -907.9908, 12.32635));
        CreateRobberyNPC("Магазин 24/7 #12", "mp_m_shopkeep_01", new Vector3(1698.427, 4922.392, 42.063), 328f, new Vector3(1698.427, 4922.392, 42.063), new Vector3(1698.427, 4922.392, 42.063));
        CreateRobberyNPC("Магазин 24/7 #1", "mp_m_shopkeep_01", new Vector3(-46.28725, -1757.315, 29.421), 61f, new Vector3(-46.28725, -1757.315, 29.421), new Vector3(-46.28725, -1757.315, 29.421));
        CreateRobberyNPC("Магазин 24/7 #8", "mp_m_shopkeep_01", new Vector3(1727.767, 6414.963, 35.03722), 217f, new Vector3(1727.767, 6414.963, 35.03722), new Vector3(1727.767, 6414.963, 35.03722));
        CreateRobberyNPC("Магазин 24/7 #9", "mp_m_shopkeep_01", new Vector3(2678.398, 3279.246, 55.24113), 328f, new Vector3(2678.398, 3279.246, 55.24113), new Vector3(2678.398, 3279.246, 55.24113));
        CreateRobberyNPC("Магазин 24/7 #7", "mp_m_shopkeep_01", new Vector3(-3038.612, 584.4379, 7.908929), 15f, new Vector3(-3038.612, 584.4379, 7.908929), new Vector3(-3038.612, 584.4379, 7.908929));
        //CreateRobberyNPC("Магазин 24/7 #15", "mp_m_shopkeep_01", new Vector3(1392.335, 3606.429, 34.98089), 191f, new Vector3(1392.335, 3606.429, 34.98089), new Vector3(1392.335, 3606.429, 34.98089));
        CreateRobberyNPC("Магазин 24/7 #16", "mp_m_shopkeep_01", new Vector3(-706.1262, -912.9778, 19.21559), 80f, new Vector3(-706.1262, -912.9778, 19.21559), new Vector3(-706.1262, -912.9778, 19.21559));
        CreateRobberyNPC("Магазин 24/7 #4", "mp_m_shopkeep_01", new Vector3(372.539, 326.2779, 103.5665), 248f, new Vector3(372.539, 326.2779, 103.5665), new Vector3(372.539, 326.2779, 103.5665));
        CreateRobberyNPC("Магазин 24/7 #3", "mp_m_shopkeep_01", new Vector3(1134.11, -981.9982, 46.41585), 275f, new Vector3(1134.11, -981.9982, 46.41585), new Vector3(1134.11, -981.9982, 46.41585));
        CreateRobberyNPC("Магазин 24/7 #5", "mp_m_shopkeep_01", new Vector3(1164.663, -322.4549, 69.20505), 96f, new Vector3(1164.663, -322.4549, 69.20505), new Vector3(1164.663, -322.4549, 69.20505));
        CreateRobberyNPC("Магазин 24/7 #14", "mp_m_shopkeep_01", new Vector3(1166.283, 2710.754, 38.15771), 173f, new Vector3(1166.283, 2710.754, 38.15771), new Vector3(1164.663, -322.4549, 69.20505));
        CreateRobberyNPC("Магазин 24/7 #11", "mp_m_shopkeep_01", new Vector3(1960.146, 3739.983, 32.34378), 292f, new Vector3(1960.146, 3739.983, 32.34378), new Vector3(1960.146, 3739.983, 32.34378));
        CreateRobberyNPC("Магазин 24/7 #6", "mp_m_shopkeep_01", new Vector3(-3242.17, 1000.008, 12.83071), 353f, new Vector3(-3242.17, 1000.008, 12.83071), new Vector3(-3242.17, 1000.008, 12.83071));
        CreateRobberyNPC("Магазин 24/7 #13", "mp_m_shopkeep_01", new Vector3(549.0481, 2671.355, 42.1564), 90f, new Vector3(549.0481, 2671.355, 42.1564), new Vector3(549.0481, 2671.355, 42.1564));
        //CreateRobberyNPC("Магазин 24/7 #15", "mp_m_shopkeep_01", new Vector3(-2966.496, 390.5234, 15.0433), 80f, new Vector3(-2966.496, 390.5234, 15.0433), new Vector3(-2966.496, 390.5234, 15.0433));
        CreateRobberyNPC("Магазин 24/7 #10", "mp_m_shopkeep_01", new Vector3(2557.383, 380.8376, 108.623), 0f, new Vector3(2557.383, 380.8376, 108.623), new Vector3(2557.383, 380.8376, 108.623));

        OnRobberyTimer();
    }

    public static void OnPlayerConnect(Player player)
    {
        player.ResetData("store_rob");
        player.SetData<bool>("status", true);
        player.SetSharedData("Player_Aiming_To", -1);
        int index = 0;
        foreach (var robbery in robbery_npc)
        {
            player.TriggerEvent("CreateRobberyNPC", "robbery_npc_" + index, robbery.model, robbery.position, robbery.heading, index);
            index++;
        }
    }


    public void OnRobberyTimer()
    {
        TimerEx.SetTimer(() =>
        {
            int index = 0;
            foreach (var robbery in robbery_npc)
            {

                if (robbery.robbery_state == 1)
                {
                    if (robbery.time_remaining > 0)
                    {
                        foreach (var player in NAPI.Pools.GetAllPlayers())
                        {
                            if (player.GetData<bool>("status") == true && Main.IsInRangeOfPoint(player.Position, robbery.position, 30.0f) && player.GetSharedData<int>("Player_Aiming_To") == index)
                            {
                                robbery.time_remaining--;
                                Alyx.Trigger.ClientEvent(player, "client::setTimerHud", robbery.time_remaining);
                            }
                            else 
                            {
                                Alyx.Trigger.ClientEvent(player, "client::setTimerHud", 0);
                            }
                        }
                    }

                    if (robbery.time_remaining == 0)
                    {
                        robbery.robbery_state = 0;
                        robbery.time_remaining = 0;

                        robbery.time_vulnerable = DateTime.Now.AddMinutes(60);
                        UpdateRobberyState(index, 0);
                    }

                    int count = 0;
                    foreach (var player in NAPI.Pools.GetAllPlayers())
                    {
                        if (player.GetData<bool>("status") == true && Main.IsInRangeOfPoint(player.Position, robbery.position, 30.0f) && player.GetSharedData<int>("Player_Aiming_To") == index)
                        {
                            count++;
                        }
                        if (robbery.time_remaining == 0 && player.GetData<bool>("status") == true && Main.IsInRangeOfPoint(player.Position, robbery.position, 30.0f) && player.GetSharedData<int>("Player_Aiming_To") == index)
                        {
                            robbery.lastfraction = Main.Players[player].FractionID;
                            var rnd = new Random().Next(1500, 14000);
                            Notify.Succ(player, $"Вы ограбили магазин на {rnd}$");
                            var item = new nItem(ItemType.MoneyHeist, rnd);
                            robbery.activeintime = false;
                            var obj = NAPI.Object.CreateObject(nInventory.ItemModels[ItemType.MoneyHeist], robbery.position + new Vector3(0, 0, 1), new Vector3(0, 0, 0), 255, 0);
                            obj.SetSharedData("TYPE", "DROPPED");
                            obj.SetSharedData("PICKEDT", false);
                            obj.SetSharedData("HEISTMONEY", true);
                            obj.SetData("ITEM", item);
                            var id = new Random().Next(1500, 14000);
                            while (Items.ItemsDropped.Contains(id)) id = new Random().Next(5000, 13999);
                            obj.SetData("ID", id);
                        }
                    }

                    if (count == 0)
                    {
                        UpdateRobberyState(index, 0);
                    }
                    else
                    {
                        UpdateRobberyState(index, 1);
                    }
                }
                else
                {
                    UpdateRobberyState(index, 0);
                }
                index++;
            }
        }, 1000, 0);
    }

    public static void UpdateRobberyState(int index, int state)
    {
        foreach (var player in NAPI.Pools.GetAllPlayers())
        {
            if (player.GetData<bool>("status") == true && Main.IsInRangeOfPoint(player.Position, robbery_npc[index].position, 30.0f))
            {
                player.TriggerEvent("SetRobberyState", "robbery_npc_" + index, state);
            }
        }
    }

    [RemoteEvent("Players_Aiming_To")]
    public static void startRobbery(Player player, int index)
    {
        if (index != -1)
        {
            if (Main.IsInRangeOfPoint(player.Position, robbery_npc[index].position, 10.0f))
            {
                if (robbery_npc[index].robbery_state == 0)
                {
                    if (robbery_npc[index].activeintime == false)
                    {
                        if (Main.Players[player].FractionID != robbery_npc[index].lastfraction) { 
                            if (!player.HasData("temp_message"))
                            {
                                player.SetData("temp_message", false);
                            }
                            if (player.HasData("temp_message") && player.GetData<bool>("temp_message") == false)
                            {
                                player.SetData("temp_message", true);
                                Notify.Error(player, $"Этот магазин уже был ограблен");
                            }

                            TimerEx.SetTimer(() =>
                            {
                                player.SetData("temp_message", false);
                            }, 10000, 1);
                            return;
                        }
                    }

                    if (!player.HasData("ActiveWeaponRobbey") || player.GetData<bool>("ActiveWeaponRobbey") == false)
                    {
                        return;
                    }

                    int can_pass = 0;

                    if (NAPI.Player.GetPlayerCurrentWeapon(player) != WeaponHash.Unarmed)
                    {
                        can_pass = 1;
                    }
                    else if (NAPI.Player.GetPlayerCurrentWeapon(player) != WeaponHash.Unarmed)
                    {
                        can_pass = 1;
                    }

                    if (can_pass == 0)
                    {
                        return;
                    }

                    if (Main.Players[player].FractionID == 0 || Alyx.Fractions.Manager.FractionTypes[Main.Players[player].FractionID] == 2)
                    {
                        return;
                    }

                    int count = 0;
                    foreach (var target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.GetData<bool>("status") == true && Main.IsInRangeOfPoint(player.Position, target.Position, 20.0f))
                        {
                            count++;
                        }
                    }

                    int faction_id = Main.Players[player].FractionID;

                    foreach (var target in NAPI.Pools.GetAllPlayers())
                    {
                        if (Main.Players[target].FractionID == 7 || Main.Players[target].FractionID == 9 || Main.Players[target].FractionID == 18)
                        {
                            Alyx.Trigger.ClientEvent(target, "createHeistBizMark", robbery_npc[index].position, robbery_npc[index].id);
                            Alyx.Fractions.Manager.fractionChat(target, $"Совершенно ограбление в {robbery_npc[index].name}");
                            Notify.Info(player, "Поступил вызов полиции");
                            NAPI.Task.Run(() =>
                            {
                                try
                                {
                                    if (target != null)
                                    {
                                        Alyx.Trigger.ClientEvent(target, "deleteHeistBizMark", robbery_npc[index].id);
                                    }
                                }
                                catch { }
                            }, 600000);
                        }
                    }

                    foreach (var target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.GetData<bool>("status") == true && Main.IsInRangeOfPoint(target.Position, player.Position, 20.0f))
                        {
                            
                        }
                    }

                    robbery_npc[index].time_remaining = 60;
                    robbery_npc[index].cash_amount = 400;
                    robbery_npc[index].robbery_state = 1;
                    UpdateRobberyState(index, 1);
                }
            }
        }
        player.SetSharedData("Player_Aiming_To", index);
    }
}
public class TimerEx : Script
{

    /// <summary>A sorted List of Timers</summary>
    private static readonly List<TimerEx> timer = new List<TimerEx>();
    /// <summary>List used to put the Timers in timer-List after the possible List-iteration</summary>
    private static List<TimerEx> insertAfterList = new List<TimerEx>();
    /// <summary>Stopwatch to get the tick counts (Environment.TickCount is only int)</summary>
    private static Stopwatch stopwatch = new Stopwatch();

    /// <summary>The Action getting called by the Timer. Can be changed dynamically.</summary>
    public Action Func;
    /// <summary>After how many milliseconds (after the last execution) the timer should get called. Can be changed dynamically</summary>
    public readonly uint ExecuteAfterMs;
    /// <summary>When the Timer is ready to execute (Stopwatch is used).</summary>
    private ulong executeAtMs;
    /// <summary>How many executes the timer has left - use 0 for infinitely. Can be changed dynamically</summary>
    public uint ExecutesLeft;
    /// <summary>If the Timer should handle exceptions with a try-catch-finally. Can be changed dynamically</summary>
    public bool HandleException;
    /// <summary>If the Timer will get removed.</summary>
    private bool willRemoved = false;
    /// <summary>Use this to check if the timer is still running.</summary>
    public bool IsRunning
    {
        get
        {
            return !willRemoved;
        }
    }

    /// <summary>
    /// Only used for Script!
    /// </summary>
    public TimerEx()
    {
        stopwatch.Start();
    }

    /// <summary>
    /// Constructor used to create the Timer.
    /// </summary>
    /// <param name="thefunc">The Action which you want to get called.</param>
    /// <param name="executeafterms">Execute the Action after milliseconds. If executes is more than one, this gets added to executeatms.</param>
    /// <param name="executeatms">Execute at milliseconds.</param>
    /// <param name="executes">How many times to execute. 0 for infinitely.</param>
    /// <param name="handleexception">If try-catch-finally should be used when calling the Action</param>
    private TimerEx(Action thefunc, uint executeafterms, ulong executeatms, uint executes, bool handleexception)
    {
        Func = thefunc;
        ExecuteAfterMs = executeafterms;
        executeAtMs = executeatms;
        ExecutesLeft = executes;
        HandleException = handleexception;
    }

    /// <summary>
    /// Use this method to create the Timer.
    /// </summary>
    /// <param name="thefunc">The Action which you want to get called.</param>
    /// <param name="executeafterms">Execute after milliseconds.</param>
    /// <param name="executes">Amount of executes. Use 0 for infinitely.</param>
    /// <param name="handleexception">If try-catch-finally should be used when calling the Action</param>
    /// <returns></returns>
    public static TimerEx SetTimer(Action thefunc, uint executeafterms, uint executes = 1, bool handleexception = false)
    {
        ulong executeatms = executeafterms + GetTick();
        TimerEx thetimer = new TimerEx(thefunc, executeafterms, executeatms, executes, handleexception);
        insertAfterList.Add(thetimer);   // Needed to put in the timer later, else it could break the script when the timer gets created from a Action of another timer.
        return thetimer;
    }

    /// <summary>
    /// Method to get the elapsed milliseconds.
    /// </summary>
    /// <returns>Elapsed milliseconds</returns>
    private static ulong GetTick()
    {
        return (ulong)stopwatch.ElapsedMilliseconds;
    }

    /// <summary>
    /// Use this method to stop the Timer.
    /// </summary>
    public void Kill()
    {
        willRemoved = true;
    }

    /// <summary>
    /// Executes a timer.
    /// </summary>
    private void ExecuteMe()
    {
        Func();
        if (ExecutesLeft == 1)
        {
            ExecutesLeft = 0;
            willRemoved = true;
        }
        else
        {
            if (ExecutesLeft != 0)
                ExecutesLeft--;
            executeAtMs += ExecuteAfterMs;
            insertAfterList.Add(this);
        }
    }

    /// <summary>
    /// Executes a timer with try-catch-finally. 
    /// </summary>
    private void ExecuteMeSafe()
    {
        try
        {
            Func();
        }
        catch (Exception ex)
        {
            //Log.Error ( ex.ToString() );
            NAPI.Util.ConsoleOutput(ex.ToString());
        }
        finally
        {
            if (ExecutesLeft == 1)
            {
                ExecutesLeft = 0;
                willRemoved = true;
            }
            else
            {
                if (ExecutesLeft != 0)
                    ExecutesLeft--;
                executeAtMs += ExecuteAfterMs;
                insertAfterList.Add(this);
            }
        }
    }

    /// <summary>
    /// Executes the timer now.
    /// </summary>
    /// <param name="changeexecutems">If the timer should change it's execute-time like it would have been executed now. Use false to ONLY execute it faster this time.</param>
    public void Execute(bool changeexecutems = true)
    {
        if (changeexecutems)
        {
            executeAtMs = GetTick();
        }
        if (HandleException)
            ExecuteMeSafe();
        else
            ExecuteMe();
    }

    /// <summary>
    /// Used to insert the timer back to timer-List with sorting.
    /// </summary>
    private void InsertSorted()
    {
        bool putin = false;
        for (int i = timer.Count - 1; i >= 0 && !putin; i--)
            if (executeAtMs <= timer[i].executeAtMs)
            {
                timer.Insert(i + 1, this);
                putin = true;
            }

        if (!putin)
            timer.Insert(0, this);
    }

    /// <summary>
    /// Iterate the timers and call the Action of the ready/finished ones.
    /// If IsRunning is false, the timer gets removed/killed.
    /// Because the timer-List is sorted, the iteration stops when a timer is not ready yet, cause then the others won't be ready, too.
    /// </summary>

    [ServerEvent(Event.Update)]
    public static void OnUpdateFunc()
    {
        ulong tick = GetTick();
        for (int i = timer.Count - 1; i >= 0; i--)
        {
            if (!timer[i].willRemoved)
            {
                if (timer[i].executeAtMs <= tick)
                {
                    TimerEx thetimer = timer[i];
                    timer.RemoveAt(i);   // Remove the timer from the list (because of sorting and executeAtMs will get changed)
                    if (thetimer.HandleException)
                        thetimer.ExecuteMeSafe();
                    else
                        thetimer.ExecuteMe();
                }
                else
                    break;
            }
            else
                timer.RemoveAt(i);
        }

        // Put the timers back in the list
        if (insertAfterList.Count > 0)
        {
            foreach (TimerEx timer in insertAfterList)
            {
                timer.InsertSorted();
            }
            insertAfterList.Clear();
        }
    }
}
