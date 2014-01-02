
require.config({
    paths: {
        'jquery': 'jquery-1.7.2',
        'kendo/mobile': 'kendo.mobile'
    },
    shim: {
        'kendo/mobile': ['jquery']
    },
    hbs: {
        disableI18n: true,
        templateExtension: "handlebars",
        helperDirectory: "Views/Helpers/"
    }
});

require(['jquery',
    'kendo/mobile'],
    function ($) {
        $(function () {
           
            var app = new kendo.mobile.Application(document.body);
            
            var viewModel = kendo.observable({
                Title: " ",
                StartDate: null,
                EndDate: null,
                

                register: function (e) {
                    e.preventDefault();
                    alert(this.Title);
                    this.set("confirmed", true);
                    app.navigate("#item-complete");
                    
                    return false;
                },
                startOver: function (e) {
                    e.preventDefault();
                }
            });

            kendo.bind($("#item-form form"), viewModel);

        });
    });