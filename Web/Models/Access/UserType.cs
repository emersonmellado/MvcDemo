using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class UserType : IAuditedEntity
    {
        [DisplayName(@"User type")]
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        
        //Foreign keys
        [DisplayName(@"User"), Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

		//Interface contract
        public bool Active { get; set; }
    }
}