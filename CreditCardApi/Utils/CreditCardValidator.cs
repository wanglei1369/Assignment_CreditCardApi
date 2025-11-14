using CreditCardApi.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CreditCardApi.Utils;

public static class CreditCardValidator
{
    /// <summary>
    /// Validating the dto data
    /// </summary>
    /// <param name="dto">Credit card data from frontend form </param>
    /// <param name="errors">List of viladation error message</param>
    /// <returns>vaild or invalid</returns>
    public static bool TryValidate(CreditCardDto dto, out List<string> errors)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto,context,validationResults, true);

        errors = validationResults.Select(v => v.ErrorMessage ?? "Validation error").ToList();

        return isValid;
    }
}