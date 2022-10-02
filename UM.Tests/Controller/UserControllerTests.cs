using Microsoft.AspNetCore.Mvc;
using Moq;
using UM.REST.Areas.Identity;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.User.Request;
using UM.Sdk.Model.Identity.User.Response;
using UM.ServiceProvider.Service.Identity;
using Xunit;

namespace UM.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService = new();
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userController = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async void UpdateAsync_ReturnStatus200OK_WhenUserUpdated()
        {
            //Arrenge
            var request = new UpdateRequest
            {
                UserId = 1,
                Email = "TestEmail",
                Age = 20,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = Sdk.Enum.Gender.Female,
                NationalCode = "TestNationalCode",
                Username = "Username"
            };

            var response = new UpdateResponse { UserId = request.UserId };
            _mockUserService.Setup(x => x.UpdateAsync(request, default))
                .ReturnsAsync(response);

            //Act
            var result = await _userController.UpdateAsync(request, default);

            //Assert
            _mockUserService.Verify(x => x.UpdateAsync(request, default), Times.Once);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var updateResponse = Assert.IsType<UpdateResponse>(okresult.Value);
            Assert.Equal(RequestResult.SuccessfullCompleted, updateResponse.ResultCode);
            Assert.Equal(BaseResponseCollection.GetMessage(RequestResult.SuccessfullCompleted),
                updateResponse.Message);
            Assert.Equal(request.UserId, updateResponse.UserId);
        }
        
        [Fact]
        public async void UpdateAsync_ReturnStatus404NotFound_WhenInvalidUserId()
        {
            //Arrenge
            //var request = new UpdateRequest
            //{
            //    UserId = -1
            //};

            var response = BaseResponseCollection
                .GetBaseResponse<UpdateResponse>(RequestResult.InvalidUserId);
            _mockUserService.Setup(x => x.UpdateAsync(It.IsAny<UpdateRequest>(), default))
                .ReturnsAsync(response);

            //Act
            var result = await _userController.UpdateAsync(new UpdateRequest(), default);

            //Assert
            _mockUserService.Verify(x => x.UpdateAsync(It.IsAny<UpdateRequest>(), default), Times.Once);

            var badResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var baseResponse = Assert.IsType<BaseResponse>(badResult.Value);
            Assert.Equal(RequestResult.InvalidUserId, baseResponse.ResultCode);
            Assert.Equal(BaseResponseCollection.GetMessage(RequestResult.InvalidUserId),
                baseResponse.Message);
        }
        
        [Fact]
        public async void UpdateAsync_ReturnStatus500InternalServerError_WhenThrowException()
        {
            //Arrenge
            var response = BaseResponseCollection
                .GetBaseResponse(RequestResult.UnhandledExceptionError);
            _mockUserService.Setup(x => x.UpdateAsync(It.IsAny<UpdateRequest>(), default))
                .ThrowsAsync(new System.Exception("Kaboom!"));

            //Act
            var result = await _userController.UpdateAsync(new UpdateRequest(), default);

            //Assert
            _mockUserService.Verify(x => x.UpdateAsync(It.IsAny<UpdateRequest>(), default), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            var baseResponse = Assert.IsType<BaseResponse>(objectResult.Value);
            Assert.Equal(RequestResult.UnhandledExceptionError, baseResponse.ResultCode);
            Assert.Equal(BaseResponseCollection.GetMessage(RequestResult.UnhandledExceptionError),
                baseResponse.Message);
        }

        //[Fact]
        //public async void UpdateAsync_ReturnStatus404_WhenUserUpdated()
        //{
        //    //Arrenge
        //    var request = new UpdateRequest
        //    {
        //        UserId = 1,
        //        Email = "TestEmail",
        //        Age = 20,
        //        FirstName = "TestFirstName",
        //        LastName = "TestLastName",
        //        Gender = Sdk.Enum.Gender.Female,
        //        NationalCode = "TestNationalCode",
        //        Username = "Username"
        //    };

        //    var response = new UpdateResponse { UserId = request.UserId };
        //    _mockUserService.Setup(x => x.UpdateAsync(request, default))
        //        .ReturnsAsync(response);

        //    //Act
        //    var result = await _userController.UpdateAsync(request, default);

        //    //Assert
        //    _mockUserService.Verify(x => x.UpdateAsync(request, default), Times.Once);

        //    var okresult = Assert.IsType<OkObjectResult>(result.Result);
        //    var updateResponse = Assert.IsType<UpdateResponse>(okresult.Value);
        //    Assert.Equal(RequestResult.SuccessfullCompleted, updateResponse.ResultCode);
        //    Assert.Equal(BaseResponseCollection.GetMessage(RequestResult.SuccessfullCompleted),
        //        updateResponse.Message);
        //    Assert.Equal(request.UserId, updateResponse.UserId);
        //}
    }
}
