let pedlist = [
    { Name: 'GarryScotland', Hash: 0x07DD91AC, Pos: new mp.Vector3(-495.19247, -684.2751, 33.21198), Angle: -145, cameraRotate: -55.0, label: "Гарри Скотленд" }, // Garry Scotland QuestNPC 1
    { Name: 'RonBuilder', Hash: 0xD7DA9E99, Pos: new mp.Vector3(-169.25494, -1026.9185, 27.292077), Angle: 167, cameraRotate: 30.0, label: "Прораб Рон" }, // Garry Scotland QuestNPC 1
	{ Name: 'AutoschoolPED', Hash: 0xE7565327, Pos: new mp.Vector3(-710.6738, -1306.976, 5.1126276), Angle: 5, cameraRotate: 0.0, label: "Артур Шторм" }, // autoschool
	{ Name: 'EndryStivens', Hash: 0xA1435105, Pos: new mp.Vector3(-58.036095, -800.47437, 44.22729), Angle: -40, cameraRotate: 50.0, label: "Эндрю Стивенс" }, // Endry Stivens QuestNPC 2
	{ Name: 'GOPOSTAL', Hash: 0x62599034, Pos: new mp.Vector3(-260.96762, -904.54315, 32.310844), Angle: 25.6695, cameraRotate: 290.0, label: "GoPostal" }, // GOPOSTAL WORK
	
	{ Name: 'Government', Hash: 0x2E420A24, Pos: new mp.Vector3(-555.82623, -185.82977, 38.22264), Angle: -148.3471, cameraRotate: -58.0, label: "Джон Джей" }, // Мэрия
	{ Name: 'GOVStock', Hash: 0xACCCBDB6, Pos: new mp.Vector3(-571.3435, -203.25368, 38.178862), Angle: -61.3471, cameraRotate: 120.0, label: "Джейкоб Хэлл" }, // Мэрия Склад
	{ Name: 'LSPD', Hash: 368603149, Pos: new mp.Vector3(442.77942, -981.9034, 30.699666), Angle: 85.1411, cameraRotate: 175.0, label: "Нэнси Спунген" }, // Nancy_Spungen

    { Name: 'SANGStock', Hash: 1925237458, Pos: new mp.Vector3(-2356.147, 3251.33, 32.810737), Angle: 58.991623, cameraRotate: 220.0, label: "Дэвид Браун" }, // SANGCraft 
    { Name: 'SANG', Hash: 0xB3F3EE34, Pos: new mp.Vector3(-2347.958, 3268.936, 32.81076), Angle: 240.8822, cameraRotate: -90.0, label: "Патрик Дэвис" }, // SANG  
	
	{ Name: 'Ballas', Hash: 0x231AF63F, Pos: new mp.Vector3(-95.97823, -1798.8623, 28.994218), Angle: 0, cameraRotate: 20.0, label: "Дарнелл Стивенс" }, // Балласы
	{ Name: 'BallasCraft', Hash: 0xA70B4A92, Pos: new mp.Vector3(-76.67501, -1812.6608, 20.807304), Angle: 135, cameraRotate: 100.0, label: "Брок" }, // Балласы
	{ Name: 'GrooveCraft', Hash: 0xDB729238, Pos: new mp.Vector3(-240.0167, -1496.9425, 27.972406), Angle: 140, cameraRotate: 100.0, label: "Диего" }, // Грув
	{ Name: 'Groove', Hash: 0x33A464E5, Pos: new mp.Vector3(-256.3166, -1518.8059, 31.560752), Angle: -134, cameraRotate: -90.0, label: "Фрэнк Грув" }, // Грув
	{ Name: 'Vagos', Hash: 0x5AA42C21, Pos: new mp.Vector3(977.9547, -1834.5425, 36.007457), Angle: 84, cameraRotate: 200.0, label: "Эмма Вагос" }, // Вагос
	{ Name: 'VagosCraft', Hash: 0x03B8C510, Pos: new mp.Vector3(961.9523, -1841.8513, 31.279239), Angle: -91, cameraRotate: -50.0, label: "Упырь" }, // Вагос
	{ Name: 'Crips', Hash: 0x278C8CB7, Pos: new mp.Vector3(1435.455, -1491.4196, 63.6117), Angle: 167, cameraRotate: 100.0, label: "Адамс Крипс" }, // Крипс
	{ Name: 'CripsCraft', Hash: 0x3273A285, Pos: new mp.Vector3(1443.818, -1492.9783, 66.53294), Angle: 70, cameraRotate: 20.0, label: "Саливан" }, // Крипс
	{ Name: 'Bloods', Hash: 0x9D0087A8, Pos: new mp.Vector3(472.02426, -1308.6912, 29.235564), Angle: -150, cameraRotate: -110.0, label: "Мигель Бладс" }, // Бладс
	{ Name: 'BloodsCraft', Hash: 0x7E0961B8, Pos: new mp.Vector3(515.3959, -1340.065, 29.373294), Angle: 130, cameraRotate: 90.0, label: "Рожок" }, // Бладс
	
	{ Name: 'EMS', Hash: 0xD47303AC, Pos: new mp.Vector3(309.62247, -593.79224, 43.284097), Angle: 26.7343, cameraRotate: 296.0, label: "Дэвид Харис" }, //Емс выдача хп
    { Name: 'EMSStock', Hash: 0xAB594AB6, Pos: new mp.Vector3(303.598, -600.72876, 43.284097), Angle: -62.7343, cameraRotate: 152.0, label: "Эллисон Бауэрман" }, //Емс выдача хп
	
	{ Name: 'Bones_Bulldog', Hash: 1581098148, Pos: new mp.Vector3(487.26984, -996.9788, 30.699801), Angle: 91.12, cameraRotate: 180.0, label: "" }, // Bones_Bulldog
	
    { Name: 'FIBStock', Hash: 0x5CDEF405, Pos: new mp.Vector3(2526.4375, -334.23035, 101.893254), Angle: 45.02704, cameraRotate: 250.0, label: "Стив Хэйн" }, //  Steve_Hain 
    { Name: 'FIB', Hash: 0x585C0B52, Pos: new mp.Vector3(2510.3142, -429.4314, 94.58207), Angle: 310.02704, cameraRotate: 100.0, label: "Брайн Лоуренс" }, //  Steve_Hain 
    { Name: 'LSSD', Hash: 0xB144F9B9, Pos: new mp.Vector3(-448,5447, 6012,75, 31,716455), Angle: 101.684555, cameraRotate: 100.0, label: "Ронан Мори" }, // LSSD
    { Name: 'LSSDCraft', Hash: 0x4161D042, Pos: new mp.Vector3(-436,35355, 5999,2847, 31,716099), Angle: 42.42207, cameraRotate: 100.0, label: "Аманда Смит" }, // LSSDCraft

    { Name: 'YAK', Hash: 0x7C851464, Pos: new mp.Vector3(-1034.8304, 314.58453, 66.99373), Angle: -154, cameraRotate: -54.0, label: "Такахаси Кимура" }, // Yakuza
    { Name: 'YakuzaCraft', Hash: 0xDC5C5EA5, Pos: new mp.Vector3(-1034.5316, 312.03116, 71.669), Angle: -66, cameraRotate: 120.0, label: "Сато Ямада" }, // YakuzaCraft

    { Name: 'AM', Hash: 0xCA56FA52, Pos: new mp.Vector3(-1884.7914, 2048.2656, 140.97118), Angle: 163, cameraRotate: 120.0, label: "Антарам Карамян" }, // Армянская мафия
    { Name: 'AMCraft', Hash: 0x9FD4292D, Pos: new mp.Vector3(-1899.6624, 2074.7715, 140.99332), Angle: -180, cameraRotate: -80.0, label: "Шнорик Самарян" }, // Армянская мафия крафт

    { Name: 'LCN', Hash: 0x89768941, Pos: new mp.Vector3(1393.6703, 1138.0675, 114.44233), Angle: 86, cameraRotate: 0.0, label: "Изабелла ЛаКоста" }, // ЛКН
	{ Name: 'LCNCraft', Hash: 0x040EABE3, Pos: new mp.Vector3(1399.5895, 1141.4763, 107.32543), Angle: 99, cameraRotate: 9.0, label: "Джошуа Вито" }, // ЛКН

    { Name: 'RMafia', Hash: 0xF1E823A2, Pos: new mp.Vector3(-1522.515, 846.8073, 181.55153), Angle: -156, cameraRotate: 26.637217, label: "Дмитрий Медведь" }, // рашн
	{ Name: 'RMafiaCraft', Hash: 0xFDA94268, Pos: new mp.Vector3(-1501.5396, 834.6476, 178.65323), Angle: -63, cameraRotate: 115.78371, label: "Олег Бобров" }, // Рашн
	
	{ Name: 'CasinoSecurity', Hash: 0xAD4C724C, Pos: new mp.Vector3(923.0305, 47.859875, 81.10636), Angle: 60.6695, cameraRotate: -90.0, label: "" }, // казино
	{ Name: 'BarmanCasino', Hash: 0xE5A11106, Pos: new mp.Vector3(1110.1207, 208.32074, -49.44006), Angle: 84.6695, cameraRotate: 180.0, label: "" }, // Бармэн

    { Name: 'weaponshop39', Hash: 0x719D27F4, Pos: new mp.Vector3(841.3726, -1035.507, 28.19486), Angle: 341.3947, cameraRotate: 30.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop40', Hash: 0x719D27F4, Pos: new mp.Vector3(23.84422, -1105.745, 29.79701), Angle: 134.4664, cameraRotate: 90.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop38', Hash: 0x719D27F4, Pos: new mp.Vector3(-1118.184, 2700.745, 18.55413), Angle: 197.8389, cameraRotate: -45.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop37', Hash: 0x719D27F4, Pos: new mp.Vector3(1692.979, 3761.729, 34.70532), Angle: 207.8013, cameraRotate: -45.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop36', Hash: 0x719D27F4, Pos: new mp.Vector3(-661.189, -933.6142, 21.82921), Angle: 159.1136, cameraRotate: 45.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop35', Hash: 0x719D27F4, Pos: new mp.Vector3(808.9966,-2159.235, 29.619), Angle: 333.8325, cameraRotate: 45.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop34', Hash: 0x719D27F4, Pos: new mp.Vector3(-1304.398, -395.6584, 36.69575), Angle: 56.81636, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'weaponshop33', Hash: 0x719D27F4, Pos: new mp.Vector3(253.4281, -51.5676, 69.94106), Angle: 41.85976, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'PremiumClothes', Hash: 0xB7C61032, Pos: new mp.Vector3(-708.8664, -151.8637, 37.41513), Angle: 114.6154, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'PremiumClothes2', Hash: 0xB7C61032, Pos: new mp.Vector3(-165.0003, -302.8776, 39.73326), Angle: 248.5414, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'PremiumClothes3', Hash: 0xB7C61032, Pos: new mp.Vector3(-1448.999, -238.2128, 49.81324), Angle: 49.63409, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes', Hash: 0xB58D2529, Pos: new mp.Vector3(-1193.256, -766.5146, 17.31632), Angle: 212.0163, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes2', Hash: 0xB58D2529, Pos: new mp.Vector3(126.5512, -225.5727, 54.557), Angle: 63.66494, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes3', Hash: 0xB58D2529, Pos: new mp.Vector3(612.8331, 2763.936, 42.08815), Angle: 275.0396, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes4', Hash: 0xB58D2529, Pos: new mp.Vector3(-3169.805, 1041.827, 20.86321), Angle: 60.38713, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes5', Hash: 0xB58D2529, Pos: new mp.Vector3(77.21005, -1387.452, 29.37612), Angle: 172.7294, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
    { Name: 'LowClothes6', Hash: 0xB58D2529, Pos: new mp.Vector3(-817.1215, -1072.2, 11.3281), Angle: 112.8105, cameraRotate: -90.0, label: "" }, //Магазин 24/7 3
	
	{ Name: 'Rentcar', Hash: 0x75C34ACA, Pos: new mp.Vector3(-767.13214, -1472.6307, 5.00052), Angle: -60.6695, cameraRotate: 180.0, label: "Аренда лодок" }, // arenda1
	{ Name: 'Rentcar2', Hash: 0x75C34ACA, Pos: new mp.Vector3(-207.65958, -1010.3089, 29.145309), Angle: 68.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda2
	{ Name: 'Rentcar3', Hash: 0x75C34ACA, Pos: new mp.Vector3(325.21597, -1370.408, 31.907945), Angle: 92.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda3
	{ Name: 'Rentcar4', Hash: 0x75C34ACA, Pos: new mp.Vector3(-498.68393, -289.67007, 35.570645), Angle: 17.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda4
	
	{ Name: 'Rentcar5', Hash: 0x75C34ACA, Pos: new mp.Vector3(819.0702, -44.77016, 80.57926), Angle: 96.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda5
	{ Name: 'Rentcar6', Hash: 0x75C34ACA, Pos: new mp.Vector3(-1241.6735, 251.6, 64.90314), Angle: -158.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda6
	{ Name: 'Rentcar7', Hash: 0x75C34ACA, Pos: new mp.Vector3(-475.0324, -608.9132, 31.324247), Angle: 159.6695, cameraRotate: 180.0, label: "Аренда транспорта" }, // arenda7
	{ Name: 'KillerBuro', Hash: 0x613E626C, Pos: new mp.Vector3(905.77386, -3230.835, -98.29437), Angle: 3.6695, cameraRotate: 0.0, label: "Джейсон" }, // парк2
	{ Name: 'KillerBuro2', Hash: 0x2A797197, Pos: new mp.Vector3(2193.4214, 5594.003, 53.759023), Angle: -15.6695, cameraRotate: 0.0, label: "Джэйкоб" }, // парк2
	
	{ Name: 'SnowShop', Hash: 0x855E36A3, Pos: new mp.Vector3(149.7237, -968.9656, 30.223231), Angle: -90, cameraRotate: 0, label: "Новогодний торговец" }, // snowshop
];
var Peds = [];
//setTimeout(function () {
    pedlist.forEach(ped => {
        Peds[ped.Name] = {
            uid : ped.Name,
            entity : mp.peds.new(ped.Hash, ped.Pos, ped.Angle, 0),
            extra_rotate : ped.cameraRotate,
            labelText : ped.label,
            position : ped.Pos,
            labelObject : createLabel(ped.label, ped.Pos),
            colShape : null,
            createShape() { 
                this.removeShape();
                this.colShape = mp.colshapes.newSphere(this.position.x, this.position.y, this.position.z, 2); 
                this.colShape.interact = ped.Name; 
                this.colShape.in = false;
            },
            removeShape() { 
                if(this.colShape!=null){
                    if(this.colShape.in){
                        mp.players.local.npcInteract = null;
                    }
                    this.colShape.destroy(); 
                }
                this.colShape = null;
            }
        }
    });
   // }, 5000);

function createLabel(label, position){
    return label=="" ? null : mp.labels.new("~y~NPC ~w~" + label, position.add(new mp.Vector3(0, 0, 0.3)),
    {
        los: false,
        font: 0,
        color: [255,255,255,100],
        drawDistance: 5,
    })
}

mp.events.add('playerEnterColshape', (shape)=>{
        if(shape.interact!=null){
            shape.in = true;
            mp.players.local.npcInteract = shape.interact;
        }
});

mp.events.add('playerExitColshape', (shape)=>{
    if(shape.interact!=null){
        shape.in = false;
        mp.players.local.npcInteract = null;
    }
});



let hiding;
let handCamera;
let hidedPlayers = [];
let hidedLabels =[];
let oldpos;
function toRadian(x){
    return Math.PI*x/180;
}

mp.events.add('NPC.ColShape.Local', (npcName, flag)=>{
    if(Peds[npcName]!=null){
        if(flag) Peds[npcName].createShape();
            else Peds[npcName].removeShape();

    }
});


mp.events.add('NPC.cameraOn', (pedName, transitionTime = 0) => {
    handCamera = mp.cameras.new('default', new mp.Vector3(0,  0,  0), new mp.Vector3(0,0,0), 30);
    handCamera.setActive(true);
    handCamera.pointAtPedBone(Peds[pedName].entity.handle, 31086, 0, 0, 0, true);
    handCamera.setCoord(Peds[pedName].entity.getCoords(true).x + (Math.sin(toRadian(Peds[pedName].entity.getHeading()+Peds[pedName].extra_rotate))*2), Peds[pedName].entity.getCoords(true).y+(Math.cos(toRadian(Peds[pedName].entity.getHeading()+Peds[pedName].extra_rotate))*2), Peds[pedName].entity.getCoords(true).z+0.5);
    mp.game.cam.renderScriptCams(true, transitionTime>0, transitionTime, true, false);
    if(Peds[pedName].labelObject!=null){
        Peds[pedName].labelObject.destroy();
        Peds[pedName].labelObject=null;
        hidedLabels.push(pedName);
    }
    hiding = startHide(Peds[pedName].entity.getCoords(true));
    ///////////////////
    oldpos = mp.players.local.position;
    mp.players.local.position = new mp.Vector3(Peds[pedName].entity.getCoords(true).x + (Math.sin(toRadian(Peds[pedName].entity.getHeading()+Peds[pedName].extra_rotate))*2), Peds[pedName].entity.getCoords(true).y+(Math.cos(toRadian(Peds[pedName].entity.getHeading()+Peds[pedName].extra_rotate))*2), Peds[pedName].entity.getCoords(true).z+0.5);
});

mp.events.add('NPC.cameraOff', (transitionTime = 0)=>{
    if(hiding!=null){
    clearInterval(hiding);
    hiding = null;
    }
    if(handCamera!=null){
        mp.game.cam.renderScriptCams(false, transitionTime>0, transitionTime, true, true);
        handCamera.destroy();
        handCamera = null;
    }
    mp.players.local.position = oldpos;
    oldpos = null;
    
    
    setTimeout(()=>{
        while(hidedLabels.length>0){
            var pedName = hidedLabels.pop();
            Peds[pedName].labelObject = createLabel(Peds[pedName].labelText, Peds[pedName].position);
        }

        while(hidedPlayers.length>0)hidedPlayers.pop().setAlpha(255); 
    }, transitionTime/2);
});


function startHide(pos){
    mp.players.local.setAlpha(0);
    hidedPlayers.push(mp.players.local);
    return setInterval(function (vector){
        mp.players.forEachInStreamRange(player => {
            if(vector.subtract(player.position).length()<10){
                if(player.getAlpha()>0)
                {
                player.setAlpha(0);
                hidedPlayers.push(player);
                }
            }else if(hidedPlayers.includes(player)){
                hidedPlayers.splice(hidedPlayers.indexOf(player), 1);
            }
        });
    }, 1000, pos);
}

mp.peds.new(0x1BCC157B, new mp.Vector3(-1268.3584, -1450.1157, 4.5948085), 22.6695, 0);
mp.peds.new(0x14C3E407, new mp.Vector3(-1268.6962, -1448.8938, 4.5953885), -168.6695, 0);
mp.peds.new(0x81441B71, new mp.Vector3(-1257.2031, -1479.01, 4.3489405), -146.6695, 0);
mp.peds.new(0x80E59F2E, new mp.Vector3(-1257.5394, -1478.6963, 4.3489405), -146.6695, 0);
mp.peds.new(0x7B0E452F, new mp.Vector3(-1240.1415, -1474.7213, 4.3285243), 70.6695, 0);
mp.peds.new(0xE7A963D9, new mp.Vector3(-1233.934, -1455.4933, 4.254521), 160.6695, 0);
mp.peds.new(0x0B4A6862, new mp.Vector3(-1234.3534, -1456.988, 4.269508), -16.6695, 0);
mp.peds.new(0xF0AC2626, new mp.Vector3(-1255.3428, -1453.0127, 4.5531675), 77.6695, 0);
mp.peds.new(0x46E39E63, new mp.Vector3(-1277.3029, -1419.1233, 4.3491164), -30.6695, 0);
mp.peds.new(0x3521A8D2, new mp.Vector3(-1257.7683, -1491.4387, 4.3305098), -76.6695, 0);
mp.peds.new(0xD1FEB884, new mp.Vector3(-1266.9015, -1486.9998, 4.3204976), -38.6695, 0);
mp.peds.new(0xD1FEB884, new mp.Vector3(-1287.7128, -1428.6512, 4.5681049), -55.6695, 0);
// mp.peds.new(-1686040670, new mp.Vector3(-495.14215, -684.2896, 33.21113), -146.6695, 0);
mp.peds.new(193817059, new mp.Vector3(694.5975, 252.09875, 93.47306), 149.6695, 0);
mp.peds.new(0x3C438CD2, new mp.Vector3(-1225.3202, -1498.2853, 4.3722717), 92.6695, 0);

mp.peds.new(2014052797, new mp.Vector3(130.07095, -1284.9728, 29.249323), 121.6695, 0);
mp.peds.new(0x5C14EDFA, new mp.Vector3(102.82939, -1290.1981, 29.229707), -24.6695, 0);
mp.peds.new(0x52580019, new mp.Vector3(104.6681, -1294.2317, 29.229707), -70.6695, 0);
mp.peds.new(2014052797, new mp.Vector3(4905.7383, -4941.266, 4.4659426), 21.6695, 0);
mp.peds.new(2014052797, new mp.Vector3(-1391.7186, -605.5828, 30.39955), 117.6695, 0);
mp.peds.new(0x5C14EDFA, new mp.Vector3(-1383.4264, -612.297, 31.736133), 121.6695, 0);
mp.peds.new(0x52580019, new mp.Vector3(-1379.9653, -617.60425, 31.737846), 101.6695, 0);
mp.peds.new(0xB594F5C3, new mp.Vector3(-305.2498, 6268.427, 31.506794), -131.6695, 0);
mp.peds.new(0xFD5537DE, new mp.Vector3(987.3634, -95.67454, 74.825945), -143.6695, 0);
mp.peds.new(0xFBB374CA, new mp.Vector3(1984.4019, 3054.5178, 47.195165), -112.6695, 0);
mp.peds.new(0x3053E555, new mp.Vector3(-561.7963, 287.26086, 82.15646), -95.6695, 0);






