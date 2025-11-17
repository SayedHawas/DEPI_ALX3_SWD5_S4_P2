using System.ComponentModel.DataAnnotations;

namespace MVCDemoLab.CustomValidation
{
    public class IsExistAttribute : ValidationAttribute
    {
        public string MyErrorMessage { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return null;
            string name = value.ToString();
            // db
            MVCDbContext db = (MVCDbContext)validationContext.GetService(typeof(MVCDbContext));
            //check 
            var result = db.Categories.FirstOrDefault(c => c.Name == name);
            if (result != null)
            {
                //return new ValidationResult("this Name Already Exist ...");
                return new ValidationResult(MyErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
