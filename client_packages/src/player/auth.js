let player = mp.players.local;
player.setAlpha(0);

const Natives = {
    SWITCH_OUT_PLAYER: '0xAAB3200ED59016BC',
    SWITCH_IN_PLAYER: '0xD8295AF639FD9CB8',
    IS_PLAYER_SWITCH_IN_PROGRESS: '0xD9D2CFFF49FAB35F',
    IS_SWITCH_READY_FOR_DESCENT: '0xDFA80CB25D0A19B3',
    STOP_PLAYER_SWITCH: '0x95C0A5BBDC189AA1',
    ALLOW_PLAYER_SWITCH_ASCENT: '0x8E2A065ABDAE6994',
    ALLOW_PLAYER_SWITCH_DESCENT: '0xAD5FDF34B81BFE79',
    ALLOW_PLAYER_SWITCH_OUTRO: '0x74DE2E8739086740',
    START_PLAYER_SWITCH: '0xFAA23F2CBA159D67',
};
var respawn = mp.browsers["new"]('http://package/browser/respawn.html');
var auth = mp.browsers["new"]('http://package/browser/modules/Auth/index.html');
auth.execute(`slots.server=${serverid};`);
mp.gui.cursor.visible = true;

var lastButAuth = 0;
var lastButSlots = 0;

setTimeout(function () { 
	mp.game.invoke(Natives.STOP_PLAYER_SWITCH);
	mp.game.invoke(Natives.SWITCH_OUT_PLAYER, player.handle, 0, parseInt(1));
}, 500);

setTimeout(function () { 
    if (mp.storage.data.account)
    {
        auth.execute(`document.getElementById("entry-login-id").value = "${mp.storage.data.account.username}";`);
        auth.execute(`document.getElementById("entry-password-id").value = "${mp.storage.data.account.pass}";`);
        auth.execute(`document.getElementById("entry-savemy").checked = true;`);
    }
}, 150);
mp.events.add('auth::fadescreen', () => {
	mp.game.cam.doScreenFadeOut(1100);
	mp.game.wait(1800);
	auth.execute(`enterloadpage()`);
	global.soundCEF.execute(`playSound("loadingauth");`);
	mp.game.wait(6500);
	mp.game.cam.doScreenFadeIn(1100);
	auth.execute(`enternextpage()`);
});
// events from cef
mp.events.add('signin', function (authData) {
    if (new Date().getTime() - lastButAuth < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButAuth = new Date().getTime();

    authData = JSON.parse(authData);

    var username = authData['entry-login'],
        pass = authData['entry-password'];
        check = authData['entry-savemy'];
	
	var checks = true;
    if (checks) {
        mp.storage.data.account = {
            username: username,
            pass: pass
        };
    } else delete mp.storage.data.account;

    mp.events.callRemote('signin', username, pass)
});

mp.events.add('restorepass', function (state, authData) {
    if (new Date().getTime() - lastButAuth < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButAuth = new Date().getTime();

    authData = JSON.parse(authData);

    var nameorcode = authData['entry-login'];

    mp.events.callRemote('restorepass', state, nameorcode)
});

mp.events.add('signup', function (regData) {
    if (new Date().getTime() - lastButAuth < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButAuth = new Date().getTime();

    regData = JSON.parse(regData);
    var username = regData['new-user__login'],
        email = regData['new-user__email'],
        promo = regData['new-user__promo-code'],
        pass1 = regData['new-user__pw'],
        pass2 = regData['new-user__pw-repeat'];

    if (checkLgin(username) || username.length > 50) {
        mp.events.call('notify', 1, 9, 'Логин не соответствует формату или слишком длинный!', 3000);
        return;
    }

    if (pass1 != pass2 || pass1.length < 3) {
        mp.events.call('notify', 1, 9, 'Ошибка при вводе пароля!', 3000);
        return;
    }

    mp.events.callRemote('signup', username, pass1, email, promo);
});
var characterselected = false;
mp.events.add('selectChar', function (slot) {
    if (new Date().getTime() - lastButSlots < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButSlots = new Date().getTime();
	if (auth != null) {
		characterselected = true;
		mp.events.callRemote('selectchar', slot);
    }
});

mp.events.add('newChar', function (slot, name, lastname) {
    if (checkName(name) || !checkName2(name) || name.length > 25 || name.length <= 2) {
        mp.events.call('notify', 1, 9, 'Правильный формат имени: 3-25 символов и первая буква имени заглавная', 3000);
        return;
    }

    if (checkName(lastname) || !checkName2(lastname) || lastname.length > 25 || lastname.length <= 2) {
        mp.events.call('notify', 1, 9, 'Правильный формат фамилии: 3-25 символов и первая буква фамилии заглавная', 3000);
        return;
    }

    if (new Date().getTime() - lastButSlots < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButSlots = new Date().getTime();

    mp.events.callRemote('newchar', slot, name, lastname);
});

mp.events.add('delChar', function (slot, name, lastname, pass) {
    if (checkName(name) || name.length > 25 || name.length <= 2) {
        mp.events.call('notify', 1, 9, 'Правильный формат имени: 3-25 символов и первая буква имени заглавная', 3000);
        return;
    }

    if (checkName(lastname) || lastname.length > 25 || lastname.length <= 2) {
        mp.events.call('notify', 1, 9, 'Правильный формат фамилии: 3-25 символов и первая буква фамилии заглавная', 3000);
        return;
    }

    if (new Date().getTime() - lastButSlots < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButSlots = new Date().getTime();

    mp.events.callRemote('delchar', slot, name, lastname, pass);
});

mp.events.add('transferChar', function (slot, name, lastname, pass) {
    if (checkName(name)) {
        mp.events.call('notify', 1, 9, 'Имя не соответствует формату или слишком длинное!', 3000);
        return;
    }

    if (checkName(lastname)) {
        mp.events.call('notify', 1, 9, 'Фамилия не соответствует формату или слишком длинное!', 3000);
        return;
    }

    if (new Date().getTime() - lastButSlots < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
    lastButSlots = new Date().getTime();

    mp.events.callRemote('transferchar', slot, name, lastname, pass);
});

mp.events.add('spawn::auth', function (data) {
	mp.events.callRemote('spawn', data);
});

mp.events.add('spawnchecksPos', function (data) {
    if (new Date().getTime() - lastButSlots < 100) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
	// if (auth != null) {
		if (inodownCam) {
			mp.game.invoke(Natives.STOP_PLAYER_SWITCH);
			mp.game.invoke(Natives.SWITCH_OUT_PLAYER, player.handle, 0, parseInt(1));
			auth.execute(`slots.setspawnshow=false`)
			auth.execute(`slots.btnpressed=true`)
			setTimeout(() => {
				mp.events.callRemote('spawnchecksPos', data);
				setTimeout(() => {
					mp.game.invoke(Natives.SWITCH_IN_PLAYER, player.handle);
				}, 1000);
				checkCamInAirReadyForDescent();
			}, 1000);
		}
		else {
			mp.events.callRemote('spawnchecksPos', data);
			setTimeout(() => {
				mp.game.invoke(Natives.SWITCH_IN_PLAYER, player.handle);
			}, 500);
			checkCamInAirReadyForDescent();
		}
    // }
});

mp.events.add('spawnsucces', function () {
	if (auth != null) {
		localplayer.freezePosition(true);
		lastButSlots = new Date().getTime();
		setTimeout(() => {
			mp.game.invoke(Natives.SWITCH_IN_PLAYER, player.handle);
			player.setAlpha(255);
			mp.events.call('sound.load');
		}, 100);
		setTimeout(() => {
			if (auth != null) {
				auth.destroy();
				auth = null;
			}
			localplayer.freezePosition(false);
		}, 800);
    }
});

mp.events.add('buyNewSlot', function (data) {
    if (new Date().getTime() - lastButSlots < 3000) {
        mp.events.call('notify', 4, 9, "Слишком быстро", 3000);
        return;
    }
	lastButSlots = new Date().getTime();
	mp.events.callRemote('donate', 8, data);
});

// events from server
mp.events.add('delCharSuccess', function (data) {
    auth.execute(`delchar(${data})`);
});

mp.events.add('unlockSlot', function (data) {
    auth.execute(`unlockSlot(${data})`);
});

mp.events.add('toslots', function (data) {
	mp.events.call('sound.main')
    auth.execute(`toslots('${data}')`);
});

mp.events.add('spawnShow', function (data) {
    if (data === false) {
        if (auth != null) {
            auth.destroy();
            auth = null;
        }
    }
    else {
		if (characterselected) {
			auth.execute(`slots.set('${data}')`);
			auth.execute(`removeallregs()`);
		}
    }
	mp.events.call('sound.main.next');
    // if (auth != null) {
        // auth.destroy();
        // auth = null;
    // }
});

mp.events.add('ready', function () {
    global.loggedin = true;
	
	checkCamInAirCharSelect()
	
    mp.events.call('hideTun');
    mp.game.player.setHealthRechargeMultiplier(0);

    global.menu = mp.browsers["new"]('http://package/browser/menu.html');
    // global.helpmenu = mp.browsers["new"]('http://package/browser/help.html');
	
	setTimeout(() => {
		if (auth != null) {
			auth.destroy();
			auth = null;
		}
	}, 700);
});

function checkCamInAirCharSelect() {
    if (mp.game.invoke(Natives.IS_PLAYER_SWITCH_IN_PROGRESS)) {
        setTimeout(() => {
            checkCamInAirCharSelect();
        }, 400);
    } else {
        global.menuClose();
		mp.events.call('showHUD', true);
		localplayer.freezePosition(false);
    }
}
var inodownCam = false;
function checkCamInAirReadyForDescent() {
    if (mp.game.invoke(Natives.IS_PLAYER_SWITCH_IN_PROGRESS)) {
        setTimeout(() => {
            checkCamInAirReadyForDescent();
        }, 400);
    } else {
        auth.execute(`slots.setspawnshow=true`)
        auth.execute(`slots.btnpressed=false`)
		inodownCam = true;
    }
}

function checkLgin(str) {
    return !(/^[a-zA-Z1-9]*$/g.test(str));
}

function checkName(str) {
    return !(/^[a-zA-Z]*$/g.test(str));
}

function checkName2(str) {
    let ascii = str.charCodeAt(0);
    if (ascii < 65 || ascii > 90) return false; // Если первый символ не заглавный, сразу отказ
    let bsymbs = 0; // Кол-во заглавных символов
    for (let i = 0; i != str.length; i++) {
        ascii = str.charCodeAt(i);
        if (ascii >= 65 && ascii <= 90) bsymbs++;
    }
    if (bsymbs > 2) return false; // Если больше 2х заглавных символов, то отказ. (На сервере по правилам разрешено иметь Фамилию, например McCry, то есть с приставками).
    return true; // string (имя или фамилия) соответствует
}

mp.events.add('authNotify', (type, layout, msg, time) => {
    if(auth != null) auth.execute(`notify(${type},${layout},'${msg}',${time})`);
	else mp.events.call('notify', type, layout, msg, time);
});