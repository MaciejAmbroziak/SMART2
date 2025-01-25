using Microsoft.EntityFrameworkCore;
using SMART2.Controllers;
using SMART2.Domain;
using Xunit.Sdk;

namespace SMART2.UnitTest
{
    public class ServiceControllerTests
    {
        private readonly List<ProcessEquipment> _processEquipmentList;
        private readonly List<ProductionFacility> _productionFacilityList;
        private readonly List<EquipmentContract> _equipmentContractList;

        public ServiceControllerTests()
        {
            _processEquipmentList = new List<ProcessEquipment>
        {
            new ProcessEquipment { Name = "Lorem ipsum", Code = "swhdfuoirhgfweorgh", Area = 100 },
            new ProcessEquipment { Name = "lwsbnhjbfnv", Code = "owehfgwegro", Area = 120 },
            new ProcessEquipment { Name = "lwnfgwngfwerg", Code = "wefjrwejwigtj", Area = 12 },
            new ProcessEquipment { Name = " sdfgrg rgerg grrgreg rr  ", Code = "wpfjwpgrfjprweg", Area = 24 }
        };

            _productionFacilityList = new List<ProductionFacility>
        {
            new ProductionFacility { Name = "Room 1", StandardArea = 10.5, Code = "AAABBBCCC1234567890", Occupied = true },
            new ProductionFacility { Name = "Room 2", StandardArea = 12.31, Code = "BBBBBBB12", Occupied = true },
            new ProductionFacility { Name = "Room 3", StandardArea = 14.2, Code = "1234567890", Occupied = true },
            new ProductionFacility { Name = "Room 4", StandardArea = 200, Code = "Lsingdlwnbgkj-12", Occupied = true },
            new ProductionFacility { Name = "Room 5", StandardArea = 200, Code = "ABC 123456", Occupied = true },
            new ProductionFacility { Name = "Room 6", StandardArea = 201, Code = "ABC 123456", Occupied = true }
        };

            _equipmentContractList = new List<EquipmentContract>
        {
            new EquipmentContract { ProcessEquipments = _processEquipmentList, ProductionFacilities = _productionFacilityList, TotalEquipmentUnits = _processEquipmentList.Count() },
            new EquipmentContract { ProcessEquipments = _processEquipmentList.Skip(1).ToList(), ProductionFacilities = _productionFacilityList.Skip(1).ToList(), TotalEquipmentUnits = _processEquipmentList.Skip(1).Count() },
            new EquipmentContract { ProcessEquipments = _processEquipmentList.Skip(2).ToList(), ProductionFacilities = _productionFacilityList.Skip(2).ToList(), TotalEquipmentUnits = _processEquipmentList.Skip(2).Count() }
        };
        }

        [Fact]
        public async Task GetEquipmentContracts_DoesNotReturnNull()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContracts();

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        public async Task GetEquipmentContracts_Value_DoesNotReturnNull()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContracts();

            // Assert
            Assert.NotNull(sut.Value);
        }

        [Fact]
        public async Task GetEquipmentContracts_ReturnProperNumberOfContracts()
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContracts();

            // Assert
            Assert.Equal(sut.Value.Count(), _equipmentContractList.Count());
        }

        [Fact]
        public async Task GetEquipmentContracts_ForEachContract_ReturnsTheSameSetbeOfEquipmentContract()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContracts();

            // Assert
            Assert.Equal(sut.Value, _equipmentContractList.AsEnumerable());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetEquipmentContract_DoesNotReturnNull(int id)
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContract(id);

            //Asert
            Assert.NotNull(sut);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetEquipmentContract_Value_DoesNotReturnNull(int id)
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContract(id);

            //Asert
            Assert.NotNull(sut.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetEquipmentContract_IfPRductionFacilityInEquipmentContract_HasValueOcupiedEqualsTrue(int id)
        {
            //Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContract(id);

            // Assert
            Assert.True(sut.Value.ProductionFacilities.All(a=>a.Occupied));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetEquipmentContract_ForEachContract_ReturnsTheSameEquipmentContract(int id)
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DomainDbContext(dbContextOptions);
            context.AddRange(_processEquipmentList);
            context.AddRange(_productionFacilityList);
            context.AddRange(_equipmentContractList);
            context.SaveChanges();
            var service = new ServiceController(context);

            // Act
            var sut = await service.GetEquipmentContract(id);

            // Assert
            Assert.Equal(sut.Value, _equipmentContractList[id -1]);
        }
    }
}