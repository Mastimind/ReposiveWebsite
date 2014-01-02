define(['knockout'], function(ko) {

    return function (el) {
        this.el = $(el);
        
        this.items = ko.observableArray([]);
        this.selectedItem = ko.observable();
       
    };

});