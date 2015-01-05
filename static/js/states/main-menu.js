function MainMenu() {
	Phaser.State.call(this);
}

MainMenu.prototype = Object.create(Phaser.State.prototype);
MainMenu.prototype.constructor = MainMenu;


MainMenu.prototype.preload = function () {
	this.load.image('background', 'static/img/shop_formal.png');
}

MainMenu.prototype.create = function () {
	this.add.sprite(0, 0, 'background');
}
