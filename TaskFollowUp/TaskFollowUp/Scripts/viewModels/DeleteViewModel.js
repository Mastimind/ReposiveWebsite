define(['jquery', 'knockout', 'sammy', 'hbs!views/createItem', 'gfx/effects'], function ($, ko, Sammy) {

    var viewModel = function (el) {
         
        this.url = App.urlBuilder("sprint");
        var context = this;
        this.el = $(el);
        this.item = null;
        
        $("body").bind("delete-contact", function (e) {
            context.item = e.Item;
            context.el.modal('show');
        });

        this.render = function () {
            //this.el.html(require("hbs!views/createItem")()).trigger("rendered");
        }.bind(this);

        this.removeItem = function() {
            $.ajax({
                url: context.url("DeleteSprint"),
                dataType: 'json',
                type: "DELETE",
                data: JSON.stringify({ Id: context.item.Id }),
                accept: 'application/json',
                contentType: 'application/json'
            }).success(function () {
                toastr.success("Deleted");
                context.el.modal('hide');
            }).fail(function () {
                toastr.error("Failed to save record :'(");
            });
        };
    };

    return viewModel;

});