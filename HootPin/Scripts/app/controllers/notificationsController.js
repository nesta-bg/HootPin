var NotificationsController = function (notificationService) {

    var init = function () {
        notificationService.getNotificationObject(function (value) {
            getNotifications(value);
        });
    };

    var getNotifications = function (notifications) {
        if (notifications.recentNotifications.length == 0 && notifications.numberOfNewNotifications == 0)
            return;

        if (notifications.numberOfNewNotifications > 0) {
            $(".js-notifications-count")
                .text(notifications.numberOfNewNotifications)
                .removeClass("hide")
                .addClass("animated bounceInDown");
        }

        $(".notifications").popover({
            html: true,
            title: "Notifications",
            content: function () {
                if (notifications.numberOfNewNotifications > 0)
                    notifications.recentNotifications = notifications.recentNotifications.slice(0, notifications.numberOfNewNotifications);

                var compiled = _.template($("#notifications-template").html());
                return compiled({ notifications: notifications.recentNotifications });
            },
            placement: "bottom",
            template: '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        }).on("shown.bs.popover", function () {
            if ($(".js-notifications-count").hasClass("animated bounceInDown")) {
                notificationService.markAsRead(done);
            }
        });
    };

    var done = function () {
        $(".js-notifications-count")
        .text("")
        .addClass("hide");
    };

    return {
       init: init
    } /* Revealing Module Pattern */

}(NotificationService); /* Immediately Invoked Function Expression (IIFE) */

