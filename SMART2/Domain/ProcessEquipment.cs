namespace SMART2.Domain
{
    public class ProcessEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public string Code { get; set; }
        public IEnumerable<EquipmentContract> EquipmentContracts { get; set; }

    }
}
