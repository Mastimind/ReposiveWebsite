({
    baseUrl: ".",
    paths: {
        'spine': 'spine/spine',
        'spine/route': 'spine/route',
        'spine/manager': 'spine/manager',
        'spine/local': 'spine/local',
        'spine/ajax': 'spine/ajax',
        'jquery': 'jquery-1.7.2',
        'jquery/mobile/loader': 'jquery.mobile.loader',
        'signalr': 'jquery.signalR-0.5.2.min',
        'jquery/extensions': '_extensions',
        'TaskController': 'controllers/TaskController',
        'DetailController': 'controllers/DetailController',
        'SettingsController': 'controllers/SettingsController',
        'signalr/taskHub': 'taskHubProxy'
    },
    shim: {
        'jquery/mobile/loader': ['jquery'],
        'spine': ['jquery'],
        'spine/manager': ['spine'],
        'spine/route': ['spine'],
        'spine/local': ['spine'],
        'spine/ajax': ['spine'],
        "signalr": ["jquery"],
        "signalr/taskHub": ["signalr"]
    },
    hbs: {
        disableI18n: true,
        templateExtension: "handlebars",
        helperDirectory: "Views/Helpers/"
    },
    name: "main",
    out: "main-built.js"
})