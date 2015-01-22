$(document).ready(function () {
	// TODO: Ideally the controller would take care of this instead of making the UI
	// call directly...
	var clothing = null;
	$.ajax({
		type: "GET",
		url: 'static/json/clothing.json',
		dataType: "json",
		data: {},
		async: false,
		success: function (data) {
			clothing = data;
		}
	});
	
	
	var clothingKeys = Object.keys(clothing);
	var imgs = $(".item-cell > img");
	for (var i = 0; i < clothingKeys.length; i++) {
		imgs[i].src = clothing[clothingKeys[i]].image;
	}
});
