global.circleEntity = null;
global.circleOpen = false;
var circleTitle = "";

global.OpenCircle = function(title, data) {
    if (menuCheck() || circleOpen) return;
    global.board.execute(`circle.show("${title}",${data})`);
    circleTitle = title;
    global.circleOpen = true;
    global.menuOpen();
}
global.CloseCircle = function(hide) {
    if(hide) global.board.execute("circle.hide()");
    global.circleOpen = false;
    global.menuClose();
}

// // //
mp.events.add('circleCallback', (index) => {
    if (index == -1) {
        CloseCircle(false);
        global.soundCEF.execute(`playSounds('./sounds/closed_g.mp3')`);
    } else {
        CloseCircle(false);
        switch (circleTitle) {
            case "Машина":
                switch (index) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
					case 4:
					case 5:
					case 6:
                        if (global.entity == null) return;
                        if(global.entity.getVariable("ACCESS") == "DUMMY") return;
                        if (index == 1 || index == 3) {
                            const boneID = global.entity.getBoneIndexByName("boot"); 
                            var trunkpos = global.entity.getWorldPositionOfBone(boneID);
                            if(boneID <= 0) trunkpos = global.localplayer.position;
                            mp.events.callRemote('vehicleSelected', global.entity, index, trunkpos.x, trunkpos.y, trunkpos.z);
                            return;
                        }
						else if (index == 5) {
                            bonnetpos = global.localplayer.position;
                            mp.events.callRemote('vehicleSelected', global.entity, index, bonnetpos.x, bonnetpos.y, bonnetpos.z);
                        }
                        else if (index == 0) {
                            const boneID = global.entity.getBoneIndexByName("bonnet");
                            var bonnetpos = global.entity.getWorldPositionOfBone(boneID);
                            if(boneID <= 0) bonnetpos = global.localplayer.position;
                            mp.events.callRemote('vehicleSelected', global.entity, index, bonnetpos.x, bonnetpos.y, bonnetpos.z);
                            return;
                        }
                        else {
                            const boneID = global.entity.getBoneIndexByName("bodyshell");
                            var bodyshell = global.entity.getWorldPositionOfBone(boneID);
                            if(boneID <= 0) bodyshell = global.localplayer.position;
                            mp.events.callRemote('vehicleSelected', global.entity, index, bodyshell.x, bodyshell.y, bodyshell.z);
                            return;
                        }
                }
                return;
            case "Игрок":
                if (global.entity == null) return;
                switch (index) {
                    case 0:
                        mp.events.callRemote('pSelected', global.entity, "Передать деньги");
                        return;
                    case 1:
                        mp.events.callRemote('pSelected', global.entity, "Предложить обмен");
                        return;
                    case 2:
                        if (pFraction === 0 || pFraction === 15) return;
                        global.OpenCircle("Фракция", pFraction);
                        return;
                    case 3:
                        //mp.gui.chat.push(">>" + global.entity);
                        mp.events.callRemote('passport', global.entity);
                        return;
                    case 4:
                        //mp.gui.chat.push(">>" + global.entity);
                        mp.events.callRemote('licenses', global.entity);
                        return;
                    case 5:
                        mp.events.callRemote('pSelected', global.entity, "Вылечить");
                        return;
                    case 6:
                        global.OpenCircle("Дом", 0);
                        return;
                    case 7:
                        global.OpenCircle("Социальное", 0);
                        return;
                }
                return;
			case "Социальное":
                switch (index) {
                    case 0:
                        mp.events.callRemote('pSelected', global.entity, "Пожать руку");
                        return;
                    case 1:
                        mp.events.callRemote('pSelected', global.entity, "Поцеловать");
                        return;
					case 2:
                        mp.events.callRemote('pSelected', global.entity, "Взять на руки");
                        return;
                }
                return;
            case "Дом":
                switch (index) {
                    case 0:
                        mp.events.callRemote('pSelected', global.entity, "Продать машину");
                        return;
                    case 1:
                        mp.events.callRemote('pSelected', global.entity, "Продать дом");
                        return;
                    case 2:
                        mp.events.callRemote('pSelected', global.entity, "Заселить в дом");
                        return;
                    case 3:
                        mp.events.callRemote('pSelected', global.entity, "Пригласить в дом");
                        return;
                }
                return;
            case "Фракция":
                if (global.entity == null) return;
                circleEntity = global.entity;
                if (fractionActions[pFraction] == undefined) return;
                mp.events.callRemote('pSelected', global.entity, fractionActions[pFraction][index]);
                return;
            case "Категории":
                if (index == 7) {
					if(!localplayer.isFalling()) mp.events.callRemote('aSelected', -1, -1);
                    return;
                }
                switch (index) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
					case 6:
                        aCategory = index;
                        global.OpenCircle("Анимации", index);
                        return;
                }
                return;
            case "Анимации":
				if(aCategory == 1 && index == 7) {
					aCategory = 10;
                    global.OpenCircle("Анимации", 10);
				} else if(aCategory == 4 && index == 7) {
					aCategory = 13;
                    global.OpenCircle("Анимации", 13);
				} else if(aCategory == 5 && index == 7) {
					aCategory = 7;
                    global.OpenCircle("Анимации", 7);
				} else if(aCategory == 6 && index == 7) {
					aCategory = 12;
                    global.OpenCircle("Анимации", 12);
				} else if(aCategory == 7 && index == 7) {
					aCategory = 8;
                    global.OpenCircle("Анимации", 8);
				} else if(aCategory == 8 && index == 7) {
					aCategory = 9;
                    global.OpenCircle("Анимации", 9);
				} else if(aCategory == 10 && index == 7) {
					aCategory = 11;
                    global.OpenCircle("Анимации", 11);
				} else mp.events.callRemote('aSelected', aCategory, index);
                return;
        }
    }
});

var aCategory = -1;

// // //
var pFraction = 0;
var fractionActions = [];
fractionActions[1] = ["Ограбить", "Украсть оружие", "Мешок"];
fractionActions[2] = ["Ограбить", "Украсть оружие", "Мешок"];
fractionActions[3] = ["Ограбить", "Украсть оружие", "Мешок"];
fractionActions[4] = ["Ограбить", "Украсть оружие", "Мешок"];
fractionActions[5] = ["Ограбить", "Украсть оружие", "Мешок"];
fractionActions[6] = ["Вести за собой"];
fractionActions[7] = ["Вести за собой", "Обыскать", "Изъять оружие", "Изъять нелегал", "Сорвать маску", "Выписать штраф", "Выдать лицензию на оружие", "Посадить в КПЗ"];
fractionActions[8] = ["Продать аптечку", "Предложить лечение", "Выдать медицинскую карту"];
fractionActions[9] = ["Вести за собой", "Обыскать", "Изъять оружие", "Изъять нелегал", "Сорвать маску"];
fractionActions[10] = ["Вести за собой", "Мешок", "Ограбить", "Украсть оружие"];
fractionActions[11] = ["Вести за собой", "Мешок", "Ограбить", "Украсть оружие"];
fractionActions[12] = ["Вести за собой", "Мешок", "Ограбить", "Украсть оружие"];
fractionActions[13] = ["Вести за собой", "Мешок", "Ограбить", "Украсть оружие"];
fractionActions[14] = ["Вести за собой", "Обыскать", "Изъять оружие", "Изъять нелегал", "Сорвать маску"];
fractionActions[15] = ["Вести за собой"];
fractionActions[16] = ["Вести за собой"];
fractionActions[17] = ["Вести за собой", "Мешок", "Обыскать", "Изъять нелегал", "Сорвать маску"];
fractionActions[18] = ["Вести за собой", "Обыскать", "Изъять оружие", "Изъять нелегал", "Сорвать маску", "Выписать штраф"];
mp.events.add('fractionChange', (fraction) => {
    pFraction = fraction;
});