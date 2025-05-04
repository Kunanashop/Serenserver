var HUD = new Vue({
    el: ".inGameHud",
    data: {
        show: false,
		bonusblock: false,
		lastbonus: null,
        ammo: -1,
        money: "117 000 000",
        bank: 0,
        mic: false,
        time: "00:00",
        date: "00.00.00",
        street: "Freedom",
        crossingRoad: "Groups",
        server: 1,
        playerId : 0,
		personId: 1,
        eat: 0,
        water: 0,
        online: 74,	
        inVeh: false,
		belt: false,
        engine: false,
        doors: false,
        speed: 0,
        fuel: 0,
        rpm: 0,
		hint: false,
        hp: 0,
		gear: 0,
		rpm: 0,
		mile: 0,
		greenzone: false,
		fishzone: false,
		farmzone: false,
		adminmod: false,
        helpkeys: false,
        helpkey: "",
        helptextkey: "",
		timerheist: 0,
		minimapFix: 0,
		weatherg: 6,
		demorgan: false,
		demorganadmin: "Scoolit",
		demorgantime: "200",
		demorganreason: "By Scoolit",
		
		activequest: false,
		namequest: "Первые шаги",
		countquest: 100,
		textquest: "Купите чипсы в любом 24/7 и отнесите их Гарри",
		
		statinons: ["LSIA Terminal","LSIA Parking","Puerto Del Sol","Strawberry","Pillbox North","Burton","Portola Drive","Del Perro","Little Seoul","Pillbox South","Davis"],
		showhelpmetro: false,
		stationmetro: 0,
		
		gun: 984333226,
		
		iid: 611,
		icount: 10,
		iname: "Лопатка",
		istate: false,
		delay: 0,
		
		att: 1,
		def: 2,
		min: 1,
		sec: 20,
		fracatt: 1,
		fracdef: 2,
		gangzone: false,
		allfrac: ["The Families", "The Ballas Gang", "Los Santos Vagos", "Marabunta Grande", "The Bloods Gang"],
		allcolorfrac: ["#9cddb6","#a688f4","#ffd860","#bdeaff","#d42626"],
    },
    methods: {
        setTime: (time, date) => {
            this.time = time;
            this.date = date;
        }, 
		showBonus(){
			this.bonusblock = !this.bonusblock;
		},
		rpmm: function(rpm) {
            this.rpm = rpm;
        },
		setmission: function(state, name, count, txt) {
			this.activequest = state;
			this.namequest = name;
			this.countquest = count;
			this.textquest = txt;
		},
        getSpeed(){
            let num = 504.295 + (this.speed * 100 / 300) * 5;
            return num > 1007.295 ? 1007.295 : num;
        },
        getRpm(){
            let num = 504.295 + (this.rpm * 300 / 300) * 5;
            return num > 675.295 ? 675.295 : num;
        },
		showadditem: function(id, count, name) {
			this.delay = 4000;
			this.istate = true;
			this.iid = id;
			this.icount = count;
			this.iname = name;
			setTimeout(() => {
				this.istate = false;
			}, this.delay);
		},
        updateSpeed(currentspeed, maxspeed = 650)
        {
            this.speed = currentspeed;
            const meters = document.querySelectorAll('svg[data-value] .meter');

            meters.forEach( (path) => {
            
                let length = path.getTotalLength();
                

            
                let c = parseInt(path.parentNode.getAttribute('data-value'));
                
                let value = 0.4166666666666667 * c;
                let rotate = -130 + (value * 2.61153846153846);
                
                let to = length * ((100 - value) / 100);
                //alert(to);
                path.getBoundingClientRect();
                
                path.style.strokeDashoffset = Math.max(0, to);  
            });
        },
    }
})