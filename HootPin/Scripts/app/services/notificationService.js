var NotificationService = function () {

    var getNotificationObject = function (callback) {
        $.getJSON("/api/notifications", function (notification) {
            callback(notification);
        });
    };

    var markAsRead = function (done) {
         $.post("/api/notifications/markAsRead")
                    .done(done);
    }; 

    return {
        getNotificationObject: getNotificationObject,
        markAsRead: markAsRead
    }
}(); 