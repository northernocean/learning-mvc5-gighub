let gigsController = function () {

    var init = function () {
        $(".js-toggle-attendance").click(function (e) {
            var button = e.target;
            if ($(button).hasClass("btn-secondary")) {
                console.log("toggling to attending")
                $.ajax({
                    url: "/api/attendances/" + button.getAttribute("data-gig-id"),
                    method: "GET",
                    success: function () {
                        $(button).removeClass("btn-secondary")
                            .addClass("btn-info")
                            .text("Attending");
                    },
                    error: function () {
                        alert("An error occurred.");
                    }
                });
            }
            else if ($(button).hasClass("btn-info")) {
                console.log("toggling to not attending")
                $.ajax({
                    url: "/api/attendances/" + button.getAttribute("data-gig-id"),
                    method: "DELETE",
                    success: function () {
                        $(button).removeClass("btn-info")
                            .addClass("btn-secondary")
                            .text("Attend");
                    },
                    error: function () {
                        alert("An error occurred.");
                    }
                });
            }
            else {
                Console.log("Error resolving attendance status for toggling attendance.")
            }
        });
    };

    return {
        init: init
    };

}();


