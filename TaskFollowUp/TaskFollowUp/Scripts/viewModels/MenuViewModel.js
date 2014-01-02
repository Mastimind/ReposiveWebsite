define(['knockout', 'Sammy'], function(ko, Sammy) {

    return function (el) {

        this.el = $(el);

        this.enterRegistration = function() {
            this.el.trigger("new-registration");
        }.bind(this);
        
    };

});