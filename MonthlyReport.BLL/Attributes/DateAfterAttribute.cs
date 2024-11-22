using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace MonthlyReport.BLL.Attributes
{
    public class DateGreaterThanAttribute(string comparisonProperty) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTimeOffset?)value;

            var property = validationContext.ObjectType.GetProperty(comparisonProperty);

            if (property == null)
                return new ValidationResult($"Unknown property: {comparisonProperty}");

            var comparisonValue = (DateTimeOffset?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                var stringLocalizer = (IStringLocalizer)validationContext.GetRequiredService(typeof(IStringLocalizer));

                var currentProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName!);

                var currentValueName = stringLocalizer[currentProperty!.Name];

                var comparisonPropertyName = stringLocalizer[comparisonProperty];

                var errorMessage = string.Format(stringLocalizer[ErrorMessage!], comparisonPropertyName, currentValueName);

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}