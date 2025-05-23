﻿global.playerheading = require("src/libs/rotatorplayer.js");
global.cameraRotator = require("browser/assets/js/vie.js"); //rotate
global.needped = [];
global.isInSafeZone = false;
global.debounceEvent = (ms, triggerCouns, fn) => {
    let g_swapDate = Date.now();
    let g_triggersCount = 0;

    return (...args) => {
        if (++g_triggersCount > triggerCouns) {
            let currentDate = Date.now();

            if ((currentDate - g_swapDate) > ms) {
                g_swapDate = currentDate;
                g_triggersCount = 0;
            } else {
                return true; // cancel event trigger
            }
        }

        fn(...args);
    };
};

/* Events error handling */
global.train = null;

const eventsMap = new Map();
const eventsAdd = Symbol('eventsAdd');
const rendersTicks = new Map();
let renderId = -1;
let isRenderDebugActive = false;
global.isGodModeActive = false;
var to = false;

mp.events[eventsAdd] = mp.events.add;
const __eventAdd__ = (eventName, eventFunction, name) => {
    if (
        eventName === 'render' &&
        (
            typeof name !== 'string' ||
            !name.length
        )
    ) {
        renderId++;
        name = renderId;
    }

    const proxyEventFunction = new Proxy(eventFunction, {
        apply: (target, thisArg, argumentsList) => {
            try {
                const start = Date.now();

                target.apply(thisArg, argumentsList);

                if (eventName === 'render') {
                    rendersTicks.set(name, Date.now() - start);
                }
            } catch(e) {
                mp.game.graphics.notify(`${eventName}:error:1`);
                mp.console.logError(`${eventName}:` + e.toString());
                mp.gui.chat.push(`${eventName}:` + e.toString());
            }
        }
    });

    eventsMap.set(eventFunction, proxyEventFunction);

    mp.events[eventsAdd](eventName, proxyEventFunction);
};

mp.events.add = (eventNameOrObject, ...args) => {
    if (typeof eventNameOrObject === 'object') {
        mp.events[eventsAdd](eventNameOrObject);

        return;
    }

    __eventAdd__(eventNameOrObject, ...args);
};

mp.events.add('render', () => {
    if (!isRenderDebugActive) {
        return;
    }

    const rendersTicksValues = [...rendersTicks.entries()];

    for (let i = 0; i < rendersTicksValues.length; i++) {
        mp.game.graphics.drawText(`${rendersTicksValues[i][0]} - ${rendersTicksValues[i][1]}ms`,
            [0.5, 0.1 + (i * 0.03)],
            {
                scale: [0.3, 0.3],
                outline: true,
                color: [255, 255, 255, 255],
                font: 4
            }
        );
    }

}, 'index-render');

mp.events.add('debug.render', () => {
    isRenderDebugActive = !isRenderDebugActive;
});

mp.events.add('admin.toggleGodMode', () => {
    global.isGodModeActive = !global.isGodModeActive;

    mp.players.local.setInvincible(global.isGodModeActive);

    mp.events.call('notify', 4, 9, `GM - ${global.isGodModeActive ? 'включен' : 'выключен'}`, 3000);
});

global.chatActive = false;
global.loggedin = false;
global.localplayer = mp.players.local;

mp.gui.execute("window.location = 'http://package/browser/hud.html'");
if (mp.storage.data.chatcfg == undefined) {
    mp.storage.data.chatcfg = {
		timestamp: 0,
		chatsize: 0,
		fontstep: 0,
		alpha: 1
	};
    mp.storage.flush();
}

setTimeout(function () { 
    mp.gui.execute(`newcfg(0,${mp.storage.data.chatcfg.timestamp}); newcfg(1,${mp.storage.data.chatcfg.chatsize}); newcfg(2,${mp.storage.data.chatcfg.fontstep}); newcfg(3,${mp.storage.data.chatcfg.alpha});`);
	mp.events.call('showHUD', false); 
}, 1000);

global.pedsaying = null;
global.pedtext = "";
global.pedtext2 = null;
global.pedtimer = false;

var personalLabels = [];

var accessRoding = false;
var pentloaded = false;
var emsloaded = false;
var showCords = false;

const walkstyles = [null,"move_m@brave","move_m@confident","move_m@drunk@verydrunk","move_m@shadyped@a","move_m@sad@a","move_f@sexy@a","move_ped_crouched"];
const moods = [null,"mood_aiming_1", "mood_angry_1", "mood_drunk_1", "mood_happy_1", "mood_injured_1", "mood_stressed_1"];
mp.game.streaming.requestClipSet("move_m@brave");
mp.game.streaming.requestClipSet("move_m@confident");
mp.game.streaming.requestClipSet("move_m@drunk@verydrunk");
mp.game.streaming.requestClipSet("move_m@shadyped@a");
mp.game.streaming.requestClipSet("move_m@sad@a");
mp.game.streaming.requestClipSet("move_f@sexy@a");
mp.game.streaming.requestClipSet("move_ped_crouched");
global.admingm = false;

mp.game.audio.setAudioFlag("DisableFlightMusic", true);

global.NativeUI = require("./src/libs/nativeui");
global.Menu = NativeUI.Menu;
global.UIMenuItem = NativeUI.UIMenuItem;
global.UIMenuListItem = NativeUI.UIMenuListItem;
global.UIMenuCheckboxItem = NativeUI.UIMenuCheckboxItem;
global.UIMenuSliderItem = NativeUI.UIMenuSliderItem;
global.BadgeStyle = NativeUI.BadgeStyle;
global.Point = NativeUI.Point;
global.ItemsCollection = NativeUI.ItemsCollection;
global.Color = NativeUI.Color;
global.ListItem = NativeUI.ListItem;
global.match = false;

function SetWalkStyle(entity, walkstyle) {
	try {
		if (walkstyle == null) entity.resetMovementClipset(0.0);
		else entity.setMovementClipset(walkstyle, 0.0);
	} catch (e) { }
}

function SetMood(entity, mood) {
	try {
		if (mood == null) entity.clearFacialIdleAnimOverride();
		else mp.game.invoke('0xFFC24B988B938B38', entity.handle, mood, 0);
	} catch (e) { }
}

mp.events.add('chatconfig', function (a, b) {
	if(a == 0) mp.storage.data.chatcfg.timestamp = b;
    else if(a == 1) mp.storage.data.chatcfg.chatsize = b;
	else if(a == 2) mp.storage.data.chatcfg.fontstep = b;
	else mp.storage.data.chatcfg.alpha = b;
	mp.storage.flush();
});

mp.events.add('setClientRotation', function (player, rots) {
	if (player !== undefined && player != null && localplayer != player) player.setRotation(0, 0, rots, 2, true);
});

mp.events.add('setWorldLights', function (toggle) {
	try {
		mp.game.graphics.resetLightsState();
		for (let i = 0; i <= 16; i++) {
			if(i != 6 && i != 7) mp.game.graphics.setLightsState(i, toggle);
		}
	} catch { }
});

mp.events.add('changeChatState', function (state) {
    chatActive = state;
});

mp.events.add('allowRoding', function (toggle) {
    accessRoding = toggle;
});

mp.events.add('UpdateMoney', function (temp, amount) {
  mp.events.call('UpdateMoneyHud', temp, amount);
  mp.events.call('UpdateMoneyPhone', temp, amount);
});

mp.events.add('UpdateBank', function (temp, amount) {
  mp.events.call('UpdateBankHud', temp, amount);
  mp.events.call('UpdateBankPhone', temp, amount);
});

require('./src/libs/animList');
// // // // // // //

//client========================
require('./src/client/utils/keys.js');
require('./src/client/render');
require('./src/client/player/afksystem.js');
require('./src/client/voice');
require('./src/client/walkie');
require('./src/client/objectsync'); 
require('./src/client/utils/utils');
require('./src/client/basicsync');
//==============================

require('./src/banks/atm');

//configs=======================
require('./src/configs/tattoo');
require('./src/configs/barber');
require('./src/configs/clothes');
require('./src/configs/natives');
require('./src/configs/tuning');
//==============================

//admin=========================
require('./src/admin/fly');
require('./src/admin/admesp');
require("./src/admin/spmenu");
require('./src/admin/adminpanel');
require('./src/admin/reportpanel');
require('./src/admin/tpwp');
//==============================

//casino========================
require('./src/casino/luckywheel');
require('./src/casino/carlottery');
require('./src/casino/casinomarket/index.js');
require('./src/casino/bar/index.js');
require('./src/casino/casino/casino.js');
require('./src/casino/dedinside/insidetrack.js');
require('./src/casino/blackjack/blackjack.js');
//==============================

//fractions=====================
require('./src/fractions/vehiclespawner');
require('./src/fractions/gangzones');
require('./src/fractions/craft');
require('./src/fractions/changeclothes');
require('./src/fractions/drone');
require('./src/fractions/hijacking');
//==============================

//markets=======================
require('./src/markets/fish');
require('./src/markets/mush');
require('./src/markets/black');
require('./src/markets/other');
//==============================

//house=========================
// require('./src/house/furniture');
require('./src/house/housemenu');
require('./src/house/parking');
//==============================

//jobs==========================
require('./src/jobs/truckers');
require('./src/jobs/orange');
require('./src/jobs/drug');
require('./src/jobs');
require('./src/jobs/fish/fish.js');
//==============================

//modules=======================
require('./src/modules/island/heistisland');
require('./src/modules/business');
require('./src/modules/cinema');
//==============================

//player========================
require('./src/player/circle');
require('./src/player/elections');
require('./src/player/fingerpoints');
require('./src/player/menus');
require('./src/player/character');
require('./src/player/animation');
require('./src/player/CarryPlayer');
require('./src/player/weaponDamage');
require('./src/player/phone');
require('./src/player/main');
require('./src/player/board');
require('./src/player/hud');
require('./src/player/gamertag');
require('./src/player/makeexterior');
require('./src/player/playermenu');
//==============================

//utils=========================
require('./src/utils/animator');
require('./src/utils/bigmap');
require('./src/utils/checkpoints');
require('./src/utils/discord');
require('./src/utils/notify');
require('./src/utils/screeneffects');
//==============================

//vehicle=======================
require('./src/vehicle/vehiclesync');
require('./src/vehicle/rentcar');
require('./src/vehicle/autopilot');
require('./src/vehicle/lscustoms');
require('./src/vehicle/SpeedCheck');
require('./src/vehicle/trunk');
require('./src/vehicle/radiosync');
//==============================

//weapon========================
require('./src/weapon');
//==============================

//world=========================
require('./src/world/environment');
require('./src/world/animals');
require('./src/world/peds');
require('./src/world/doormanager');
require('./src/world/metro');
require('./src/world/ipls');
require('./src/world/xmr');
require('./src/world/sex');
//==============================
// // // // // // // // // // //

if (mp.storage.data.friends == undefined) {
    mp.storage.data.friends = {};
    mp.storage.flush();
}

mp.game.invoke("0x7F06937B0CDCBC1A", 1);
// // // // // // //
const mSP = 30;
var prevP = mp.players.local.position;
var localWeapons = {};

function distAnalyze() {
	if(new Date().getTime() - global.lastCheck < 100) return; 
	global.lastCheck = new Date().getTime();
    let temp = mp.players.local.position;
    let dist = mp.game.gameplay.getDistanceBetweenCoords(prevP.x, prevP.y, prevP.z, temp.x, temp.y, temp.z, true);
    prevP = mp.players.local.position;
    if (mp.players.local.isInAnyVehicle(true)) return;
    if (dist > mSP) {
        mp.events.callRemote("acd", "fly");
    }
}

global.serverid = 1;

mp.events.add('ServerNum', (server) => {
   global.serverid = server;
});

global.acheat = {
    pos: () => prevP = mp.players.local.position,
    guns: () => localWeapons = playerLocal.getAllWeapons(),
    start: () => {
        setInterval(distAnalyze, 2000);
    }
}

mp.events.add('authready', () => {
    mp.console.clear();
    require('./src/player/auth');
})

mp.events.add('acpos', () => {
    global.acheat.pos();
})
// // // // // // //
global.spectating = false;
global.sptarget = null;

//mp.game.invoke(getNative("REMOVE_ALL_PED_WEAPONS"), localplayer.handle, false);

mp.keys.bind(Keys.VK_R, false, function () { // R key
	try {
		if (!loggedin || chatActive || new Date().getTime() - global.lastCheck < 1000 || mp.gui.cursor.visible) return;
		var current = currentWeapon();
		if (current == -1569615261 || current == 911657153) return;
		var ammo = mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, current);
		if (mp.game.weapon.getWeaponClipSize(current) == ammo) return;
		mp.events.callRemote("playerReload", current, ammo);
		global.lastCheck = new Date().getTime();
	} catch { }
});

var ammosweap = 0;
var givenWeapon = -1569615261;
const currentWeapon = () => mp.game.invoke(getNative("GET_SELECTED_PED_WEAPON"), localplayer.handle);
mp.events.add('wgive', (weaponHash, ammo, isReload, equipNow) => {
    weaponHash = parseInt(weaponHash);
	if (weaponHash == 126349499)
	{
		to = weaponHash;
	}
		
    ammo = parseInt(ammo);
    ammo = ammo >= 9999 ? 9999 : ammo;
    givenWeapon = weaponHash;
    ammo += mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, weaponHash);
    mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, weaponHash, 0);
	ammosweap = ammo;
    mp.gui.execute(`HUD.ammo=${ammo};`);
    mp.gui.execute(`HUD.gun=${weaponHash};`);
    // GIVE_WEAPON_TO_PED //
    mp.game.invoke(getNative("GIVE_WEAPON_TO_PED"), localplayer.handle, weaponHash, ammo, false, equipNow);

    if (isReload) {
        mp.game.invoke(getNative("MAKE_PED_RELOAD"), localplayer.handle);
    }
});
mp.events.add('takeOffWeapon', (weaponHash) => {
    try {
        weaponHash = parseInt(weaponHash);
        var ammo = mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, weaponHash);
		if(ammo == ammosweap) mp.events.callRemote('playerTakeoffWeapon', weaponHash, ammo, 0);
		else mp.events.callRemote('playerTakeoffWeapon', weaponHash, ammosweap, 1);
		ammosweap = 0;
		mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, weaponHash, 0);
		mp.game.invoke(getNative("REMOVE_WEAPON_FROM_PED"), localplayer.handle, weaponHash);
		givenWeapon = -1569615261;
		mp.gui.execute(`HUD.ammo=-1;`);
    } catch (e) { }
});
mp.events.add('serverTakeOffWeapon', (weaponHash) => {
    try {
        weaponHash = parseInt(weaponHash);
        var ammo = mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, weaponHash);
		if(ammo == ammosweap) mp.events.callRemote('takeoffWeapon', weaponHash, ammo, 0);
		else mp.events.callRemote('takeoffWeapon', weaponHash, ammosweap, 1);
		ammosweap = 0;
		mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, weaponHash, 0);
		mp.game.invoke(getNative("REMOVE_WEAPON_FROM_PED"), localplayer.handle, weaponHash);
		givenWeapon = -1569615261;
		mp.gui.execute(`HUD.ammo=-1;`);
		
    } catch (e) { }
});

var petathouse = null;
mp.events.add('petinhouse', (petName, petX, petY, petZ, petC, Dimension) => {
	if(petathouse != null) {
		petathouse.destroy();
		petathouse = null;
	}
	switch(petName) {
		case "Husky":
			petName = 1318032802;
			break;
		case "Poodle":
			petName = 1125994524;
			break;
		case "Pug":
			petName = 1832265812;
			break;
		case "Retriever":
			petName = 882848737;
			break;
		case "Rottweiler":
			petName = 2506301981;
			break;
		case "Shepherd":
			petName = 1126154828;
			break;
		case "Westy":
			petName = 2910340283;
			break;
		case "Cat":
			petName = 1462895032;
			break;
		case "Rabbit":
			petName = 3753204865;
			break;
	}
	petathouse = mp.peds.new(petName, new mp.Vector3(petX, petY, petZ), petC, Dimension);
});
var checkTimer = setInterval(function () {
    var current = currentWeapon();
    if (localplayer.isInAnyVehicle(true)) {
        var vehicle = localplayer.vehicle;
        if (vehicle == null) return;

        if (vehicle.getClass() == 15) {
            if (vehicle.getPedInSeat(-1) == localplayer.handle || vehicle.getPedInSeat(0) == localplayer.handle) return;
        }
        else {
            if (canUseInCar.indexOf(current) == -1) return;
        }
    }

    if (currentWeapon() != givenWeapon) {
		ammosweap = 0;
        mp.game.invoke(getNative("GIVE_WEAPON_TO_PED"), localplayer.handle, givenWeapon, 1, false, true);
        mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, givenWeapon, 0);
        localplayer.taskReloadWeapon(false);
        localplayer.taskSwapWeapon(false);
        mp.gui.execute(`HUD.ammo=0;`);
    }
}, 100);
var canUseInCar = [
    453432689,
    1593441988,
    -1716589765,
    -1076751822,
    -771403250,
    137902532,
    -598887786,
    -1045183535,
    584646201,
    911657153,
    1198879012,
    324215364,
    -619010992,
    -1121678507,
];
mp.events.add('playerWeaponShot', (targetPosition, targetEntity) => {
	if (match) return;
    var current = currentWeapon();
    var ammo = mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, current);
    mp.gui.execute(`HUD.ammo=${ammo};`);	
	if (current != -1569615261 && current != 911657153) {
		if(ammosweap > 0) ammosweap--;
		if(ammosweap == 0 && ammo != 0) {
			mp.events.callRemote('takeoffWeapon', current, 0, 1);
			ammosweap = 0;
			mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, current, 0);
			mp.game.invoke(getNative("REMOVE_WEAPON_FROM_PED"), localplayer.handle, current);
			givenWeapon = -1569615261;
			mp.gui.execute(`HUD.ammo=0;`);
		}
	}	
	if (ammo <= 0) {
		ammosweap = 0;
        localplayer.taskSwapWeapon(false);
        mp.gui.execute(`HUD.ammo=0;`);
    }	
	if (to)
	{
        var ammo = mp.game.invoke(getNative("GET_AMMO_IN_PED_WEAPON"), localplayer.handle, current);
		if(ammo == ammosweap) mp.events.callRemote('playerTakeoffWeapon', current, ammo, 0);
		else mp.events.callRemote('playerTakeoffWeapon', current, ammosweap, 1);
		ammosweap = 0;
		mp.game.invoke(getNative("SET_PED_AMMO"), localplayer.handle, current, 0);
		mp.game.invoke(getNative("REMOVE_WEAPON_FROM_PED"), localplayer.handle, current);
		givenWeapon = -1569615261;
		mp.gui.execute(`HUD.ammo=0;`);
		to = false;
		mp.events.callRemote('takeoffWeapon', current, 0, 1);
	}	
});
mp.events.add('render', () => {
    try {
        mp.game.controls.disableControlAction(2, 45, true); // reload control
        //localplayer.setCanSwitchWeapon(false);

        //     weapon switch controls       //
		mp.game.controls.disableControlAction(1, 243, true); // CCPanelDisable
		
        mp.game.controls.disableControlAction(2, 12, true);
        mp.game.controls.disableControlAction(2, 13, true);
        mp.game.controls.disableControlAction(2, 14, true);
        mp.game.controls.disableControlAction(2, 15, true);
        mp.game.controls.disableControlAction(2, 16, true);
        mp.game.controls.disableControlAction(2, 17, true);

        mp.game.controls.disableControlAction(2, 37, true);
        mp.game.controls.disableControlAction(2, 99, true);
        mp.game.controls.disableControlAction(2, 100, true);

        mp.game.controls.disableControlAction(2, 157, true);
        mp.game.controls.disableControlAction(2, 158, true);
        mp.game.controls.disableControlAction(2, 159, true);
        mp.game.controls.disableControlAction(2, 160, true);
        mp.game.controls.disableControlAction(2, 161, true);
        mp.game.controls.disableControlAction(2, 162, true);
        mp.game.controls.disableControlAction(2, 163, true);
        mp.game.controls.disableControlAction(2, 164, true);
        mp.game.controls.disableControlAction(2, 165, true);

        mp.game.controls.disableControlAction(2, 261, true);
        mp.game.controls.disableControlAction(2, 262, true);
        //      weapon switch controls       //

        if (currentWeapon() != -1569615261) { // heavy attack controls
            mp.game.controls.disableControlAction(2, 140, true);
            mp.game.controls.disableControlAction(2, 141, true);
            mp.game.controls.disableControlAction(2, 143, true);
            mp.game.controls.disableControlAction(2, 263, true);
        }
    } catch (e) { }
});

mp.events.add("Player_SetMood", (player, index) => {
    try {
        if (player !== undefined) {
            if (index == 0) player.clearFacialIdleAnimOverride();
			else mp.game.invoke('0xFFC24B988B938B38', player.handle, moods[index], 0);
        }
    } catch (e) {
		mp.gui.chat.push("SetMood Debug: " + e.toString());
	}
});

mp.events.add("Player_SetWalkStyle", (player, index) => {
    try {
        if (player !== undefined) {
            if (index == 0) player.resetMovementClipset(0.0);
			else player.setMovementClipset(walkstyles[index], 0.0);
        }
    } catch (e) {
		mp.gui.chat.push("SetWalkStyle Debug: " + e.toString());
	}
});

mp.events.add("playerDeath", function (player, reason, killer) {
    givenWeapon = -1569615261;
});

mp.events.add("removeAllWeapons", function () {
    givenWeapon = -1569615261;
});

mp.events.add('svem', (pm, tm) => {
	var vehc = localplayer.vehicle;
	vehc.setEnginePowerMultiplier(pm);
	vehc.setEngineTorqueMultiplier(tm);
});

var f10rep = new Date().getTime();

mp.events.add('f10report', (report) => {
	if (!loggedin || new Date().getTime() - f10rep < 3000) return;
    f10rep = new Date().getTime();
	mp.events.callRemote('f10helpreport', report);
});

mp.events.add('dmgmodif', (multi) => {
	mp.game.ped.setAiWeaponDamageModifier(multi);
});

mp.game.ped.setAiWeaponDamageModifier(0.5);
mp.game.ped.setAiMeleeWeaponDamageModifier(0.4);

mp.game.player.setMeleeWeaponDefenseModifier(0.25);
mp.game.player.setWeaponDefenseModifier(1.3);

var resistStages = {
    0: 0.0,
    1: 0.05,
    2: 0.07,
    3: 0.1,
};
mp.events.add("setResistStage", function (stage) {
    mp.game.player.setMeleeWeaponDefenseModifier(0.25 + resistStages[stage]);
    mp.game.player.setWeaponDefenseModifier(1.3 + resistStages[stage]);
});

mp.events.add('loadProp', (x, y, z, prop) => {
    var interior = mp.game.interior.getInteriorAtCoords(x, y, z);
    mp.game.interior.enableInteriorProp(interior, prop);
    mp.game.interior.refreshInterior(interior);
});

mp.events.add('UnloadProp', (x, y, z, prop) => {
    var interior = mp.game.interior.getInteriorAtCoords(x, y, z);
    mp.game.interior.disableInteriorProp(interior, prop);
    mp.game.interior.refreshInterior(interior);
});

setInterval(() => {
    mp.game.invoke('0x9E4CFFF989258472'); // void _INVALIDATE_VEHICLE_IDLE_CAM();
    mp.game.invoke('0xF4F2C0D4EE209E20'); // void INVALIDATE_IDLE_CAM();
  }, 25000);
mp.game.controls.disableControlAction(2, 36, true);