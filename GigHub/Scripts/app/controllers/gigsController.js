let GigsController = function (attendanceService) {
    
    let button;

    let init = function (container) {
        console.log("initializing");
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
        console.log($(container));
    };

    let toggleAttendance = function (e) {
        button = $(e.target);
        if (button.hasClass("btn-secondary"))
            attendanceService.createAttendance(button.attr("data-gig-id"), toggleAttendanceButton, alertError);
        else if (button.hasClass("btn-info")) 
            attendanceService.deleteAttendance(button.attr("data-gig-id"), toggleAttendanceButton, alertError);
        else {
            Console.log("Error resolving attendance status for toggling attendance.")
        }
    };

    let toggleAttendanceButton = function () {
        button.toggleClass("btn-info").toggleClass("btn-secondary");
        let text = (button.text() == "Attending") ? "Attend" : "Attending";
        console.log(button.text());
        console.log(text);
        button.text(text);
        console.log(button.text());
    };

    let alertError = function () { alert("An error occurred") };

    return {
        init: init
    };

}(AttendanceService);

