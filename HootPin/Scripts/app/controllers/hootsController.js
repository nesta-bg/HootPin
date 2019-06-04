var HootsController = function (attendanceService) {

    var button;

    var init = function (container) {
        //$(".js-toggle-attendance").click(toggleAttendance);
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var hootId = button.attr("data-hoot-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(hootId, done, fail);
        else
            attendanceService.deleteAttendance(hootId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";        
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something Failed!");
    };

    return {
        init: init
    } /* Revealing Module Pattern */

}(AttendanceService); /* Immediately Invoked Function Expression (IIFE) */ 