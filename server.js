var express = require('express');
var fs = require('fs');

var app = express();
app.use(express.static(__dirname + '/Release'));

app.get("/", function (req, res) {
	console.log(req.url);
    res.writeHead(200);
    res.write(fs.readFileSync(__dirname + "/Release/Release.html"));
    res.end();
});

var port = process.env.PORT || 5000;
app.listen(port);
console.log("Listening on port " + port);

