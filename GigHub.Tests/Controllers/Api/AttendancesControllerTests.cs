using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {

        private AttendancesController _controller;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IAttendanceRepository> _attendanceRepository;
        private string _userId;

        public AttendancesControllerTests()
        {
            _attendanceRepository = new Mock<IAttendanceRepository>();

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.SetupGet(a => a.Attendances).Returns(_attendanceRepository.Object);

            _userId = "1";
            _controller = new AttendancesController(_unitOfWork.Object);
            _controller.MockCurrentUser(_userId, "user@example.com");
        }


        [TestMethod]
        public void Attend_UserIsAlreadyAttending_ReturnsBadRequest()
        {
            var attendance = new Attendance();
            _attendanceRepository.Setup(r => r.IsAttending(1, _userId)).Returns(true);

            var result = _controller.Attend(1);

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ReturnsOk()
        {
            _attendanceRepository.Setup(r => r.IsAttending(1, _userId)).Returns(false);
            var result = _controller.Attend(1);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Cancel_UserIsNotAttending_ReturnsBadRequest()
        {
            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<BadRequestResult>();

        }

        [TestMethod]
        public void Cancel_ValidRequest_ReturnsOk()
        {
            var attendance = new Attendance();
            _attendanceRepository.Setup(r => r.IsAttending(1, _userId)).Returns(true);

            var result = _controller.CancelAttendance(1);

        }

        [TestMethod]
        public void Cancel_ValidRequest_ReturnsInt()
        {
            var attendance = new Attendance();
            _attendanceRepository.Setup(r => r.IsAttending(1, _userId)).Returns(true);
            _attendanceRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);


            var result = (OkNegotiatedContentResult<int>)_controller.CancelAttendance(1);

            result.Content.Should().Be(1);
        }

    }
}
