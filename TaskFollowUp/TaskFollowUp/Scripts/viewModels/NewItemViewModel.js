define(['jquery', 'knockout', 'sammy', 'hbs!views/createItem', 'gfx/effects'], function ($, ko, Sammy) {

    var newItem = function() {
        return {
            Id: null, Title: null, StartDate: null, EndDate: null
        };
    };
    
    ko.bindingHandlers.jqmsubmit = {
        init: function (el, accessor, allbindings, vm) {
            ko.bindingHandlers.submit.init(el, accessor, allbindings, vm);
            $(el).submit(function (e) {
                // prevent the submit behavior
                e.preventDefault();
                e.stopPropagation();
                return false;
            });
        }
    };

    var viewModel = function (el) {
        this.url = App.urlBuilder("sprint");
        var context = this;
        this.el = $(el);
        $("body").bind("new-registration", function () {
            if (!!context.existingItem) delete context.existingItem;
            ko.mapping.fromJS(newItem(), {}, context.properties);
            context.el.modal('show');
        });
        $("body").bind("edit-registration", function (e) {
            context.existingItem = e.item;
            ko.mapping.fromJS(e.item, {}, context.properties);
            context.el.modal('show');
        });
        $("body").bind("close-registration", function () {
            context.el.modal('hide');
        });
        $("body").bind("update-by-id", function (e) {
            if (!!context.properties.Id && context.properties.Id() > 0 && context.properties.Id() == e.item.Id && context.el.is(":visible")) {
                ko.mapping.fromJS(e.item, {}, context.properties);
                toastr.warning("The item you are editing has been updated because it was saved by another user.");
            }
        });

        this.properties = {
            Title: ko.observable(),
            StartDate: ko.observable(),
            EndDate: ko.observable(),
        };

        this.render = function () {
            this.el.html(require("hbs!views/createItem")());
        }.bind(this);

        this.createItem = function (e) {
            var isExisting = (!!context.properties.Id && context.properties.Id() > 0);
            var data = JSON.stringify(ko.mapping.toJS(context.properties));
            var id = context.properties.Id;
            delete context.properties.Id;
            $.ajax({
                url: context.url(),
                dataType: 'json',
                type: isExisting ? "PUT" : "POST",
                data: data,
                accept: 'application/json',
                contentType: 'application/json'
            }).success(function () {
                context.el.modal('hide');
                toastr.success('Item saved!');
            }).fail(function () {
                context.properties.Id = id;
                toastr.error("Failed to save records, something went wrong :'(");
            });
            
            return false;
            
        };
    };

    return viewModel;

});