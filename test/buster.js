var config = module.exports;

config["tests"] = {
    environment: "browser",
    rootPath: "../",
    libs: [
    	"static/js/lib/**/*.js"
    ],
    sources: [
    	"static/js/model/**/*.js",
    	"static/js/controller/**/*.js",
    	"!static/js/model/main.js"
    ],
    tests: [
    	"test/**/*.js", "!test/buster.js",
    ]
};
