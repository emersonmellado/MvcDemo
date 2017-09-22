using System.ComponentModel;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class FunctionalityPermission: IAuditedEntity
    {
        [DisplayName(@"Permission by Functionality")]
        public long FunctionalityPermissionId { get; set; }

        [DisplayName(@"Functionality")]
        public long FunctionalityId { get; set; }

        [DisplayName(@"Permission group")]
        public long PermissionGroupId { get; set; }

        [DisplayName(@"Read")]
        public bool Read { get; set; }

        [DisplayName(@"Write")]
        public bool Write { get; set; }

        [DisplayName(@"Edit")]
        public bool Edit { get; set; }

        [DisplayName(@"Delete")]
        public bool Delete { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }
        public virtual Functionality Functionality { get; set; }
        public bool Active { get; set; }
    }
}