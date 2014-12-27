var express = require('express');
var fs = require('fs');

var app = express();
app.use('/static', express.static(__dirname + '/static'))

app.get("/", function (req, res) {
	console.log(req.url);
    res.writeHead(200);
    res.write(fs.readFileSync(__dirname + "/tutorial/part1.html"));
    res.end();
});

var port = process.env.PORT || 5000;
app.listen(port);
console.log("Listening on port " + port);

