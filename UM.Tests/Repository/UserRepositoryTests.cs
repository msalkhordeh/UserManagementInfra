using System.Collections.Generic;
using System.Linq;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Tests.Mock;
using Xunit;

namespace UM.Tests.Repository
{
    public class UserRepositoryTests
    {
        private IUserRepository _userRepository;
        private DbFactory _dbFactory = new DbFactory();

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository(_dbFactory.Context);
        }

        [Fact]
        public async void Add_SuccessfullyAddedUser()
        {
            //Arrenge 
            var user = new User
            {
                Username = "TestUsername",
                Email = "TestEmail"
            };

            //Act
            var result = await _userRepository.Add(user);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(100)]
        [InlineData(120)]
        public async void GetAll_SuccessfullyGetAllUsersInTable(int count)
        {
            //Arrenge 
            GenerateUser(count);

            //Act
            var result = await _userRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public void Update_ShouldUpdateUserParameters()
        {
            //Arrenge 
            var user = GenerateUser()[5];
            var username = user.Username;
            user.Username = "Changed";

            //Act
            var result = _userRepository.Update(user);

            //Assert
            Assert.NotEqual(username, result.Username);
            Assert.True(username != result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async void Delete_ShouldDeleteUser()
        {
            //Arrenge 
            var user = GenerateUser()[0];

            //Act
            _userRepository.Delete(user);
            _dbFactory.Context.SaveChanges();
            var afterDeleteUser = await _userRepository.GetById(user.Id);

            //Assert
            Assert.Null(afterDeleteUser);
        }

        private List<User> GenerateUser(int count = 10)
        {
            List<User> users = new();
            for (int i = 0; i < count; i++)
            {
                users.Add(new User
                {
                    Username = "TestUsername" + i,
                    Email = "TestEmail" + i
                });
            }
            _dbFactory.Context.AddRange(users);
            _dbFactory.Context.SaveChanges();
            return _dbFactory.Context.Users.ToList();
        }
    }
}
