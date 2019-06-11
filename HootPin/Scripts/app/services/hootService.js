var HootService = function () {

    var deleteHoot = function (hootId, done, fail) {
        $.ajax({
            url: "/api/hoots/" + hootId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteHoot: deleteHoot
    }
}();