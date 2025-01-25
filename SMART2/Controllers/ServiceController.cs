using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART2.Domain;

namespace SMART2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly DomainDbContext _context;

        public ServiceController(DomainDbContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentContract>>> GetEquipmentContracts()
        {
            return await _context.EquipmentContracts
                .Include(a => a.ProcessEquipments)
                .Include(b => b.ProductionFacilities)
                .ToListAsync();
        }

        // GET: api/Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentContract>> GetEquipmentContract(int id)
        {
            var equipmentContract = await _context.EquipmentContracts
                .Where(a=>a.Id == id)
                .Include(a => a.ProcessEquipments)
                .Include(b => b.ProductionFacilities)
                .FirstOrDefaultAsync();

            if (equipmentContract == null)
            {
                return NotFound();
            }

            return equipmentContract;
        }

        [HttpGet("{productionFacilityCode}/{processEquipmentCode}/{equipmmentQuantity}")]
        public async Task<ActionResult<EquipmentContract>> GetEquipmentContract(string productionFacilityCode, string processEquipmentCode, string equipmentQuantity)
        {
            int equipmentQuantityInt;
            if (!int.TryParse(equipmentQuantity, out equipmentQuantityInt))
            {
                return BadRequest();
            }

            var equipmentContractList = await _context.EquipmentContracts.Where(c => c.TotalEquipmentUnits == equipmentQuantityInt)
                .Include(a => a.ProcessEquipments.Where(a => a.Code == processEquipmentCode))
                .Include(b => b.ProductionFacilities.Where(a => a.Code == productionFacilityCode))
                .ToListAsync();


            if (equipmentContractList.Count() == 0)
            {
                return NotFound();
            }
            else if (equipmentContractList.Count() > 1) 
            {
                string ids = "";
                foreach(var equipmentContract in equipmentContractList)
                {
                    ids += " " + equipmentContract.Id.ToString();
                }
                return BadRequest("Method returned more than one result - refine by contract id:" + ids);
            }

            return equipmentContractList.FirstOrDefault()!;
        }



        // PUT: api/Service/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentContract(int id, EquipmentContract equipmentContract)
        {
            if (id != equipmentContract.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipmentContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentContractExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Service
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ByModel")]
        public async Task<ActionResult<EquipmentContract>> PostEquipmentContract(EquipmentContract equipmentContract)
        {
            _context.EquipmentContracts.Add(equipmentContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentContract", new { id = equipmentContract.Id }, equipmentContract);
        }

        [HttpPost("ByParameters")]
        public async Task<ActionResult<EquipmentContract>> PostEquipmentContract(string productionFacilityName, string processEquipmentCode, int equipmentQuantity)
        {
            IEnumerable<ProcessEquipment> processEquipments = new List<ProcessEquipment>();

            var processEquipment = _context.ProcessEquipments.Where(a => a.Code == processEquipmentCode);

            if (!processEquipment.Any())
            {
                return BadRequest("Try to create process equipment first");
            }
            else if (processEquipment.Count() > 1)
            {
                return BadRequest("There are more than one process euqipment in database of this type allready - please contact adninistrator");
            }
            var equipment = processEquipment.First();

            for (int i = 0; i < equipmentQuantity; i++)
            {
                processEquipments.Append(equipment);
            }


            if (!_context.ProductionFacilities.Where(a => !a.Occupied).Any())
            {
                return BadRequest("Thera are no unocupied facilities");
            }

            var productionFacilities = await _context.ProductionFacilities.Where(a => !a.Occupied).ToListAsync();
            
            
           
            return await AddContractAsync(productionFacilities, processEquipments);
        }

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentContract(int id)
        {
            var equipmentContract = await _context.EquipmentContracts.FindAsync(id);
            if (equipmentContract == null)
            {
                return NotFound();
            }

            _context.EquipmentContracts.Remove(equipmentContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaAvailable(IEnumerable<ProcessEquipment> processEquipment, IEnumerable<ProductionFacility> productionFacilities)
        {
            var processEquipmentTotalArea = processEquipment.Sum(a=>a.Area);
            var productionFacilitiesTotalArea = productionFacilities.Sum(a=>a.StandardArea);
            return productionFacilitiesTotalArea > processEquipmentTotalArea;
        }

        private async Task<ActionResult<EquipmentContract>> AddContractAsync(IEnumerable<ProductionFacility> productionFacilities, IEnumerable<ProcessEquipment> processEquipments)
        {
            IEnumerable<ProductionFacility> result = new List<ProductionFacility>();

            if (!AreaAvailable(processEquipments, productionFacilities))
            {
                return BadRequest("There is not enough space");
            }

            var largestProductionFacilityStandardArea = productionFacilities.OrderBy(a => a.StandardArea).Last().StandardArea;
            var largestProcessEquipmentArea = processEquipments.OrderBy(a => a.Area).Last().Area;
            if (largestProductionFacilityStandardArea < largestProcessEquipmentArea)
            {
                return BadRequest("The largest facility is smaller than the largest process equipment");
            }

            foreach (var processEquipment in processEquipments)
            {
                ProductionFacility pick;
                try
                {
                    pick = productionFacilities.Where(a => a.StandardArea > processEquipment.Area).OrderBy(a => a.StandardArea).First();
                    pick.Occupied = true;
                }
                catch (ArgumentNullException ex)
                {
                    return BadRequest(ex.Message + "There is no space for particular process equipment");
                }                
                result.Append(pick);
            }

            var contract = new EquipmentContract();
            contract.ProductionFacilities = productionFacilities;
            contract.ProcessEquipments = processEquipments;
            contract.TotalEquipmentUnits = processEquipments.Count();

            await _context.AddRangeAsync(contract);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEquipmentContract", new { id = contract.Id }, contract);
        }

        private bool EquipmentContractExists(int id)
        {
            return _context.EquipmentContracts.Any(e => e.Id == id);
        }
    }
}
