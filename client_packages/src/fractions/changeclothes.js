let changeclothes = null
let changeclothescam = null

mp.events.add('OpenClothesChange', (data, name, fracid) => {
    if (changeclothes == null) {
        changeclothes = mp.browsers.new("http://package/browser/ChangeClothesFraction/index.html")
		menuOpen();
		changeclothes.active = true
		changeclothes.execute(`changeclothes.name = '${name}'`);
		changeclothes.execute(`changeclothes.items = ${data}`);
		CameraChangeClothes(fracid);
	}
});
mp.events.add('ChangeNowFracClothes', (name) => {
    mp.events.callRemote("ChangeNowFracClothes:Server", name);
});
function CameraChangeClothes(id){
	let playerPosition;
	let CreateCamPosition;
	let camPoint = {X: 123, Y: 123, Z: 1252};
	switch (id) {
		case 6:
			playerPosition = new mp.Vector3(-571.0057, -195.54567, 38.17885);
			CreateCamPosition = new mp.Vector3(-573.7934, -197.00546, 39.4885);
			camPoint.X = -571.0057;  camPoint.Y = -195.54567; camPoint.Z = 38.04884;
			break;
		case 8:
			playerPosition = new mp.Vector3(298.6097, -598.26526, 43.284093);
			CreateCamPosition = new mp.Vector3(300.8047, -598.9901, 44.471152);
			camPoint.X = 298.6097; camPoint.Y = -598.26526; camPoint.Z = 43.254093;
			break;
		case 7: 
			playerPosition = new mp.Vector3(493.5568, -999.0581, 30.699565); 
			CreateCamPosition = new mp.Vector3(491.12445, -998.9995, 31.569565); 
			camPoint.X = 493.5568; camPoint.Y = -999.0581; camPoint.Z = 30.799565;
			break;
		case 9:
			playerPosition = new mp.Vector3(2518.5847, -340.4426, 100.773346);
			CreateCamPosition = new mp.Vector3(2517.0728, -342.20483, 102.763346);
			camPoint.X = 2518.5847; camPoint.Y = -340.4426; camPoint.Z = 100.873346;
			break;
		case 14:
			playerPosition = new mp.Vector3(-2358.2505, 3254.0122, 32.890718);
			CreateCamPosition = new mp.Vector3(-2355.2505, 3251.0122, 34.390718);
			camPoint.X = -2359.692; camPoint.Y = 3255.435; camPoint.Z = 33.053226;
			break;
	}
	changeclothescam = mp.cameras.new('default', CreateCamPosition, new mp.Vector3(0,0,0), 35);
	changeclothescam.pointAtCoord(camPoint.X, camPoint.Y, camPoint.Z);
	mp.players.local.position = playerPosition;
	changeclothescam.setActive(true);
	mp.game.cam.renderScriptCams(true, true, 1200, true, false);
}
mp.events.add('ClearPersonFractionChanger', () => {
	mp.events.callRemote("ClearPersonFractionChanger:Server");
});
mp.events.add('CloseFracClothes', () => {
	menuClose();
	mp.game.cam.renderScriptCams(false, true, 1000, true, false);
	changeclothescam.destroy();
	changeclothes.destroy();
	changeclothes = null;
    changeclothescam = null;
});