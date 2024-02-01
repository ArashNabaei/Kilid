namespace Kilid.Entities
{
    public class Building
    {
        public int Id { get; set; }

        public string? Address { get; set; }

        public int Area { get; set; }

        public bool IsRentable { get; set; }

        public bool IsBuyable { get; set; }

        public bool IsFullRentable { get; set; }

        public float Price { get; set; }

        public int RoomsCount { get; set; }

        public int Age { get; set; }

        public string Photo { get; set; }
    }
}
