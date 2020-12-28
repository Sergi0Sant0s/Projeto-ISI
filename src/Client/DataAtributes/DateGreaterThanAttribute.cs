using System;
using System.ComponentModel.DataAnnotations;

namespace Client.DataAtributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class DateLessThanAttribute : ValidationAttribute
    {
        public DateLessThanAttribute(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime laterDate = (DateTime)value;

            DateTime earlierDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Data inferior a data de inicio");
            }
        }
    }
}
