using System.ComponentModel;
using Web.Models.Annotations;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class UserGroupPermission: IAuditedEntity
    {
        [DisplayName(@"User Group")]
        public long UserGroupPermissionId { get; set; }

        [DisplayName(@"User"), RequiredCustom]
        public long UserId { get; set; }

        [DisplayName(@"Permission Group"), RequiredCustom]
        public long PermissionGroupId { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }
        public virtual User User { get; set; }
        public bool Active { get; set; }
    }
}