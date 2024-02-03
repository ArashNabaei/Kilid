namespace Kilid.Entities
{
    public class Advertisement
    {
        public int Id { get; set; }

        public int BuildingId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Conditions { get; set; }

        public string Features { get; set; }
    }
}
