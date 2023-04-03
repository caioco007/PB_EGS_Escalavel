using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using PB_EGS_Escalavel.API.Controllers;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Application.ViewModels;
using PB_EGS_Escalavel.Core.Entities;
using PB_EGS_Escalavel.Core.Repositories;

namespace PB_EGS_Escalavel.UnitTeste.Application.Services
{
    public class CourseTest
    {
        private readonly Mock<ICourseService> _courseServiceMock;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;

        public CourseTest()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _courseRepositoryMock = new Mock<ICourseRepository>();
        }

        [Fact]
        public async Task InputDataIsOk_Executed_ReturnCourseId()
        {
            // Arrange

            var newCourseInputModel = new NewCourseInputModel
            {
                Title = "Teste title",
                Description = "Test description",
                IdTeacher = 2,
                TotalHours = 8.6M
            };

            var course = new Course
            (
                "Teste title",
                "Test description",
                2,
                8.6M
            );


            //Act            
            _courseServiceMock.Setup(x => x.CreateAsync(It.IsAny<NewCourseInputModel>())).Returns(Task.FromResult(1));

            var controller = new CoursesController(_courseServiceMock.Object);
            var actual = controller.Post(newCourseInputModel);

            //Assert
            Assert.True(actual.Id >= 0);

            _courseRepositoryMock.Verify(us => us.AddAsync(course), Times.Never);
        }

        [Fact]
        public async Task GetDataIdIsOk_Executed_ReturnCourse()
        {
            // Arrange

            var courseViewModel = new CourseViewModel
            (
                1,
                "Teste title",
                DateTime.Now
            );

            var courseDetailsViewModel = new CourseDetailsViewModel
            (
                1,
                "Teste title",
                "Test description",
                8.6M,
                "Teacher FullName"
            );



            //Act            
            _courseServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(courseDetailsViewModel));

            var controller = new CoursesController(_courseServiceMock.Object);
            var actual = controller.GetById(1);
            var actualResult = actual.Result as OkObjectResult;

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);

            _courseRepositoryMock.Verify(us => us.GetByIdAsync(1), Times.Never);
        }

        [Fact]
        public async Task GetDataIsOk_Executed_ReturnCourse()
        {
            // Arrange

            var courseViewModel = new CourseViewModel
            (
                1,
                "Teste title",
                DateTime.Now
            );

            var courseViewModelList = new List<CourseViewModel> { courseViewModel };

            var courseDetailsViewModel = new CourseDetailsViewModel
            (
                1,
                "Teste title",
                "Test description",
                8.6M,
                "Teacher FullName"
            );



            //Act            
            _courseServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).Returns(Task.FromResult(courseViewModelList));

            var controller = new CoursesController(_courseServiceMock.Object);
            var actual = controller.Get("");
            var actualResult = actual.Result as OkObjectResult;

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);

            _courseRepositoryMock.Verify(us => us.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task PutDataIsOk_Executed_ReturnCourse()
        {
            // Arrange

            var courseViewModel = new CourseViewModel
            (
                1,
                "Teste title",
                DateTime.Now
            );

            var courseViewModelList = new List<CourseViewModel> { courseViewModel };

            var courseDetailsViewModel = new CourseDetailsViewModel
            (
                1,
                "Teste title",
                "Test description",
                8.6M,
                "Teacher FullName"
            );

            var updateCourseInputModel = new UpdateCourseInputModel
            {
                Id = 1,
                Title = "Teste title",
                Description = "Test description",
                TotalHours = 8.6M
            };

            //Act            
            _courseServiceMock.Setup(x => x.UpdateAsync(It.IsAny<UpdateCourseInputModel>())).Returns(Task.FromResult(true));

            var controller = new CoursesController(_courseServiceMock.Object);
            var actual = controller.Put(1 , updateCourseInputModel);

            //Assert
            Assert.True(actual.Id >= 0);

            _courseRepositoryMock.Verify(us => us.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddStudentDataIsOk_Executed_ReturnCourseId()
        {
            // Arrange

            var newCourseInputModel = new NewStudentCourseInputModel
            {
                IdCourse = 1,
                IdUser = 2
            };

            var userCourse = new UserCourse
            (
                1,
                2
            );


            //Act            
            _courseServiceMock.Setup(x => x.AddStudentCourseAsync(It.IsAny<NewStudentCourseInputModel>())).Returns(Task.FromResult(true));

            var controller = new CoursesController(_courseServiceMock.Object);
            var actual = controller.AddStudentToCourse(newCourseInputModel);

            //Assert
            Assert.True(actual.Id >= 0);

            _courseRepositoryMock.Verify(us => us.AddStudentToCourseAsync(userCourse), Times.Never);
        }


    }
}
