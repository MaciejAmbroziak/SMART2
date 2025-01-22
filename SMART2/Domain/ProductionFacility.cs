namespace SMART2.Domain
{
    public class ProductionFacility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double StandardArea { get; set; }
        public string Code { get; set; }
        public bool Occupied { get; set; }
    }
}