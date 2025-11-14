using System.ComponentModel.DataAnnotations;

namespace CreditCardApi.DTOs;

public class CreditCardDto
{
    [Required(ErrorMessage = "Card holder name is required")]
    public string CardHolder {get; set;} = string.Empty;

    [Required(ErrorMessage = "Card number is required")]
    //[CreditCard(ErrorMessage = "Invalid card number")]
    public string CardNumber {get; set;} = string.Empty;

    [Required(ErrorMessage = "Expiry is required")]
    [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Expiry must be in MM/YY format")]
    public string Expiry {get; set;} = string.Empty;

    [Required(ErrorMessage = "CVC is required")]
    [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "CVC must be 3 digits")]
    public string Cvc {get; set;} = string.Empty;
}