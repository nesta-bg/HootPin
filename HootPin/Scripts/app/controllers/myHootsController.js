var MyHootsController = function (hootService) {

    var link;

    var init = function (container) {
        $(container).on("click", ".js-cancel-hoot", cancel);
    };

    var cancel = function (e) {
        link = $(e.target);
        var hootId = link.attr("data-hoot-id");

        bootbox.dialog({
            title: "Confirm",
            message: "Are you sure you want to cancel this hoot?",
            buttons: {
                no: {
                    label: "No",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className: "btn-danger",
                    callback: function () {
                        hootService.deleteHoot(hootId, done, fail);
                    }
                }
            }
        });
    };

    var done = function () {
        link.parents("li").fadeOut(function () {
            $(this).remove();
        });
    };

    var fail = function () {
        alert("Something Failed!");
    };

    return {
        init: init
    } /* Revealing Module Pattern */

}(HootService); /* Immediately Invoked Function Expression (IIFE) */ 