using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMART2.Domain;
using System.Configuration;
using System.Linq.Expressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        List<ProductionFacility> productionFacilityList = new List<ProductionFacility>();
        List<ProcessEquipment> processEquipmentList = new List<ProcessEquipment>();
        List<EquipmentContract> equipmentContractsList = new List<EquipmentContract>();

        Random random = new Random();

        var config = new ConfigurationBuilder()
                    .AddUserSecrets<Program>()
                    .Build();

        var options = new DbContextOptionsBuilder<DomainDbContext>()
            .UseSqlServer(config.GetConnectionString("SMART2Database"));


        using (DomainDbContext _context = new DomainDbContext(options.Options))
        {
            for (int i = 0; i < 200; i++)
            {
                productionFacilityList.Add(new ProductionFacility()
                {
                    Name = $"Room {i}",
                    Code = Guid.NewGuid().ToString(),
                    Occupied = random.NextDouble() >= 0.5,
                    StandardArea = random.NextDouble() * 100,
                });
            }
            for (int i = 0; i < 10000; i++)
            {
                processEquipmentList.Add(new ProcessEquipment()
                {
                    Area = random.NextDouble() * 50,
                    Code = Guid.NewGuid().ToString(),
                    Name = $"Process equipment {i}",
                });
            }

            await _context.ProductionFacilities.AddRangeAsync(productionFacilityList);
            await _context.ProcessEquipments.AddRangeAsync(processEquipmentList);
            await _context.SaveChangesAsync();

            List<ProductionFacility> listOfProductionFacilities = await _context.ProductionFacilities.Where(a => a.Occupied).ToListAsync();
            int listOfProductionFacilitiesInContracts = listOfProductionFacilities.Count();
            List<ProcessEquipment> listOfProcessEquipment = await _context.ProcessEquipments.ToListAsync();
            for (int i = 0; i < listOfProductionFacilitiesInContracts - 1; i++)
            {
                List<ProcessEquipment> listOfProcessEquipmentInContract = new List<ProcessEquipment>();
                for (int j = 0; j < random.Next(1, 6); j++)
                {
                    int randomEuipmentId = random.Next(1, listOfProcessEquipment.Count);
                    listOfProcessEquipmentInContract.Add(listOfProcessEquipment.Skip(randomEuipmentId).Take(1).First());
                }
                equipmentContractsList.Add(new EquipmentContract()
                {
                    ProductionFacilities = listOfProductionFacilities,
                    ProcessEquipments = listOfProcessEquipmentInContract,
                    TotalEquipmentUnits = listOfProcessEquipmentInContract.Count()
                });

            }
            await _context.EquipmentContracts.AddRangeAsync(equipmentContractsList);
            await _context.SaveChangesAsync();
        }
    }
}