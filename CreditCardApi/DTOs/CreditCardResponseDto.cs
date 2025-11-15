namespace CreditCardApi.DTOs;

public class CreditCardResponseDto
{
    public int Id { get; set; }
    public string CardHolder { get; set; } = "";
    public string Expiry { get; set; } = "";
    public string MaskedNumber { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
