using System.ComponentModel.DataAnnotations;

namespace Atmira.Asteroids.Web.Attributes;

public class DateGreaterThanAttribute : ValidationAttribute
{
    public string DateToCompare { get; set; }

    public DateGreaterThanAttribute(string dateToCompare)
    {
        DateToCompare = dateToCompare;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) 
        {        
            return base.IsValid(value, validationContext);
        }        

        var FromDate = (DateTime?)validationContext.ObjectType?.GetProperty(DateToCompare)?.GetValue(validationContext.ObjectInstance);
        if (FromDate is null)
        {
            return new ValidationResult(ErrorMessage ?? $"{DateToCompare} debe tener un valor");
        }

        var date = (DateTime)value;
        if (date.CompareTo(FromDate) < 0) 
        {
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} debe ser mayor a {DateToCompare}");
        }

        return ValidationResult.Success;
    }
}
