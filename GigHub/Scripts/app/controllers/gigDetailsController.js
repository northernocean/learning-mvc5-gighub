let GigDetailsController = function (followerService) {

    let init = function (container) {
        $(container).on("click", ".js-follow-artist", toggleFollowing);
    }

    let button;

    let toggleFollowing = function (e) {
        button = $(e.target);
        if (button.text() === "Follow")
            followerService.addFollower(button.attr("data-artist-id"), toggleButton, alertError);
        else if (button.text() !== "Follow")
            followerService.removeFollower(button.attr("data-artist-id"), toggleButton, alertError);
        else
            alert("Error resolving target for follow/unfollow action.");
    };

    let alertError = function () {
        console.log("An error occurred.")
    };

    let toggleButton = function () {
        button.toggleClass("btn-primary").toggleClass("btn-secondary");
        let text = (button.text() === "Follow") ? "Stop Following" : "Follow";
        button.text(text);
    }

    return { init: init };

}(FollowerService);