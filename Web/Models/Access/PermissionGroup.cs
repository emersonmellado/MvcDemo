using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Access;
using Web.Models.Annotations;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class PermissionGroup: IAuditedEntity
    {

        [DisplayName(@"Permission Group")]
        public long PermissionGroupId { get; set; }

        [Index("IX_CodePermissionGroupUnique", 1, IsUnique = true)]
        [DisplayName(@"Code"), StringLenghtCustom(5, 5), RequiredCustom]
        public string Code { get; set; }

        [DisplayName(@"Name"), RequiredCustom, MaxLenghtCustom(50)]
        public string Name { get; set; }

        [DisplayName(@"Profile Type"), RequiredCustom]
        public ProfileType ProfileTypeId { get; set; }

        [NotMapped]
        public string GrupoPermissaoDdl => $"{Code} - {Name}";

        public bool Active { get; set; }
    }
}