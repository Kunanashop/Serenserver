setInterval(function () {
    let label = 'Alyx RP';
    try {
        if (localplayer.getVariable('REMOTE_ID') == undefined) {
            label = 'В меню авторизации';
        }
        else
			{
			if (localplayer.getVariable('ON_WORK'))
                label = 'Работает где-то на';
			else if (localplayer.getVariable('CARROOMID'))
                label = 'Просматривает список машин на';
			else if (localplayer.getVariable('ON_PLAYER_POLIGON'))
                label = 'Стреляет в полигоне на';
			else if (localplayer.getVariable('PLAYER_IN_CASINO'))
                label = 'Находиться в казино на';
			else if (localplayer.getVariable('PLAYER_IN_METRO'))
                label = 'Едет в метро на';
			else if (localplayer.getVariable('PLAYER_HAS_DILDO'))
                label = 'Играется с дилдо на';
			else if (localplayer.getVariable('INSNOWBOARD'))
                label = 'Катается на сноуборде на';
            else if (mp.players.local.isDiving())
                label = 'Занимается дайвингом на';
            else if (mp.players.local.isSwimming() || mp.players.local.isSwimmingUnderWater())
                label = 'Плавает на';
            else if (mp.players.local.isFalling())
                label = 'Падает с высоты на';
            else if (mp.players.local.isRagdoll())
                label = 'Лежит на земле на';
            else if (mp.players.local.isDead())
                label = 'Умирает на';
            else if (mp.players.local.isInAnyVehicle(false))
                label = 'Ездит на';
            else if (mp.players.local.isRunning() || mp.players.local.isSprinting())
                label = 'Бегает на';
            else if (mp.players.local.isShooting())
                label = 'Стреляется на';
            else if (mp.players.local.isWalking())
                label = 'Бродит на';
            else
                label = 'Проводит время на';
        }    
    }
    catch (e) {
    }
    mp.discord.update(label, 'Alyx RP (by ragemp.pro)');

}, 10000);	