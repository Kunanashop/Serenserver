var changeclothes = new Vue({
    el: ".changeclothes",
    data: {
		style: 0,
		name: "GOVER",
		menuid: "",
		items: {
			Майки: [
				"asd",
				"asdas",
				"asdas"
			],
			Вверх: [
				"ГОВНО",
				"ИТАЛО ЛОХ",
				"asdasda"
			]
		}
    },
    methods:{
        gostyle: function(index) {
            this.style = index;
        },
		setFracChange: function(nameClothes) {
			mp.trigger("ChangeNowFracClothes", nameClothes);
		},
		clearPerson() {
			mp.trigger("ClearPersonFractionChanger");
		},
		exit: function() {
			mp.trigger("CloseFracClothes");
		},
    }
});