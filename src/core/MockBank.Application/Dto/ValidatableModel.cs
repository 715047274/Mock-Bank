using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockBank.Application.Dto
{
    public class ValidatableModel
    {
        public string Validate()
        {
            var ctx = new ValidationContext(this);
            string validationErrors = "";
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, ctx, results, true))
            {
                foreach (var error in results)
                {
                    validationErrors += $"{error} ";
                }
            }
            return validationErrors;
        }
    }
}