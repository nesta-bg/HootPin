var FolloweesController = function (followingService) {

    var unfollowButton;

    var init = function () {
        $(".js-unfollow").click(unfollow);
    };

    var unfollow = function (e) {
        unfollowButton = $(e.target);
        var followeeId = unfollowButton.attr("data-user-id");

        followingService.deleteFollowing(followeeId, done, fail);
    };

    var done = function () {
        unfollowButton.parents("li").remove();
    };

    var fail = function () {
        alert("Something Failed!");
    };

    return {
        init: init
    } /* Revealing Module Pattern */

}(FollowingService); /* Immediately Invoked Function Expression (IIFE) */ 