namespace SMART2.Domain
{
    public class EquipmentContract
    {
        public int Id { get; set; }
        public IEnumerable<ProcessEquipment> ProcessEquipment { get; set; }
        public ProductionFacility ProductionFacility { get; set; }
        public int TotalEquipmentUnits { get; set; }
    }
}