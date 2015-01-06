var DUB = DUB || {};
DUB.states = DUB.states || {};

(function (ns) {
	ns.MainMenu = function () {
		Phaser.State.call(this);
	}

	ns.MainMenu.prototype = Object.create(Phaser.State.prototype);
	ns.MainMenu.prototype.constructor = ns.MainMenu;


	ns.MainMenu.prototype.preload = function () {
		this.load.image('background', 'static/img/shop_formal.png');
	}

	ns.MainMenu.prototype.create = function () {
		this.add.sprite(0, 0, 'background');
	}
}(DUB.states));
