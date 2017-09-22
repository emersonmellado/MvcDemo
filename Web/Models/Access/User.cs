using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.Annotations;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class User: IAuditedEntity
    {
        [DisplayName(@"User")]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public bool ChangePassword { get; set; }

        //Interface contract
        public bool Active { get; set; }

        //FK
        [DisplayName(@"Company"), RequiredCustom]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}