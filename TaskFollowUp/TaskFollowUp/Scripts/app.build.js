({
    baseUrl: ".",
    paths: {
        'knockout': 'knockout-2.1.0.debug',
        'jquery': 'jquery-1.7.2',
        'jquery/quicksand': 'jquery.quicksand',
        'jquery/easing': 'jquery.easing.1.3',
        'jquery/rotate': 'jquery-animate-css-rotate-scale',
        'modernizr': 'modernizr-2.6',
        'gfx': 'gfx/gfx',
        'gfx/effects': 'gfx/gfx.effects',
        'gfx/flip': 'gfx/gfx.flip',
        'signalR': 'jquery.signalR-0.5.3.min',
        'signalr/hubs': 'empty:',
    },
    shim: {
        "bootstrap": ["jquery"],
        "gfx": ["jquery"],
        "gfx/effects": ["gfx"],
        "gfx/flip": ["gfx"],
        "jquery/quicksand": ["jquery"],
        "jquery/easing": ["jquery"],
        "jquery/rotate": ["jquery"],
        "toastr": ["jquery"],
        "signalR": ["jquery"]
    },
    hbs: {
        disableI18n: true,
        templateExtension: "handlebars",
        helperDirectory: "Views/Helpers/"
    },
    name: "main",
    out: "main-built.js"
})