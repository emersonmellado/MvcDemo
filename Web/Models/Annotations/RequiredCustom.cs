using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class RequiredCustom : RequiredAttribute
    {
        public RequiredCustom()
        {
            ErrorMessage = Strings.FieldRequired;
        }
    }
}