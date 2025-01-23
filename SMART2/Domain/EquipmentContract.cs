namespace SMART2.Domain
{
    public class EquipmentContract
    {
        public int Id { get; set; }
        public IEnumerable<ProcessEquipment> ProcessEquipments { get; set; }
        public IEnumerable<ProductionFacility> ProductionFacilities { get; set; }
        public int TotalEquipmentUnits { get; set; }
    }
}