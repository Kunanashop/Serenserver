// По поводу заказа
// Merumond#4799

// Так же ищу проект, где можно поработать! Знания:
// HTML / CSS / JS / VUEJS / C# / MySQL(Базово)

// Created by

// ███╗░░░███╗███████╗██████╗░██╗░░░██╗███╗░░░███╗░█████╗░███╗░░██╗██████╗░
// ████╗░████║██╔════╝██╔══██╗██║░░░██║████╗░████║██╔══██╗████╗░██║██╔══██╗
// ██╔████╔██║█████╗░░██████╔╝██║░░░██║██╔████╔██║██║░░██║██╔██╗██║██║░░██║
// ██║╚██╔╝██║██╔══╝░░██╔══██╗██║░░░██║██║╚██╔╝██║██║░░██║██║╚████║██║░░██║
// ██║░╚═╝░██║███████╗██║░░██║╚██████╔╝██║░╚═╝░██║╚█████╔╝██║░╚███║██████╔╝
// ╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚═╝░╚═════╝░╚═╝░░░░░╚═╝░╚════╝░╚═╝░░╚══╝╚═════╝░

var janna;
var sexmod = false;
var hintpshow = false;
var animplayed = false;
var animplayedenter = false;
var blockActions = false;
let secondssexa = 50; //Сколько секунд трахаться
var debug = false; //Дебаг (Вдруг вам надо)
mp.game.streaming.requestAnimDict("mini@prostitutes@sexlow_veh");
mp.game.streaming.requestAnimDict("mini@prostitutes@sexlow_veh_first_person");
mp.game.streaming.requestAnimDict("rcmcollect_paperleadinout@");

global.popPros = mp.browsers.new("http://package/browser/modules/ProstitutcaMenu/index.html") //Путь к меню, проверьте на всякий случай!
global.popPros.active = false;

var mainanim = "mini@prostitutes@sexlow_veh_first_person" //Можно поставить еще три другие анимации (Отличаются немного)

mp.events.add('client::seatprostitutca', (a) => {
	if (localplayer.isInAnyVehicle(false) && localplayer.vehicle.getPedInSeat(-1) == localplayer.handle) {
		janna = a
		if (debug) mp.gui.chat.push(`Посадил`);
		janna.taskEnterVehicle(localplayer.vehicle.handle, 10000, 0, 1, 1, 0);
		jannaVehCheck();
	}
	else {
		if (debug) mp.gui.chat.push(`не Посадил`);
	}
});
mp.events.add('client::tososat', (id) => {
	if (id == 2) {
		animplayedenter = true;
		janna.taskPlayAnim(mainanim, "low_car_prop_to_sit_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
		localplayer.taskPlayAnim(mainanim, "low_car_prop_to_sit_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
		if (debug) mp.gui.chat.push(`Начало`);
		setTimeout(() => {
			animplayedenter = false;
			janna.taskPlayAnim("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 8, 1, -1, 1, 0, false, false, false);
			localplayer.taskPlayAnim("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 8, 1, -1, 1, 0, false, false, false);
			mp.game.invoke("0x5A4F9EDF1673F704", 0);
			blockActions = false;
			localplayer.vehicle.freezePosition(false);
		}, 2000);
	}
	else {
		if (animplayed || animplayedenter) return;		
	}
	mp.events.callRemote('server::tososat', id);
	global.popPros.active = false
	global.menuClose2();
	mp.game.invoke("0x5A4F9EDF1673F704", 4);
});

mp.events.add("freezeped", (a, state) => { //Хуйня не работает UPD Работает
	a.freezePosition(state)
});

function jannaVehCheck() {
	if (janna.isInAnyVehicle(false) && localplayer.vehicle.getPedInSeat(0) == janna.handle) {
		sexmod = true;
		mp.events.call('clinet::helpkeysonHUD', true, "SPACE", "ПОСМОТРЕТЬ ЧТО МОЖЕТ ЖАННА") //А это короче подсказка на худ, у вас ее не будет, я вам не дам
	}
	else {
		setTimeout(() => {
			jannaVehCheck();
		}, 400);
	}
}
mp.keys.bind(Keys.VK_SPACE, false, function() {
	let speed = (mp.players.local.vehicle.getSpeed() * 3.6).toFixed();
	if (!loggedin || chatActive || editing || global.menuOpened || global.isInSafeZone || animplayedenter || animplayed || !sexmod || speed > 5) return;
	mp.game.invoke("0x5A4F9EDF1673F704", 4);
	blockActions = true;
	if (debug) mp.gui.chat.push(`Начало`);
	setTimeout(() => {
		mp.events.call('client::opendialogprostitutca');
		if (debug) mp.gui.chat.push(`Конец`);
	}, 500);
});
mp.events.add('client::opendialogprostitutca', () => {
	if (!loggedin || chatActive || editing || global.menuOpened || global.isInSafeZone || animplayedenter || animplayed || !sexmod) return;
	if (localplayer.vehicle != null) {
		localplayer.vehicle.freezePosition(true);
		global.menuOpen2();
		global.popPros.active = true;
		janna.taskPlayAnim(mainanim, "low_car_sit_to_prop_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
		localplayer.taskPlayAnim(mainanim, "low_car_sit_to_prop_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
		animplayedenter = true;
		if (debug) mp.gui.chat.push(`Начало`);
		setTimeout(() => {
			janna.taskPlayAnim(mainanim, "low_car_prop_loop_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			localplayer.taskPlayAnim(mainanim, "low_car_prop_loop_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			animplayedenter = false;
			if (debug) mp.gui.chat.push(`Конец`);
		}, 3000);
	}
});
mp.events.add('prostitutcasosat', () => {
	animplayed = true;
	janna.taskPlayAnim(mainanim, "low_car_prop_to_bj_p1_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
	localplayer.taskPlayAnim(mainanim, "low_car_prop_to_bj_p1_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
	if (debug) mp.gui.chat.push(`Начало сосать`);
	setTimeout(() => {
		if (localplayer != null && janna != null) {
			janna.taskPlayAnim(mainanim, "low_car_bj_loop_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			localplayer.taskPlayAnim(mainanim, "low_car_bj_loop_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			if (debug) mp.gui.chat.push(`Сосет долго`);
			setTimeout(() => {
				if (localplayer != null && janna != null) {
					janna.taskPlayAnim(mainanim, "low_car_bj_to_prop_p2_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
					localplayer.taskPlayAnim(mainanim, "low_car_bj_to_prop_p2_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
					if (debug) mp.gui.chat.push(`Перестает сосать`);
					setTimeout(() => {
						if (localplayer != null && janna != null) {
							animplayed = false;
							mp.events.call('client::opendialogprostitutca');
							if (debug) mp.gui.chat.push(`Тригер`);
						}
					}, 5000);
				}
			}, secondssexa * 1000);
		}
	}, 4700);
});

mp.events.add('prostitutcakon', () => {
	animplayed = true;
	janna.taskPlayAnim(mainanim, "low_car_prop_to_sex_p1_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
	localplayer.taskPlayAnim(mainanim, "low_car_prop_to_sex_p1_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
	if (debug) mp.gui.chat.push(`Начало коня`);
	setTimeout(() => {
		if (localplayer != null && janna != null) {
			janna.taskPlayAnim(mainanim, "low_car_prop_to_sex_p2_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			localplayer.taskPlayAnim(mainanim, "low_car_prop_to_sex_p2_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
			if (debug) mp.gui.chat.push(`Начало коня продолжение`);
			setTimeout(() => {
				if (localplayer != null && janna != null) {
					janna.taskPlayAnim(mainanim, "low_car_sex_loop_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
					localplayer.taskPlayAnim(mainanim, "low_car_sex_loop_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
					if (debug) mp.gui.chat.push(`Конь долго`);
					setTimeout(() => {
						if (localplayer != null && janna != null) {
							janna.taskPlayAnim(mainanim, "low_car_sex_to_prop_p1_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
							localplayer.taskPlayAnim(mainanim, "low_car_sex_to_prop_p1_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
							if (debug) mp.gui.chat.push(`Конец коня`);
							setTimeout(() => {
								if (localplayer != null && janna != null) {
									janna.taskPlayAnim(mainanim, "low_car_sex_to_prop_p2_female", 8.0, 1.0, -1, 1, 0.0, false, false, false);
									localplayer.taskPlayAnim(mainanim, "low_car_sex_to_prop_p2_player", 8.0, 1.0, -1, 1, 0.0, false, false, false);
									if (debug) mp.gui.chat.push(`Конец коня продолжение`);
									setTimeout(() => {
										if (localplayer != null && janna != null) {
											animplayed = false;
											mp.events.call('client::opendialogprostitutca');
											if (debug) mp.gui.chat.push(`Тригер`);
										}
									}, 5000);
								}
							}, 2000);
						}
					}, secondssexa * 1000);
				}
			}, 4000);
		}
	}, 3000);
});

mp.events.add('client::prostitutcaleave', () => {
	janna.taskPlayAnim("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 8, 1, -1, 1, 0, false, false, false);
	localplayer.taskPlayAnim("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 8, 1, -1, 1, 0, false, false, false);
	janna.taskLeaveVehicle(localplayer.vehicle.handle, 0);
	global.popPros.active = false
	global.menuClose2();
	mp.game.invoke("0x5A4F9EDF1673F704", 0);
	blockActions = false;
	sexmod = false;
	mp.events.call('clinet::helpkeysonHUD', false, "SPACE", "ПОСМОТРЕТЬ ЧТО МОЖЕТ ЖАННА"); //А это короче подсказка на худ, у вас ее не будет, я вам не дам
	janna.taskGoToCoordAnyMeans(localplayer.position.x + 100, localplayer.position.y, localplayer.position.z, 1, 0, false, 12, 1000);
	setTimeout(() => {
		if (localplayer != null)
			mp.events.callRemote('server::jannaposChange');
	}, 10000);
	if (localplayer.vehicle != null) {
		localplayer.vehicle.freezePosition(false);
	}
});

mp.events.add('render', () => {
	if (blockActions) {
		mp.game.controls.disableControlAction(2, 0, true);
		mp.game.controls.disableControlAction(2, 75, true);
	}
});