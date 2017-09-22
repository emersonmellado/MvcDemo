using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class MaxLenghtCustom : MaxLengthAttribute
    {
        public MaxLenghtCustom(int limit)
            : base(limit)
        {
            ErrorMessage = Strings.MaxLenghtCustom;
        }
    }
}