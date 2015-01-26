var assert = buster.referee.assert;
var refute = buster.referee.refute;
var WardrobePaginator = DUB.model.WardrobePaginator;

function createData(size) {
	var data = [];
	for (var i = 1; i <= size; i++) {
		data.push(i);
	}
	return data;
}

buster.testCase("Wardrobe Paginator tests", {
	"paginator returns same page if at end of data": function () {
		var paginator = new WardrobePaginator(createData(16));
		var page1 = paginator.current();
		var page2 = paginator.next();
		var page3 = paginator.next();
		assert.isArray(page1);
		refute.equals(page1, page2);
		assert.equals(page2, page3);
	},
	
	"paginator has 15 as default page size": function () {
		var paginator15 = new WardrobePaginator(createData(15));
		var paginator16 = new WardrobePaginator(createData(16));
		
		assert.equals(paginator15.current().length, 15);
		assert.equals(paginator16.current().length, 15);
	},
	
	"previous returns same page if at beginning of data": function() {
		var paginator = new WardrobePaginator(createData(16));
		paginator.next();
		var page = paginator.previous();
		assert.equals(paginator.previous(), page);
	},
	
	"current returns current page": function () {
		var paginator = new WardrobePaginator(createData(33));
		assert.equals(paginator.current(), [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]);
		
		var page = paginator.next();
		assert.equals(paginator.current(), page);
		
		var page = paginator.next();
		assert.equals(paginator.current(), page);
	},
	
	"next returns next set of data": function () {
		var paginator = new WardrobePaginator(createData(10), 3);
		assert.equals(paginator.current(), [1, 2, 3]);
		assert.equals(paginator.next(), [4, 5, 6]);
		assert.equals(paginator.next(), [7, 8, 9]);
		assert.equals(paginator.next(), [10]);
	},
	
	"previous returns previous set of data": function () {
		var paginator = new WardrobePaginator(createData(10), 3);
		paginator.next();
		paginator.next();
		assert.equals(paginator.next(), [10]);
		assert.equals(paginator.previous(), [7, 8, 9]);
		assert.equals(paginator.previous(), [4, 5, 6]);
		assert.equals(paginator.previous(), [1, 2, 3]);
	}
});

