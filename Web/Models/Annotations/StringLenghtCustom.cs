using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class StringLenghtCustom : StringLengthAttribute
    {
        public StringLenghtCustom(int min, int max)
            : base(max)
        {
            MinimumLength = min;
            ErrorMessage = Strings.MaxLenghtCustom;
        }
    }
}