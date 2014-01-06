
require.config({
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
        'highcharts':'js/highcharts'
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
        "signalR": ["jquery"],
        "highcharts" : ["jquery"],
        "signalr/hubs": ["signalR"],
        "jquery": {
            exports: function (lib) {
                window.jQuery = lib;
                return lib;
            }
        }
    },
    hbs: {
        disableI18n: true,
        templateExtension: "handlebars",
        helperDirectory: "Views/Helpers/"
    }
});

require(['jquery',
    'Sammy',
    'handlebars',
    'knockout',
    'knockout.mapping',
    'viewModels/MenuViewModel',
    'viewModels/MainViewModel',
    'viewModels/SideViewModel',
    'viewModels/NewItemViewModel',
    'bootstrap',
    'modernizr',
    'moment',
    'gfx/effects',
    'gfx/flip',
    'jquery/quicksand',
    'jquery/easing',
    'jquery/rotate',
    'toastr',
    'signalr/hubs',
    'highcharts'],
    function ($, Sammy, Handlebars, ko, mapping) {
        $(function () {
            ko.mapping = mapping;
            window.jQuery = $;
            
            var routes = [];

            window.Handlebars = Handlebars;
            function processRoutes() {
                for (var r in routes) {
                    for (var f in routes[r]) {
                        this.get(f, routes[r][f]);
                    }
                }
            };
            window.App = {
                log: function () {
                    ///<summary>Logs to the console</summary>
                    try {
                        console.log.apply(console, arguments);
                    } catch (e) {
                        try {
                            opera.postError.apply(opera, arguments);
                        } catch (e) {  /* browser doesn't support logging */ }
                    }
                },
                
                navigate: function () {
                    var args = 1 <= arguments.length ? [].slice.call(arguments, 0) : [];
                    args.unshift("#");
                    window.location.hash = args.join('/');
                },
                
                urlBuilder: function (baseUrl) {
                    return function () {
             
                        var args = 1 <= arguments.length ? [].slice.call(arguments, 0) : [];
                        args.unshift(baseUrl);
                        args.unshift("%ApplicationPath%api");
                        return args.join('/');
                    };
                },
                
                createViewModel: function (name, elementId) {
         
                    var viewModelClass = require(name);
                    var el = $(elementId);
                    if (el.length === 0) {
                        throw elementId + " not found."; }
                    return new viewModelClass(el);
                },
                
                applyBindings: function (viewModel, node) {
                    if (!!viewModel.__bound) return;
                    viewModel.__bound = true;
                    if (!!viewModel.render) viewModel.render();
                    if (!!viewModel.routes) routes.push(viewModel.routes());
                    var el = (node || viewModel.el);
                    if (!!el.length) el = el[0];
                    ko.applyBindings(viewModel, el);
                },
                
                setupHub: function () {
                    try {
                       
                        var sprints = $.connection.sprintHub;
                        $.connection.hub.start();
                        sprints.refresh = function (e) {
                            App.log('Changes detected, need to refresh: ' + e);
                            $("body").trigger({ type: 'update-by-id', item: e, callback: "update-item" });
                        };
                        sprints.remove = function (e) {
                            App.log('Changes detected, need to remove: ' + e);
                            $("body").trigger({ type: 'update-by-id', item: e, callback: "delete-item-confirmed" });
                        };
                    } catch (e) {
                        App.log(e);
                    }
                }
            };

            var viewModel = App.createViewModel("viewModels/MainViewModel", ".main-content");
            App.applyBindings(viewModel);
            
            var menuVm = App.createViewModel("viewModels/MenuViewModel", ".navbar-fixed-top");
            App.applyBindings(menuVm);
            
            var sideVm = App.createViewModel("viewModels/SideViewModel", ".sidebar-nav");
            sideVm.mainView = viewModel;
            App.applyBindings(sideVm);
            
            var newRegistrationVm = App.createViewModel("viewModels/NewItemViewModel", "#createItem");
            App.applyBindings(newRegistrationVm);

            //var deleteItemsVm = App.createViewModel("viewModels/DeleteItemViewModel", "#confirmDelete");
            //App.applyBindings(deleteItemsVm);

            $("body").on("click", "li.item .front, li.item .close", function (e) {
                
                e.preventDefault();
                var el = $(this).closest("li");
                var anyFlipped = $("li.item.flipped");
                if (anyFlipped.length > 0 && anyFlipped[0]!==el[0])
                    anyFlipped.trigger("flip");
                if(el.data("flippable") == true) {
                    el.trigger("flip");
                } else {
                    el.data("flippable", true);
                    el.gfxFlip({ width: 180, height: 170 }).delay(50).queueNext(function () { el.trigger("flip"); });
                }
            });
            $("#source").on("click", "li.item .back a:not(.close)", function (e) {
                e.preventDefault();
                var el = $(e.target).closest("a");
                var parent = el.parents("li");
                var sourceElements = $("#source li[data-id='" + parent.attr("data-id") + "'] .back a");
                var index = sourceElements.index(el);
                var link = $("#destination li[data-id='" + parent.attr("data-id") + "'] .back a").get(index);
                $(link).trigger("click");
            });

            window.getTime = function (val) {
                if (typeof val === "string")
                    return moment(val);
                if(val() == false) {
                    return moment();
                }
                return moment(val());
            };

            Sammy(processRoutes).run('#/');
            
            try {
                App.setupHub();
            }catch (e) {
                App.log("Error setting up hub: " + e);
            }

        });
    });