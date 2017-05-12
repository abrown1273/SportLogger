using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SportLogger.Models
{
   // This class file would be used to create custom annotations [ValidValues] on the model. 
   // It is not in use.

    public class ValidValuesAttribute : ValidationAttribute
    {
        string[] _args;

        public ValidValuesAttribute(params string[] args)
        {
            _args = args;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_args.Contains((string)value))
                return ValidationResult.Success;

            return new ValidationResult("Invalid value.");
        }
    }

    /*
    public class CombinedMinLengthAttribute : ValidationAttribute
    {
        public CombinedMinLengthAttribute(int minLength, params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
            this.MinLength = minLength;
        }

        public string[] PropertyNames { get; private set; }
        public int MinLength { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
            var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
            if (totalLength < this.MinLength)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
    */
}
