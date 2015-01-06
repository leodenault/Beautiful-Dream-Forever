var DUB = DUB || {};
DUB.states = DUB.states || {};

(function (ns) {
	ns.Wardrobe = function () {
		Phaser.State.call(this);
	}
	
	ns.Wardrobe.prototype = Object.create(Phaser.State.prototype);
	ns.Wardrobe.prototype.constructor = ns.Wardrobe;
	
	ns.Wardrobe.prototype.preload = function () {
		this.load.image("wardrobe-background", "static/img/ui/wardrobe_background.png");
	}
	
	ns.Wardrobe.prototype.create = function () {
		this.add.sprite(0, 0, "wardrobe-background");
	}
}(DUB.states));
