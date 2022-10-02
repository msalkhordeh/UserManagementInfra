using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Tests.Mock;
using Xunit;

namespace UM.Tests.Repository
{
    public class AddressRepositoryTests
    {
        private IAddressRepository _addressRepository;
        private DbFactory _dbFactory = new DbFactory();

        public AddressRepositoryTests()
        {
            _addressRepository = new AddressRepository(_dbFactory.Context);
        }

        [Fact]
        public async void Add_SuccessfullyAddedAddress()
        {
            //Arrenge 
            var address = new Address
            {
                FullAddress = "TestFullAddress" ,
                City = "TestCity" ,
                Country = "TestCountry" 
            };

            //Act
            var result = await _addressRepository.Add(address);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
        }

        private List<Address> GenerateAddress(int count = 10)
        {
            List<Address> addresses = new();
            for (int i = 0; i < count; i++)
            {
                addresses.Add(new Address
                {
                    FullAddress = "TestFullAddress" + i,
                    City= "TestCity" + i,
                    Country= "TestCountry" + i
                });
            }
            _dbFactory.Context.AddRange(addresses);
            _dbFactory.Context.SaveChanges();
            return _dbFactory.Context.Addresses.ToList();
        }
    }
}
