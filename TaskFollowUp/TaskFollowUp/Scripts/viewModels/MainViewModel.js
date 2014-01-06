/*jshint browser:true*/
/// <reference path="../knockout-2.1.0.debug.js" />

define(['knockout', 'Sammy', 'moment', 'toastr', 'gfx/effects', 'jquery'], function (ko, Sammy, moment) {


    var itemSpan = function () {
        var startDate = moment(this.StartDate).year(moment().year());
        var endDate = moment(this.EndDate).year(moment().year());
        var isCurrentSprint = endDate > moment() && moment() >= startDate;
        return isCurrentSprint;
    };

    var updateDisplay = function () {
        $("#source").queueNext(function () {
            $("#source").quicksand($("#destination li"), {
                duration: 800,
                easing: 'easeInOutQuad',
                adjustHeight: false,
                useScaling: false
            }, function () {
            });
        });
    };

    var sortByTitle = function (x, y) {
        return x.Title.localeCompare(y.Title);
    };

    var sortByStartDate = function (x, y) {
        var a = moment(x.StartDate).year(moment().year());
        var b = moment(y.StartDate).year(moment().year());
        var retval = 0;
        if (a > b) retval = 1;
        else if (a < b) retval = -1;
        return retval;
    };

    var sortByEndDate = function (x, y) {
        var a = moment(x.EndDate).year(moment().year());
        var b = moment(y.EndDate).year(moment().year());
        var retval = 0;
        if (a > b) retval = 1;
        else if (a < b) retval = -1;
        return retval;
    };

    var updateModel = function (model) {
        $.extend(model, { Title: model.Title, IsCurrentSprint: itemSpan });
    };

    ////+ "  " + moment(model.StartDate).format('MMM YYYY')
    return function (el) {
        var context = this;
        this.el = $(el);
        this.contactsView = this.el.find("#view-contacts");
        this.exportView = this.el.find("#export-contacts");

        this.url = App.urlBuilder("sprint");

        this.title = ko.observable("Sprints");
        this.subTitle = ko.observable("List");
        this.items = ko.observableArray([]);
        this.allItems = [];
        this.selectedItem = ko.observable(null);
        this.sortBy = ko.observable(null);
        this.searchText = ko.observable(null);
        this.forExport = ko.observableArray([]);
        this._updateUIVersion = 0;

        $("body").bind("delete-contact-confirmed", function (e) {
            var index = ko.utils.arrayIndexOf(context.selectedItem().Items, e.item);
            var indexAll = ko.utils.arrayIndexOf(context.allItems, e.item);
            if (index >= 0) {
                context.selectedItem().Items.splice(index, 1);
                context.selectedItem().ComputedList.remove(e);
            }
            if (indexAll > 0) {
                context.allItems.splice(indexAll, 1);
            }
            updateDisplay();
        });
        $("body").bind("update-contact", function (e) {
            updateModel(e.item, context);
            var oldItem = e.oldItem;
            var indexAll = ko.utils.arrayIndexOf(context.allItems, oldItem || {});

            $.each(context.items(), function (i, type) {
                var index = ko.utils.arrayIndexOf(type.Items, e.oldItem || {});
                if (index > -1) {
                    type.Items[index] = e.item;
                    context.selectedItem(type);
                } else if (type.Id == moment(e.item.Id)) {
                    type.Items.push(e.item);
                    context.allItems.push(e.item);
                    type.ComputedList(type.Items.slice());
                    context.selectedItem(type);
                }
            });

            if (indexAll > -1) {
                ko.mapping.fromJS(e.item, {}, context.allItems[indexAll]);
                context.allItems[indexAll] = e.item;
            }
            $("#source li.flipped[data-id='" + e.item.Id + "']").trigger("flip");
        });
        $("body").bind("update-by-id", function (e) {
            var oldItem = null;
            $.each(context.allItems, function () {
                if (this.Id === e.item.Id) {
                    oldItem = this;
                    return;
                }
            });
            context.el.trigger({ type: e.callback, oldItem: oldItem, item: e.item });
        });

        this.selectedItemSort = ko.computed(function () {
            var ar = context.selectedItem();
            var s = context.sortBy();
            var search = context.searchText();

            if (typeof ar == 'undefined' || ar == null) return null;

            var updateDelegate = function () {
                ar.ComputedList = ar.ComputedList || ko.observableArray(ar.Items.slice());
                ar.ComputedList.removeAll();

                $.each(ar.Items, function (i, type) {
                    if (search != null && search.length > 0) {
                        if (type.Title.toLowerCase().indexOf(search.toLowerCase()) > -1) {
                            ar.ComputedList.push(type);
                        }
                    } else {
                        ar.ComputedList.push(type);
                    }
                });

                var sortby = (s == "title" ? sortByTitle : (s == "startdate" ? sortByStartDate : sortByEndDate));
                ar.ComputedList.sort(sortby);
                updateDisplay();
            };

            context._updateUIVersion++;

            var thisVersion = context._updateUIVersion;
            setTimeout(function () {
                if (thisVersion == context._updateUIVersion) {
                    updateDelegate.call(context);
                }
            }, 300);

            return ar;

        }, this);


        this.deleteClicked = function (data, event) {
            event.preventDefault();
            context.el.trigger({ type: "delete-contact", item: data });
        }.bind(this);

        this.editClicked = function (data, event) {
            event.preventDefault();
            context.el.trigger({ type: "edit-registration", item: data });
        }.bind(this);


        this.showPoP = function (data) {
            var trackingChart = new Highcharts.Chart({
                chart: {
                    renderTo: 'popup_chart',
                    defaultSeriesType: 'line',
                    margin: [30, 40, 60, 30],
                    height: 400,
                    width: 950,
                    zoomType: 'x'
                },

                title: {
                    text: data.Title + ' Burndown Chart',
                    style: {
                        margin: '10px 100px 0 0' // center it
                    }
                },
                xAxis: {
                    title: {
                        text: moment(data.StartDate).format('MMM YYYY')
                    },
                    categories: data.categories
                },
                yAxis: {
                    title: {
                        text: 'Remaining Work (Hours)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }],
                    min: 0,

                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: 0,
                    y: 100
                },

                tooltip: {
                    formatter: function () {
                        return 'Remaining work ' +
                            this.y + '(hrs) as of ' + this.x + ' ' + moment(data.StartDate).format('MMM YYYY');
                    }
                },

                series: [{
                    data: data.data
                }]
            });

        }

        this.loadData = function () {

            var loaded = $.Deferred();
            $.ajax({
                url: context.url("Get"),
                dataType: 'json',
                type: "GET",
                accept: 'application/json',
                contentType: 'application/json'
            }).success(function (data) {

                context.items.removeAll();

                $.each(data, function (i, type) {
                    $.each(type.Items, function () {
                        updateModel(this, context);
                        context.allItems.push(this);
                    });
                    type.ComputedList = type.ComputedList || ko.observableArray(type.Items.slice());
                    context.items.push(type);
                });
                loaded.resolve();
            }).fail(function () { loaded.reject(); });
            return loaded.promise();
        };

        this.populateItems = function (title) {
            $.each(context.items(), function (i, type) {
                if (type.Title == title) {
                    context.selectedItem(type);
                }
            });
            updateDisplay();
        }.bind(this);

        this.routes = function () {
            return {
                "#/": function () {
                    context.loadData();
                    context.selectedItem(null);
                },
                "#/items/:title": function (s) {
                    context.forExport.removeAll();

                    if (context.items().length == 0) {
                        context.loadData().done(function () { context.populateItems(s.params.title); });
                    }
                    else {
                        context.contactsView
                            .queueNext(function () {
                                if (context.exportView.is(":visible")) {
                                    context.exportView.gfxRaisedOut().hide();
                                }
                            })
                            .queueNext(function () {
                                if (context.contactsView.is(":visible") == false) {
                                    context.contactsView.gfxRaisedIn();
                                    context.contactsView.removeAttr("style");
                                }
                            }).queueNext(function () {
                                context.populateItems(s.params.title);
                            });
                    }

                }
            };
        };
    };

});