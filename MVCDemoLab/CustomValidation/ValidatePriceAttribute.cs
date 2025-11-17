using System.ComponentModel.DataAnnotations;

namespace MVCDemoLab.CustomValidation
{
    public class ValidatePriceAttribute
    {
        public static ValidationResult ValidatePrice(decimal price, ValidationContext context)
        {
            if (price < 500)
                return new ValidationResult("Price  must be Gt than 500.");
            return ValidationResult.Success;
        }
    }
}
