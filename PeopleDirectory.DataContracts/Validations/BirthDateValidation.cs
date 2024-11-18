using PeopleDirectory.DataContracts.Resources;
using System.ComponentModel.DataAnnotations;

namespace PeopleDirectory.DataContracts.Validations
{
    public class BirthDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                int age = DateTime.Today.Year - birthDate.Year;
                if (birthDate > DateTime.Today.AddYears(-age)) age--;

                if (age >= 18)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ValidationMessages.GetMessage("BirthDateInvalid"));
        }
    }
}
