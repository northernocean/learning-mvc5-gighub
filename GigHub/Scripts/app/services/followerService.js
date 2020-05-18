let FollowerService = function () {

    let addFollower = function (id, success, error) {
        console.log("in addfollower");
        $.ajax({
            url: "/api/followers/" + id,
            method: "GET",
            success: success,
            error: error
        });
    };

    let removeFollower = function (id, success, error) {
        console.log("in removefollower");
        $.ajax({
            url: "/api/followers/" + id,
            method: "DELETE",
            success: success,
            error: error
        });
    };

    return {
        addFollower: addFollower,
        removeFollower: removeFollower
    }

}();





