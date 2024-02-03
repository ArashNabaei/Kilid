namespace Kilid.Entities
{
    public class Agency
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public int EmployeeCount { get; set; }
    }
}
