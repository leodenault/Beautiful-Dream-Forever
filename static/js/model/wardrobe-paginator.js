var DUB = DUB || {};
DUB.model = DUB.model || {};

(function (ns) {
	ns.WardrobePaginator = function (data, pageSize) {
		var DEFAULT_PAGE_SIZE = 15;
	
		pageSize = typeof pageSize === 'undefined' ? DEFAULT_PAGE_SIZE : pageSize;
		
		var sets = [];
		var index = 0;

		for (var i = 0; i < data.length; i += pageSize) {
			sets.push(data.slice(i, i + Math.min(pageSize, data.length - i)));
		}
		
		return {
			next: function () {
				index = (index == sets.length - 1) ? index : ++index;
				return sets[index];
			},
			
			previous: function () {
				index = (index > 0) ? --index : index;
				return sets[index];
			},
			
			current: function () {
				return sets[index];
			}
		}
	}
}(DUB.model));
