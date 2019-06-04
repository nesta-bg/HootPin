var AttendanceService = function () {

    var createAttendance = function (hootId, done, fail) {
        $.post("/api/attendances", { hootId: hootId })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function (hootId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + hootId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}(); 