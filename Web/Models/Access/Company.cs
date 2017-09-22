using Web.Models.Entities;

namespace Web.Models.Access
{
    public class Company: IAuditedEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public bool Active { get; set; }
    }
}