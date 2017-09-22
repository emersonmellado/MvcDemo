using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.Annotations;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class Functionality: IAuditedEntity
    {
        [DisplayName(@"Functionality")]
        public long FunctionalityId { get; set; }

        [Index("IX_CodeFunctionalityUnique", 1, IsUnique = true)]
        [DisplayName(@"Code"), StringLenghtCustom(5, 5), RequiredCustom]
        public string Code { get; set; }

        [DisplayName(@"Name"), RequiredCustom, MaxLenghtCustom(50)]
        public string Name { get; set; }

        [DisplayName(@"URL *"), RequiredCustom, MaxLenghtCustom(50)]
        public string Url { get; set; }

        [DisplayName(@"Order")]
        public int Order { get; set; }

        [DisplayName(@"Module")]
        public long ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public bool Active { get; set; }
    }
}