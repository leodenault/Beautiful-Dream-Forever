var config = module.exports;

config["tests"] = {
    environment: "node",
    rootPath: "../",
    sources: [
    	"static/js/model/state-tracker.js"
    ],
    tests: [
    	"test/model/*.js"
    ]
};
