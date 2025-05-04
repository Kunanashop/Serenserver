const positions = [
   { 'position': { 'x': 1481.6602, 'y': -1435.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1481.6602, 'y': -1575.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1341.6602, 'y': -1575.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -1715.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -1715.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1341.6602, 'y': -1715.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -1575.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -2415.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1341.6602, 'y': -1855.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1341.6602, 'y': -1995.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1341.6602, 'y': -2135.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -1855.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -1995.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -2135.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -1855.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -1995.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -2275.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1201.6602, 'y': -2275.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -2135.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 1061.6602, 'y': -2415.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -2415.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -2135.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -1855.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -1995.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -1715.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -1575.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -2275.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 921.6602, 'y': -1435.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -2415.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -2135.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -1855.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -1995.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -1715.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -1575.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -2275.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 781.6602, 'y': -1435.6931, 'z': 67.30145 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -1315.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -1875.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -2015.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 511.44025, 'y': -2155.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -1315.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -1875.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -2015.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 371.44025, 'y': -2155.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -1315.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -1875.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -2015.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 231.44025, 'y': -2155.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -1875.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -2015.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': 91.44025, 'y': -2155.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -49.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -49.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -49.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -49.44025, 'y': -1875.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -189.44025, 'y': -1455.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -189.44025, 'y': -1595.7262, 'z': 28.146452 }, 'color': 10 },
	{ 'position': { 'x': -189.44025, 'y': -1735.7262, 'z': 28.146452 }, 'color': 10 },
]
if (mp.storage.data.gangzones == undefined) {
    mp.storage.data.gangzones = [];
    mp.storage.flush();
}
mp.events.add('loadCaptureBlips', function (json) {
    var colors = JSON.parse(json);
    for (var i = 0; i < colors.length; i++) {
        positions[i].color = colors[i];
	}
	if(mp.storage.data.gangzones.length !== 0) {
		mp.events.call('unloadCaptureBlips');
	}
    positions.forEach(element => {
        const blip = mp.game.ui.addBlipForRadius(element.position.x, element.position.y, element.position.z, 70);
        mp.game.invoke(getNative("SET_BLIP_SPRITE"), blip, 5);
        mp.game.invoke(getNative("SET_BLIP_ALPHA"), blip, 100);
		mp.game.invoke(getNative("SET_BLIP_COLOUR"), blip, element.color);
        mp.storage.data.gangzones.push(blip);
    });
});
mp.events.add('unloadCaptureBlips', () =>{
	mp.storage.data.gangzones.forEach(element =>{
		mp.game.ui.removeBlip(element);
	});
	mp.storage.data.gangzones = [];
	mp.storage.flush();
});

var isCapture = false;
var captureAtt = 0;
var captureDef = 0;
var captureMin = 0;
var captureSec = 0;
var fracAtt = 0;
var fracDef = 0;

mp.events.add('sendCaptureInformation', function (att, def, min, sec, a, b) {
    captureAtt = att;
    captureDef = def;
    captureMin = min;
    captureSec = sec;
    fracAtt = b;
    fracDef = a;
});

mp.events.add('captureHud', function (argument) {
    isCapture = argument;
});

mp.events.add('setZoneColor', function (id, color) {
    if (mp.storage.data.gangzones.length == 0) return;
    mp.game.invoke(getNative("SET_BLIP_COLOUR"), mp.storage.data.gangzones[id], color);
});
mp.events.add('setZoneFlash', function (id, state, color) {
    if (mp.storage.data.gangzones.length == 1 || mp.storage.data.gangzones.length == 0) {
        if (state) {
            const blip = mp.game.ui.addBlipForRadius(positions[id].position.x, positions[id].position.y, positions[id].position.z, 70);
            mp.game.invoke(getNative("SET_BLIP_SPRITE"), blip, 5);
            mp.game.invoke(getNative("SET_BLIP_ALPHA"), blip, 175);
            mp.game.invoke(getNative("SET_BLIP_COLOUR"), blip, color);
            mp.storage.data.gangzones[id] = blip;
        }
        else {
            if (mp.storage.data.gangzones.length == 0) return;
            mp.game.invoke(getNative("SET_BLIP_ALPHA"), mp.storage.data.gangzones[id], 0);
        }
    }

    mp.game.invoke(getNative("SET_BLIP_FLASH_TIMER"), mp.storage.data.gangzones[id], 1000);
    mp.game.invoke(getNative("SET_BLIP_FLASHES"), mp.storage.data.gangzones[id], state);
});

var zonestatus =
{
    active: false,
    capDef: 0,
    capAtt: 0,
    capMin: 0,
    capSec: 0,
	fracAtt: 0,
	fracDef: 0,
}
mp.events.add('sendkillinfo', (object) => {
	wrapper.execute(`wrapper.addKills(${object});`);
});
var wrapper = mp.browsers.new('http://package/browser/modules/Fractions/kill/index.html');
mp.events.add('render', () => {
	if (isCapture) {
        if (zonestatus.active == false)
		wrapper.execute(`wrapper.active=true`); 
        {
            zonestatus.active = true;
            mp.gui.execute(`HUD.gangzone=true`);
			
			zonestatus.fracAtt = fracAtt;
            zonestatus.fracDef = fracDef;
            mp.gui.execute(`HUD.fracatt=${fracAtt}`);
            mp.gui.execute(`HUD.fracdef=${fracDef}`);
            
        }		
        if (captureDef !== zonestatus.capDef || captureAtt !== zonestatus.capAtt)
        {
            zonestatus.capAtt = captureAtt;
            zonestatus.capDef = captureDef;
            mp.gui.execute(`HUD.att=${captureAtt}`);
            mp.gui.execute(`HUD.def=${captureDef}`);
           
        }
        if (captureMin !== zonestatus.capMin || captureSec !== zonestatus.capSec)
        {
            zonestatus.capMin = captureMin;
            zonestatus.capSec = captureSec;
            mp.gui.execute(`HUD.min=${captureMin}`);
            mp.gui.execute(`HUD.sec=${captureSec}`);
            
        }
    }
    else
    {
		wrapper.execute(`wrapper.active=false`); 
        if (zonestatus.active == true)
        {
            zonestatus.active = false;
			mp.gui.execute(`HUD.gangzone=false`);

            
        }
    }

    if (mp.storage.data.gangzones.length !== 0) {
        mp.storage.data.gangzones.forEach(blip => {
			mp.game.invoke(getNative("SET_BLIP_ROTATION"), blip, 0);
        })
    }
});

mp.events.add('client::playsoundurl', (a) => {
	global.soundCEF.execute(`playerSoundFromUrl('./sounds/` + a + `.mp3');`);
});