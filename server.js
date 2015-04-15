var express = require('express');
var fs = require('fs');

var app = express();
app.use(express.static(__dirname + '/Release'));

var LINK = "'<br><a href='/privacyPolicy.html'>Privacy Policy</a>";

app.get("/", function (req, res) {
	var html = fs.readFileSync(__dirname + "/Release/Release.html", "utf8");
	var newhtml = addPrivacyPolicy(html);
    renderHtml(req, res, newhtml);
});

app.get("*.html", function (req, res) {
	renderHtml(req, res, fs.readFileSync(__dirname + req.url));
});

function renderHtml(req, res, html) {
	console.log(req.url);
    res.writeHead(200);
    res.write(html);
    res.end();
}

function addPrivacyPolicy(html) {
	suffix = html.match(/<\/p>\s*<\/body>/g);
	newhtml = html.replace(suffix, LINK + suffix);
	return newhtml;
}

var port = process.env.PORT || 5000;
app.listen(port);
console.log("Listening on port " + port);

