using System;
using System.ComponentModel.DataAnnotations;

namespace MO9Project.Models.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext ctx)
        {
            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if (dateToCheck < DateTime.Today)
                {
                    return ValidationResult.Success;
                }
            }

            string msg = base.ErrorMessage ??
                $"{ctx.DisplayName} must be a valid past date.";
            return new ValidationResult(msg);
        }
    }
}
