using SMART2.Domain;

namespace SMART2.UnitTest
{
    public class ServiceControllerTests
    {
        private readonly List<ProcessEquipment> _processEquipmentList;
        private readonly List<ProductionFacility> _productionFacilityList;
        private readonly List<EquipmentContract> _equipmentContractList;
        public ServiceControllerTests()
        {
            _processEquipmentList = new List<ProcessEquipment>();
            _productionFacilityList = new List<ProductionFacility>();
            _equipmentContractList = new List<EquipmentContract>();

            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "Lorem ipsum",
                Code = "swhdfuoirhgfweorgh",
                Area = 100
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "lwsbnhjbfnv",
                Code = "owehfgwegro",
                Area = 120,
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = "lwnfgwngfwerg",
                Code = "wefjrwejwigtj",
                Area = 12
            });
            _processEquipmentList.Add(new ProcessEquipment()
            {
                Name = " sdfgrg rgerg grrgreg rr  ",
                Code = "wpfjwpgrfjprweg",
                Area = 24
            });

            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 1,
                    Name = "Room 1",
                    StandardArea = 10.5,
                    Code = "AAABBBCCC1234567890",
                    Occupied = true,
                });
            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 2,
                    Name = "Room 2",
                    StandardArea = 12.31,
                    Code = "BBBBBBB12",
                    Occupied = false
                });
            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 3,
                    Name = "Room 3",
                    StandardArea = 14.2,
                    Code = "1234567890",
                    Occupied = true,
                });
            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 4,
                    Name = "Room 4",
                    StandardArea = 200,
                    Code = "Lsingdlwnbgkj-12",
                    Occupied = false
                });




            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 5,
                    Name = "Room 5",
                    StandardArea = 200,
                    Code = "ABC  123456",
                    Occupied = false
                });
            _productionFacilityList.Add(
                new ProductionFacility()
                {
                    Id = 6,
                    Name = "Room 6",
                    StandardArea = 201,
                    Code = "ABC 123456",
                    Occupied = false
                });
        }

        [Fact]
        public void GetEquipmentContracts_ReturnsAllContracts()
        {

        }
    }
}