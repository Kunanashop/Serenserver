var respawn = new Vue({
    el: '.respawn',
    data: {
        active: false,
		ishouse: false,
		isorg: false,
    },
    methods: {
        set: function (data) {
            this.active = true
			this.ishouse = data[2]
            this.isorg = data[1]
        },
        spawn: function (id) {
            mp.trigger('spawn', id);
        },
    }
})