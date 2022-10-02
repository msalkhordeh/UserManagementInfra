using AutoMapper;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UM.DataAccess.Repository.Identity;
using UM.Sdk.Model.Identity.User.Request;
using UM.ServiceProvider;
using UM.ServiceProvider.Service.Identity;
using Xunit;

namespace UM.Tests.Service
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
        private readonly Mock<IUserRepository> _mockUserRepository = new();
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            var config = new MapperConfiguration(op =>
            op.AddMaps(Assembly.GetAssembly(typeof(REST.Program))));
            //op.AddMaps(Assembly.GetAssembly(typeof(REST.Infrastructure.DomainProfile.UserDomainProfle))));
            _mapper = config.CreateMapper();

            _userService = new UserService(_mapper,
                _mockUserRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async void AddAsync_ShouldReturnUserID()
        {
            //Arrenge
            var request = new AddRequest
            {
                Email = "TestEmail",
                Age = 20,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = Sdk.Enum.Gender.Female,
                NationalCode = "TestNationalCode",
                Username = "TestUsername"
            };
            var user = _mapper.Map<DataAccess.Entity.Identity.User>(request);
            _mockUserRepository
                .Setup(x => x.Add(It.Is<DataAccess.Entity.Identity.User>(u => u.Id == user.Id), default))
                .ReturnsAsync(new DataAccess.Entity.Identity.User { Id = 1 });
            _mockUnitOfWork.Setup(x => x.CompleteAsync(default))
                .ReturnsAsync(1);

            //Act
            var result = await _userService.AddAsync(request, default);

            //Assert
            _mockUserRepository.Verify(x => x.Add(It.IsAny<DataAccess.Entity.Identity.User>(),
                It.IsAny<CancellationToken>()), Times.Once);
            //_mockUserRepository.Verify(x => x.Add(
            //    It.Is<DataAccess.Entity.Identity.User>(
            //    u => u.LastName == "TestLastName20"), default)
            //    , Times.Exactly(10));

            _mockUnitOfWork.Verify(x => x.CompleteAsync(default)
               , Times.Once);

            Assert.IsType<int>(result);
            Assert.True(result > 0);
        }

        [Fact]
        public async void UpdateAsync_SuccessfullyUpdateExistingUser_WhenUserIsNotNull()
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
            var user = new DataAccess.Entity.Identity.User { Id = 1, Username = "TestUsername" };
            _mockUserRepository.Setup(x => x.GetById(request.UserId, default))
                .ReturnsAsync(user);

            _mockUserRepository.Setup(x => x.Update(user)).Returns(user);
            _mockUnitOfWork.Setup(x => x.CompleteAsync(default))
               .ReturnsAsync(1);

            //Act
            var result = await _userService.UpdateAsync(request, default);

            //Assert
            _mockUserRepository.Verify(x => x.GetById(request.UserId, default), Times.Once);
            _mockUserRepository.Verify(x => x.Update(It.Is<DataAccess.Entity.Identity.User>(u =>
                u.Id == request.UserId)), Times.Once);
            _mockUnitOfWork.Verify(x => x.CompleteAsync(default), Times.Once);
            Assert.NotNull(result);
            Assert.NotEqual(0, result.UserId);
            Assert.Equal(user.Username, request.Username);
            Assert.Equal(Sdk.Enum.RequestResult.SuccessfullCompleted, result.ResultCode);
        }

        [Fact]
        public async void UpdateAsync_ReturnInvalidUserId_WhenUserIsNull()
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
            _mockUserRepository.Setup(x => x.GetById(It.IsAny<int>(), default))
                .ReturnsAsync(default(DataAccess.Entity.Identity.User));
            _mockUnitOfWork.Setup(x => x.CompleteAsync(default))
               .ReturnsAsync(1);

            //Act
            var result = await _userService.UpdateAsync(request, default);

            //Assert
            _mockUserRepository.Verify(x => x.GetById(request.UserId, default), Times.Once);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<DataAccess.Entity.Identity.User>())
                , Times.Never);
            _mockUnitOfWork.Verify(x => x.CompleteAsync(default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(Sdk.Enum.RequestResult.InvalidUserId, result.ResultCode);
            Assert.Equal(0, result.UserId);
        }

        [Theory]
        [InlineData(2, "test", false)]
        [InlineData(1, "test2", true)]
        //[MemberData(nameof(TestUser))]
        //[ClassData(nameof(TestUser))]
        public async void UpdateAsync_ReturnResponse_WhenTheGivenRequest(int userId, 
            string userName,
            bool isNull)
        {
            var request = new UpdateRequest
            {
                UserId = userId,
                Username = userName
            };
            if (isNull)
            {
                _mockUserRepository.Setup(x => x.GetById(request.UserId, default))
         .ReturnsAsync(default(DataAccess.Entity.Identity.User));


            }
            else
            {
                _mockUserRepository.Setup(x => x.GetById(request.UserId, default))
                .ReturnsAsync(new DataAccess.Entity.Identity.User { Id = request.UserId, Username = request.Username });
                _mockUserRepository.Setup(x => x.Update(It.Is<DataAccess.Entity.Identity.User>
                    (u => u.Id == request.UserId))).Returns(new DataAccess.Entity.Identity.User() { Id = request.UserId, Username = "done" });
            }
            _mockUnitOfWork.Setup(x => x.CompleteAsync(default))
               .ReturnsAsync(1);
            //Act
            var result = await _userService.UpdateAsync(request, default);

            //Assert
            if (isNull)
            {
                _mockUserRepository.Verify(x => x.GetById(request.UserId, default), Times.Once);
                _mockUserRepository.Verify(x => x.Update(It.IsAny<DataAccess.Entity.Identity.User>())
                    , Times.Never);
                _mockUnitOfWork.Verify(x => x.CompleteAsync(default), Times.Once);
                Assert.NotNull(result);
                Assert.Equal(Sdk.Enum.RequestResult.InvalidUserId, result.ResultCode);
                Assert.Equal(0, result.UserId);
            }
            else
            {
                _mockUserRepository.Verify(x => x.GetById(request.UserId, default), Times.Once);
                _mockUserRepository.Verify(x => x.Update(It.IsAny<DataAccess.Entity.Identity.User>())
                    , Times.Once);
                _mockUnitOfWork.Verify(x => x.CompleteAsync(default), Times.Once);
                Assert.NotNull(result);
                Assert.NotEqual(0, result.UserId);
                Assert.NotEqual("done", request.Username);
                Assert.Equal(Sdk.Enum.RequestResult.SuccessfullCompleted, result.ResultCode);
            }
        }

        //public static IEnumerable<object[]> TestUser = new List<object[]>
        //{
        //    new object [] {5,"TestUsername", true},
        //    new object [] {15,"TestUsername1", false},
        //    new object [] {20,"TestUsername2", false},
        //    new object [] {3,"TestUsername3", true},
        //    new object [] {3,"TestUsername3", true},
        //    new object [] {3,"TestUsername3", true},
        //    new object [] {3,"TestUsername3", true},
        //};
    }
}
