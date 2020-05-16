let AttendanceService = function () {

    console.log("Attendance Service Created");

    let createAttendance = function (gigId, success, error) {
        console.log("createing attenance" + gigId);
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "GET",
            success: success,
            error: error
        });
    };

    let deleteAttendance = function (gigId, success, error) {
        console.log("removing attenance" + gigId);
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE",
            success: success,
            error: error 
        });
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    };

}();
