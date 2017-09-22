using System.ComponentModel;
using Web.Models.Annotations;
using Web.Models.Entities;

namespace Web.Models.Access
{
    public class Module: IAuditedEntity
    {
        [DisplayName(@"Module")]
        public long ModuleId { get; set; }

        [DisplayName(@"Code"), StringLenghtCustom(5, 5), RequiredCustom]
        public string Code { get; set; }

        [DisplayName(@"Name"), RequiredCustom, MaxLenghtCustom(50)]
        public string Name { get; set; }

        [DisplayName(@"Icon"), MaxLenghtCustom(250)]
        public string Icon { get; set; }

        [DisplayName(@"Order")]
        public int Order { get; set; }

        public bool Active { get; set; }
    }
}